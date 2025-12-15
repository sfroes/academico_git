using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class TurnoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqCursoOferta { get; set; }

        public long? SeqLocalidade { get; set; }
    }
}