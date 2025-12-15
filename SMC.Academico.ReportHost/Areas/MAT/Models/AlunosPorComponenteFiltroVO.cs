using SMC.Framework.Mapper;

namespace SMC.Academico.ReportHost.Areas.MAT.Models
{
    public class AlunosPorComponenteFiltroVO : ISMCMappable
    {
        public long SeqCicloLetivo { get; set; }

        public long SeqCursoOfertaLocalidade { get; set; }

        public long SeqTurno { get; set; }

        public long? SeqTurma { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public bool? ExibirSolicitanteMatrículaNaoFinalizada { get; set; }

        public long SeqInstituicaoEnsino { get; set; }
    }
}