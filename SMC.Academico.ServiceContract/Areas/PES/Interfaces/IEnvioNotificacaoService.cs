using System.ServiceModel;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IEnvioNotificacaoService : ISMCService
    {
        SMCPagerData<EnvioNotificacaoListarData> BuscarEnvioNotificacoes(EnvioNotificacaoFiltroData filtro);

        SMCPagerData<EnvioNotificacaoPessoasListarData> BuscarPessoasEnvioNotificacoes(EnvioNotificacaoFiltroSelecaoData filtro);

        SMCPagerData<EnvioNotificacaoPessoasListarData> BuscarDestinatariosVisualizarNotificacao(VisualizarDestinatariosNotificacaoData filtro);

        SMCPagerData<EnvioNotificacaoPessoasListarData> BuscarPessoasEnvioNotificacoesConfirmacao(EnvioNotificacaoData filtro);

        void SalvarEEnviarNotificacao(EnvioNotificacaoData model);

        void ValidaTagsEnvioNotificacao(EnvioNotificacaoData model);
        void RealizarEnvioNotificacaoJob(RealizarEnvioNotificacaoSATData model);

    }
}