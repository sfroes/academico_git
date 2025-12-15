using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class HistoricoSolicitacaoEtapaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoServicoEtapa { get; set; }

        [SMCSize(SMCSize.Grid20_24)]
        [SMCCssClass("smc-size-md-22 smc-size-xs-22 smc-size-sm-22 smc-size-lg-22")]
        public string Etapa { get; set; }

        [SMCSize(SMCSize.Grid1_24)]
        [SMCCssClass("smc-size-md-1 smc-size-xs-1 smc-size-sm-1 smc-size-lg-1")]
        public PossuiBloqueio PossuiBloqueio { get; set; }

        [SMCHidden]
        public string BackUrl { get; set; }
    }
}