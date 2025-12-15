using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CicloLetivoCopiaItemViewModel : SMCDynamicViewModel
    {
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string Descricao { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public SMCMasterDetailList<CicloLetivoNivelEnsinoListarViewModel> NiveisEnsino { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public short Ano { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public short Numero { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string DescricaoRegimeLetivo { get; set; }
    }
}