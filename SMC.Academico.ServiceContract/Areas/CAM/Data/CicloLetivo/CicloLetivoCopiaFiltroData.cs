using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CicloLetivoCopiaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public List<long> SeqsCiclosLetivos { get; set; }

        public short AnoDestino { get; set; }
    }
}