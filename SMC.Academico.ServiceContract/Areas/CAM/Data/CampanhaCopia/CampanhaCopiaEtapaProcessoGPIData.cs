using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaCopiaEtapaProcessoGPIData : ISMCMappable
    {
        public long SeqProcessoSeletivo { get; set; }

        public List<CampanhaCopiaEtapaProcessoGPIItemData> EtapasGPI { get; set; }
    }
}