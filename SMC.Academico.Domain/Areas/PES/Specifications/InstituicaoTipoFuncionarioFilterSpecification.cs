using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class InstituicaoTipoFuncionarioFilterSpecification : SMCSpecification<InstituicaoTipoFuncionario>
    {
        public long? SeqInstituicaoEnsino { get; set; }
        public long? SeqTipoFuncionario { get; set; }

        public override Expression<Func<InstituicaoTipoFuncionario, bool>> SatisfiedBy()
        {
            AddExpression(SeqInstituicaoEnsino, w => w.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            AddExpression(SeqTipoFuncionario, w => w.SeqTipoFuncionario == SeqTipoFuncionario);

            return GetExpression();
        }
    }
}