using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class DocumentosSolicitacaoViewModel
    {
        public long Seq { get; set; }

        [SMCFile(ActionDownload = "DownloadAnexo")]
        public SMCUploadFile ArquivoAnexado { get; set; }

        public string Observacao { get; set; }

        public string ObservacaoSecretaria { get; set; }

        public string DescricaoTipoDocumento { get; set; }
    }
}