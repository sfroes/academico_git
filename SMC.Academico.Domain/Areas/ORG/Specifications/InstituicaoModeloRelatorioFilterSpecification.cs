using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class InstituicaoModeloRelatorioFilterSpecification : SMCSpecification<InstituicaoModeloRelatorio>
    {
        public long? SeqInstituicaoEnsino { get; set; }

        public ModeloRelatorio ModeloRelatorio { get; set; } 


        public override Expression<Func<InstituicaoModeloRelatorio, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqInstituicaoEnsino, p => p.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino);
            AddExpression(() => ModeloRelatorio != ModeloRelatorio.Nenhum, p => p.ModeloRelatorio == ModeloRelatorio);

            return GetExpression();
        }
    }
} 