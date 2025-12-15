using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class TipoDivisaoComponenteFilterSpecification : SMCSpecification<TipoDivisaoComponente>
    {
        public long? Seq { get; set; }

        public long? SeqTipoComponenteCurricular { get; set; }

        public long? SeqModalidade { get; set; }

        public List<long> ListSeqModalidade { get; set; } = new List<long>();

        public TipoGestaoDivisaoComponente[] TiposGestaoDivisaoComponente { get; set; }

        public override Expression<Func<TipoDivisaoComponente, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => this.Seq.Value == p.Seq);
            AddExpression(this.SeqTipoComponenteCurricular, p => this.SeqTipoComponenteCurricular.Value == p.SeqTipoComponenteCurricular);
            AddExpression(this.SeqModalidade, p => (p.SeqModalidade.HasValue && this.SeqModalidade.Value == p.SeqModalidade.Value));
            AddExpression(this.ListSeqModalidade, p => (p.SeqModalidade.HasValue && this.ListSeqModalidade.Contains(p.SeqModalidade.Value)));
            AddExpression(this.TiposGestaoDivisaoComponente, p => this.TiposGestaoDivisaoComponente.Contains(p.TipoGestaoDivisaoComponente));

            return GetExpression();
        }
    }
}