using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class IngressanteDocumentoVO : ISMCMappable
    {
        public long? SeqArquivoAnexado { get; set; }

        public long SeqTipoDocumento { get; set; }

        public DateTime? DataEntrega { get; set; }

        public DateTime? DataPrazoEntrega { get; set; }

        public FormaEntregaDocumento? FormaEntregaDocumento { get; set; }

        public VersaoDocumento? VersaoDocumento { get; set; }

        public string Observacao { get; set; }

        public SituacaoEntregaDocumento SituacaoEntregaDocumento { get; set; }

        public string DescricaoTipoDocumento { get; set; }
    }
}
