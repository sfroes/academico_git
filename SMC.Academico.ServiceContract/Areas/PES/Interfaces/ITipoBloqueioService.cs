using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface ITipoBloqueioService : ISMCService
    {
        /// <summary>
        /// Busca todos os tipos de bloqueio
        /// </summary>
        /// <returns>Tipos de bloqueio encontrados</returns>
        List<SMCDatasourceItem> BuscarTiposBloqueiosSelect();
    }
}