using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class LimitesCargaHorariaVO : ISMCMappable
    {
        public double? CargaHorariaMinima { get; set; }
        public double? CargaHorariaMaxima { get; set; }
        public double? CargaHorariaParaTotal { get; set; }
    }
}
