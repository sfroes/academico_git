using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class CargaHorariaComEtiquetaVO : ISMCMappable
    {
        public string Etiqueta { get; set; }
        public string Tipo { get; set; } // enum HoraAula, HoraRelogio
        public int? HoraAula { get; set; }
        public double? HoraRelogio { get; set; }
    }
}
