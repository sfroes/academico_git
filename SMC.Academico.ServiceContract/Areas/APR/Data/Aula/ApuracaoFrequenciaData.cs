using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.APR.Data.Aula
{
    public class ApuracaoFrequenciaData : ISMCMappable
    {
        public bool AlunoComHistorico { get; set; }

        public string NumeroRegistroAcademico { get; set; }

        public string NomeAluno { get; set; }

        public int NumeroFaltas { get; set; }

        public int Total { get; set; }

        public int FaltasAtuais { get; set; }

        public long Seq { get; set; }

        public long SeqAlunoHistoricoCicloLetivo { get; set; }

        public long SeqAula { get; set; }

        public bool AlunoFormado { get; set; }
    }
}