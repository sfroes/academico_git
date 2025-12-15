using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class EtiquetaVO : ISMCMappable
    {
        public string Codigo { get; set; }
        public int CargaHorariaEmHoraAula { get; set; }
        public double? CargaHorariaEmHoraRelogio { get; set; }
    }
}
