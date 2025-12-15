using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class CurriculoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public long? Seq { get; set; }

        [CursoLookup]
        [SMCFilter]
        [SMCSize(SMCSize.Grid15_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid9_24)]
        [SMCRequired]
        public CursoLookupViewModel SeqCurso { get; set; }

        [SMCFilter]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid4_24)]
        public bool Ativo { get; set; } = true;
    }
}