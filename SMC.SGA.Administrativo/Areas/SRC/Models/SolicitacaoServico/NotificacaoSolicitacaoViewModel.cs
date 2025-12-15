using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class NotificacaoSolicitacaoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqTipoNotificacao { get; set; }

        [SMCCssClass("smc-size-md-7 smc-size-xs-7 smc-size-sm-7 smc-size-lg-7")]
        [SMCSortable(true, false, "NotificacaoEmail.ConfiguracaoTipoNotificacao.TipoNotificacao.Descricao")]
        public string DescricaoTipoNotificacao { get; set; }

        [SMCHidden]
        public string TokenTipoNotificacao { get; set; }

        [SMCCssClass("smc-size-md-11 smc-size-xs-11 smc-size-sm-11 smc-size-lg-11")]
        [SMCSortable(true, true, "NotificacaoEmail.Assunto")]
        public string Assunto { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public bool? SucessoEnvio { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        [SMCSortable(true, false)]
        public DateTime? DataProcessamento { get; set; }
    }
}