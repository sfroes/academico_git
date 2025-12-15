using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class PlanoEstudoItemTurmasAlunoVO : ISMCMappable
    {
        public long SeqAlunoHistoricoCicloLetivo { get; set; }

        public long? SeqDivisaoTurma { get; set; }
    }
}
