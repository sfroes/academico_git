using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class CargaHorariaData : ISMCMappable
    {
        public string CargaHorariaFormatada => $"{CargaHoraria} {TipoHora} - {FormaExecucao}";

        public int CargaHoraria { get; set; }

        public string TipoHora { get; set; }

        public string FormaExecucao { get; set; }
    }
}