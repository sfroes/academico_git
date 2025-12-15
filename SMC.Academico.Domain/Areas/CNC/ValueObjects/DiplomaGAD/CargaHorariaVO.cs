using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class CargaHorariaVO : ISMCMappable
    {
        public string Tipo { get; set; } //Enum HoraAula, HoraRelogio
        public int? HoraAula { get; set; }
        public double? HoraRelogio { get; set; }
    }
}
