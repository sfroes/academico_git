using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.Domain.Areas.GRD.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.FIN.Specifications
{
    public class EventoAulaFilterSpecification : SMCSpecification<EventoAula>
    {
        public long? Seq { get; set; }
        public List<long> Seqs { get; set; }
        public long? SeqTurma { get; set; }
        public long? SeqDivisaoTurma { get; set; }
        public List<long> SeqsDivisaoTurma { get; set; }
        public List<long> SeqsColaborador { get; set; }
        public List<DateTime> Datas { get; set; }
        public long? SeqHorarioAgd { get; set; }
        public DateTime? Data { get; set; }
        public bool? DataFutura { get; set; }
        public SituacaoApuracaoFrequencia? SituacaoApuracaoFrequencia { get; set; }
        public bool? ComPrimeiraApuracao { get; set; }
        public DateTime? InicioPeriodo { get; set; }
        public DateTime? FimPeriodo { get; set; }
        public long? SeqCicloLetivo { get; set; }
        public bool? ComColaborador { get; set; }
        public bool? ComLocal { get; set; }
        public int? CodigoLocalSEF { get; set; }

        public override Expression<Func<EventoAula, bool>> SatisfiedBy()
        {
            AddExpression(Seq, a => a.Seq == Seq);
            AddExpression(Seqs, a => Seqs.Contains(a.Seq));
            AddExpression(SeqTurma, a => a.DivisaoTurma.SeqTurma == SeqTurma);
            AddExpression(SeqDivisaoTurma, a => a.SeqDivisaoTurma == SeqDivisaoTurma);
            AddExpression(SeqsDivisaoTurma, a => SeqsDivisaoTurma.Contains(a.SeqDivisaoTurma));
            AddExpression(SeqsColaborador, a => a.Colaboradores.Any(ac => SeqsColaborador.Contains(ac.SeqColaboradorSubstituto.Value) 
                                                                            || (ac.SeqColaboradorSubstituto == null && SeqsColaborador.Contains(ac.SeqColaborador))));
            AddExpression(Datas, a => Datas.Contains(a.Data));
            AddExpression(SeqHorarioAgd, a => a.SeqHorarioAgd == SeqHorarioAgd);
            AddExpression(Data, a => a.Data == Data);
            AddExpression(DataFutura, a => a.Data > DateTime.Today == DataFutura);
            AddExpression(SituacaoApuracaoFrequencia, a => a.SituacaoApuracaoFrequencia == SituacaoApuracaoFrequencia);
            AddExpression(ComPrimeiraApuracao, a => (a.UsuarioPrimeiraApuracaoFrequencia != null) == ComPrimeiraApuracao);
            AddExpression(InicioPeriodo, a => a.Data >= InicioPeriodo);
            AddExpression(FimPeriodo, a => a.Data <= FimPeriodo);
            AddExpression(SeqCicloLetivo, a => a.DivisaoTurma.Turma.SeqCicloLetivoInicio == SeqCicloLetivo);
            AddExpression(ComColaborador, a => a.Colaboradores.Any() == ComColaborador);
            AddExpression(ComLocal, a => !string.IsNullOrEmpty(a.Local) == ComLocal);
            AddExpression(CodigoLocalSEF, a => a.CodigoLocalSEF == CodigoLocalSEF);

            return GetExpression();
        }
    }
}