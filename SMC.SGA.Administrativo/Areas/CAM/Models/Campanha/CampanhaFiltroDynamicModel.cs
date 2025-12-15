using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter]
        [SMCSize(SMCSize.Grid4_24)]
        public long? Seq { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid14_24)]
        public string Descricao { get; set; }

        [SMCFilter]
        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid6_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }
    }
}