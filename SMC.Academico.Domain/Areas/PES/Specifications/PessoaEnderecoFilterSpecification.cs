using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using SMC.Localidades.Common.Areas.LOC.Enums;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class PessoaEnderecoFilterSpecification : SMCSpecification<PessoaEndereco>
    {
        public long? SeqPessoa { get; set; }

        public TipoEndereco? TipoEndereco { get; set; }

        public long[] Seqs { get; set; }

        public override Expression<Func<PessoaEndereco, bool>> SatisfiedBy()
        {
            AddExpression(Seqs, w => Seqs.Contains(w.Seq));
            AddExpression(SeqPessoa, w => w.SeqPessoa == SeqPessoa.Value);
            AddExpression(TipoEndereco, w => w.Endereco.TipoEndereco == TipoEndereco.Value);

            return GetExpression();
        }
    }
}