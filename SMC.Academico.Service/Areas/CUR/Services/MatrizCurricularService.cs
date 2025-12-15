using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class MatrizCurricularService : SMCServiceBase, IMatrizCurricularService
    {
        #region [ DomainService ]

        private MatrizCurricularDomainService MatrizCurricularDomainService
        {
            get { return this.Create<MatrizCurricularDomainService>(); }
        }

        private MatrizCurricularOfertaDomainService MatrizCurricularOfertaDomainService
        {
            get { return this.Create<MatrizCurricularOfertaDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca maior sequencial de acordo com o curriculo curso oferta
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do curriculo curso oferta</param>
        /// <returns>Dados para inserir uma matriz curricular</returns>
        public MatrizCurricularData BuscarConfiguracaoMatrizCurricular(long seqCurriculoCursoOferta)
        {
            var matrizCurricular = MatrizCurricularDomainService.BuscarConfiguracaoMatrizCurricular(seqCurriculoCursoOferta);
            return matrizCurricular.Transform<MatrizCurricularData>();
        }

        /// <summary>
        /// Busca a descrição da matriz curricular de acordo com os valores selecionado
        /// </summary>
        /// <param name="seqDivisaoCurricular">Sequencial da divisao curricular selecionada</param>
        /// <param name="seqModalidade">Sequencial da modalidade selecionada</param>
        /// <returns>Dados para inserir uma matriz curricular</returns>
        public string BuscarConfiguracaoDescricaoMatrizCurricular(long seqDivisaoCurricular, long seqModalidade)
        {
            return MatrizCurricularDomainService.BuscarConfiguracaoDescricaoMatrizCurricular(seqDivisaoCurricular, seqModalidade);
        }

        /// <summary>
        /// Busca o codigo do item da matriz curricular de acordo com os valores selecionado
        /// </summary>
        /// <param name="seqCursoOfertaLocalidade">Sequencial do curso oferta localidade</param>
        /// <param name="codigo">codigo da matriz curricular</param>
        /// <returns>Dados para inserir uma matriz curricular</returns>
        public string BuscarConfiguracaoLocalidadeMatrizCurricular(long seqCursoOfertaLocalidade, string codigo)
        {
            return MatrizCurricularDomainService.BuscarConfiguracaoLocalidadeMatrizCurricular(seqCursoOfertaLocalidade, codigo);
        }

        /// <summary>
        /// Busca a matriz curricular com dados para selecionar o datasource de unidade/localidade
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular</param>
        /// <returns>Dados da matriz curricular</returns>
        public MatrizCurricularData BuscarMatrizCurricular(long seq)
        {
            var matrizCurricular = MatrizCurricularDomainService.BuscarMatrizCurricular(seq);
            return matrizCurricular.Transform<MatrizCurricularData>();
        }

        /// <summary>
        /// Busca as matrizes curricular que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros">Objeto matriz curricular com o sequencial curriculo curso oferta</param>
        /// <returns>SMCPagerData com a lista de matrizes curricular</returns>
        public SMCPagerData<MatrizCurricularListaData> BuscarMatrizesCurricular(MatrizCurricularFiltroData filtros)
        {
            var matrizesCurricular = MatrizCurricularDomainService.BuscarMatrizesCurricular(filtros.Transform<MatrizCurricularFilterSpecification>());

            return matrizesCurricular.Transform<SMCPagerData<MatrizCurricularListaData>>();
        }

        /// <summary>
        /// Busca as matrizes curricular que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros">Objeto matriz curricular com o sequencial curriculo curso oferta</param>
        /// <returns>SMCPagerData com a lista de matrizes curricular</returns>
        public SMCPagerData<MatrizCurricularListaData> BuscarMatrizesCurricularLookupOferta(MatrizCurricularOfertaFiltroData filtros)
        {
            var matrizesCurricular = MatrizCurricularDomainService.BuscarMatrizesCurricularLookupOferta(filtros.Transform<MatrizCurricularOfertaFiltroVO>());

            return matrizesCurricular.Transform<SMCPagerData<MatrizCurricularListaData>>();
        }

        /// <summary>
        /// Busca a matrize curricular com a oferta selecionada para retorno do lookup
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular oferta</param>
        /// <returns>Objeto matriz curricular oferta</returns>
        public MatrizCurricularOfertaData BuscarMatrizesCurricularLookupOfertaSelecionado(long seq)
        {
            var matrizesCurricular = MatrizCurricularDomainService.BuscarMatrizesCurricularLookupOfertaSelecionado(seq);

            return matrizesCurricular.Transform<MatrizCurricularOfertaData>();
        }

        /// <summary>
        /// Busca matriz curricular oferta para o cabeçalho
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular oferta</param>
        /// <returns>Matriz curricular oferta data para o cabeçalho</returns>
        public MatrizCurricularOfertaCabecalhoData BuscarMatrizCurricularOfertaCabecalho(long seq)
        {
            var matrizCurricularOferta = MatrizCurricularOfertaDomainService.BuscarMatrizCurricularOfertaCabecalho(seq);
            return matrizCurricularOferta.Transform<MatrizCurricularOfertaCabecalhoData>();
        }

        /// <summary>
        /// Busca os dados do curso, curriculo, oferta e matriz para o cabeçalho da consulta de matriz curricular
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular</param>
        /// <returns>Dados do cabeçalho</returns>
        public MatrizCurricularCabecalhoData BuscarMatrizCurricularCabecalho(long seq)
        {
            return this.MatrizCurricularDomainService.BuscarMatrizCurricularCabecalho(seq).Transform<MatrizCurricularCabecalhoData>();
        }

        /// <summary>
        /// Busca a matriz curricular para com todas as configurações e divisões curriculares para o relatório
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular</param>
        /// <returns>Dados da matriz curricular para o relatório</returns>
        public MatrizCurricularRelatorioData BuscarMatrizCurricularRelatorio(long seq)
        {
            return MatrizCurricularDomainService.BuscarMatrizCurricularRelatorio(seq).Transform<MatrizCurricularRelatorioData>();
        }

        /// <summary>
        /// Grava uma matriz curricular e suas divisões
        /// </summary>
        /// <param name="matrizCurricular">Dados da matriz curricular a ser gravada</param>
        /// <returns>Sequencial da matriz curricular gravada</returns>
        /// <exception cref="HistoricoSituacaoMatrizCurricularOfertaAtivaException">Caso já exista uma matriz curricular como "Ativa" para a mesma oferta de curso-localidade-turno RN_CUR_057</exception>
        /// <exception cref="MatrizCurricularEdicaoNaoPermitidaException">Caso seja para atualizar as divisões e tenha dependências cadastrada</exception>
        public long SalvarMatrizCurricular(MatrizCurricularData matrizCurricular)
        {
            return this.MatrizCurricularDomainService.SalvarMatrizCurricular(matrizCurricular.Transform<MatrizCurricularVO>());
        }

        /// <summary>
        /// Recupera a matriz curricular de um aluno num ciclo letivo
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo</param>
        /// <param name="considerarMatriz">Considerar a matriz do aluno</param>
        /// <returns>Retorna a matriz do aluno ou nulo caso não seja encontrada uma matriz para o ciclo e aluno informados ou seja informado para não considerar a matriz</returns>
        public MatrizCurricularData BuscarMatrizCurricularAluno(long seqAluno, long seqCicloLetivo, bool considerarMatriz)
        {
            return this.MatrizCurricularDomainService.BuscarMatrizCurricularAluno(seqAluno, seqCicloLetivo, considerarMatriz).Transform<MatrizCurricularData>();
        }

        /// <summary>
        /// Recupera os sequenciais de matriz curriculares do usuário logado (Considerando filtro de dados)
        /// </summary>
        public List<long> BuscarSeqsMatrizCurricularUsuarioLogado()
        {
            return MatrizCurricularDomainService.BuscarSeqsMatrizCurricularUsuarioLogado();
        }
    }
}