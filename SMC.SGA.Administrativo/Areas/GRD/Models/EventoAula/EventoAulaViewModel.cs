using SMC.Calendarios.Common.Areas.CLD.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.GRD.Models
{
    public class EventoAulaViewModel : ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqDivisaoTurma { get; set; }
        public string SeqEventoAgd { get; set; }
        public long SeqHorarioAgd { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public int DiaSemana { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFim { get; set; }
        public int Turno { get; set; }
        public string Local { get; set; }
        public Guid? CodigoRecorrencia { get; set; }
        public string DiaSemanaFormatada { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public int? CodigoLocalSEF { get; set; }
        public bool Feriado { get; set; }
        public List<EventoAulaColaboradorViewModel> Colaboradores { get; set; }
    }
}