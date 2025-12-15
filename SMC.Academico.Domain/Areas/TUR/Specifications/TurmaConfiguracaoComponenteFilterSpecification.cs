using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;
using System.Linq;
using SMC.Academico.Common.Areas.TUR.Enums;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.TUR.Specifications
{
    public class TurmaConfiguracaoComponenteFilterSpecification : SMCSpecification<TurmaConfiguracaoComponente>
    {
        public long? Seq { get; set; }

        public long? SeqTurma { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }
        public List<long> SeqsConfiguracaoComponente { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqMatrizCurricular { get; set; }

        public SituacaoTurma? SituacaoTurmaAtual { get; set; }

        public long? SeqAssunto { get; set; }

        public override Expression<Func<TurmaConfiguracaoComponente, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.SeqTurma, p => p.SeqTurma == this.SeqTurma);
            AddExpression(this.SeqConfiguracaoComponente, p => p.SeqConfiguracaoComponente == this.SeqConfiguracaoComponente);
            AddExpression(SeqComponenteCurricular, p => p.ConfiguracaoComponente.SeqComponenteCurricular == SeqComponenteCurricular.Value);
            AddExpression(SeqCicloLetivo, p => p.Turma.SeqCicloLetivoInicio == SeqCicloLetivo.Value);
            AddExpression(SeqMatrizCurricular, p => p.RestricoesTurmaMatriz.Any(a => a.MatrizCurricularOferta.MatrizCurricular.Seq == SeqMatrizCurricular));
            AddExpression(SituacaoTurmaAtual, p => p.Turma.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoTurma == this.SituacaoTurmaAtual);
            AddExpression(SeqAssunto, p => p.RestricoesTurmaMatriz.Any(a => a.SeqComponenteCurricularAssunto == SeqAssunto));
            AddExpression(this.SeqsConfiguracaoComponente, p => SeqsConfiguracaoComponente.Contains( p.SeqConfiguracaoComponente));

            return GetExpression();
        }
    }
}
