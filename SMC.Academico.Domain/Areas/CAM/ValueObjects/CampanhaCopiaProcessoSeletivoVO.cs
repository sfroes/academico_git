using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CampanhaCopiaConvocacaoProcessoSeletivoVO : ISMCMappable
    {
        public long SeqProcessoSeletivo { get; set; }

        public List<CampanhaCopiaConvocacaoProcessoSeletivoItemVO> Convocacoes { get; set; }
    }
}