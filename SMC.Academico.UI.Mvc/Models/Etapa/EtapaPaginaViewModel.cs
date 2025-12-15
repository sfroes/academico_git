using SMC.Framework.UI.Mvc;

namespace SMC.Academico.UI.Mvc.Models
{
    public class EtapaPaginaViewModel : SMCViewModelBase
    {
        public string Titulo { get; set; }

        public long Seq { get; set; }

        public long? SeqSituacaoEtapaInicial { get; set; }

        public long? SeqSituacaoEtapaFinal { get; set; }
    }
}