using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.OFC.Models
{
    public class OfertaCursoCicloLetivoBotaoConfigurarViewModel : SMCViewModelBase, ISMCMappable
    {
        public SMCEncryptedLong SeqOfertaCursoCicloLetivo { get; set; }
        public OfertaCursoCicloLetivoActionsEnum Action { get; set; }
    }
}