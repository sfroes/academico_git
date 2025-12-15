using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class ConfiguracaoEtapaBloqueioService : SMCServiceBase, IConfiguracaoEtapaBloqueioService
    {
        private ConfiguracaoEtapaBloqueioDomainService ConfiguracaoEtapaBloqueioDomainService
        {
            get { return this.Create<ConfiguracaoEtapaBloqueioDomainService>(); }
        }

        public ConfiguracaoEtapaBloqueioData BuscarConfiguracaoEtapaBloqueio(long seqConfiguracaoEtapaBloqueio)
        {
            return ConfiguracaoEtapaBloqueioDomainService.BuscarConfiguracaoEtapaBloqueio(seqConfiguracaoEtapaBloqueio).Transform<ConfiguracaoEtapaBloqueioData>();
        }

        public SMCPagerData<ConfiguracaoEtapaBloqueioListarData> BuscarConfiguracoesEtapaBloqueio(ConfiguracaoEtapaBloqueioFiltroData filtro)
        {
            return this.ConfiguracaoEtapaBloqueioDomainService.BuscarConfiguracoesEtapaBloqueio(filtro.Transform<ConfiguracaoEtapaBloqueioFiltroVO>()).Transform<SMCPagerData<ConfiguracaoEtapaBloqueioListarData>>();
        }

        public long Salvar(ConfiguracaoEtapaBloqueioData modelo)
        {
            return this.ConfiguracaoEtapaBloqueioDomainService.Salvar(modelo.Transform<ConfiguracaoEtapaBloqueioVO>());
        }

        public void ValidarModeloSalvar(ConfiguracaoEtapaBloqueioData modelo)
        {
            this.ConfiguracaoEtapaBloqueioDomainService.ValidarModeloSalvar(modelo.Transform<ConfiguracaoEtapaBloqueioVO>());
        }       

        public void Excluir(long seq)
        {
            this.ConfiguracaoEtapaBloqueioDomainService.Excluir(seq);
        }

        public void ValidarModeloExcluir(long seq)
        {
            this.ConfiguracaoEtapaBloqueioDomainService.ValidarModeloExcluir(seq);
        }        
    }
}
