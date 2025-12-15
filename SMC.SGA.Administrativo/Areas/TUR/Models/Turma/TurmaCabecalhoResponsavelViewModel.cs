using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaCabecalhoResponsavelViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqColaborador { get; set; }
        
        public string NomeColaborador { get; set; }
        
    }
}