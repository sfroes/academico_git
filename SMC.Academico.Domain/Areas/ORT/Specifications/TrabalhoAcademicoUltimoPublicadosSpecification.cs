using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.Specifications
{
    public class TrabalhoAcademicoUltimoPublicadosSpecification : SMCSpecification<TrabalhoAcademico>
    {
        public long SeqInstituicaoLogada { get; set; }

        public override Expression<Func<TrabalhoAcademico, bool>> SatisfiedBy()
        {
            var dataAnterior = Convert.ToDateTime(DateTime.Now.AddMonths(-1).ToShortDateString());
            AddExpression(x => x.SeqInstituicaoEnsino == SeqInstituicaoLogada);
            AddExpression(x => x.PublicacaoBdp.Any(f => f.DataPublicacao.HasValue && dataAnterior < f.DataPublicacao.Value));

            return GetExpression();
        }
    }
}
