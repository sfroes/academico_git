using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ReportHost.Areas.MAT.Models
{
    public class IdentidadeAcademicaFiltroVO : ISMCMappable
    {
        public long SeqInstituicaoEnsino { get; set; }
        public List<long> SeqsAlunos { get; set; }
        public List<long> SeqsColaboradores { get; set; }
    }
}