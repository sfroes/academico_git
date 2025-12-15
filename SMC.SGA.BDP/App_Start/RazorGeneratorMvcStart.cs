using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using RazorGenerator.Mvc;
using SMC.Academico.UI.Mvc;
using SMC.Framework.UI.Mvc.Global;
using SMC.Seguranca.UI.Mvc;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(SMC.SGA.BDP.RazorGeneratorMvcStart), "Start")]

namespace SMC.SGA.BDP
{
    public static class RazorGeneratorMvcStart
    {
        public static void Start()
        {
            Configure();
        }

        public static void Configure()
        {
            bool view = false;
#if DEBUG
            view = HttpContext.Current.Request.IsLocal;
#endif

            var framework = new PrecompiledMvcEngine(typeof(CSharpRazorViewEngine).Assembly)
            {
                UsePhysicalViewsIfNewer = view
            };

            var sistema = new PrecompiledMvcEngine(typeof(RazorGeneratorMvcStart).Assembly)
            {
                UsePhysicalViewsIfNewer = view
            };

            var uiMVC = new PrecompiledMvcEngine(typeof(AcademicoExternalViews).Assembly)
            {
                UsePhysicalViewsIfNewer = view
            };

            var uiSeguranca = new PrecompiledMvcEngine(typeof(SegurancaExternalViews).Assembly)
            {
                UsePhysicalViewsIfNewer = view
            };

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(framework);
            ViewEngines.Engines.Add(uiMVC);
            ViewEngines.Engines.Add(sistema);

            ViewEngines.Engines.Add(uiSeguranca);

            ViewEngines.Engines.Add(new CSharpRazorViewEngine());

            // StartPage lookups are done by WebPages. 
            VirtualPathFactoryManager.RegisterVirtualPathFactory(framework);
        }
    }
}
