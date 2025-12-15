using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaCopiaEtapaProcessoGPIItemViewModel : SMCViewModelBase, ISMCStatefulView
    {
        [SMCSize(SMCSize.Grid1_24)]
        [SMCHideLabel]
        public bool? Checked { get; set; }

        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqProcessoSeletivo { get; set; }

        [SMCHidden]
        public string Token { get; set; }

        [SMCSize(SMCSize.Grid7_24)]
        [SMCDisplay]
        [SMCConditionalRequired(nameof(Checked), true)]
        public string Descricao { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCConditionalRequired(nameof(Checked), true)]
        [SMCConditionalReadonly(nameof(Checked), false)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataInicioEtapa { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCConditionalRequired(nameof(Checked), true)]
        [SMCConditionalReadonly(nameof(Checked), false)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCMinDate(nameof(DataInicioEtapa))]
        public DateTime? DataFimEtapa { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCSelect]
        [SMCConditionalRequired(nameof(Checked), true)]
        [SMCConditionalReadonly(nameof(Checked), false)]
        public bool? CopiarConfiguracaoEtapa { get; set; }
    }
}