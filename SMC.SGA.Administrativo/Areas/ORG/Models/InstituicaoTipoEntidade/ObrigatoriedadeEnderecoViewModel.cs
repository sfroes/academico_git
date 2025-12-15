using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Localidades.Common.Areas.LOC.Enums;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class ObrigatoriedadeEnderecoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqInstituicaoTipoEntidade { get; set; }

        [SMCRequired]
        [SMCSelect()]
        [SMCSize(Framework.SMCSize.Grid12_24)]
        public TipoEndereco TipoEndereco { get; set; }

        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(Framework.SMCSize.Grid10_24)]
        public bool Obrigatorio { get; set; }
    }
}