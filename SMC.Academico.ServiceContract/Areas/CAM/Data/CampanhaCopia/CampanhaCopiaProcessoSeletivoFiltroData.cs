using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaCopiaProcessoSeletivoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqCampanhaOrigem { get; set; }
    }
}