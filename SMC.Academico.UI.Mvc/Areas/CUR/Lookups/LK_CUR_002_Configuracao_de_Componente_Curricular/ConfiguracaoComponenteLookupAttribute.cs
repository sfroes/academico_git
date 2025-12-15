using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class ConfiguracaoComponenteLookupAttribute : SMCLookupAttribute
    {
        public ConfiguracaoComponenteLookupAttribute() :
            base("ConfiguracaoComponente")
        {
            ModalWindowSize = SMCModalWindowSize.Largest;
            AutoSearch = true;
            HideSeq = true;
            Model = typeof(ConfiguracaoComponenteLookupViewModel);
            Filter = typeof(ConfiguracaoComponenteLookupFiltroViewModel);
            PrepareFilter = typeof(ConfiguracaoComponenteLookupPrepareFilter);
            Service<IConfiguracaoComponenteService>(nameof(IConfiguracaoComponenteService.BuscarConfiguracoesComponentesLookup));
            CustomView = "SMC.Academico.UI.Mvc.dll/Areas/CUR/Lookups/LK_CUR_002_Configuracao_de_Componente_Curricular/_List";
        }
    }
}