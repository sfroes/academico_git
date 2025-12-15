using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class InstituicaoNivelService : SMCServiceBase, IInstituicaoNivelService
    {
        #region Domain Service

        private InstituicaoNivelDomainService InstituicaoNivelDomainService
        {
            get { return this.Create<InstituicaoNivelDomainService>(); }
        }

        #endregion Domain Service

        /// <summary>
        /// Busca a lista de níveis de ensino da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de níveis de ensino com sequencial do InstituicaoNivel</returns>
        public List<SMCDatasourceItem> BuscarNiveisEnsinoDaInstituicaoSelect()
        {
            var lista = InstituicaoNivelDomainService.SearchAll(i => i.NivelEnsino.Descricao, IncludesInstituicaoNivel.NivelEnsino);
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            foreach (var item in lista)
            {
                retorno.Add(new SMCDatasourceItem(item.Seq, item.NivelEnsino.Descricao));
            }
            return retorno;
        }

        public long BuscarSeqUnidadeResponsavelAgd(long seqInstituicaoNivel)
        {
            var unidade = InstituicaoNivelDomainService.SearchProjectionByKey(new SMCSeqSpecification<InstituicaoNivel>(seqInstituicaoNivel),
                i => new { SeqUnidadeResponsavelAgd = i.InstituicaoEnsino.SeqUnidadeResponsavelAgd });
            return unidade.SeqUnidadeResponsavelAgd.GetValueOrDefault();
        }

        /// <summary>
        /// Busca a lista de níveis de ensino da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de níveis de ensino com sequencial do NivelEnsino</returns>
        public List<SMCDatasourceItem> BuscarNiveisEnsinoSelect()
        {
            var lista = InstituicaoNivelDomainService.SearchProjectionAll(x => new SMCDatasourceItem()
            {
                Seq = x.NivelEnsino.Seq,
                Descricao = x.NivelEnsino.Descricao
            },
            i => i.NivelEnsino.Descricao).ToList();

            return lista;
        }

        /// <summary>
        /// Busca a lista de instituições níveis de ensino da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de níveis de ensino com sequencial da Instituicao Nivel</returns>
        public List<SMCDatasourceItem> BuscarNiveisEnsinoComSequencialInstituicaoNivelSelect()
        {
            var lista = InstituicaoNivelDomainService.SearchProjectionAll(x => new SMCDatasourceItem()
            {
                Seq = x.Seq,
                Descricao = x.NivelEnsino.Descricao
            },
            i => i.NivelEnsino.Descricao).ToList();

            return lista;
        }

        /// <summary>
        /// Busca a instituição nível de um curso
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Dados da instituição nível</returns>
        public InstituicaoNivelData BuscarInstituicaoNivelPorCurso(long seqCurso)
        {
            return this.InstituicaoNivelDomainService
                .BuscarInstituicaoNivelPorCurso(seqCurso)
                .Transform<InstituicaoNivelData>();
        }

        /// <summary>
        /// Busca a instituição nível de um nível ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Dados da nível de ensino na instituição</returns>
        public InstituicaoNivelData BuscarInstituicaoNivelPorNivelEnsino(long seqNivelEnsino)
        {
            return this.InstituicaoNivelDomainService
                .BuscarInstituicaoNivelPorNivelEnsino(seqNivelEnsino)
                .Transform<InstituicaoNivelData>();
        }

        /// <summary>
        /// Busca a instituição nível de um nível ensino
        /// </summary>
        /// <param name="seq">Sequencial do instituição nível de ensino</param>
        /// <returns>Dados da nível de ensino na instituição</returns>
        public InstituicaoNivelData BuscarInstituicaoNivel(long seq)
        {
            return this.InstituicaoNivelDomainService
                .SearchByKey(new SMCSeqSpecification<InstituicaoNivel>(seq), IncludesInstituicaoNivel.NivelEnsino)
                .Transform<InstituicaoNivelData>();
        }

        /// <summary>
        /// Busca a lista de níveis de ensino com reconhecimento LDB da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de níveis de ensino com sequencial do NivelEnsino</returns>
        public List<SMCDatasourceItem> BuscarNiveisEnsinoReconhecidoLDBSelect()
        {
            return InstituicaoNivelDomainService.BuscarNiveisEnsinoReconhecidoLDBSelect();
        }

        public List<SMCDatasourceItem> BuscarNiveisEnsinoPorEntidadeSelect(long seqEntidade)
        {
            return InstituicaoNivelDomainService.BuscarNiveisEnsinoPorEntidadeSelect(seqEntidade);
        }

        /// <summary>
        /// Busca os níveis de ensino associados ao ciculo letivo do processo informado
        /// </summary>
        /// <param name="seqProcessoSeletivo">Sequencial do processo</param>
        /// <returns>Dados dos níveis de ensino encontrados</returns>
        public List<SMCDatasourceItem> BuscarNiveisEnsinoPorProcessoSeletivoSelect(long seqProcessoSeletivo)
        {
            return InstituicaoNivelDomainService.BuscarNiveisEnsinoPorProcessoSeletivoSelect(seqProcessoSeletivo);
        }

        /// <summary>
        /// Busca Órgão regulador, de acordo com Instituição x Nível de Ensino.
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial Nivel Ensino</param>
        /// <param name="seqInstituicaoEnsino">Sequencial Instituicao Ensino</param>
        /// <returns>Retorno do órgão regulador</returns>
        public List<SMCDatasourceItem> BuscarTipoOrgaoReguladorInstituicaoNivelEnsino(long seqNivelEnsino, long seqInstituicaoEnsino)
        {
            return InstituicaoNivelDomainService.BuscarTipoOrgaoReguladorInstituicaoNivelEnsino(seqNivelEnsino, seqInstituicaoEnsino);
        }

        /// <summary>
        /// Busca Órgão regulador, todos parametrizados no Nível de Ensino com a Instituição
        /// </summary>
        /// <param name="seqInstituicaoEnsino">Sequencial Instituicao Ensino</param>
        /// <returns>Retorno do órgão regulador</returns>
        public List<SMCDatasourceItem> BuscarTipoOrgaoReguladorInstituicao(long seqInstituicaoEnsino)
        {
            return InstituicaoNivelDomainService.BuscarTipoOrgaoReguladorInstituicao(seqInstituicaoEnsino);
        }
    }
}