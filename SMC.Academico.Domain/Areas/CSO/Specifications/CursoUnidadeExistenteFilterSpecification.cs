using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class CursoUnidadeExistenteFilterSpecification : SMCSpecification<CursoUnidade>
    {
		//Seq a ser desconsiderado na pesquisa (próprio registro)
		public long? Seq { get; set; }

		public long? SeqDiferente { get; set; }

		public long? SeqCurso { get; set; }

        /// <summary>
        /// Sequencial do HierarquiaEntidadeItem que represente a unidade
        /// </summary>
        public long? SeqUnidade { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public string Nome { get; set; }

        public override Expression<Func<CursoUnidade, bool>> SatisfiedBy()
        {
			AddExpression(Seq, w => w.Seq == this.Seq);
			AddExpression(SeqDiferente, w => w.Seq != this.SeqDiferente);
			AddExpression(SeqCurso, w => w.SeqCurso == this.SeqCurso);
            AddExpression(SeqInstituicaoEnsino, w => w.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino);
            AddExpression(Nome, w => w.Nome == this.Nome);
            AddExpression(SeqUnidade, w => w.HierarquiasEntidades.Any(a => a.SeqItemSuperior == this.SeqUnidade));

            return GetExpression();
        }
    }
}