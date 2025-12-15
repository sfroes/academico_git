using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Extensions;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class EntidadeConfiguracaoNotificacaoService : SMCServiceBase, IEntidadeConfiguracaoNotificacaoService
    {
        #region [ DomainService ]

        private EntidadeConfiguracaoNotificacaoDomainService EntidadeConfiguracaoNotificacaoDomainService => Create<EntidadeConfiguracaoNotificacaoDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca os registros de entidade configuração notificação
        /// </summary>
        /// <param name="filtro">Filtros da entidade configuração notificação</param>
        /// <returns></returns>
        public SMCPagerData<EntidadeConfiguracaoNotificacaoListarData> BuscarEntidadeConfiguracaoNotificacao(EntidadeConfiguracaoNotificacaoFiltroData filtro)
        {
            return EntidadeConfiguracaoNotificacaoDomainService.BuscarEntidadeConfiguracaoNotificacao(filtro.Transform<EntidadeConfiguracaoNotificacaoFiltroVO>()).Transform<SMCPagerData<EntidadeConfiguracaoNotificacaoListarData>>();            
        }

        /// <summary>
        /// Buscar Configuração de notificação
        /// </summary>
        /// <param name="seq">Sequencial entidade configuração notificação</param>
        /// <returns>Dados da configuração da notificação</returns>
        public EntidadeConfiguracaoNotificacaoData BuscarEntidadeConfiguracaoNotificacao(long seq)
        {
            return EntidadeConfiguracaoNotificacaoDomainService.BuscarEntidadeConfiguracaoNotificacao(seq).Transform<EntidadeConfiguracaoNotificacaoData>();
        }

        /// <summary>
        /// Salvar entidade configuração notificação
        /// </summary>
        /// <param name="modelo">Dados a serem salvos</param>
        /// <returns>Sequencial da entidade configuração notificação</returns>
        public long Salvar(EntidadeConfiguracaoNotificacaoData modelo)
        {
            return EntidadeConfiguracaoNotificacaoDomainService.Salvar(modelo.Transform<EntidadeConfiguracaoNotificacaoVO>());
        }

        /// <summary>
        /// Excluir a configuração notificação 
        /// </summary>
        /// <param name="seq">Sequencial da entidade configuração notificação</param>
        public void Excluir(long seq)
        {
            this.EntidadeConfiguracaoNotificacaoDomainService.Excluir(seq);
        }
    }
}
