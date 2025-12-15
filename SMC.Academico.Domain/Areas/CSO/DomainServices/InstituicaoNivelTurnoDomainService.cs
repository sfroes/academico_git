using SMC.Academico.Common.Areas.CSO.Exceptions;
using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class InstituicaoNivelTurnoDomainService : AcademicoContextDomain<InstituicaoNivelTurno>
    {
        #region [ DomainService ]

        private InstituicaoNivelDomainService InstituicaoNivelDomainService
        {
            get { return this.Create<InstituicaoNivelDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca turnos para a listagem de acordo com o instituição nível
        /// </summary>
        /// <param name="seqInstituicaoNivel">Sequencial da instituição nível</param>
        /// <returns>Lista de turnos</returns>
        public List<SMCDatasourceItem> BuscarInstituicaoNivelTurnoSelect(long seqInstituicaoNivel)
        {
            var specification = new InstituicaoNivelTurnoFilterSpecification() { SeqInstituicaoNivel = seqInstituicaoNivel };
            var includes = IncludesInstituicaoNivelTurno.Turno;
            var turnos = this.SearchBySpecification(specification, includes);

            var listTurnos = turnos
                .Select(s => new SMCDatasourceItem(s.SeqTurno, s.Turno.Descricao))
                .OrderBy(o => o.Descricao)
                .ToList();

            if (listTurnos.Count == 0)
            {
                throw new InstituicaoNivelTurnoNaoAssociadoException();
            }

            return listTurnos;
        }

        /// <summary>
        /// Busca turnos para a listagem de acordo com o instituição
        /// </summary>
        /// <returns>Lista de turnos</returns>
        public List<SMCDatasourceItem> BuscarTurnosPorInstituicaoSelect()
        {
            var seqInstituicao = this.InstituicaoNivelDomainService.SearchAll().Select(s => s.SeqInstituicaoEnsino).First();
            var spec = new InstituicaoNivelTurnoFilterSpecification() { SeqInstituicao = seqInstituicao };

            var turnos = this.SearchProjectionBySpecification(spec, p => p.Turno).GroupBy(g => g.Seq);

            var lista = turnos.Select(s => new SMCDatasourceItem() { Seq = s.Key, Descricao = s.First().Descricao })
                .OrderBy(o => o.Descricao)
                .ToList();

            return lista;
        }

        public long SalvarInstituicaoNivelTurno(InstituicaoNivelTurnoVO modelo)
        {
            ValidarModelo(modelo);

            var dominio = modelo.Transform<InstituicaoNivelTurno>();

            this.SaveEntity(dominio);

            return dominio.Seq;
        }

        private void ValidarModelo(InstituicaoNivelTurnoVO modelo)
        {
            if (modelo.HoraLimiteFim < modelo.HoraLimiteInicio)
                throw new InstituicaoNivelCursoHoraFimMaiorInicioException();
        }
    }
}