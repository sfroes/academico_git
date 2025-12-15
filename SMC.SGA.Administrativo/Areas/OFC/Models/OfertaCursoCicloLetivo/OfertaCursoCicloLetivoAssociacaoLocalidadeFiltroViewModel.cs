using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.OFC.Models
{
    public class OfertaCursoCicloLetivoAssociacaoLocalidadeFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        [SMCFilterKey]
        public long SeqOfertaCursoCicloLetivo { get; set; }

        public OfertaCursoCicloLetivoDadosViewModel DadosOfertaCursoCicloLetivo { get; set; }
    }
}