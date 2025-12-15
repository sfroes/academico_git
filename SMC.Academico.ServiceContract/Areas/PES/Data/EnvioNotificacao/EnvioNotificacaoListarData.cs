using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class EnvioNotificacaoListarData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }
        public string Assunto { get; set; }
        public string Remetente { get; set; }
        public TipoAtuacao TipoAtuacao { get; set; }
        public DateTime DataEnvio { get; set; }
        public string UsuarioEnvio { get; set; }
        public List<long> SeqsDestinatarios { get; set; }
        public long SeqConfiguracaoTipoNotificacao { get; set; }
        public virtual Nullable<long> SeqLayoutMensagemEmail { get; set; }
    }
}