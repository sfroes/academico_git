using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class DivisaoTurmaRelatorioAlunoData : ISMCMappable
    {
        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        public long SeqDivisaoTurma { get; set; }

        public string DescricaoCursoOfertaLocalidadeTurno { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string NomeAluno { get; set; }

        public bool AlunoDI { get; set; }

        public bool AlunoFormado { get; set; }
    }
}