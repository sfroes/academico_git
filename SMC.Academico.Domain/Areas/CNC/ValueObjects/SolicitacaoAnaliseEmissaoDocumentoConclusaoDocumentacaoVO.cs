using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoDocumentacaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqDocumentoRequerido { get; set; }

        public SituacaoEntregaDocumento SituacaoEntregaDocumento { get; set; }

        public FormaEntregaDocumento? FormaEntregaDocumento { get; set; }

        public VersaoDocumento? VersaoDocumento { get; set; }

        public DateTime? DataEntrega { get; set; }

        public long SeqTipoDocumento { get; set; }

        public string DescricaoTipoDocumento { get; set; }

        public string Observacao { get; set; }

        public long? SeqArquivoAnexado { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public DateTime? DataPrazoEntrega { get; set; }

        public string DescricaoInconformidade { get; set; }

        public bool? EntregaPosterior { get; set; }

        public string ObservacaoSecretaria { get; set; }
    }
}
