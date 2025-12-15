using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class EntidadeImagemListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCSize(Framework.SMCSize.Grid24_24)]
        public TipoImagem TipoImagem { get; set; }
    }
}