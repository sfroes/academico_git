using SMC.Academico.Common.Constants;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface ITipoRelatorioService : ISMCService
    {
        /// <summary>
        /// Busca os tipos de relatórios que o usuário logado dem acesso
        /// </summary>
        /// <returns>Lista dos tipos de relatório</returns>
        List<SMCDatasourceItem> BuscarTiposRelatorioSelect();
    }
}