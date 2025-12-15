using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class TipoHierarquiaClassificacaoService : SMCServiceBase, ITipoHierarquiaClassificacaoService
    {
        #region Serviço de Dominio

        internal TipoHierarquiaClassificacaoDomainService TipoHierarquiaClassificacaoDomainService
        {
            get { return this.Create<TipoHierarquiaClassificacaoDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Busca os dados de um tipo de hierarquia de classificação
        /// </summary>
        /// <param name="seq">Sequencial do tipo de hierarquia de classificação</param>
        /// <returns>Informações do tipo de hierarquia de classificação</returns>
        public TipoHierarquiaClassificacaoData BuscarTipoHierarquiaClassificacao(long seq)
        {
            return TipoHierarquiaClassificacaoDomainService.SearchByKey<TipoHierarquiaClassificacao, TipoHierarquiaClassificacaoData>(seq);
        }
    }
}
