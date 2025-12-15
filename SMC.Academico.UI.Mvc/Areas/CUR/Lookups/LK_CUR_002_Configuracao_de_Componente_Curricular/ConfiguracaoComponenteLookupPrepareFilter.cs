using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class ConfiguracaoComponenteLookupPrepareFilter : ISMCFilter<ConfiguracaoComponenteLookupFiltroViewModel>
    {
        public ConfiguracaoComponenteLookupFiltroViewModel Filter(SMCControllerBase controllerBase, ConfiguracaoComponenteLookupFiltroViewModel filter)
        {
            if (filter.ConfiguracaoComponenteAtivo.HasValue)
            {
                filter.Ativo = filter.ConfiguracaoComponenteAtivo.Value;
                filter.AtivoLeitura = true;
            }
            else
                filter.Ativo = true;
            
            return filter;
        }
    }
}
