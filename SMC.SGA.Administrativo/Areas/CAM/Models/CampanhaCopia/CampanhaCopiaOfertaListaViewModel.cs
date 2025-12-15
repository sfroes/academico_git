using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaCopiaOfertaListaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        public string TipoOferta { get; set; }

        public string Oferta { get; set; }

        public int Vagas { get; set; }
    }
}