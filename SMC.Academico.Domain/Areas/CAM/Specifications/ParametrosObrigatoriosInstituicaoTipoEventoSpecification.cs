using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class ParametrosObrigatoriosInstituicaoTipoEventoSpecification : SMCSpecification<InstituicaoTipoEventoParametro>
    {
        public string TokenTipoEvento { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqCicloLetivo { get; set; }

        public override Expression<Func<InstituicaoTipoEventoParametro, bool>> SatisfiedBy()
        {
            AddExpression(p => p.InstituicaoTipoEvento.Token == this.TokenTipoEvento);
            AddExpression(p => p.InstituicaoTipoEvento.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino);
            AddExpression(p => p.CicloLetivoTipoEventoParametros.Any(f => f.CicloLetivoTipoEvento.SeqCicloLetivo == this.SeqCicloLetivo));

            return GetExpression();
        }
    }
}
