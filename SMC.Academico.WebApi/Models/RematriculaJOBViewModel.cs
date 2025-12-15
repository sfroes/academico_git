using SMC.Framework.Mapper;

namespace SMC.Academico.WebApi.Models
{
    public class RematriculaJOBViewModel : ISMCMappable
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long SeqProcesso { get; set; }

        public string IdUsuario { get; set; }
    }
}