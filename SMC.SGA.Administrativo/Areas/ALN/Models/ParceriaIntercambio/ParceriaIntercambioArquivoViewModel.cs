using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class ParceriaIntercambioArquivoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqParceriaIntercambio { get; set; }

        [SMCHidden]
        public long SeqArquivoAnexado { get; set; }

        [SMCRequired]
        [SMCCssClass("smc-sga-upload-linha-unica")]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true)]
        public SMCUploadFile ArquivoAnexado { get; set; } 

        [SMCSize(SMCSize.Grid9_24)]
        public string Observacao { get; set; }
    }
}