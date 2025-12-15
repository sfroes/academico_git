using SMC.DadosMestres.Common.Areas.PES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaFiliacaoReadOnlyVO
    {
        public long Seq { get; set; }

        public long SeqPessoa { get; set; }

        public TipoParentesco TipoParentescoReadOnly { get; set; }

        public string NomeFiliacaoReadOnly { get; set; }
    }
}
