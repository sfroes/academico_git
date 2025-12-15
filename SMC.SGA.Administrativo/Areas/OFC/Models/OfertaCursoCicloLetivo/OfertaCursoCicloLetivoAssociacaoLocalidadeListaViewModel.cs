using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.OFC.Models
{
    public class OfertaCursoCicloLetivoAssociacaoLocalidadeListaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long SeqOfertaCursoCicloLetivo { get; set; }

        [SMCHidden]
        public long SeqAssociacaoLocalidade { get; set; }

        public string NomeLocalidade { get; set; }
    }
}