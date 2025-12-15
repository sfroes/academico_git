using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class RematriculaJOBData : ISMCMappable
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long SeqProcesso { get; set; }
    }
}