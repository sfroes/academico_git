using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaCicloLetivoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqCampanha { get; set; }

        public long? SeqCicloLetivo { get; set; }
    }
}