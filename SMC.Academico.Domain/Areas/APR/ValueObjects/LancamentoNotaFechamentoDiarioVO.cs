using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class LancamentoNotaFechamentoDiarioVO : ISMCMappable
    {
        public long SeqProfessor { get; set; }

        public long SeqOrigemAvaliacao { get; set; }

        public string MateriaLecionada { get; set; }

        public bool FechamentoIndividual { get; set; }

        public List<LancamentoNotaFechamentoDiarioAlunoVO> Alunos { get; set; }
    }
}
