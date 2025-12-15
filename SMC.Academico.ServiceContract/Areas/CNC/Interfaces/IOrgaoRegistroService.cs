using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Interfaces
{
    public interface IOrgaoRegistroService : ISMCService
    {
        /// <summary>
        /// Busca a lista de orgãos de registros da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de orgãos de registros/returns>
        List<SMCDatasourceItem> BuscarOrgaosRegistroSelect();
    }
}
