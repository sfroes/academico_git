using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaCopiaProcessoConvocacaoItemViewModel : SMCViewModelBase
    {
        [SMCSize(SMCSize.Grid1_24)]
        [SMCHideLabel]
        public bool Checked { get; set; }

        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqProcessoSeletivo { get; set; }

        [SMCSize(SMCSize.Grid17_24)]
        public string Descricao { get; set; }

        [SMCSelect("CiclosLetivos")]
        [SMCSize(SMCSize.Grid6_24)]
        public long SeqCicloLetivo { get; set; }

        [SMCSize(SMCSize.Grid7_24)]
        public string DescricaoCicloLetivo { get; set; }
    }
}