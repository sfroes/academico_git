using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class EnvioNotificacaoDestinatarioListarData : ISMCMappable, ISMCSeq
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