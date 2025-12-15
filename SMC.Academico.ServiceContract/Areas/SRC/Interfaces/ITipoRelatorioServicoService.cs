using SMC.Academico.Common.Constants;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface ITipoRelatorioServicoService : ISMCService
    {
        /// <summary>
        /// Busca os tipos de relatórios de serviços que o usuário logado dem acesso
        /// </summary>
        /// <returns>Lista dos tipos de relatório</returns>
        List<SMCDatasourceItem> BuscarTiposRelatorioServicoSelect();
    }
}
