using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class DadosCargaHorariaHistoricoData : ISMCMappable
    {
        public double CargaHorariaCurso { get; set; }

        public double CargaHorariaCursoIntegralizada { get; set; }

    }
}