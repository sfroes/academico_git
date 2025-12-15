using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class CalculoTotalCargaHorariaData : ISMCMappable
    {
        public int TotalHoras { get; set; }

        public int TotalHorasAula { get; set; }
        public int TotalCreditos { get; set; }
    }
}