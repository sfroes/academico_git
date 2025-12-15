using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class SolicitacaoHistoricoSituacaoFilterSpecification : SMCSpecification<SolicitacaoHistoricoSituacao>
    {
        public SolicitacaoHistoricoSituacaoFilterSpecification()
        {
            SetOrderByDescending(s => s.DataInclusao);
        }

        public long? SeqSolicitacaoServico { get; set; }

        public long? Seq { get; set; }
        public CategoriaSituacao? CategoriaSituacao { get; set; }
        public bool? ValidarDataExclusao { get; set; }

        public override Expression<Func<SolicitacaoHistoricoSituacao, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, a => a.Seq == this.Seq);
            AddExpression(this.SeqSolicitacaoServico, a => a.SolicitacaoServicoEtapa.SeqSolicitacaoServico == this.SeqSolicitacaoServico);
            //FIX: Pode ser removido quando a regra de verificação da ultima situação etapa for atualizada
            AddExpression(this.ValidarDataExclusao, a => !a.DataExclusao.HasValue);
            AddExpression(this.CategoriaSituacao, a => a.CategoriaSituacao == this.CategoriaSituacao);

            return GetExpression();
        }
    }
}