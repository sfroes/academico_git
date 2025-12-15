using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.OFC.Models
{
    public class OfertaCursoCicloLetivoDadosViewModel : SMCViewModelBase, ISMCMappable
    {
        public string OfertaCurso { get; set; }
        public string FormaIngresso { get; set; }
        public string CicloLetivo { get; set; }
    }
}