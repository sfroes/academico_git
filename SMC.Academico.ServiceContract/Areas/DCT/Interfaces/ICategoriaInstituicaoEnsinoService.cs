using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Interfaces
{
    public interface ICategoriaInstituicaoEnsinoService : ISMCService
    {
        /// <summary>
        /// Busca os dados de todas as categorias de instituição de ensino cadastradas
        /// </summary>
        /// <returns>Lista de SMCDatasourceItem com o sequencial e descrição das categorias</returns>
        List<SMCDatasourceItem> BuscarCategoriasInstituicaoEnsinoSelect();
    }
}