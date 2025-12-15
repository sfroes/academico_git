using SMC.DadosMestres.Common.Areas.PES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaFiliacaoVO
    {
        public long Seq { get; set; }

        public long SeqPessoa { get; set; }

        public TipoParentesco TipoParentesco { get; set; }

        public string Nome { get; set; }
    }
}
