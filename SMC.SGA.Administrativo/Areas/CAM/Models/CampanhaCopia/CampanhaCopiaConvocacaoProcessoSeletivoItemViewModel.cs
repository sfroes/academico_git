using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaCopiaConvocacaoProcessoSeletivoItemViewModel : SMCViewModelBase, ISMCStatefulView
    {
        [SMCSize(SMCSize.Grid1_24)]
        [SMCHideLabel]
        public bool Checked { get; set; }

        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqProcessoSeletivo { get; set; }

        [SMCSize(SMCSize.Grid17_24)]
        [SMCConditionalRequired(nameof(Checked), true)]
        [SMCConditionalReadonly(nameof(Checked), SMCConditionalOperation.Equals, false)]
        public string Descricao { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect("CiclosLetivos", AutoSelectSingleItem = true)]
        [SMCConditionalRequired(nameof(Checked), true)]
        [SMCConditionalReadonly(nameof(Checked), SMCConditionalOperation.Equals, false)]
        public long SeqCicloLetivo { get; set; }

        [SMCSize(SMCSize.Grid7_24)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.Insert | SMCViewMode.List | SMCViewMode.Edit)]
        [SMCDisplay]
        public string DescricaoCicloLetivo { get; set; }
    }
}