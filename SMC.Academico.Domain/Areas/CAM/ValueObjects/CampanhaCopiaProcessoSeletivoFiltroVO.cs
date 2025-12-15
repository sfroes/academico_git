using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CampanhaCopiaProcessoSeletivoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqCampanhaOrigem { get; set; }
    }
}