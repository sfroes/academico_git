using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.CustomFilters
{
    public class ViewCentralSolicitacaoServicoCustomFilter : SMCCustomFilter<ViewCentralSolicitacaoServico>
    {
        [SMCFilterParameter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL, true)]
        public long[] SeqsHierarquias { get; set; }

        public override Expression<Func<ViewCentralSolicitacaoServico, bool>> SatisfiedBy()
        {
            return x => SeqsHierarquias.Contains(x.SolicitacaoServico.EntidadeResponsavel.HierarquiasEntidades.FirstOrDefault().Seq) ||
                        (x.SeqEntidadeCompartilhada.HasValue && SeqsHierarquias.Contains(x.SolicitacaoServico.EntidadeCompartilhada.HierarquiasEntidades.FirstOrDefault().Seq));
        }
    }
}