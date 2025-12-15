using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Data.ConfiguracaoAvaliacaoPpa;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    /// <summary>
    /// Seriço de Configuração de Avaliação.
    /// </summary>
    public interface IConfiguracaoAvaliacaoPpaService : ISMCService
    {
        /// <summary>
        /// Busca uma lista paginada de avaliações pelos filtros informados
        /// </summary>
        /// <param name="filtros">Filtros de pesquisa</param>
        /// <returns>Lista paginada de avaliações</returns>
        [OperationContract]
        SMCPagerData<ConfiguracaoAvaliacaoPpaListaData> BuscarAvaliacoes(ConfiguracaoAvaliacaoPpaFiltroData filtros);

        /// <summary>
        /// Retorna uma lista de origem amostra com codigo e descrição
        /// </summary>
        /// <param name="amostraAtiva">Amostras ativas</param>
        [OperationContract]
        List<SMCDatasourceItem> BuscarOrigemAmostraPpaSelect(bool amostraAtiva);

        /// <summary>
        /// Retornar avaliações institucionais.
        /// </summary>
        /// <returns>Popular o select da viewmodel coms os codigos encontrados.</returns>
        [OperationContract]
        List<SMCDatasourceItem> BuscarAvaliacoesIntitucionaisSelect();

        /// <summary>
        /// Retornar tipos instrumentos.
        /// </summary>
        /// <returns>Popular o select da viewmodel coms os codigos  e descrição encontrados.</returns>
        [OperationContract]
        List<SMCDatasourceItem> BuscarTiposInstrumentosSelect();

        /// <summary>
        /// Consulta no PPA o codigo sequencial dos avaliadores
        /// </summary>
        /// <param name="seq">Codigo de avaliação Institucional informado ao selecionar</param>
        /// <returns>Lista dos codigos de especie de avaliadores encontrados</returns>
        [OperationContract]
        ConfiguracaoAvaliacaoPpaCabecalhoData BuscarCabecalhoConfiguracaoAvaliacaoPpa(long seq);

        [OperationContract]
        long SalvarConfiguracaoAvaliacao(ConfiguracaoAvaliacaoPpaData configuracao);

        [OperationContract]
        ConfiguracaoAvaliacaoPpaData BuscarConfiguracaoAvaliacao(long seq);

        [OperationContract]
        void SalvarAlteracaoDataLimiteResposta(long seq, DateTime novaData);

        [OperationContract]
        void ExcluirConfiguracaoAvaliacao(long seq);

        [OperationContract]
        List<SMCDatasourceItem> BuscarInstrumentosSelect(int? codigoAvaliacao);
    }
}
