using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class DocumentoItemData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public long SeqTipoDocumento { get; set; }

        public long? SeqArquivoAnexado { get; set; }

        public DateTime? DataEntrega { get; set; }

        public FormaEntregaDocumento? FormaEntregaDocumento { get; set; }

        public VersaoDocumento? VersaoDocumento { get; set; }

        public VersaoDocumento? VersaoExigida { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }

        public SituacaoEntregaDocumento SituacaoEntregaDocumento { get; set; }

        public SituacaoEntregaDocumento SituacaoEntregaDocumentoInicial { get; set; }

        public bool BloquearTodosOsCampos { get; set; }

        public DateTime? DataPrazoEntrega { get; set; }

        public string Observacao { get; set; }

        public string DescricaoInconformidade { get; set; }

        public string ObservacaoSecretaria { get; set; }

        public DateTime? DataLimiteEntrega { get; set; }

        public bool? EntregaPosterior { get; set; }

        public bool TemAnexoAnterior { get; set; }

        public bool EntregueAnteriormente { get; set; }
    }
}