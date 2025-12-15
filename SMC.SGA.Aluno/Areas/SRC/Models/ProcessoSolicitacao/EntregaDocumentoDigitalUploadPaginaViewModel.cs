using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class EntregaDocumentoDigitalUploadPaginaViewModel : SMCViewModelBase
    {
        [SMCRequired]
        [SMCSelect(nameof(EntregaDocumentoDigitalPaginaViewModel.TiposDocumento), NameDescriptionField = nameof(DescricaoTipoDocumento))]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqTipoDocumento { get; set; }

        [SMCHidden]
        public string DescricaoTipoDocumento { get; set; }

        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid11_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCFile(HideDescription = true, DisplayFilesInContextWindow = true, ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", AreaDownload = "", MaxFileSize = 5242880, AllowedFileExtensions = new string[] { "doc", "docx", "xls", "xlsx", "jpg", "jpeg", "png", "pdf", "rar", "zip", "ps" })]
        public SMCUploadFile ArquivoAnexado { get; set; }
    }
}