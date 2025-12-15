using SMC.Academico.Domain.Models;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class TrabalhoAcademicoSegundoDepositoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqTrabalhoAcademico { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        public DateTime DataAutorizacaoSegundoDeposito { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCMultiline(Rows = 5)]
        public string JustificativaSegundoDeposito { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCFile(
            ActionDownload = "DownloadFileGuid", 
            AreaDownload = "",
            ControllerDownload = "Home", 
            MaxFiles = 1, 
            HideDescription = true,
            DisplayFilesInContextWindow = true)]
        public SMCUploadFile ArquivoAnexadoSegundoDeposito { get; set; }
    }
}