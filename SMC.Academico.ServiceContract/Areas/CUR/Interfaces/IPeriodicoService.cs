using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Data.Periodico;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IPeriodicoService : ISMCService
    {
        /// <summary>
        /// Listar todas as areas de avaliação baseadas no na classificação periodica
        /// </summary>
        /// <param name="seqClassificacaoPeriodico">Seq classificação periodico</param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarAreaAvaliacaoSelect(long seqClassificacaoPeriodico);

        /// <summary>
        /// Listar todas as qulis capes baseadas no na classificação periodica
        /// </summary>
        /// <param name="seqClassificacaoPeriodico">Seq classificação periodico</param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarQualiCapesSelect(long seqClassificacaoPeriodico);

        /// <summary>
        /// Salvar a importação do periodico
        /// </summary>
        /// <param name="periodico"></param>
        void SalvarPeriodo(PeriodicoData periodico);

        /// <summary>
        /// Buscar os periodicos baseados nos filtros
        /// </summary>
        /// <param name="filtro">Parametros configurados na tela</param>
        /// <returns></returns>
        SMCPagerData<PeriodicoListarData> BuscarPeriodicosCapes(PeriodicoFiltroData filtro);

        /// <summary>
        /// Busca os periodicos para listar no lookup de periódicos.
        /// </summary>
        /// <param name="filtro">Parametros do filtro.</param>
        /// <returns></returns>
        SMCPagerData<PeriodicoListarLookupData> BuscarPeriodicosLookup(PeriodicoFiltroData filtro);

        /// <summary>
        /// Busca o periodico selecionado no lookup.
        /// </summary>
        PeriodicoListarLookupData BuscarPeriodicoLookup(long seq);

        /// <summary>
        /// Prepara o modelo do periódico.
        /// </summary>
        PeriodicoData PrepararModeloPeriodico(PeriodicoFiltroData filtro);
    }
}