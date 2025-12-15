using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface IEntidadeHistoricoSituacaoService : ISMCService
    {
        /// <summary>
        /// Busca o histórico de entidade e situação com o sequencial informado
        /// </summary>
        /// <param name="seq">Sequencial do histórioco de situação</param>
        /// <returns>Dados do histórico de situação com sua situação</returns>
        EntidadeHistoricoSituacaoData BuscarEntidadeHistoricoSituacao(long seq);

        /// <summary>
        /// Grava um registro de entidade histórico situação e caso tenha um registro anterior,
        /// atualiza a data final deste como a data inicial do novo registro -1
        /// </summary>
        /// <param name="entidadeHistoricoSituacao">Dados do histórico de situação</param>
        /// <returns>Sequencial do registro gravado</returns>
        long SalvarEntidadeHistoricoSituacao(EntidadeHistoricoSituacaoData entidadeHistoricoSituacao);

        /// <summary>
        /// Exclui um registro de histórico de entidade situação e caso tenha um registro anterior,
        /// atualiza a data final deste para nulo
        /// </summary>
        /// <param name="seqEntidadeHistoricoSituacao">Sequencial do registro a ser excluído</param>
        void ExcluirEntidadeHistoricoSituacao(long seqEntidadeHistoricoSituacao);

        /// <summary>
        /// Busca os registros de histórico de situação com suas situações
        /// </summary>
        /// <param name="filtros">Filtros dos registros a serem recuperados</param>
        /// <returns>Dados dos registros de histórico paginados</returns>
        SMCPagerData<EntidadeHistoricoSituacaoData> BuscarSituacoes(EntidadeHistoricoSituacaoFiltroData filtros);
    }
}