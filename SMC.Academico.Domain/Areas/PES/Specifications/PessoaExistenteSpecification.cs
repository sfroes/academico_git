using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class PessoaExistenteSpecification : SMCSpecification<Pessoa>
    {
        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public override Expression<Func<Pessoa, bool>> SatisfiedBy()
        {
            AddExpression(Cpf, w => w.Cpf == Cpf);
            AddExpression(NumeroPassaporte, w => w.NumeroPassaporte == NumeroPassaporte);

            return GetExpression();
        }
    }
}
