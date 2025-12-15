using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface ITipoFuncionarioService : ISMCService
    {
        /// <summary>
        /// Buscar todos os tipos de funcionários para select
        /// </summary>
        /// <returns>Lista de todos os funcionários</returns>
        List<SMCDatasourceItem> BuscarTiposFuncionarioSelect();
    }
}