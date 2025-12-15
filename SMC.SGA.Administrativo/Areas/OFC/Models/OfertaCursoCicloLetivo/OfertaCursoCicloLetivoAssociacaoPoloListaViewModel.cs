using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.OFC.Models
{
    public class OfertaCursoCicloLetivoAssociacaoPoloListaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long SeqOfertaCursoCicloLetivo { get; set; }

        [SMCHidden]
        public long SeqAssociacaoPolo { get; set; }

        public string NomePolo { get; set; }
    }
}