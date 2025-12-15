using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models.EntregaOnline
{
    public class HistoricoSituacaoEntregaOnlineItemViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCOrder(0)]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public DateTime Data { get; set; }

        [SMCOrder(1)]
        [SMCCssClass("smc-size-md-5 smc-size-xs-5 smc-size-sm-5 smc-size-lg-5")]
        public SituacaoEntregaOnline SituacaoEntregaOnline { get; set; }

        [SMCOrder(2)]
        [SMCCssClass("smc-size-md-8 smc-size-xs-8 smc-size-sm-8 smc-size-lg-8")]
        public string NomeResponsavel { get; set; }

        [SMCOrder(3)]
        [SMCCssClass("smc-size-md-7 smc-size-xs-7 smc-size-sm-7 smc-size-lg-7")]
        public string Observacao { get; set; }
    }
}