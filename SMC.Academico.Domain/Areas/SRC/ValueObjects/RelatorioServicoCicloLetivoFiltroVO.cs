using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class RelatorioServicoCicloLetivoFiltroVO : ISMCMappable
    {
        public long? SeqCicloLetivo { get; set; }

        public long? SeqServico { get; set; }

        public List<long> SeqsEntidadeResponsavel { get; set; }

    }
}