using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class TipoServicoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCFilter]
        [SMCSortable(true)]
        public override long Seq { get; set; }

        [SMCFilter]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCOrder(1)]
        [SMCDescription]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }
         
        [SMCSize(SMCSize.Grid7_24)]
        [SMCMaxLength(100)]
        [SMCOrder(2)]
        [SMCRegularExpression(REGEX.TOKEN)]
        public string Token { get; set; } 
    }
} 