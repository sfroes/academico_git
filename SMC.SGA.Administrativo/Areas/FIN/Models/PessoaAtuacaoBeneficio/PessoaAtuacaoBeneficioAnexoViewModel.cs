using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class PessoaAtuacaoBeneficioAnexoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacaoBeneficio { get; set; }

        [SMCHidden]
        public long? SeqArquivoAnexado { get; set; }

        [SMCHidden]
        public Guid? UidArquivoAnexado { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid3_24)]
        public DateTime? DataInclusao { get; set; }

        [SMCRequired]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCCssClass("smc-sga-upload-linha-unica")]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true)]
        public SMCUploadFile ArquivoAnexado { get; set; }

        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid9_24)]
        public string Observacao { get; set; }
    }
}