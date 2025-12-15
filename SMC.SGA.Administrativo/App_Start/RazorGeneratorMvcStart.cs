using RazorGenerator.Mvc;
using SMC.Academico.UI.Mvc;
using SMC.AgendadorTarefa.UI.Mvc;
using SMC.Framework.UI.Mvc.Global;
using SMC.Notificacoes.UI.Mvc;
using SMC.Seguranca.UI.Mvc;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(SMC.SGA.Administrativo.RazorGeneratorMvcStart), "Start")]

namespace SMC.SGA.Administrativo
{
    public static class RazorGeneratorMvcStart {
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

            var uiNotificacao = new PrecompiledMvcEngine(typeof(NotificacaoExternalViews).Assembly)
            {
                UsePhysicalViewsIfNewer = view
            };

            var uiAgendadorTarefa = new PrecompiledMvcEngine(typeof(AgendadorTarefaExternalViews).Assembly)
            {
                UsePhysicalViewsIfNewer = view
            };

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(framework);
            ViewEngines.Engines.Add(uiMVC);
            ViewEngines.Engines.Add(sistema);
   
            ViewEngines.Engines.Add(uiSeguranca);
            ViewEngines.Engines.Add(uiNotificacao);
            ViewEngines.Engines.Add(uiAgendadorTarefa);

            ViewEngines.Engines.Add(new CSharpRazorViewEngine());

            // StartPage lookups are done by WebPages. 
            VirtualPathFactoryManager.RegisterVirtualPathFactory(framework);
        }
    }
}
