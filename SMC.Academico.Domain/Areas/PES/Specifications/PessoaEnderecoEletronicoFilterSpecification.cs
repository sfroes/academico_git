using SMC.Academico.Domain.Areas.PES.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class PessoaEnderecoEletronicoFilterSpecification : SMCSpecification<PessoaEnderecoEletronico>
    {
        public long? SeqPessoa { get; set; }

        public TipoEnderecoEletronico? TipoEnderecoEletronico { get; set; }

        public long[] Seqs { get; set; }

        public override Expression<Func<PessoaEnderecoEletronico, bool>> SatisfiedBy()
        {
            AddExpression(Seqs, w => Seqs.Contains(w.Seq));
            AddExpression(SeqPessoa, w => w.SeqPessoa == SeqPessoa.Value);
            AddExpression(TipoEnderecoEletronico, w => w.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Value);

            return GetExpression();
        }
    }
}