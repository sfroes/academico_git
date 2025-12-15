using SMC.Framework.Model;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models.SolicitacaoServico
{
    public class ImpressaoSolicitacaoPadraoDocumentoViewModel
    {
        public string DescricaoTipoDocumento { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }

        public string Observacao { get; set; }

        public string ObservacaoSecretaria { get; set; }

        public long Seq { get; set; }
    }
}