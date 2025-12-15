using SMC.Framework.UI.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.MAT.Models
{
    public class TipoBloqueioViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string Token { get; set; }

        public bool Ativo { get; set; }
    }
}