using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class TipoVinculoAlunoDomainService : AcademicoContextDomain<TipoVinculoAluno>
    {
        #region [ DomainService ]

        private InstituicaoNivelServicoDomainService InstituicaoNivelServicoDomainService { get => this.Create<InstituicaoNivelServicoDomainService>(); }

        private ProcessoSeletivoDomainService ProcessoSeletivoDomainService { get => Create<ProcessoSeletivoDomainService>(); }

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService { get => Create<InstituicaoNivelTipoVinculoAlunoDomainService>(); }

        #endregion [ DomainService ]

        public List<SMCDatasourceItem> BuscarTiposVinculoAlunoSelect()
        {
            var lista = SearchAll(i => i.Descricao).OrderBy(w => w.Descricao);
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            foreach (var item in lista)
                retorno.Add(new SMCDatasourceItem(item.Seq, item.Descricao));
            return retorno;
        }

        public List<SMCDatasourceItem> BuscarTiposVinculoAlunoPorServicoSelect(long seqServico)
        {
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            var spec = new InstituicaoNivelServicoFilterSpecification() { SeqServico = seqServico };

            var instituicaoNivelServico = InstituicaoNivelServicoDomainService.SearchBySpecification(spec,
                IncludesInstituicaoNivelServico.InstituicaoNivelTipoVinculoAluno
                | IncludesInstituicaoNivelServico.InstituicaoNivelTipoVinculoAluno_TipoVinculoAluno);

            foreach (var item in instituicaoNivelServico)
            {
                var vinculo = item.InstituicaoNivelTipoVinculoAluno.TipoVinculoAluno;
                retorno.Add(new SMCDatasourceItem(vinculo.Seq, vinculo.Descricao));
            }

            return retorno.SMCDistinct(s => s.Seq).OrderBy(a => a.Descricao).ToList();
        }

        /// <summary>
        /// Busca o tipo de processo vinculado ao processo
        /// </summary>
        /// <param name="seqProcessoSeletivo"></param>
        /// <returns></returns>
        public List<SMCDatasourceItem> BuscarTiposVinculoAlunoPorProcessoSeletivo(long seqProcessoSeletivo)
        {
            // Recupera o tipo do processo
            var seqTipoProcessoSeletivo = ProcessoSeletivoDomainService.SearchProjectionByKey(new SMCSeqSpecification<ProcessoSeletivo>(seqProcessoSeletivo), p => p.SeqTipoProcessoSeletivo);

            // Recupera os vínculos associados ao tipo de processo informado
            var specVinculo = new InstituicaoNivelTipoVinculoAlunoFilterSpecification() { SeqTipoProcessoSeletivo = seqTipoProcessoSeletivo };

            return InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionBySpecification(specVinculo, p => new SMCDatasourceItem()
            {
                Seq = p.SeqTipoVinculoAluno,
                Descricao = p.TipoVinculoAluno.Descricao
            }, isDistinct: true).OrderBy(o => o.Descricao).ToList();
        }

        /// <summary>
        /// Busca todos tipos de vínculos de aluno configurados na instituição
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Dados dos tipos de vínculo de aluno</returns>
        public List<SMCDatasourceItem> BuscarTipoVinculoAlunoNaInstituicaoSelect(long? seqNivelEnsino)
        {
            var spec = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
            {
                SeqNivelEnsino = seqNivelEnsino
            };
            spec.SetOrderBy(o => o.TipoVinculoAluno.Descricao);
            return InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
            {
                Seq = p.SeqTipoVinculoAluno,
                Descricao = p.TipoVinculoAluno.Descricao
            }, true)
            .OrderBy(o => o.Descricao)//FIX: Remover quando o spec ordenar corretamente
            .ToList();
        }

        public List<SMCDatasourceItem> BuscarTipoVinculoAlunoPorTipoProcessoSeletivo(long seqTipoProcessoSeletivo)
        {
            var spec = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
            {
                SeqTipoProcessoSeletivo = seqTipoProcessoSeletivo
            };
            spec.SetOrderBy(o => o.TipoVinculoAluno.Descricao);
            return InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
            {
                Seq = p.SeqTipoVinculoAluno,
                Descricao = p.TipoVinculoAluno.Descricao
            }, true)
            .OrderBy(o => o.Descricao)
            .ToList();
        }
    }
}