using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class TipoAtoNormativoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid2_24)]       
        public long Seq { get; set; }
               
        [SMCFilter(true, true)]
        [SMCMaxLength(255)]       
        [SMCSize(SMCSize.Grid10_24)]       
        public string Descricao { get; set; }

        [SMCFilter(true, true)]        
        [SMCSelect]
        [SMCSize(SMCSize.Grid4_24)]       
        public bool? Ativo { get; set; }
    }
}