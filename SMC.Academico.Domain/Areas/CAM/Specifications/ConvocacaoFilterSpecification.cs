using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class ConvocacaoFilterSpecification : SMCSpecification<Convocacao>
    {
        public long? SeqCampanha { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqTipoProcessoSeletivo { get; set; }

        public long? SeqProcessoSeletivo { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public long[] Seqs { get; set; }

        public override Expression<Func<Convocacao, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqCampanha, p => p.ProcessoSeletivo.Campanha.Seq == SeqCampanha);
            AddExpression(this.SeqCicloLetivo, p => p.ProcessoSeletivo.Campanha.CiclosLetivos.Any(cl => cl.SeqCicloLetivo == SeqCicloLetivo));
            AddExpression(this.SeqTipoProcessoSeletivo, p => p.ProcessoSeletivo.SeqTipoProcessoSeletivo == SeqTipoProcessoSeletivo);
            AddExpression(this.SeqProcessoSeletivo, p => p.SeqProcessoSeletivo == SeqProcessoSeletivo);
            AddExpression(this.SeqEntidadeResponsavel, p => p.CampanhaCicloLetivo.Campanha.SeqEntidadeResponsavel == SeqEntidadeResponsavel.Value);
            AddExpression(this.Seqs, p => Seqs.Contains(p.Seq));

            return GetExpression();
        }
    }
}