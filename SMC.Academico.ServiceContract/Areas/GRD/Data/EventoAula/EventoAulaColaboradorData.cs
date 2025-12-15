using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.GRD.Data
{
    public class EventoAulaColaboradorData : ISMCMappable
    {
        public long Seq { get; set; }
        public long? SeqColaborador { get; set; }
        public long? SeqColaboradorSubstituto { get; set; }
        public string DescricaoFormatada { get; set; }
    }
}
