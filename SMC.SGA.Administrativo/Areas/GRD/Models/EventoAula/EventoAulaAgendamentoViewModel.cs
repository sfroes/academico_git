using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.GRD.Models
{
    public class EventoAulaAgendamentoViewModel : ISMCMappable
    {
        public long SeqDivisaoTurma { get; set; }

        public List<EventoAulaViewModel> Eventos { get; set; }
    }
}