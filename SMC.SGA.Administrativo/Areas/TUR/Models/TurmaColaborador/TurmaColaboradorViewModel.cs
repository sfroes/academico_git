using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaColaboradorViewModel : SMCViewModelBase
    {
        [ColaboradorLookup]
        [SMCDependency("SeqTurma")]
        [SMCSize(SMCSize.Grid22_24)]
        [SMCRequired]
        public ColaboradorLookupNomeViewModel Seq { get; set; }
    }
}