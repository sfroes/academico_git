using SMC.Academico.UI.Mvc.Areas.ALN.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class RelatorioIdentidadeAcademicaAlunoDetailViewModel : SMCViewModelBase
    {
        [AlunoLookup]
        [SMCSize(SMCSize.Grid22_24)]
        [SMCUnique]
        public AlunoLookupViewModel Aluno { get; set; }
    }
}