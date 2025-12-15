using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Interfaces
{
    public interface IInstituicaoExternaService : ISMCService
    {
        /// <summary>
        /// Busca as instituições externas que atendam aos filtros informados
        /// </summary>
        /// <param name="filtros">Filtros das instituições externas ou sequenciais selecionados</param>
        /// <returns>Dados das instituições externas paginados</returns>
        SMCPagerData<InstituicaoExternaListaData> BuscarInstituicoesExternas(InstituicaoExternaFiltroData filtros);

        /// <summary>
        /// Buscar as instituições externas associadas a um colaborador
        /// </summary>
        /// <param name="filtros">Filtros das instituições externas ou sequenciais selecionados</param>
        /// <returns>SMCSelecetList das instituições externas</returns>
        List<SMCDatasourceItem> BuscarInstituicaoExternaPorColaboradorSelect(InstituicaoExternaFiltroData filtros);
    }
}