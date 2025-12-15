using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface IClassificacaoService : ISMCService
    {
        /// <summary>
        /// Busca as classificações de uma hierarquia
        /// </summary>
        /// <param name="classificacaoFiltroData">Dados dos filtros</param>
        /// <returns>Dados da hierarquia de classificação</returns>
        ClassificacaoData[] BuscarClassificacaoPorHierarquiaClassificacao(ClassificacaoFiltroData classificacaoFiltroData);


        // <summary>
        /// Busca as classificações de uma hierarquia
        /// </summary>
        /// <param name="seqClassificacao"></param>
        /// <returns>Dados da hierarquia de classificação</returns>
        ClassificacaoData[] BuscarClassificacaoPorHierarquiaClassificacaoLookup(long[] seqClassificacao);

        /// <summary>
        /// Busca as áreas de conhecimento para o BDP.
        /// </summary>
        List<SMCDatasourceItem> BuscarClassificacoesBDP();
    }
}