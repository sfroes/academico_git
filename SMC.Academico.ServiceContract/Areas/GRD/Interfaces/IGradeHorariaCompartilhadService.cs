using SMC.Academico.ServiceContract.Areas.GRD.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.GRD.Interfaces
{
    public interface IGradeHorariaCompartilhadService : ISMCService
    {
        /// <summary>
        /// Buscar Grades de horarias compartilhadas
        /// </summary>
        /// <param name="filtro">Filtros da pesquisa</param>
        /// <returns>Grades comprtilhadas filtradas</returns>
        SMCPagerData<GradeHorariaCompartilhadaListarData> BuscarGradesHorariasCompartilhada(GradeHorariaCompartilhadaFitroData filtro);

        /// <summary>
        /// Grava um compartilhamento de grade
        /// </summary>
        /// <param name="data">Modelo</param>
        /// <returns>Sequencial do compartilhamento criado</returns>
        long SalvarGradeHorariaCompartilhada(GradeHorariaCompartilhadaData data);

        /// <summary>
        /// Busca um compartilhamento de grade
        /// </summary>
        /// <param name="seq">Sequencial do compartilhamento</param>
        /// <returns>Dados do compartilhamento</returns>
        GradeHorariaCompartilhadaData BuscarGradeHorariaCompartilhada(long seq);
    }
}
