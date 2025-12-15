using SMC.Academico.Domain.Areas.CNC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Interfaces
{
    public interface IGrupoRegistroService : ISMCService
    {
        /// <summary>
        /// Busca as grupors de registro que atendam os filtros informados
        /// </summary>
        /// <param name="filtros">Filtros da listagem dos grupos de registro</param>
        /// <returns>Lista de grupos de registros</returns>
        SMCPagerData<GrupoRegistroData> BuscarGruposRegistros(GrupoRegistroFiltroData filtros);

        /// <summary>
        /// Busca a lista de grupos de registros da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de grupos de registros/returns>
        List<SMCDatasourceItem> BuscarGruposRegistroSelect();
    }
}
