using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class InstituicaoNivelTipoCursoFilterSpecification : SMCSpecification<InstituicaoNivelTipoCurso>
    {
        public long? Seq { get; set; }
               
        public long? SeqInstituicaoNivel { get; set; }

        public TipoCurso? TipoCurso { get; set; }
        
        public long? SeqNivelEnsino { get; set; }

        public override Expression<Func<InstituicaoNivelTipoCurso, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == this.Seq);
            AddExpression(SeqInstituicaoNivel, w => this.SeqInstituicaoNivel.Value == w.SeqInstituicaoNivel);
            AddExpression(TipoCurso, w => this.TipoCurso.Value == w.TipoCurso);
            AddExpression(SeqNivelEnsino, w => this.SeqNivelEnsino.Value == w.InstituicaoNivel.SeqNivelEnsino);

            return GetExpression();
        }

    }
}
