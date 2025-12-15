using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class DetalheNotificacaoSolicitacaoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long? SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public long? SeqNotificacaoEmailDestinatario { get; set; }

        public string TipoNotificacao { get; set; }

        [SMCHidden]
        public string TokenTipoNotificacao { get; set; }

        [SMCHidden]
        public PermiteReenvio PermiteReenvio { get; set; }

        [SMCHidden]
        public bool TemEmail { get; set; }

        public string NomeOrigem { get; set; }

        public string EmailOrigem { get; set; }

        public string EmailResposta { get; set; }

        public string Assunto { get; set; }

        public DateTime? DataPrevistaEnvio { get; set; }

        public DateTime? DataProcessamento { get; set; }

        public bool? SucessoEnvio { get; set; }

        public string EmailDestinatario { get; set; }

        public string EmailCopia { get; set; }

        public string EmailCopiaOculta { get; set; }

        public string Mensagem { get; set; }
    }
}