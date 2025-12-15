using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ConfiguracaoEtapaFilterSpecification : SMCSpecification<ConfiguracaoEtapa>
    {
        public long? Seq { get; set; }

        public long? SeqProcessoEtapa { get; set; }

        public long? SeqConfiguracaoProcesso { get; set; }

        public string TokenProcessoEtapa { get; set; }

        public string[] TokensProcessoEtapa { get; set; }

        public string TokenProcessoEtapaContains { get; set; }

        public override Expression<Func<ConfiguracaoEtapa, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == this.Seq.Value);
            AddExpression(SeqProcessoEtapa, w => w.SeqProcessoEtapa == this.SeqProcessoEtapa.Value);
            AddExpression(SeqConfiguracaoProcesso, w => w.SeqConfiguracaoProcesso == this.SeqConfiguracaoProcesso.Value);
            AddExpression(TokenProcessoEtapa, w => w.ProcessoEtapa.Token == TokenProcessoEtapa);
            AddExpression(TokenProcessoEtapaContains, w => w.ProcessoEtapa.Token.Contains(TokenProcessoEtapaContains));
            AddExpression(TokensProcessoEtapa, w => TokensProcessoEtapa.Contains(w.ProcessoEtapa.Token));

            return GetExpression();
        }
    }
}