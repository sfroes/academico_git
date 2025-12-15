using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ParametroEnvioNotificacaoFilterSpecification : SMCSpecification<ParametroEnvioNotificacao>
    {
        public long? Seq { get; set; }

        public long? SeqProcessoEtapaConfiguracaoNotificacao { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }

        public override Expression<Func<ParametroEnvioNotificacao, bool>> SatisfiedBy()
        {
            AddExpression(Seq, a => a.Seq == Seq.Value);
            AddExpression(SeqProcessoEtapaConfiguracaoNotificacao, a => a.SeqProcessoEtapaConfiguracaoNotificacao == SeqProcessoEtapaConfiguracaoNotificacao);
            AddExpression(SeqGrupoEscalonamento, a => a.SeqGrupoEscalonamento == SeqGrupoEscalonamento.Value);

            return GetExpression();
        }
    }
}