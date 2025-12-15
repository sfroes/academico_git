using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ArquivoSecaoPaginaViewModel : SMCViewModelBase, ISMCMappable
    {       
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoEtapaPagina { get; set; }

        [SMCHidden]
        public long SeqSecaoPaginaSgf { get; set; }

        [SMCHidden]
        public string TokenSecao { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24)]
        public int Ordem { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24)]
        public string LinkArquivo { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid9_24)]
        public string MensagemArquivo { get; set; }

        [SMCHidden]
        public long SeqArquivoAnexado { get; set; }
            
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCFile(HideDescription = true, DisplayFilesInContextWindow = true, MaxFileSize = 26214400, ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", AreaDownload = "")]
        public SMCUploadFile ArquivoAnexado { get; set; }
    }
}