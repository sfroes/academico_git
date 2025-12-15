using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class MatrizCurricularRelatorioFiltroViewModel : SMCViewModelBase
    {
        [SMCRequired]
        [MatrizCurricularLookup]
        public MatrizCurricularLookupViewModel SeqMatrizCurricular { get; set; }
    }
}