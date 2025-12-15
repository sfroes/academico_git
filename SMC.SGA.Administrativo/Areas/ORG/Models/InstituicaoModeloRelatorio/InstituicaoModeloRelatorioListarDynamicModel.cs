using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class InstituicaoModeloRelatorioListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCSortable(true, true/*, "InstituicaoEnsino.Nome"*/)]
        public string DescricaoInstituicaoEnsino { get; set; }

        [SMCSortable(false, true)]
        public ModeloRelatorio ModeloRelatorio { get; set; }

        [SMCSortable(false, true)]
        public Idioma? Idioma { get; set; }

        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true)]
        [SMCCssClass("smc-sga-upload-linha-unica")]
        public SMCUploadFile ArquivoModelo { get; set; }
    }
}