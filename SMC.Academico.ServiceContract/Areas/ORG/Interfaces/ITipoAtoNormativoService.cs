using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface ITipoAtoNormativoService : ISMCService
    {
        /// <summary>
        /// Buscar os tipos de atos normativos
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns>Tipos atos normativos select</returns>
        List<SMCDatasourceItem> BuscarTiposAtoNormativoSelect(TipoAtoNormativoFiltroData filtro);

        /// <summary>
        /// Buscar tipo ato normativo
        /// </summary>
        /// <param name="seqTipoAtoNormativo">Sequencial tipo ato normativo</param>
        /// <returns>Tipo ato normativo</returns>
        TipoAtoNormativoData BuscarTipoAtoNormativo(long seqTipoAtoNormativo);
    }
}