using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Interfaces
{
    public interface IEscalaApuracaoItemService : ISMCService
    {
        /// <summary>
        /// Busca o percentual / descrição dos itens de uma escala de apuração.
        /// </summary>
        /// <param name="seqEscalaApuracao">Sequencial da escalda de apuração.</param>
        /// <returns></returns>
        List<EscalaApuracaoItemData> BuscarEscalaApuracaoItens(long seqEscalaApuracao);

        /// <summary>
        /// Busca os itens de uma escala de apuração para um datasource.
        /// </summary>
        /// <param name="filtroData">Dados de filtro</param>
        /// <returns>Dados dos itens da escala de apuração</returns>
        List<SMCDatasourceItem> BuscarEscalaApuracaoItensSelect(EscalaApuracaoItemFiltroData filtroData);
    }
}