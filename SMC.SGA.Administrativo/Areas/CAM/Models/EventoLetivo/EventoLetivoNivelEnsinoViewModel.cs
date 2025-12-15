using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class EventoLetivoNivelEnsinoViewModel : SMCViewModelBase
    {
        [SMCSelect("ListaNiveisEnsino", AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid22_24)]
        public long SeqNivelEnsino { get; set; }
    }
}