using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class InstituicaoTipoAtuacaoFilterSpecification : SMCSpecification<InstituicaoTipoAtuacao>
    {
        public long? SeqInstituicaoEnsino { get; set; }

        public TipoAtuacao? TipoAtuacao { get; set; }

        public override Expression<Func<InstituicaoTipoAtuacao, bool>> SatisfiedBy()
        { 
            AddExpression(SeqInstituicaoEnsino, w => w.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino.Value);
            AddExpression(TipoAtuacao, w => w.TipoAtuacao == this.TipoAtuacao.Value);

            return GetExpression();
        }
    }
}