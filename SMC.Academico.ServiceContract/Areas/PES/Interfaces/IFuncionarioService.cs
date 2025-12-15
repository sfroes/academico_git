using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IFuncionarioService : ISMCService
    {
        /// <summary>
        /// Busca funcionários com seus dados pessoais
        /// </summary>
        /// <param name="filtro">Filtros para busca</param>
        /// <returns>Dados paginados dos funcionários</returns>
        SMCPagerData<FuncionarioListaData> BuscarFuncionarios(FuncionarioFiltroData filtro);

        /// <summary>
        /// Recupera um funcionário com seus dados pessoais e de contato
        /// </summary>
        /// <param name="seq">Sequencial do funcionário</param>
        /// <returns>Dados do funcionario com suas dependências</returns>
        FuncionarioData BuscarFuncionario(long seq);


        /// <summary>
        /// Valida as configurações iniciais do funcionário
        /// </summary>
        /// <returns>Novo objeto de colaborador caso o tipo de vínculo esteja configurado</returns>
        /// <exception cref="InstituicaoTipoAtuacaoNaoConfiguradaException">Caso o tipo de atuação não esteja configurado na instituição</exception>
        FuncionarioData BuscarConfiguracaoFuncionario();

        /// <summary>
        /// Grava um funcionario com seus dados pessoais e dados de contato
        /// </summary>
        /// <param name="funcionario">Dados do funcionario a ser gravado</param>
        /// <returns>Sequencial do funcionario</returns>
        long SalvarFuncionario(FuncionarioData funcionario);

        /// <summary>
        /// Lista os funcionarios com o vinculo ativo na data vigente, com codigo e nome do funcionario baseado no filtro do tipoTokenFuncionario
        /// </summary>
        /// <param name="tokenTipoFuncionario"></param>
        /// <returns>Retorna lista dos funcionarios com vinculo ativo filtrados pelo tipo de funcionario</returns>
        List<SMCDatasourceItem> ListarFuncionariosVinculoAtivo(string tokenTipoFuncionario);
    }
}