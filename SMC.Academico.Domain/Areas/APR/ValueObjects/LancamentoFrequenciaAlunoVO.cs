using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class LancamentoFrequenciaAlunoVO : ISMCMappable
    {
        public long SeqAlunoHistoricoCicloLetivo { get; set; }
        public long NumeroRegistroAcademico { get; set; }
        public string Nome { get; set; }
        public List<ApuracaoFrequenciaGradeVO> Apuracoes { get; set; }
        public long SeqAluno { get; set; }
        public bool AlunoFormado { get; set; }
        public bool AlunoHistoricoEscolar { get; set; }
    }
}
