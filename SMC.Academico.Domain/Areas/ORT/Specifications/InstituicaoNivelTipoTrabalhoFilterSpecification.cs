using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.Specifications
{
    public class InstituicaoNivelTipoTrabalhoFilterSpecification : SMCSpecification<InstituicaoNivelTipoTrabalho>
    {
        public long? SeqTipoTrabalho { get; set; }

        public long? SeqInstituicaoNivel { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public bool? GeraFinanceiroEntregaTrabalho { get; set; }

        public bool? PublicacaoBibliotecaObrigatoria { get; set; }

        public bool? PermiteInclusaoManual { get; set; }

        public bool? TrabalhoQualificacao { get; set; }

        public override Expression<Func<InstituicaoNivelTipoTrabalho, bool>> SatisfiedBy()
        {
            AddExpression(SeqTipoTrabalho, w => w.SeqTipoTrabalho == SeqTipoTrabalho);
            AddExpression(SeqInstituicaoNivel, w => w.SeqInstituicaoNivel == SeqInstituicaoNivel);
            AddExpression(SeqInstituicaoEnsino, x => x.InstituicaoNivel.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            AddExpression(SeqNivelEnsino, w => w.InstituicaoNivel.SeqNivelEnsino == SeqNivelEnsino);
            AddExpression(GeraFinanceiroEntregaTrabalho, w => w.GeraFinanceiroEntregaTrabalho);
            AddExpression(PublicacaoBibliotecaObrigatoria, w => w.PublicacaoBibliotecaObrigatoria);
            AddExpression(PermiteInclusaoManual, w => w.PermiteInclusaoManual);
            AddExpression(TrabalhoQualificacao, w => w.TrabalhoQualificacao == TrabalhoQualificacao);

            return GetExpression();
        }
    }
}