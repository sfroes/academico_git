using SMC.Framework.UI.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class TextoSecaoPaginaViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoEtapaPagina { get; set; }

        public long SeqSecaoPaginaSgf { get; set; }

        public string TokenSecao { get; set; }

        public string Texto { get; set; }
    }
}