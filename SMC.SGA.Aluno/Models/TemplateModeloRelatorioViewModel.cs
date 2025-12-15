using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Aluno.Models
{
    public class TemplateModeloRelatorioViewModel
    { 
        public long Seq { get; set; }
         
        public long? SeqArquivo { get; set; }
         
        [SMCFile(HideDescription = true, DisplayFilesInContextWindow = true, MaxFileSize = 26214400,
            ActionDownload = "DownloadFileGuid", ControllerDownload = "Home")]
        [SMCSize(Framework.SMCSize.Grid5_24)]
        public SMCUploadFile ArquivoModelo { get; set; }
          
        public ModeloRelatorio TipoTemplateRelatorio { get; set; }
    }
}