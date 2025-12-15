using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class GrupoEscalonamentoLookupAttribute : SMCLookupAttribute
    {
        public GrupoEscalonamentoLookupAttribute()
            : base("GrupoEscalonamento")
        {
            AutoSearch = true;
            HideSeq = true;
            ModalWindowSize = SMCModalWindowSize.Large;            
            Model = typeof(GrupoEscalonamentoLookupListaViewModel);
            Filter = typeof(GrupoEscalonamentoLookupFiltroViewModel);
            PrepareFilter = typeof(GrupoEscalonamentoLookupPrepareFilter);
            Service<IGrupoEscalonamentoService>(nameof(IGrupoEscalonamentoService.BuscarGruposEscalonamento));
            SelectService<IGrupoEscalonamentoService>(nameof(IGrupoEscalonamentoService.BuscarGruposEscalonamentoGridLookup));
        }
    }
}