using SMC.Framework.DataFilters;
using SMC.Framework.Domain.DataFilters;
using SMC.Framework.Service;

namespace SMC.Academico.Service
{
    /// <summary>
    /// Customização do SMCDataFilterProviderService
    /// </summary>
    public class DataFilterProviderService : SMCServiceBase, ISMCDataFilterProviderService
    {
        #region DomainService

        /// <summary>
        /// Domain service para filtro de dados.
        /// </summary>
        private SMCDataFilterProviderDomainService DataFilterProviderDomainService
        {
            get { return Create<SMCDataFilterProviderDomainService>(); }
        }

        #endregion

        public SMCDataFilter GetDataFilter(string token)
        {
            return DataFilterProviderDomainService.GetDataFilter(token);
        }
    }
}
