using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Lookups
{
    public class TurmaLookupAttribute : SMCLookupAttribute
    {
        public TurmaLookupAttribute()
           : base("Turma")
        {
            this.HideSeq = true;
            this.AutoSearch = false;
            this.Filter = typeof(TurmaLookupFiltroViewModel);
            this.Model = typeof(TurmaLookupViewModel);
            this.PrepareFilter = typeof(TurmaLookupPrepareFilter);
            this.Service<ITurmaService>(nameof(ITurmaService.BuscarTurmas));
            this.SelectService<ITurmaService>(nameof(ITurmaService.BuscarTurmaLookup));
        }
    }
}