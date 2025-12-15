using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaConsultarCandidatoCabecalhoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        public string Descricao { get; set; }
    }
}