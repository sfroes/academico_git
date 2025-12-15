using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Interfaces
{
    public interface ICampanhaCicloLetivoService : ISMCService
    {
        List<SMCDatasourceItem> BuscarCampanhasCicloLetivoSelect();

        /// <summary>
        /// Busca um campanha ciclo letivo pelos filtros informados
        /// </summary>
        /// <param name="filtros">Dados dos filtros</param>
        /// <returns>Campanha ciclo letivo encontrada</returns>
        CampanhaCicloLetivoData BuscarCampanhaCicloLetivo(CampanhaCicloLetivoFiltroData filtros);
    }
}