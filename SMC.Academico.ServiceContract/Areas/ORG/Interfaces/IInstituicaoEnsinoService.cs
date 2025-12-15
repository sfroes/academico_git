using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface IInstituicaoEnsinoService : ISMCService
    {
        /// <summary>
        /// Busca as configurações de entidade para cadastro de uma instituição de ensino
        /// </summary>
        /// <returns>Dados da configuração de entidade</returns>
        EntidadeData BuscaConfiguracoesEntidadeInstutuicaoEnsino();

        /// <summary>
        /// Buscar uma lista de instituições de ensino
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de instituições</returns>
        SMCPagerData<InstituicaoEnsinoListaData> BuscarInstituicoesEnsino(InstituicaoEnsinoFiltroData filtros);

        /// <summary>
        /// Buscar a instituições de ensino do usuário logado, utilizado em relatórios aproveita o filtro global
        /// </summary>
        /// <returns>Instituição de ensino do usuário logado</returns>
        InstituicaoEnsinoData BuscarInstituicoesEnsinoLogado();

        /// <summary>
        /// Busca uma instituição de ensino
        /// </summary>
        /// <param name="seq">Sequencial da instituição de ensino</param>
        /// <returns>Dados da insitituição com sequencial informado</returns>
        InstituicaoEnsinoData BuscarInstituicaoEnsino(long seq);

        /// <summary>
        /// Busca uma instituição de ensino com a as configurações do tipo entidade instituicao de ensino
        /// </summary>
        /// <param name="seq">Sequencial da instituição de ensino</param>
        /// <returns>Dados da insitituição com sequencial informado</returns>
        InstituicaoEnsinoData BuscarInstituicaoEnsinoComConfiguracao(long seq);

        /// <summary>
        /// Busca uma instituição de ensino pela sigla.
        /// </summary>
        /// <param name="sigla">Sigla da instituição.</param>
        /// <returns>Dados da insitituição encontrada.</returns>
        InstituicaoEnsinoData BuscarInstituicaoEnsinoPorSigla(string sigla);

        /// <summary>
        /// Salva uma instituicao de ensino
        /// </summary>
        /// <param name="instituicao">Dados da instituição a ser salva</param>
        /// <returns>Sequencial da instituição salva</returns>
        long SalvarInstituicaoEnsino(InstituicaoEnsinoData instituicao);

        /// <summary>
        /// Exclui uma instituição de ensino
        /// </summary>
        /// <param name="seqInstituicao">Sequencial da instituição de ensino para exclusão</param>
        void ExcluirInstituicaoEnsino(long seqInstituicao);

        /// <summary>
        /// Busca as insituições de ensino para seleção em um select.
        /// Busca apenas as que o usuário tem permissão de acordo com o filtro de dados.
        /// </summary>
        /// <returns>Lista de instituições de ensino que o usuário logado tem permissão de filtro de dados</returns>
        List<SMCDatasourceItem> BuscarInstituicoesEnsinoSelect(bool ignorarFiltroDados = false);

        /// <summary>
        /// Busca a instituição de ensino da pessoa atuação logada
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Dados da instituição de ensino</returns>
        InstituicaoEnsinoData BuscarInstituicaoEnsinoPorPessoaAtuacao(long seqPessoaAtuacao);
    }
}