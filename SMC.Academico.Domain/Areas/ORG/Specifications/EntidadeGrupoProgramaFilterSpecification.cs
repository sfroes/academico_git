using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class EntidadeGrupoProgramaFilterSpecification : SMCSpecification<Entidade>
    {
        public override Expression<Func<Entidade, bool>> SatisfiedBy()
        {
            DateTime agora = DateTime.Now;

            AddExpression(p => p.HistoricoSituacoes.Any(h => agora >= h.DataInicio && (!h.DataFim.HasValue || agora <= h.DataFim.Value) && h.SituacaoEntidade.CategoriaAtividade != CategoriaAtividade.Inativa));
            AddExpression(p => p.HierarquiasEntidades.Any(h => h.HierarquiaEntidade.TipoHierarquiaEntidade.TipoVisao == TipoVisao.VisaoOrganizacional));
            AddExpression(p => p.HierarquiasEntidades.Any(h => agora >= h.HierarquiaEntidade.DataInicioVigencia));
            AddExpression(p => p.HierarquiasEntidades.Any(h => !h.HierarquiaEntidade.DataFimVigencia.HasValue || agora <= h.HierarquiaEntidade.DataFimVigencia));
            AddExpression(p => p.TipoEntidade.Token == TOKEN_TIPO_ENTIDADE.GRUPO_PROGRAMA);

            return GetExpression();
        }
    }
}
