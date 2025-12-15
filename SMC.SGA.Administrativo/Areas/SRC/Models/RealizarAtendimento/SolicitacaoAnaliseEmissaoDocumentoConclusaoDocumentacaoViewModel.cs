using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoDocumentacaoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqDocumentoRequerido { get; set; }

        [SMCHidden]
        public SituacaoEntregaDocumento SituacaoEntregaDocumento { get; set; }

        [SMCHidden]
        public FormaEntregaDocumento? FormaEntregaDocumento { get; set; }

        [SMCHidden]
        public VersaoDocumento? VersaoDocumento { get; set; }

        [SMCHidden]
        public DateTime? DataEntrega { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(SolicitacaoAnaliseEmissaoDocumentoConclusaoViewModel.TiposDocumento), NameDescriptionField = nameof(DescricaoTipoDocumento))]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqTipoDocumento { get; set; }

        [SMCHidden]
        public string DescricaoTipoDocumento { get; set; }

        [SMCSize(SMCSize.Grid11_24)]
        public string Observacao { get; set; }

        [SMCHidden]
        public long? SeqArquivoAnexado { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCFile(HideDescription = true, DisplayFilesInContextWindow = true, ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", AreaDownload = ""/*, MaxFileSize = 26214400, AllowedFileExtensions = new string[] { "pdf/a" }*/)]
        public SMCUploadFile ArquivoAnexado { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public DateTime? DataPrazoEntrega { get; set; }

        [SMCHidden]
        public string DescricaoInconformidade { get; set; }

        [SMCHidden]
        public bool? EntregaPosterior { get; set; }

        [SMCHidden]
        public string ObservacaoSecretaria { get; set; }
    }
}