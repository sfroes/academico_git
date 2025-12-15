using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class RelatorioServicoCicloLetivoFiltroData : ISMCMappable
    {
        public long? SeqCicloLetivo { get; set; }

        public long? SeqServico { get; set; }

        public List<long> SeqsEntidadeResponsavel { get; set; }

    }
}