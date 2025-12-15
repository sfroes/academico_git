using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class VagasProcessoSeletivoOfertaData : ISMCMappable
    {
        public long SeqProcessoSeletivo { get; set; }

        public List<ProcessoSeletivoOfertaListaData> ProcessoSeletivoOfertas { get; set; }

        public long[] SeqsConvocacoes { get; set; }
    }
}
