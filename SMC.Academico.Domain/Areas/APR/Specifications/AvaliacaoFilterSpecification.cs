using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
    public class AvaliacaoFilterSpecification : SMCSpecification<Avaliacao>
    {
        public long? Seq { get; set; }

        public long? SeqOrigemAvaliacao { get; set; }

        public TipoAvaliacao? TipoAvaliacao { get; set; }

        public string Sigla { get; set; }

        public string Descricao { get; set; }

        public long? SeqAlunoHistorico { get; set; }

        public override Expression<Func<Avaliacao, bool>> SatisfiedBy()
        {
            AddExpression(Seq, a => a.Seq == Seq);
            AddExpression(SeqOrigemAvaliacao, a => a.AplicacoesAvaliacao.Any(aa => aa.SeqOrigemAvaliacao == SeqOrigemAvaliacao));
            AddExpression(TipoAvaliacao, a => a.TipoAvaliacao == TipoAvaliacao);
            AddExpression(Sigla, a => a.AplicacoesAvaliacao.Any(aa => aa.Sigla.Contains(Sigla)));
            AddExpression(Descricao, a => a.Descricao.Contains(Descricao));
            AddExpression(SeqAlunoHistorico, a => a.AplicacoesAvaliacao.SelectMany(sm => sm.ApuracoesAvaliacao).Any(aa => aa.SeqAlunoHistorico == SeqAlunoHistorico));

            return GetExpression();
        }
    }
}