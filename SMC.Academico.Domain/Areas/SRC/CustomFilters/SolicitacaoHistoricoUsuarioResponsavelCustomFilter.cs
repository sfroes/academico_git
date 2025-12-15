using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.SRC.CustomFilters
{
    public class SolicitacaoHistoricoUsuarioResponsavelCustomFilter : SMCCustomFilter<SolicitacaoHistoricoUsuarioResponsavel>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<SolicitacaoHistoricoUsuarioResponsavel, bool>> SatisfiedBy()
        {
            return x => SeqsHierarquias.Contains(x.SolicitacaoServico.EntidadeResponsavel.HierarquiasEntidades.FirstOrDefault().Seq);
        }
    }
}
