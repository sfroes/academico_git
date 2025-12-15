using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.GRD.ValueObjects
{
    public class EventoAulaVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }
        public long SeqDivisaoTurma { get; set; }
        public string SeqEventoAgd { get; set; }
        public long SeqHorarioAgd { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public int DiaSemana { get; set; }
        public string HoraInicio { get; set; }
        public TimeSpan HoraInicioSpan { get; set; }
        public string HoraFim { get; set; }
        public TimeSpan HoraFimSpan { get; set; }
        public short Turno { get; set; }
        public string Local { get; set; }
        public Guid? CodigoRecorrencia { get; set; }
        public string DiaSemanaFormatada { get; set; }
        public string DiaSemanaDescricao { get; set; }
        public string DescricaoColaboradores { get; set; }
        public int? CodigoLocalSEF { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public List<EventoAulaColaboradorVO> Colaboradores { get; set; }
        public SituacaoApuracaoFrequencia SituacaoApuracaoFrequencia { get; set; }
        public bool Feriado { get; set; }
        public DateTime DataLimiteApuracaoFrequencia { get; set; }
        public long SeqTurma { get; set; }
    }
}
