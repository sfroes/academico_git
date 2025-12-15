using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.GRD.ValueObjects
{
    public class EventoAulaColaboradorVO : ISMCMappable
    {
        public long Seq { get; set; }
        public long? SeqColaborador { get; set; }
        public long? SeqColaboradorSubstituto { get; set; }
        public string DescricaoFormatada { get; set; }
        public string NomeColaborador { get; set; }
        public string NomeSocialColaborador { get; set; }
        public string NomeColaboradorSubstituto { get; set; }
        public string NomeSocialColaboradorSubstituto { get; set; }
    }
}
