using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data.EntregaOnline
{
    public class EntregaOnlineData : ISMCMappable
    {
        public long Seq { get; set; }

        public SituacaoEntregaOnline SituacaoEntrega { get; set; }

        public DateTime DataEntrega { get; set; }

        public string Observacao { get; set; }

        public long? SeqArquivoAnexado { get; set; }

        public Guid? UidArquivoAnexado { get; set; }

        public Guid CodigoProtocolo { get; set; }

        public long SeqAplicacaoAvaliacao { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }

        public List<EntregaOnlineParticipanteData> Participantes { get; set; }
    }
}