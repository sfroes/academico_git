using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Data.Aula;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Interfaces
{
    public interface IAvaliacaoService : ISMCService
    {
        /// <summary>
        /// Salvar avaliacao
        /// </summary>
        /// <param name="avaliacao">Dados da avaliacao</param>
        /// <returns>Sequencial da avaliação</returns>
        long SalvarAvaliacao(AvaliacaoData avaliacao);

        /// <summary>
        /// Buscar avaliação
        /// </summary>
        /// <param name="seq">Sequencial da avaliação</param>
        /// <returns>Modelo da avaliação</returns>
        AvaliacaoData BuscarAvaliacao(long seq);

        /// <summary>
        /// Buscar avaliação para a edição
        /// </summary>
        /// <param name="seq">Sequencial da avaliação</param>
        /// <returns>Modelo da avaliação</returns>
        AvaliacaoEditarData BuscarAvaliacaoEdicao(long seq);
        
        /// <summary>
        /// Lista de avaliações
        /// </summary>
        /// <param name="filtro">Filtros especificados</param>
        /// <returns>Lista avaliações</returns>
        SMCPagerData<AvaliacaoData> BuscarAvaliacoes(AvaliacaoFiltroData filtro);

        /// <summary>
        /// Deletar Avaliação
        /// </summary>
        /// <param name="seq">Sequencial da avalaiacão</param>
        void DeleteAvalicao(long seq);

        /// <summary>
        /// Consultar avaliações da turma
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        ///<returns>Dados da consulta das avaliações</returns>
        ConsultaAvaliacoesTurmaData ConsultaAvaliacoes(long seqTurma, long seqPessoaAtuacao);

        /// <summary>
        /// Inicia o preenchimento dos dados para criação de uma nova avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial da origem de avaliação</param>
        /// <returns>Objeto com os dados iniciais preenchidos</returns>
        AvaliacaoEditarData PreencherDadosNovaAvaliacao(long seqOrigemAvaliacao);
    }
}