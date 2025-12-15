using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class DocumentoSolicitacaoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long? SeqArquivoAnexado { get; set; }

        [SMCHidden]
        public Guid? UidArquivoAnexado { get; set; }

        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        public string TipoDocumento { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public DateTime? DataEntrega { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public DateTime? DataPrazoEntrega { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public FormaEntregaDocumento? FormaEntregaDocumento { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public VersaoDocumento? VersaoDocumento { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public SituacaoEntregaDocumento SituacaoEntregaDocumento { get; set; }

        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        public string Observacao { get; set; }

        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true)]
        public SMCUploadFile ArquivoAnexado { get; set; }
    }
}