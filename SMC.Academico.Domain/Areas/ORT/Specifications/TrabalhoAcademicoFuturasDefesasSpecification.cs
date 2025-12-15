using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.Specifications
{
    public class TrabalhoAcademicoFuturasDefesasSpecification : SMCSpecification<TrabalhoAcademico>
    {
        public long SeqInstituicaoLogada { get; set; }

        public override Expression<Func<TrabalhoAcademico, bool>> SatisfiedBy()
        {
            var dataFutura = Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString());
            AddExpression(x => x.SeqInstituicaoEnsino == SeqInstituicaoLogada);
            AddExpression(x => x.DivisoesComponente.Any(f => f.OrigemAvaliacao.AplicacoesAvaliacao.Any(g => g.Avaliacao.TipoAvaliacao == Common.Areas.APR.Enums.TipoAvaliacao.Banca && !g.DataCancelamento.HasValue && g.DataInicioAplicacaoAvaliacao >= dataFutura)));

            return GetExpression();
        }
    }
}
