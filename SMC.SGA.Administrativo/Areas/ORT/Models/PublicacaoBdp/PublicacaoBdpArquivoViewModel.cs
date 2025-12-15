using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{   
    public class PublicacaoBdpArquivoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public string NomeArquivo { get; set; }

        [SMCHidden]
        public long? TamanhoArquivo { get; set; }

        [SMCHidden]
        public string UrlArquivo { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect()]
        public TipoAutorizacao TipoAutorizacao { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid16_24)]
        [SMCCssClass("smc-sga-upload-linha-unica")]
        [SMCFile(AllowedFileExtensions = new string[] { "pdf" }, HideDescription = true, ActionDownload = "Download", ControllerDownload = "PublicacaoBdp", AreaDownload = "ORT")]
        public SMCUploadFile Arquivo { get; set; }

    }
}