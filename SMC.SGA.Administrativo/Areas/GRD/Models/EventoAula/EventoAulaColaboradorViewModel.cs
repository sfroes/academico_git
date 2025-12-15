using SMC.Framework.Mapper;

namespace SMC.SGA.Administrativo.Areas.GRD.Models
{
    public class EventoAulaColaboradorViewModel : ISMCMappable
    {
        public long Seq { get; set; }
        public long? SeqColaborador { get; set; }
        public long? SeqColaboradorSubstituto { get; set; }
    }
}