using SMC.Academico.Domain.Areas.PES.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class PessoaTelefoneFilterSpecification : SMCSpecification<PessoaTelefone>
    {
        public long? SeqPessoa { get; set; }

        public TipoTelefone? TipoTelefone { get; set; }

        public long[] Seqs { get; set; }

        public override Expression<Func<PessoaTelefone, bool>> SatisfiedBy()
        {
            AddExpression(Seqs, w => Seqs.Contains(w.Seq)); 
            AddExpression(SeqPessoa, w => w.SeqPessoa == SeqPessoa.Value);
            AddExpression(TipoTelefone, w => w.Telefone.TipoTelefone == TipoTelefone.Value);

            return GetExpression();
        }
    }
}
