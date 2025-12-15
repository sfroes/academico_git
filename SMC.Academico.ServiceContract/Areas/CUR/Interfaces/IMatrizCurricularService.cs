using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IMatrizCurricularService : ISMCService
    {
        /// <summary>
        /// Busca maior sequencial de acordo com o curriculo curso oferta
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do curriculo curso oferta</param>
        /// <returns>Dados para inserir uma matriz curricular</returns>
        MatrizCurricularData BuscarConfiguracaoMatrizCurricular(long seqCurriculoCursoOferta);

        /// <summary>
        /// Busca a descrição da matriz curricular de acordo com os valores selecionado
        /// </summary>
        /// <param name="seqDivisaoCurricular">Sequencial da divisao curricular selecionada</param>
        /// <param name="seqModalidade">Sequencial da modalidade selecionada</param>
        /// <returns>Dados para inserir uma matriz curricular</returns>
        string BuscarConfiguracaoDescricaoMatrizCurricular(long seqDivisaoCurricular, long seqModalidade);

        /// <summary>
        /// Busca o codigo do item da matriz curricular de acordo com os valores selecionado
        /// </summary>
        /// <param name="seqCursoOfertaLocalidade">Sequencial do curso oferta localidade</param>
        /// <param name="codigo">codigo da matriz curricular</param>
        /// <returns>Dados para inserir uma matriz curricular</returns>
        string BuscarConfiguracaoLocalidadeMatrizCurricular(long seqCursoOfertaLocalidade, string codigo);

        /// <summary>
        /// Busca a matriz curricular com dados para selecionar o datasource de unidade/localidade
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular</param>
        /// <returns>Dados da matriz curricular</returns>
        MatrizCurricularData BuscarMatrizCurricular(long seq);

        /// <summary>
        /// Busca as matrizes curricular que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros">Objeto matriz curricular com o sequencial curriculo curso oferta</param>
        /// <returns>SMCPagerData com a lista de matrizes curricular</returns>
        SMCPagerData<MatrizCurricularListaData> BuscarMatrizesCurricular(MatrizCurricularFiltroData filtros);

        /// <summary>
        /// Busca as matrizes curricular que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros">Objeto matriz curricular com o sequencial curriculo curso oferta</param>
        /// <returns>SMCPagerData com a lista de matrizes curricular</returns>
        SMCPagerData<MatrizCurricularListaData> BuscarMatrizesCurricularLookupOferta(MatrizCurricularOfertaFiltroData filtros);

        /// <summary>
        /// Busca a matrize curricular com a oferta selecionada para retorno do lookup
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular oferta</param>
        /// <returns>Objeto matriz curricular oferta</returns>
        MatrizCurricularOfertaData BuscarMatrizesCurricularLookupOfertaSelecionado(long seq);

        /// <summary>
        /// Busca matriz curricular oferta para o cabeçalho
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular oferta</param>
        /// <returns>Matriz curricular oferta data para o cabeçalho</returns>
        MatrizCurricularOfertaCabecalhoData BuscarMatrizCurricularOfertaCabecalho(long seq);

        /// <summary>
        /// Busca os dados do curso, curriculo, oferta e matriz para o cabeçalho da consulta de matriz curricular
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular</param>
        /// <returns>Dados do cabeçalho</returns>
        MatrizCurricularCabecalhoData BuscarMatrizCurricularCabecalho(long seq);

        /// <summary>
        /// Grava uma matriz curricular e suas divisões
        /// </summary>
        /// <param name="matrizCurricular">Dados da matriz curricular a ser gravada</param>
        /// <returns>Sequencial da matriz curricular gravada</returns>
        long SalvarMatrizCurricular(MatrizCurricularData matrizCurricular);

        /// <summary>
        /// Recupera os sequenciais de matriz curriculares do usuário logado (Considerando filtro de dados)
        /// </summary>
        List<long> BuscarSeqsMatrizCurricularUsuarioLogado();

        /// <summary>
        /// Busca a matriz curricular para com todas as configurações e divisões curriculares para o relatório
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular</param>
        /// <returns>Dados da matriz curricular para o relatório</returns>
        /// <exception cref="HistoricoSituacaoMatrizCurricularOfertaAtivaException">Caso já exista uma matriz curricular como "Ativa" para a mesma oferta de curso-localidade-turno RN_CUR_057</exception>
        /// <exception cref="MatrizCurricularEdicaoNaoPermitidaException">Caso seja para atualizar as divisões e tenha dependências cadastrada</exception>
        MatrizCurricularRelatorioData BuscarMatrizCurricularRelatorio(long seq);

        /// <summary>
        /// Recupera a matriz curricular de um aluno num ciclo letivo
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo</param>
        /// <param name="considerarMatriz">Considerar a matriz do aluno</param>
        /// <returns>Retorna a matriz do aluno ou nulo caso não seja encontrada uma matriz para o ciclo e aluno informados ou seja informado para não considerar a matriz</returns>
        MatrizCurricularData BuscarMatrizCurricularAluno(long seqAluno, long seqCicloLetivo, bool considerarMatriz);
    }
}