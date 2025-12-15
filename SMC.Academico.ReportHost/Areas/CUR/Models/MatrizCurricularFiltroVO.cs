using SMC.Framework.Mapper;

namespace SMC.Academico.ReportHost.Areas.CUR.Models
{
    public class MatrizCurricularFiltroVO : ISMCMappable
    {
        public long SeqMatrizCurricular { get; set; }

        public long SeqInstituicaoEnsino { get; set; }
    }
}