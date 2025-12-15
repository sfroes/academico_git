using SMC.Academico.Domain.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosModalSolicitacaoDocumentoVO  : ISMCMappable
    {
        public long? SeqArquivoAnexado { get; set; }

        public ArquivoAnexado ArquivoAnexado { get; set; }

        public string TipoDocumento { get; set; }

        public DateTime? DataEntrega { get; set; }

        public FormaEntregaDocumento? FormaEntregaDocumento { get; set; }

        public SituacaoEntregaDocumento SituacaoEntregaDocumento { get; set; }

        public VersaoDocumento? VersaoDocumento { get; set; }

        public DateTime? DataPrazoEntrega { get; set; }

        public string Observacao { get; set; }

    }
}
