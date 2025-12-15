using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class LancamentoFrequenciaAlunoData : ISMCMappable
    {
        public long SeqAlunoHistoricoCicloLetivo { get; set; }
        public long NumeroRegistroAcademico { get; set; }
        public string Nome { get; set; }
        public List<ApuracaoFrequenciaGradeData> Apuracoes { get; set; }
        public bool AlunoFormado { get; set; }
        public bool AlunoHistoricoEscolar { get; set; }
    }
}
