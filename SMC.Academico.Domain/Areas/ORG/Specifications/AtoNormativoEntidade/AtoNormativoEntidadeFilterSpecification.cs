using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications.AtoNormativoEntidade
{
    public class AtoNormativoEntidadeFilterSpecification : SMCSpecification<Models.AtoNormativoEntidade>
    {
        public long? Seq { get; set; }

        public long? SeqEntidade { get; set; }

        public long? SeqTipoEntidade { get; set; }

        public long? SeqAtoNormativo { get; set; }

        public List<long> SeqsAtoNormativo { get; set; }

        public List<long> SeqsEntidades { get; set; }

        public string NomeEntidade { get; set; }

        /// <summary>
        /// Apenas atos normativos que a data prazo validade > agora.
        /// </summary>
        public bool ConsiderarApenasAtosVigente { get; set; }

        public bool? HabilitaEmissaoDocumentoConclusao { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public override Expression<Func<Models.AtoNormativoEntidade, bool>> SatisfiedBy()
        {
            AddExpression(Seq, x => x.Seq == Seq);
            AddExpression(SeqEntidade, x => x.SeqEntidade == SeqEntidade);
            AddExpression(SeqsEntidades, x => SeqsEntidades.Contains(x.Entidade.Seq));
            AddExpression(SeqAtoNormativo, x => x.SeqAtoNormativo == SeqAtoNormativo);
            AddExpression(SeqsAtoNormativo, x => SeqsAtoNormativo.Contains(x.SeqAtoNormativo));
            AddExpression(() => ConsiderarApenasAtosVigente, x => !x.AtoNormativo.DataPrazoValidade.HasValue || x.AtoNormativo.DataPrazoValidade > DateTime.Today);
            AddExpression(HabilitaEmissaoDocumentoConclusao, x => x.AtoNormativo.AssuntoNormativo.HabilitaEmissaoDocumentoConclusao == HabilitaEmissaoDocumentoConclusao);
            AddExpression(SeqGrauAcademico, x => x.SeqGrauAcademico == SeqGrauAcademico);
            AddExpression(SeqTipoEntidade, x => x.Entidade.SeqTipoEntidade == SeqTipoEntidade);
            AddExpression(NomeEntidade, x => x.Entidade.Nome.ToLower().Contains(NomeEntidade.ToLower()));

            return GetExpression();
        }
    }
}