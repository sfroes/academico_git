using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Interfaces
{
    public interface IInstituicaoNivelTipoMembroBancaService : ISMCService
    {
        /// <summary>
        /// Busca todos os Tipos Membro Banca conforme o filtro de dados
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarTiposMembroBancaSelect(TipoMembroBancaFiltroData filtros);
    }
}
