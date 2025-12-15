using SMC.Framework.UI.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class ArquivoSecaoPaginaViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoEtapaPagina { get; set; }

        public long SeqSecaoPaginaSgf { get; set; }

        public string TokenSecao { get; set; }

        public int Ordem { get; set; }

        public string LinkArquivo { get; set; }

        public string MensagemArquivo { get; set; }

        public long SeqArquivoAnexado { get; set; }
    }
}