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
    public class TipoProcessoSeletivoPorNivelEnsinoSpecification : SMCSpecification<InstituicaoNivelTipoProcessoSeletivo>
    {
        public TipoProcessoSeletivoPorNivelEnsinoSpecification(List<long> seqsNivelEnsino)
        {
            SeqsNivelEnsino = seqsNivelEnsino;
        }

        public List<long> SeqsNivelEnsino { get; private set; }

        public override Expression<Func<InstituicaoNivelTipoProcessoSeletivo, bool>> SatisfiedBy()
        {
            AddExpression(x => SeqsNivelEnsino.Contains(x.InstituicaoNivelFormaIngresso.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqNivelEnsino));
            return GetExpression();
        }
    }
}
