using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class LancamentoNotaFechamentoDiarioData : ISMCMappable
    {
        public long SeqProfessor { get; set; }

        public long SeqOrigemAvaliacao { get; set; }

        public string MateriaLecionada { get; set; }

        public bool FechamentoIndividual { get; set; }

        public List<LancamentoNotaFechamentoDiarioAlunoData> Alunos { get; set; }
    }
}
