using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class TurnoDomainService : AcademicoContextDomain<Turno>
    {
        #region [ DomainService ]

        private CurriculoCursoOfertaDomainService CurriculoCursoOfertaDomainService
        {
            get { return this.Create<CurriculoCursoOfertaDomainService>(); }
        }

        private CursoOfertaLocalidadeTurnoDomainService CursoOfertaLocalidadeTurnoDomainService
        {
            get { return this.Create<CursoOfertaLocalidadeTurnoDomainService>(); }
        }

        private InstituicaoNivelTurnoDomainService InstituicaoNivelTurnoDomainService
        {
            get { return this.Create<InstituicaoNivelTurnoDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca turnos para a listagem de acordo com o curso oferta localidade
        /// </summary>
        /// <param name="seqCursoOferta">Sequencial do curso oferta </param>
        /// <returns>Lista de turnos</returns>
        public List<SMCDatasourceItem> BuscarTurnosPorCursoOfertaSelect(long seqCursoOferta)
        {
            var spec = new CursoOfertaLocalidadeTurnoFilterSpecification() { SeqCursoOferta = seqCursoOferta };

            var turnos = this.CursoOfertaLocalidadeTurnoDomainService.SearchProjectionBySpecification(spec, s => new SMCDatasourceItem() { Seq = s.Turno.Seq, Descricao = s.Turno.Descricao }, true).OrderBy(o => o.Descricao).ToList();

            return turnos;
        }

        /// <summary>
        /// Busca turnos para a listagem de acordo com o curso
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso </param>
        /// <returns>Lista de turnos</returns>
        public List<SMCDatasourceItem> BuscarTurnosPorCursoSelect(long seqCurso)
        {
            var spec = new CursoOfertaLocalidadeTurnoFilterSpecification() { SeqCurso = seqCurso };

            var turnos = this.CursoOfertaLocalidadeTurnoDomainService.SearchProjectionBySpecification(spec, s => new SMCDatasourceItem() { Seq = s.Turno.Seq, Descricao = s.Turno.Descricao }, true).OrderBy(o => o.Descricao).ToList();

            return turnos;
        }

        /// <summary>
        /// Busca os turnos ativos para a listagem de acordo com o curso oferta localidade
        /// </summary>
        /// <param name="seqCursoOfertaLocalidade">Sequencial do curso oferta localidade</param>
        /// <returns>Lista de turnos</returns>
        public List<SMCDatasourceItem> BuscarTurnosPorCursoOfertaLocalidadeSelect(long seqCursoOfertaLocalidade)
        {
            var spec = new CursoOfertaLocalidadeTurnoFilterSpecification() { SeqCursoOfertaLocalidade = seqCursoOfertaLocalidade, Ativo = true };

            var turnos = this.CursoOfertaLocalidadeTurnoDomainService.SearchProjectionBySpecification(spec, s => new SMCDatasourceItem() { Seq = s.Turno.Seq, Descricao = s.Turno.Descricao }, true).OrderBy(o => o.Descricao).ToList();

            return turnos;
        }

        /// <summary>
        /// Busca turnos para a listagem de acordo com a localidade
        /// </summary>
        /// <param name="seqLocalidade">Sequencial da localidade</param>
        /// <returns>Lista de turnos</returns>
        public List<SMCDatasourceItem> BuscarTurnosPorLocalidadeSelect(long seqLocalidade)
        {
            var spec = new CursoOfertaLocalidadeTurnoFilterSpecification() { SeqLocalidade = seqLocalidade };
            var turnos = this.CursoOfertaLocalidadeTurnoDomainService.SearchProjectionBySpecification(spec, s => new SMCDatasourceItem() { Seq = s.Turno.Seq, Descricao = s.Turno.Descricao }, true).OrderBy(o => o.Descricao).ToList();
            return turnos;
        }

        /// <summary>
        /// Busca os turnos que sejam do nível de ensino do curso do curriculo curso oferta informado
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do currículo curso oferta do curso com o nível de ensino em questão</param>
        /// <returns>Dados dos turnos</returns>
        public List<SMCDatasourceItem> BuscarTurnosNivelEnsinoPorCurriculoCursoOfertaSelect(long seqCurriculoCursoOferta)
        {
            // Recupera nível ensino do curso associado ao curriculo curso oferta
            long seqNivelEnsino = this.CurriculoCursoOfertaDomainService
                .SearchProjectionByKey(new SMCSeqSpecification<CurriculoCursoOferta>(seqCurriculoCursoOferta), p => p.CursoOferta.Curso.SeqNivelEnsino);

            // Recupera todas as escalas do nível de ensino do curso
            var specTurnosNivel = new InstituicaoNivelTurnoFilterSpecification() { SeqNivelEnsino = seqNivelEnsino };
            var seqsTurnosNivel = this.InstituicaoNivelTurnoDomainService
                .SearchProjectionBySpecification(specTurnosNivel, p => p.SeqTurno);

            var specInNivel = new SMCContainsSpecification<Turno, long>(p => p.Seq, seqsTurnosNivel.ToArray());

            var selectItens = this.SearchProjectionBySpecification(specInNivel,
                p => new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.Descricao }).OrderBy(o => o.Descricao);

            return selectItens
                .ToList();
        }

        public List<SMCDatasourceItem> BuscarTunos()
        {
            return this.SearchAll().Select(s => new SMCDatasourceItem
            {
                Seq = s.Seq,
                Descricao = s.Descricao
            }).OrderBy(o => o.Descricao).ToList();
        }

        /// <summary>
        /// Busca todos os turnos que atendam aos filtros informados
        /// </summary>
        /// <param name="specification">Dados dos filtros</param>
        /// <returns>Dados dos turnos que atendam aos filtros informados</returns>
        public List<SMCDatasourceItem> BuscarTurnosSelect(TurnoFilterSpecification specification)
        {
            return SearchProjectionBySpecification(specification, p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.Descricao
            }, true)
            .OrderBy(o => o.Descricao)
            .ToList();
        }
    }
}