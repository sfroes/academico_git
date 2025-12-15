using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IConfiguracaoEtapaBloqueioService : ISMCService
    {
        ConfiguracaoEtapaBloqueioData BuscarConfiguracaoEtapaBloqueio(long seqConfiguracaoEtapaBloqueio);

        SMCPagerData<ConfiguracaoEtapaBloqueioListarData> BuscarConfiguracoesEtapaBloqueio(ConfiguracaoEtapaBloqueioFiltroData filtro);

        long Salvar(ConfiguracaoEtapaBloqueioData modelo);

        void ValidarModeloSalvar(ConfiguracaoEtapaBloqueioData modelo);

        void Excluir(long seq);

        void ValidarModeloExcluir(long seq);
    }
}
