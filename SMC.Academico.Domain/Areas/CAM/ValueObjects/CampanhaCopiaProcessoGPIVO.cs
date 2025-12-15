using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CampanhaCopiaEtapaProcessoGPIVO : ISMCMappable
    {
        public long SeqProcessoSeletivo { get; set; }

        public List<CampanhaCopiaEtapaProcessoGPIItemVO> EtapasGPI { get; set; }
    }
}