using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class EntidadeHistoricoSituacaoService : SMCServiceBase, IEntidadeHistoricoSituacaoService
    {
        #region DomainService

        private EntidadeHistoricoSituacaoDomainService EntidadeHistoricoSituacaoDomainService
        {
            get { return this.Create<EntidadeHistoricoSituacaoDomainService>(); }
        }

        #endregion DomainService

        /// <summary>
        /// Busca o histórico de entidade e situação com o sequencial informado
        /// </summary>
        /// <param name="seq">Sequencial do histórioco de situação</param>
        /// <returns>Dados do histórico de situação com sua situação</returns>
        public EntidadeHistoricoSituacaoData BuscarEntidadeHistoricoSituacao(long seq)
        {
            return this.EntidadeHistoricoSituacaoDomainService
                .SearchByKey(new SMCSeqSpecification<EntidadeHistoricoSituacao>(seq), IncludesEntidadeHistoricoSituacao.SituacaoEntidade)
                .Transform<EntidadeHistoricoSituacaoData>();
        }

        /// <summary>
        /// Grava um registro de entidade histórico situação e caso tenha um registro anterior,
        /// atualiza a data final deste como a data inicial do novo registro -1
        /// </summary>
        /// <param name="entidadeHistoricoSituacao">Dados do histórico de situação</param>
        /// <returns>Sequencial do registro gravado</returns>
        public long SalvarEntidadeHistoricoSituacao(EntidadeHistoricoSituacaoData entidadeHistoricoSituacaoData)
        {
            var entidadeHistoricoSituacao = entidadeHistoricoSituacaoData.Transform<EntidadeHistoricoSituacao>();
            return this.EntidadeHistoricoSituacaoDomainService.SalvarEntidadeHistoricoSituacao(entidadeHistoricoSituacao);
        }

        /// <summary>
        /// Exclui um registro de histórico de entidade situação e caso tenha um registro anterior,
        /// atualiza a data final deste para nulo
        /// </summary>
        /// <param name="seqEntidadeHistoricoSituacao">Sequencial do registro a ser excluído</param>
        public void ExcluirEntidadeHistoricoSituacao(long seqEntidadeHistoricoSituacao)
        {
            this.EntidadeHistoricoSituacaoDomainService.ExcluirEntidadeHistoricoSituacao(seqEntidadeHistoricoSituacao);
        }

        /// <summary>
        /// Busca os registros de histórico de situação com suas situações
        /// </summary>
        /// <param name="filtros">Filtros dos registros a serem recuperados</param>
        /// <returns>Dados dos registros de histórico paginados</returns>
        public SMCPagerData<EntidadeHistoricoSituacaoData> BuscarSituacoes(EntidadeHistoricoSituacaoFiltroData filtros)
        {
            var spec = new EntidadeHistoricoSituacaoFilterSpecification()
            {
                SeqEntidade = filtros.SeqEntidade
            };
            var includes = IncludesEntidadeHistoricoSituacao.SituacaoEntidade;

            return this.EntidadeHistoricoSituacaoDomainService
                .SearchBySpecification<EntidadeHistoricoSituacaoFilterSpecification,
                                       EntidadeHistoricoSituacaoFiltroData,
                                       EntidadeHistoricoSituacaoData,
                                       EntidadeHistoricoSituacao>(filtros, includes);
        }
    }
}