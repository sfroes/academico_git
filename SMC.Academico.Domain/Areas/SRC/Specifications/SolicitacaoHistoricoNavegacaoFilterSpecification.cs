using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class SolicitacaoHistoricoNavegacaoFilterSpecification : SMCSpecification<SolicitacaoHistoricoNavegacao>
    {
        public SolicitacaoHistoricoNavegacaoFilterSpecification()
        {
            SetOrderBy(h => h.DataEntrada);
        }

        public long? SeqSolicitacaoServicoEtapa { get; set; }

        public long? Seq { get; set; }

        public long? SeqConfiguracaoEtapaPagina { get; set; }

        public override Expression<Func<SolicitacaoHistoricoNavegacao, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == this.Seq); 
            AddExpression(SeqConfiguracaoEtapaPagina, w => w.SeqConfiguracaoEtapaPagina == SeqConfiguracaoEtapaPagina.Value);
            AddExpression(SeqSolicitacaoServicoEtapa, w => w.SeqSolicitacaoServicoEtapa == SeqSolicitacaoServicoEtapa.Value);

            return GetExpression();
        }
    }
}