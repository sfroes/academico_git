using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DetalheNotificacaoSolicitacaoVO : ISMCMappable
    {
        public string TipoNotificacao { get; set; }

        public string TokenTipoNotificacao { get; set; }

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

        public PermiteReenvio? PermiteReenvio { get; set; }

        public bool TemEmail { get; set; }
    }
}