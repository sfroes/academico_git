using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class TermoIntercambioArquivoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqTermoIntercambio { get; set; }

        [SMCHidden]
        public long SeqArquivoAnexado { get; set; }

        [SMCRequired]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCCssClass("smc-sga-upload-linha-unica")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true)]
        public SMCUploadFile ArquivoAnexado { get; set; }

        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid9_24)]
        public string Observacao { get; set; }
    }
}