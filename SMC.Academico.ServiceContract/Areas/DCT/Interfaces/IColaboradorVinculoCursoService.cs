using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Interfaces
{
    public interface IColaboradorVinculoCursoService : ISMCService
    {
        /// <summary>
        /// Listar todos os cursos do vinculados ao colaborador
        /// </summary>
        /// <param name="spec">Filtros de pesquisa</param>
        /// <returns>Lista de colaboradores vinculo curso</returns>
        List<ColaboradorVinculoCursoData> ListarColaboradorVinculoCursos(ColaboradorVinculoCursoFiltroData filtro);

        /// <summary>
        /// Salvar colaborador vinculo curso
        /// </summary>
        /// <param name="modelo">Dados do vinculo a ser gravado</param>
        void SalvarColaboradorVinculoCurso(ColaboradorVinculoData modelo);


    }
}