using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class InstituicaoNivelModeloRelatorioFilterSpecification : SMCSpecification<InstituicaoNivelModeloRelatorio>
    {
        public long? SeqInstituicaoNivel { get; set; }

        public ModeloRelatorio ModeloRelatorio { get; set; } 

        public override Expression<Func<InstituicaoNivelModeloRelatorio, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqInstituicaoNivel, p => p.SeqInstituicaoNivel == this.SeqInstituicaoNivel);
            AddExpression(() => ModeloRelatorio != ModeloRelatorio.Nenhum, p => p.ModeloRelatorio == ModeloRelatorio);

            return GetExpression();
        }
    }
} 