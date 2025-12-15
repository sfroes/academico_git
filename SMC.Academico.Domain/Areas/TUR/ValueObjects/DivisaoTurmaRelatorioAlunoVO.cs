using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class DivisaoTurmaRelatorioAlunoVO : ISMCMappable
    {
        public long SeqDivisaoTurma { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string NomeAluno { get; set; }

        public long SeqPessoaAtuacao { get; set; }

    }
}
