using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class EnvioNotificacaoService : SMCServiceBase, IEnvioNotificacaoService
    {
        #region DomainService

        private EnvioNotificacaoDomainService EnvioNotificacaoDomainService => Create<EnvioNotificacaoDomainService>();

        #endregion DomainService

        public SMCPagerData<EnvioNotificacaoListarData> BuscarEnvioNotificacoes(EnvioNotificacaoFiltroData filtro)
        {
            return EnvioNotificacaoDomainService.BuscarEnvioNotificacoes(filtro.Transform<EnvioNotificacaoFiltroVO>())
                                           .Transform<SMCPagerData<EnvioNotificacaoListarData>>();
        }

        public SMCPagerData<EnvioNotificacaoPessoasListarData> BuscarPessoasEnvioNotificacoes(EnvioNotificacaoFiltroSelecaoData filtro)
        {
            return EnvioNotificacaoDomainService.BuscarPessoasEnvioNotificacoes(filtro.Transform<EnvioNotificacaoFiltroSelecaoVO>())
                                           .Transform<SMCPagerData<EnvioNotificacaoPessoasListarData>>();
        }
        public SMCPagerData<EnvioNotificacaoPessoasListarData> BuscarDestinatariosVisualizarNotificacao(VisualizarDestinatariosNotificacaoData filtro)
        {
            return EnvioNotificacaoDomainService.BuscarDestinatariosVisualizarNotificacao(filtro.Transform<VisualizarDestinatariosNotificacaoVO>())
                                           .Transform<SMCPagerData<EnvioNotificacaoPessoasListarData>>();
        }
        public SMCPagerData<EnvioNotificacaoPessoasListarData> BuscarPessoasEnvioNotificacoesConfirmacao(EnvioNotificacaoData filtro)
        {
            return EnvioNotificacaoDomainService.BuscarPessoasEnvioNotificacoesConfirmacao(filtro.Transform<EnvioNotificacaoVO>())
                                           .Transform<SMCPagerData<EnvioNotificacaoPessoasListarData>>();
        }

        public void SalvarEEnviarNotificacao(EnvioNotificacaoData model)
            => EnvioNotificacaoDomainService.SalvarEEnviarNotificacao(model.Transform<EnvioNotificacaoVO>());

        public void ValidaTagsEnvioNotificacao(EnvioNotificacaoData model)
            => EnvioNotificacaoDomainService.ValidaTagsEnvioNotificacao(model.Transform<EnvioNotificacaoVO>());

        public void RealizarEnvioNotificacaoJob(RealizarEnvioNotificacaoSATData model)
            => EnvioNotificacaoDomainService.RealizarEnvioNotificacaoJob(model.Transform<RealizarEnvioNotificacaoSATVO>());

    }
}