using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class ProcessoLookupAttribute : SMCLookupAttribute
    {
        public ProcessoLookupAttribute()
            : base("Processo")
        {
            AutoSearch = true;
            ModalWindowSize = SMCModalWindowSize.Large;
            Filter = typeof(ProcessoLookupFiltroViewModel);
            Model = typeof(ProcessoLookupListaViewModel);
            PrepareFilter = typeof(ProcessoLookupPrepareFilter);
            Service<IProcessoService>(nameof(IProcessoService.BuscarProcessos));
        }
    }
}