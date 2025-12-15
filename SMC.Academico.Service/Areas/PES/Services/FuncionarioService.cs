using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Helpers;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.DadosMestres.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class FuncionarioService : SMCServiceBase, IFuncionarioService
    {
        #region DomainService

        private FuncionarioDomainService FuncionarioDomainService => Create<FuncionarioDomainService>();

        #endregion DomainService

        /// <summary>
        /// Busca funcionários com seus dados pessoais
        /// </summary>
        /// <param name="filtros">Filtros para busca</param>
        /// <returns>Dados paginados dos funcionários</returns>
        public SMCPagerData<FuncionarioListaData> BuscarFuncionarios(FuncionarioFiltroData filtro)
        {
            return FuncionarioDomainService.BuscarFuncionarios(filtro.Transform<FuncionarioFiltroVO>())
                                           .Transform<SMCPagerData<FuncionarioListaData>>();
        }

        /// <summary>
        /// Recupera um funcionário com seus dados pessoais e de contato
        /// </summary>
        /// <param name="seq">Sequencial do funcionário</param>
        /// <returns>Dados do funcionario com suas dependências</returns>
        public FuncionarioData BuscarFuncionario(long seq)
        {
            return FuncionarioDomainService.BuscarFuncionario(seq).Transform<FuncionarioData>();
        }

        /// <summary>
        /// Valida as configurações iniciais do funcionário
        /// </summary>
        /// <returns>Novo objeto de colaborador caso o tipo de vínculo esteja configurado</returns>
        /// <exception cref="InstituicaoTipoAtuacaoNaoConfiguradaException">Caso o tipo de atuação não esteja configurado na instituição</exception>
        public FuncionarioData BuscarConfiguracaoFuncionario()
        {
            return FuncionarioDomainService.BuscarConfiguracaoFuncionario().Transform<FuncionarioData>();
        }

        /// <summary>
        /// Grava um funcionario com seus dados pessoais e dados de contato
        /// </summary>
        /// <param name="funcionarioVo">Dados do funcionario a ser gravado</param>
        /// <returns>Sequencial do funcionario</returns>
        public long SalvarFuncionario(FuncionarioData funcionario)
        {
            return FuncionarioDomainService.SalvarFuncionario(funcionario.Transform<FuncionarioVO>());
        }

        public List<SMCDatasourceItem> ListarFuncionariosVinculoAtivo(string tokenTipoFuncionario)
        {
           return FuncionarioDomainService.ListarFuncionariosVinculoAtivo(tokenTipoFuncionario);
        }
    }
}