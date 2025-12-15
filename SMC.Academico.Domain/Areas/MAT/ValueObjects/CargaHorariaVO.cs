using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class CargaHorariaVO : ISMCMappable
    {
        public int CargaHoraria { get; set; }
        public string TipoHora { get; set; }
        public string FormaExecucao { get; set; }
    }
}
