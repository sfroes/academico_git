using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface ITurnoService : ISMCService
    {
        /// <summary>
        /// Busca turnos para a listagem de acordo com o curso oferta localidade
        /// </summary>
        /// <param name="seqCursoOferta">Sequencial do curso oferta </param>
        /// <returns>Lista de turnos</returns>
        List<SMCDatasourceItem> BuscarTurnosPorCursoOfertaSelect(long seqCursoOferta);

        /// <summary>
        /// Busca turnos para a listagem de acordo com o curso
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso </param>
        /// <returns>Lista de turnos</returns>
        List<SMCDatasourceItem> BuscarTurnosPorCursoSelect(long seqCurso);

        /// <summary>
        /// Busca turnos para a listagem de acordo com o curso oferta localidade
        /// </summary>
        /// <param name="seqCursoOfertaLocalidade">Sequencial do curso oferta localidade</param>
        /// <returns>Lista de turnos</returns>
        List<SMCDatasourceItem> BuscarTurnosPorCursoOfertaLocalidadeSelect(long seqCursoOfertaLocalidade);

        /// <summary>
        /// Busca turnos para a listagem de acordo com a localidade
        /// </summary>
        /// <param name="seqLocalidade">Sequencial da localidade</param>
        /// <returns>Lista de turnos</returns>
        List<SMCDatasourceItem> BuscarTurnosPorLocalidadeSelect(long seqLocalidade);

        /// <summary>
        /// Busca os turnos que sejam do nível de ensino do curso do curriculo curso oferta informado
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do currículo curso oferta do curso com o nível de ensino em questão</param>
        /// <returns>Dados dos turnos</returns>
        List<SMCDatasourceItem> BuscarTurnosNivelEnsinoPorCurriculoCursoOfertaSelect(long seqCurriculoCursoOferta);

        /// <summary>
        /// Buscar todos os turnos cadastrados
        /// </summary>
        /// <returns>Todos os turnos cadastrados</returns>
        List<SMCDatasourceItem> BuscarTunos();

        /// <summary>
        /// Busca todos os turnos que atendam aos filtros informados
        /// </summary>
        /// <param name="filtroData">Dados dos filtros</param>
        /// <returns>Dados dos turnos que atendam aos filtros informados</returns>
        List<SMCDatasourceItem> BuscarTurnosSelect(TurnoFiltroData filtroData);
    }
}