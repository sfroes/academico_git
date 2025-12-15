using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.MAT.Models
{
    public class MotivoBloqueioViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public long SeqTipoBloqueio { get; set; }

        public string Descricao { get; set; }

        public string Token { get; set; }

        public string OrientacoesDesbloqueio { get; set; }

        public FormaBloqueio FormaBloqueio { get; set; }

        public FormaBloqueio FormaDesbloqueio { get; set; }

        public bool PermiteDesbloqueioTemporario { get; set; }

        public TipoBloqueioViewModel TipoBloqueio { get; set; }
    }
}