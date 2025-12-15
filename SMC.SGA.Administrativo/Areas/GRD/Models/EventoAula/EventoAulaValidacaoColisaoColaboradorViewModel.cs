using SMC.Framework.Mapper;
using System;

namespace SMC.SGA.Administrativo.Areas.GRD.Models
{
    public class EventoAulaValidacaoColisaoColaboradorViewModel : ISMCMappable
    {
        public long SeqColaborador { get; set; }
        public long SeqDivisaoTurma { get; set; }
        public int? CodigoLocalSEF { get; set; }
        public DateTime DataAula { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFim { get; set; }
    }
}