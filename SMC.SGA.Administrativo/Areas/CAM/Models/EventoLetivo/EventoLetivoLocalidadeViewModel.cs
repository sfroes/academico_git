using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class EventoLetivoLocalidadeViewModel : SMCViewModelBase
    {
        [SMCSelect("ListaLocalidades", AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid22_24)]
        public long SeqLocalidade { get; set; }
    }
}