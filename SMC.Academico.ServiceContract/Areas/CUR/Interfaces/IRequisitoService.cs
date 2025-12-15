using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IRequisitoService : ISMCService
    {
        /// <summary>
        /// Busca as configurações de requisito de acordo com matriz curricular
        /// </summary>
        /// <param name="filtros">Filtros da listagem de requisitos</param>
        /// <returns>Dados da configuração do requisito</returns>
        RequisitoData BuscarConfiguracoesRequisito(RequisitoFiltroData filtros);

        /// <summary>
        /// Buscar o requisito
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>Objeto requisito com seus itens</returns>
        RequisitoData BuscarRequisito(long seq);

        /// <summary>
        /// Busca os requisitos de acordo com os filtros
        /// </summary>
        /// <param name="filtros">Objeto requisito filtro</param>
        /// <returns>SMCPagerData com a lista de requisitos</returns>
        SMCPagerData<RequisitoListaData> BuscarRequisitos(RequisitoFiltroData filtros);

        /// <summary>
        /// Associar um requisito a uma matriz curricular
        /// </summary>
        /// <param name="seq">Sequencial do requisito</param>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        void AssociarRequisito(long seq, long seqMatrizCurricular);

        /// <summary>
        /// Desassociar um requisito a uma matriz curricular
        /// </summary>
        /// <param name="seq">Sequencial do requisito</param>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        /// <param name="excluirRequisito">Excluir requisito após desassociar a matriz</param>
        void DesassociarRequisito(long seq, long seqMatrizCurricular, bool excluirRequisito);

        /// <summary>
        /// Grava um requisito com seus respectivos itens
        /// </summary>
        /// <param name="requisito">Dados do requisito a ser gravado</param>
        /// <returns>Sequencial do requisito gravado</returns>
        long SalvarRequisito(RequisitoData requisito);

        /// <summary>
        /// Excluir um requisito com seus respectivos itens
        /// </summary>
        /// <param name="requisito">Objeto com sequencial do requisito e da matriz curricular</param>
        void ExcluirRequisito(RequisitoData requisito);

        /// <summary>
		/// Verificar se o(s) componente(s) selecionados possui(em) pré-Requisito(s) atendidos(s) de acordo com a matriz curricular do aluno em questão
		/// </summary>
		/// <param name="seqAluno">Sequencial do aluno</param>
		/// <param name="seqsDivisoesComponente">Sequencial das divisões a serem verificadas</param>
		/// <param name="seqsConfiguracaoComponente">Sequencial das configurações a serem verificadas</param>
		/// <param name="validaTipoGestao">Se é para validar o tipo de gestão e qual o tipo </param>
		/// <param name="seqSolicitacao">Para validar o có-requisito informar o seqSolicitacao</param>
		(bool Valido, List<string> MensagensErro) ValidarPreRequisitos(long seqAluno, List<long> seqsDivisoesComponente, List<long> seqsConfiguracaoComponente, TipoGestaoDivisaoComponente? validaTipoGestao, long? seqSolicitacao);
                        
        /// <summary>
        /// Buscando os tipos de requisito com base no componente curricular
        /// </summary>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <returns>Lista com os tipos de requisito</returns>
        List<SMCDatasourceItem> BuscarTiposRequisitoSelect(long? seqComponenteCurricular);
    }
}
