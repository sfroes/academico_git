using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class CampanhaUnidadeResponsavelCicloLetivoSpecification : SMCSpecification<Campanha>
    {
        public CampanhaUnidadeResponsavelCicloLetivoSpecification(long seqCampanha, long seqEntidadeResponsavel, IEnumerable<long> ciclosLetivos)
        {
            SeqCampanha = seqCampanha;
            SeqEntidadeResponsavel = seqEntidadeResponsavel;
            CiclosLetivos = ciclosLetivos;
        }

        public long SeqCampanha { get; set; }

        public long SeqEntidadeResponsavel { get; set; }

        public IEnumerable<long> CiclosLetivos { get; set; }

        public override Expression<Func<Campanha, bool>> SatisfiedBy()
        {
            if (SeqCampanha > 0)
            {
                AddExpression(x => x.Seq != SeqCampanha);
            }
            AddExpression(x => x.SeqEntidadeResponsavel == SeqEntidadeResponsavel);
            AddExpression(x => x.CiclosLetivos.Any(c => CiclosLetivos.Contains(c.SeqCicloLetivo)));
            return GetExpression();
        }
    }
}
