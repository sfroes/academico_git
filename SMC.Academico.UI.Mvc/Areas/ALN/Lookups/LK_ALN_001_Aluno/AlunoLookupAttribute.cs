using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.ALN.Lookups
{
    public class AlunoLookupAttribute : SMCLookupAttribute
    {
        public AlunoLookupAttribute()
           : base("Aluno")
        {
            this.AutoSearch = true;
            this.Filter = typeof(AlunoLookupFiltroViewModel);
            this.Model = typeof(AlunoLookupViewModel);
            this.PrepareFilter = typeof(AlunoLookupPrepareFilter);
            this.Service<IAlunoService>(nameof(IAlunoService.BuscarAlunos));
            this.SelectService<IAlunoService>(nameof(IAlunoService.BuscarAlunos));
        }
    }
}