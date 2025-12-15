using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class HoraRelogioComEtiquetaVO : ISMCMappable
    {
        public double HoraRelogio { get; set; }
        public string Etiqueta { get; set; }
    }
}
