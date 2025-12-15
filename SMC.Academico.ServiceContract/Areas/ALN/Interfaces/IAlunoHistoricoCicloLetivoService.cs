using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    public interface IAlunoHistoricoCicloLetivoService : ISMCService
    {
        /// <summary>
        /// Serviço que busca os dados do Relatório de Posição Consolidada Situação por Tipo de Atuação 
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns>Lista de Dados do Relatório</returns>
        List<RelatorioConsolidadoSituacaoData> BuscarDadosRelatorioConsolidadoSituacao(RelatorioConsolidadoSituacaoFiltroData filtro);
    }
}
