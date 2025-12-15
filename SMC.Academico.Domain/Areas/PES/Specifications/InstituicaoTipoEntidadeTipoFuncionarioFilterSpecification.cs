using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class InstituicaoTipoEntidadeTipoFuncionarioFilterSpecification : SMCSpecification<InstituicaoTipoEntidadeTipoFuncionario>
    {
        public long? SeqInstituicaoTipoEntidade { get; set; }
        public long? SeqTipoFuncionario { get; set; }
        public long? SeqInstituicaoEnsino { get; set; }        

        public override Expression<Func<InstituicaoTipoEntidadeTipoFuncionario, bool>> SatisfiedBy()
        {
            AddExpression(SeqInstituicaoTipoEntidade, w => w.SeqInstituicaoTipoEntidade == SeqInstituicaoTipoEntidade);
            AddExpression(SeqTipoFuncionario, w => w.SeqTipoFuncionario == SeqTipoFuncionario);
            AddExpression(SeqInstituicaoEnsino, a => a.InstituicaoTipoEntidade.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
           

            return GetExpression();
        }
    }
}