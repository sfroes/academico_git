using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.ORT.Services
{
    public class ConfiguracaoNumeracaoTrabalhoService : SMCServiceBase, IConfiguracaoNumeracaoTrabalhoService
    {
        #region DomainService

        private ConfiguracaoNumeracaoTrabalhoDomainService ConfiguracaoNumeracaoTrabalhoDomainService { get => Create<ConfiguracaoNumeracaoTrabalhoDomainService>(); }

        #endregion [DomainServices]

        public SMCPagerData<ConfiguracaoNumeracaoTrabalhoListaData> BuscarConfiguracaoNumeracaoTrabalho(ConfiguracaoNumeracaoTrabalhoFiltroData filtro)
        {
            var resultado = this.ConfiguracaoNumeracaoTrabalhoDomainService
                .BuscarConfiguracaoNumeracaoTrabalho(filtro.Transform<ConfiguracaoNumeracaoTrabalhoFilterSpecification>())
                .Transform<SMCPagerData<ConfiguracaoNumeracaoTrabalhoListaData>>();

            return resultado;
        }

        public long SalvarConfiguracaoNumeracaoTrabalho(ConfiguracaoNumeracaoTrabalhoData configuracao)
        {
            return ConfiguracaoNumeracaoTrabalhoDomainService
                .SalvarConfiguracaoNumeracaoTrabalho(configuracao.Transform<ConfiguracaoNumeracaoTrabalho>());
        }
    }
}