using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class EnvioNotificacaoDestinatarioListarVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqNotificacaoEmail { get; set; }

        public string Assunto { get; set; }

        public string Remetente { get; set; }

        public DateTime DataEnvio { get; set; }

        public string UsuarioEnvio { get; set; }

        public long? SeqNotificacaoEmailDestinatario { get; set; }
    }
}