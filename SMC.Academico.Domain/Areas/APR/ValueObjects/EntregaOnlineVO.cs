using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class EntregaOnlineVO : ISMCMappable
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

        //Conteudo Arquivo anexado
        public byte[] Conteudo { get; set; }

        public List<EntregaOnlineParticipanteVO> Participantes { get; set; }
    }
}