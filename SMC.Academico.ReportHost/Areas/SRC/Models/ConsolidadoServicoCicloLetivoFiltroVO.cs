using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ReportHost.Areas.MAT.Models
{
    public class ConsolidadoServicoCicloLetivoFiltroVO : ISMCMappable
    {
        public long SeqInstituicaoEnsino { get; set; }
        public long SeqCicloLetivo { get; set; }
        public List<long> SeqsEntidadeResponsavel { get; set; }
        public long SeqServico { get; set; }
    }
}