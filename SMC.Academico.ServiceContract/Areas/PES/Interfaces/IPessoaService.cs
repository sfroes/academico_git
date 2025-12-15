using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;
using SMC.Academico.Common.Constants;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IPessoaService : ISMCService
    {
        /// <summary>
        /// Busca uma lista de pessoas com seus dados pessoais
        /// </summary>
        /// <param name="filtro">Filtros da listagem de pessoas</param>
        /// <returns>Lista não paginada de pessoas</returns>
        /// <exception cref="PessoaAtuacaoDuplicadaException">Caso o tipo de atuação seja colaborador e o usuário já tenha uma atuação de colaborador cadastrada</exception>
        List<PessoaExistenteListaData> BuscarPessoaExistente(PessoaFiltroData filtro);

        /// <summary>
        /// Busca uma pessoa com os dados pessoais
        /// </summary>
        /// <param name="filter">Filtros a serem usados</param>
        /// <returns>Dados da pessoa</returns>
        [OperationContract]
        PessoaData BuscarPessoa(PessoaFiltroData filter);

        /// <summary>
        /// Busca pessoas com os dados pessoais
        /// </summary>
        /// <param name="filter">Filtros a serem usados</param>
        /// <returns>Dados das pessoas</returns>
        [OperationContract]
        List<PessoaData> BuscarPessoas(PessoaFiltroData filter);

        /// <summary>
        /// Busca pessoas com dados pessoais
        /// </summary>
        /// <param name="filter">Filtro a ser usado</param>
        /// <returns>Dados das pessoas</returns>
        SMCPagerData<PessoaData> BuscarPessoasLookup(PessoaFiltroData filter);

        /// <summary>
        /// Busca uma pessoa com os dados pessoais
        /// </summary>
        /// <param name="seqPessoa">Seq da pessoa</param>
        /// <returns>Dados da pessoa</returns>
        PessoaData BuscarPessoaLookup(long seqPessoa);

        /// <summary>
        /// Grava os dados de uma pessoa com suas dependências
        /// </summary>
        /// <param name="pessoa">Pessoa a ser gravada</param>
        /// <returns>Sequencial da pessoa gravada</returns>
        long SalvarPessoa(PessoaData pessoa);

        /// <summary>
        /// Valida as quantidades de validação conforme a configuração da instituição logada
        /// </summary>
        /// <param name="pessoa">Dados pessoais da pessoa a ser gravada</param>
        /// <param name="tipoAtuacao">Tipo da atuação</param>
        /// <exception cref="InstituicaoTipoAtuacaoNaoConfiguradaException">Caso o tipo de atuação não esteja configurado na instituição</exception>
        /// <exception cref="PessoaDadosPesosaisQuantidadeFiliacaoMaximaException">Caso tenha mais que a quantidade máxima de filiação configurada para o tipo de atuação</exception>
        /// <exception cref="PessoaDadosPesosaisQuantidadeFiliacaoMinimaException">Caso não tenha a quantidade mínima de filiação obrigatória configurada para o tipo de atuação</exception>
        void ValidarQuantidadesFiliacao(PessoaData pessoa, TipoAtuacao tipoAtuacao);

        /// <summary>
        /// Buscar o código de pessoa do CAD associado ao ingressante. (A pessoa do CAD está nos Dados Mestres)
        /// </summary>
        /// <param name="seqPessoa">Sequencial do pessoa</param>
        /// <param name="tipoPessoa">Tipo pessoa física ou jurídica</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Código de pessoa do CAD</returns>
        long BuscarCodigoDePessoaNosDadosMestres(long seqPessoa, TipoPessoa tipoPessoa, long seqPessoaAtuacao);

        /// <summary>
        /// Retorna todas as instituições de ensino que uma pessoa faz parte
        /// </summary>
        /// <param name="filter">Filtro a ser aplicado para busca das pessoas</param>
        /// <returns>Lista de instituições de ensino</returns>
        [OperationContract]
        List<SMCDatasourceItem> BuscarInstituicoesEnsinoPessoaSelect(PessoaFiltroData filter);

        /// <summary>
        /// Retorna a lista de credenciais de acsso de uma pessoa para uso no MOBILE
        /// </summary>
        /// <param name="codPessoaCAD">Código de pessoa no CAD</param>
        /// <returns>Lista de credenciais de acesso da pessoa</returns>
        [OperationContract]
        List<CredenciaisAcessoData> BuscarCredenciaisAcessoPessoa(int codPessoaCAD);

        /// <summary>
        /// Método para buscar o webmail da pessoa
        /// </summary>
        /// <param name="codigoPessoa">Código de pessoa para recuperar o webmail</param>
        /// <returns>Webmail encontrado ou NULL</returns>
        string BuscarWebmail(int codigoPessoa);
    }
}