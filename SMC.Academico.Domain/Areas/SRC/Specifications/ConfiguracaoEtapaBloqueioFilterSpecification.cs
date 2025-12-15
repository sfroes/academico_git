using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ConfiguracaoEtapaBloqueioFilterSpecification : SMCSpecification<ConfiguracaoEtapaBloqueio>
    {
        public long? SeqConfiguracaoEtapa { get; set; }

        public List<long> SeqsConfiguracoesEtapas { get; set; }

        public bool? GeraCancelamentoSolicitacao { get; set; }

        public List<string> TokensMotivoBloqueio { get; set; }

        public long? SeqProcessoEtapa { get; set; }

        public long? SeqMotivoBloqueio { get; set; }

        public override Expression<Func<ConfiguracaoEtapaBloqueio, bool>> SatisfiedBy()
        {
            AddExpression(SeqConfiguracaoEtapa, a => a.SeqConfiguracaoEtapa == SeqConfiguracaoEtapa.Value);
            AddExpression(SeqsConfiguracoesEtapas, a => SeqsConfiguracoesEtapas.Contains(a.ConfiguracaoEtapa.Seq));
            AddExpression(SeqProcessoEtapa, a => a.ConfiguracaoEtapa.SeqProcessoEtapa == SeqProcessoEtapa.Value);
            AddExpression(SeqMotivoBloqueio, a => a.SeqMotivoBloqueio == SeqMotivoBloqueio.Value);
            AddExpression(GeraCancelamentoSolicitacao, w => w.GeraCancelamentoSolicitacao == GeraCancelamentoSolicitacao.Value);
            AddExpression(TokensMotivoBloqueio, a => TokensMotivoBloqueio.Contains(a.MotivoBloqueio.Token));

            return GetExpression();
        }
    }
}