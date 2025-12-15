using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.Specifications
{
    public class TrabalhoAcademicoEmPublicacaoSpecification : SMCSpecification<TrabalhoAcademico>
    {
        public long SeqInstituicaoLogada { get; set; }

        public override Expression<Func<TrabalhoAcademico, bool>> SatisfiedBy()
        {
            AddExpression(x => x.SeqInstituicaoEnsino == SeqInstituicaoLogada);
            AddExpression(x => x.PublicacaoBdp.Any(f => f.HistoricoSituacoes.OrderByDescending(o => o.DataInclusao)
                                                    .FirstOrDefault().SituacaoTrabalhoAcademico == Common.Areas.ORT.Enums.SituacaoTrabalhoAcademico.CadastradaAluno)
                        || x.PublicacaoBdp.Any(f => f.HistoricoSituacoes.OrderByDescending(o => o.DataInclusao)
                                                     .FirstOrDefault().SituacaoTrabalhoAcademico == Common.Areas.ORT.Enums.SituacaoTrabalhoAcademico.AutorizadaLiberadaSecretaria)
                        || x.PublicacaoBdp.Any(f => f.HistoricoSituacoes.OrderByDescending(o => o.DataInclusao)
                                                    .FirstOrDefault().SituacaoTrabalhoAcademico == Common.Areas.ORT.Enums.SituacaoTrabalhoAcademico.LiberadaBiblioteca));

            return GetExpression();
        }
    }
}
