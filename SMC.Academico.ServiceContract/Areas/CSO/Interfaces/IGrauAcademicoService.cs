using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface IGrauAcademicoService : ISMCService
    {
        /// <summary>
        /// Busca a lista de Grau Academico para popular um Select
        /// </summary>
        /// <param name="filtro">Todos os filtros que podem ser aplicados na listagem de grau academico</param>
        /// <returns>Lista de grau academico</returns>
        List<SMCDatasourceItem> BuscarGrauAcademicoSelect(GrauAcademicoFiltroData filtro);

        /// <summary>
        /// Busca a lista de Grau Academico para popular um Select
        /// </summary>
        /// <returns>Lista de grau academico</returns>
        List<SMCDatasourceItem> BuscarGrauAcademicoAtivoSelect();

        /// <summary>
        /// Grava um grau acadêmico
        /// </summary>
        /// <param name="GrauAcademicoData">Dados do grau acadêmico a ser gravado</param>
        /// <returns>Sequencial do grau acadêmico gravado</returns>
        long SalvarGrauAcademico(GrauAcademicoData GrauAcademicoData);

        /// <summary>
        /// Busca a lista de Grau Academico para popular um Select do lookup
        /// </summary>
        /// <param name="filtro">Todos os filtros que podem ser aplicados na listagem de grau academico</param>
        /// <returns>Lista de grau academico</returns>
        List<SMCDatasourceItem> BuscarGrauAcademicoLookupSelect(GrauAcademicoFiltroData filtro);

        /// <summary>
        /// Busca a lista de Grau Academico para popular de acordo com a Entidade
        /// </summary>
        /// <param name="seqEntidade">Sequencial entidade</param>
        /// <param name="seqAtoNormativo">Sequencial ato normativo</param>
        /// <param name="seq">Sequencial entidade ato normativo</param>
        /// <returns>Lista de grau acadêmico</returns>
        List<SMCDatasourceItem> BuscarGrauAcademicoPorEntidade(long? seqEntidade, long? seqAtoNormativo, long seq);

        /// <summary>
        /// Busca a lista de Grau Academico para popular de acordo com a Entidade Curso
        /// </summary>
        /// /// <param name="seqCurso">Sequencial entidade curso</param>
        /// <returns>Lista de grau acadêmico</returns>
        List<SMCDatasourceItem> BuscarGrauAcademicoPorNivelEnsinoCurso(long seqCurso);
    }
}