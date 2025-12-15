using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class TipoFuncionarioFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter(true, true)]
        [SMCKey]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid3_24)]
        public long? Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCDescription]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid10_24)]
        public string DescricaoMasculino { get; set; }
    }
}