using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IMotivoBloqueioService : ISMCService
    {
        /// <summary>
        /// Busca todos os motivos de bloqueio baseando-se em um filtro específico.
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Motivos de bloqueios encontrados</returns>
        List<SMCDatasourceItem> BuscarMotivosBloqueioSelect(MotivoBloqueioFiltroData filtros);

        /// <summary>
        /// Busca todos os motivos de bloqueio configurados por instituição x nível de ensino e
        /// um filtro específico.
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Motivos de bloqueios encontrados</returns>
        List<SMCDatasourceItem> BuscarMotivosBloqueioInstituicaoSelect(MotivoBloqueioFiltroData filtros);

        /// <summary>
        /// Busca todos os motivos de bloqueio configurados por instituição x nível de ensino
        /// </summary>
        /// <param name="seqInstituicaoEnsino">Sequencial da instituição de ensino logada</param>
        /// <returns>Motivos de bloqueios encontrados</returns>
        List<SMCDatasourceItem> BuscarMotivosBloqueioDescricaoCompletaPorInstituicaoSelect(long seqInstituicaoEnsino);

        /// <summary>
        /// Busca todos os motivos de bloqueio cujo token seja BLOQUEIO FINANCEIRO.
        /// </summary>
        /// <returns>Tipos de bloqueio das pessoas encontrados</returns>
        List<SMCDatasourceItem> BuscarMotivosBloqueioFinanceiroSelect();

        /// <summary>
        /// Busca os motivos de bloqueio parametrizados para o serviço
        /// </summary>
        /// <param name="seqServico"></param>
        /// <returns>Lista de motivos de bloqueio</returns>
        List<SMCDatasourceItem> BuscarMotivosBloqueioServicoParcelaSelect(long seqServico);

        MotivoBloqueioData BuscarMotivoBloqueio(long seqMotivoBloqueio);

        List<SMCDatasourceItem> BuscarMotivosBloqueioFormatoManualAmbosSelect(MotivoBloqueioFiltroData filtros);
    }
}