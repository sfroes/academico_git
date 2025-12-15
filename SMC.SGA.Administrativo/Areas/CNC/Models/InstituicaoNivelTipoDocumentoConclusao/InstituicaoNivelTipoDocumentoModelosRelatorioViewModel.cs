using SMC.Academico.Common.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class InstituicaoNivelTipoDocumentoModelosRelatorioViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqInstituicaoNivelTipoDocumentoAcademico { get; set; }

        [SMCHidden]
        public long SeqArquivoModelo { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid5_24)]
        [SMCRequired]
        public Linguagem Idioma { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid16_24)]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true, AllowedFileExtensions = new string[] { "dotx" })]
        [SMCCssClass("smc-sga-upload-linha-unica")]
        public SMCUploadFile ArquivoModelo { get; set; }
    }
}