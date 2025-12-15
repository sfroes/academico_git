using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMC.Framework.Model;
using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Framework.Extensions;
using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.FIN.Models;

namespace SMC.Academico.Service.Areas.FIN.Services
{
    public class ConfiguracaoBeneficioService : SMCServiceBase, IConfiguracaoBeneficioService
    {
        #region [ DomainServices ]

        private ConfiguracaoBeneficioDomainService ConfiguracaoBeneficioDomainService
        {
            get { return this.Create<ConfiguracaoBeneficioDomainService>(); }
        }

        #endregion [ DomainServices ]

        public SMCPagerData<ConfiguracaoBeneficioData> BuscarConfiguracoesBeneficios(ConfiguracaoBeneficioFiltroData filtro)
        {
            var spec = filtro.Transform<ConfiguracaoBeneficioFilterSpecification>();
            return ConfiguracaoBeneficioDomainService.BuscarConfiguracoesBeneficios(spec).Transform<SMCPagerData<ConfiguracaoBeneficioData>>();
        }

        public bool VerificarDeducaoBeneficio(long seqInstituicaoNivelBeneficio)
        {
            return ConfiguracaoBeneficioDomainService.VerificarDeducaoBeneficio(seqInstituicaoNivelBeneficio);
        }

        public ConfiguracaoBeneficioData BuscarDadosConfiguracaoBeneficio(ConfiguracaoBeneficioData configuracaoBeneficio)
        {
            var retorno = ConfiguracaoBeneficioDomainService.BuscarDadosConfiguracaoBeneficio(configuracaoBeneficio.SeqInstituicaoNivelBeneficio).Transform<ConfiguracaoBeneficioData>();
            ///Inicia com o padrão
            //retorno.RecebeCobranca = true;
            return retorno;
        }

        public long SalvarConfiguracaoBeneficio(ConfiguracaoBeneficioData configuracaoBeneficio)
        {
            var configuracaoBeneficioDados = configuracaoBeneficio.Transform<ConfiguracaoBeneficio>();

            return ConfiguracaoBeneficioDomainService.SalvarConfiguracaoBeneficio(configuracaoBeneficioDados);
        }

        public void ExcluirConfiguracaoBeneficio(long seq)
        {
            ConfiguracaoBeneficioDomainService.ExcluirConfiguracaoBeneficio(seq);
        }

        public bool VerificarAssociacaoPessoaBeneficio(long seq)
        {
            return ConfiguracaoBeneficioDomainService.VerificarAssociacaoPessoaBeneficio(seq);
        }

        public ConfiguracaoBeneficioData AlterarConfiguracoesBeneficios(long seq)
        {
            return ConfiguracaoBeneficioDomainService.AlterarConfiguracoesBeneficios(seq).Transform<ConfiguracaoBeneficioData>();
        }
    }
}
