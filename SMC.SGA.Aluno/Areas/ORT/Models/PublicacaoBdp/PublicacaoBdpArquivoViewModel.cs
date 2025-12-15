using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.ORT.Models
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
        [SMCSize(SMCSize.Grid4_24)]
        [SMCSelect(nameof(PublicacaoBdpViewModel.TiposAutorizacoes))]
        public TipoAutorizacao TipoAutorizacao { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid18_24)]
        [SMCCssClass("smc-sga-upload-linha-unica")]
        [SMCFile(AllowedFileExtensions = new string[] { "pdf" }, HideDescription = true, ActionDownload = "Download", ControllerDownload = "PublicacaoBdp", AreaDownload = "ORT")]
        public SMCUploadFile Arquivo { get; set; }
        
    }
}