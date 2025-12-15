using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class VagasProcessoSeletivoOfertaVO : ISMCMappable
    {
        public long SeqProcessoSeletivo { get; set; }

        public List<ProcessoSeletivoOfertaListaVO> ProcessoSeletivoOfertas { get; set; }

        public long[] SeqsConvocacoes { get; set; }
    }
}
