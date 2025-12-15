using SMC.Academico.ServiceContract.Data;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class DadosModalSolicitacaoDocumentoData : ISMCMappable
    {
        public long? SeqArquivoAnexado { get; set; }

        public ArquivoAnexadoData ArquivoAnexado { get; set; }

        public string TipoDocumento { get; set; }

        public DateTime? DataEntrega { get; set; }

        public FormaEntregaDocumento? FormaEntregaDocumento { get; set; }

        public SituacaoEntregaDocumento SituacaoEntregaDocumento { get; set; }

        public VersaoDocumento? VersaoDocumento { get; set; }

        public DateTime? DataPrazoEntrega { get; set; }

        public string Observacao { get; set; }

    }
}
