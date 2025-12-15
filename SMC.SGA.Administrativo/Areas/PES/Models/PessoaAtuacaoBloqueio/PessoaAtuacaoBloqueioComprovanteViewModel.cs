using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoBloqueioComprovanteViewModel : SMCViewModelBase
    {
        public PessoaAtuacaoBloqueioComprovanteViewModel()
        {
            this.DataAnexo = DateTime.Now.Date;
        }

        #region [ Campos Edição ]

        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacaoBloqueio { get; set; }

        [SMCHidden]
        public long SeqArquivoAnexado { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCReadOnly]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24)]
        public DateTime DataAnexo { get; set; }

        [SMCCssClass("smc-sga-upload-linha-unica")]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid18_24)]
        public SMCUploadFile ArquivoAnexado { get; set; }

        [SMCDescription]
        [SMCHidden(SMCViewMode.List)]
        [SMCMaxLength(100)]
        [SMCMultiline]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        public string Descricao { get; set; }

        #endregion [ Campos Edição ]

        #region [ Campos Listagem ]

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        [SMCIgnoreProp(SMCViewMode.Insert | SMCViewMode.Edit)]
        public DateTime ListaDataAnexo => DataAnexo;

        [SMCCssClass("smc-sga-upload-linha-unica smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true)]
        [SMCIgnoreProp(SMCViewMode.Insert | SMCViewMode.Edit)]
        public SMCUploadFile ListaArquivoAnexado => ArquivoAnexado;

        [SMCCssClass("smc-size-md-12 smc-size-xs-12 smc-size-sm-12 smc-size-lg-12")]
        [SMCIgnoreProp(SMCViewMode.Insert | SMCViewMode.Edit)]
        public string ListaDescricao => Descricao;

        #endregion [ Campos Listagem ]
    }
}