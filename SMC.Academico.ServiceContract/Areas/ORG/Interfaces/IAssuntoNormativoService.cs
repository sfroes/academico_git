using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface IAssuntoNormativoService : ISMCService
    {
        /// <summary>
        /// Buscar os assuntos normativos
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns>Assuntos normativos select</returns>
        List<SMCDatasourceItem> BuscarAssuntosNormativoSelect(TipoAtoNormativoFiltroData filtro);

        /// <summary>
        /// Buscar assunto normativo
        /// </summary>
        /// <param name="seqAssuntoNormativo">Sequencial assunto normativo</param>
        /// <returns>Assunto normativo</returns>
        AssuntoNormativoData BuscarAssuntoNormativo(long seqAssuntoNormativo);
    }
}