using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class EstruturaCurricularVO : ISMCMappable
    {
        public List<UnidadeCurricularVO> UnidadesCurriculares { get; set; }
    }
}
