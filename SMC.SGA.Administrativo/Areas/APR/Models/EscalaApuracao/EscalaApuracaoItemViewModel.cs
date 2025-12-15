using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.APR.Models
{
    public class EscalaApuracaoItemViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCDescription]
        [SMCOrder(0)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid10_24)]
        public string Descricao { get; set; }

        [SMCConditionalReadonly("TipoEscalaApuracao", SMCConditionalOperation.NotEqual, TipoEscalaApuracao.Conceito)]
        [SMCConditionalRequired("TipoEscalaApuracao", SMCConditionalOperation.Equals, TipoEscalaApuracao.Conceito)]
        [SMCMaxValue(100)]
        [SMCMinValue(0)]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCDecimalDigits(2)]
        public decimal? PercentualMinimo { get; set; }

        [SMCConditionalReadonly("TipoEscalaApuracao", SMCConditionalOperation.NotEqual, TipoEscalaApuracao.Conceito)]
        [SMCConditionalRequired("TipoEscalaApuracao", SMCConditionalOperation.Equals, TipoEscalaApuracao.Conceito)]
        [SMCMaxValue(100)]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCMinValue(nameof(PercentualMinimo))]
        [SMCDecimalDigits(2)]
        public decimal? PercentualMaximo { get; set; }

        [SMCOrder(3)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public bool Aprovado { get; set; }
    }
}