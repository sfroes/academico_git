using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface IEntidadeConfiguracaoNotificacaoService : ISMCService
    {
        /// <summary>
        /// Busca os registros de entidade configuração notificação
        /// </summary>
        /// <param name="filtro">Filtros da entidade configuração notificação</param>
        /// <returns></returns>
        SMCPagerData<EntidadeConfiguracaoNotificacaoListarData> BuscarEntidadeConfiguracaoNotificacao(EntidadeConfiguracaoNotificacaoFiltroData filtro);

        /// <summary>
        /// Buscar Configuração de notificação
        /// </summary>
        /// <param name="seq">Sequencial entidade configuração notificação</param>
        /// <returns>Dados da configuração da notificação</returns>
        EntidadeConfiguracaoNotificacaoData BuscarEntidadeConfiguracaoNotificacao(long seq);

        /// <summary>
        /// Salvar entidade configuração notificação
        /// </summary>
        /// <param name="modelo">Dados a serem salvos</param>
        /// <returns>Sequencial da entidade configuração notificação</returns>
        long Salvar(EntidadeConfiguracaoNotificacaoData modelo);

        /// <summary>
        /// Excluir a configuração notificação 
        /// </summary>
        /// <param name="seq">Sequencial da entidade configuração notificação</param>
        void Excluir(long seq);
    }
}
