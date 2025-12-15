using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Calendarios.Common.Areas.CLD.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.GRD.Data
{
    public class EventoAulaFiltroData : ISMCMappable
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
        public bool? DentroPerido { get; set; }
    }
}
