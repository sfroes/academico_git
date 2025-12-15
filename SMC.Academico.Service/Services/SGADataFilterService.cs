using SMC.Framework.DataFilters;
using SMC.Seguranca.Service.Services;
using System;

namespace SMC.Academico.Service
{
    /// <summary>
    /// Customização do SMCDataFilterProviderService
    /// </summary>
    public class SGADataFilterService : DataFilterService, ISMCDataFilterCustomService
    {
        public bool CheckToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
