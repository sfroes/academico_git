using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class NotificacaoSolicitacaoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoNotificacao { get; set; }

        public string DescricaoTipoNotificacao { get; set; }

        public string TokenTipoNotificacao { get; set; }

        public string Assunto { get; set; }

        public bool? SucessoEnvio { get; set; }

        public DateTime? DataProcessamento { get; set; }
    }
}