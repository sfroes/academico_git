
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.FIN.Specifications
{
    public class PessoaJuridicaFilterSpecification : SMCSpecification<PessoaJuridica>
    {
        public long? Seq { get; set; }

        public  string RazaoSocial { get; set; }

        public  string Cnpj { get; set; }

        public  string NomeFantasia { get; set; }

        public override Expression<Func<PessoaJuridica, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.RazaoSocial, p => p.RazaoSocial.StartsWith(this.RazaoSocial));
            AddExpression(this.Cnpj, p => p.Cnpj.StartsWith(this.Cnpj));
            AddExpression(this.NomeFantasia, p => p.NomeFantasia.StartsWith(this.NomeFantasia));

            return GetExpression();
        }
    }
}
