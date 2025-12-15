using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Service.Areas.SRC.Services;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data.SolicitacaoServico
{
    public class ViewCentralSolicitacaoServicoService : SMCServiceBase, IViewCentralSolicitacaoServicoService
    {
        #region Domain Services

        public ViewCentralSolicitacaoServicoDomainService ViewCentralSolicitacaoServicoDomainService { get { return this.Create<ViewCentralSolicitacaoServicoDomainService>(); } }

        #endregion Domain Services

        public SMCPagerData<SolicitacaoServicoListarData> BuscarSolicitacoesServicoLista(SolicitacaoServicoFiltroData filtro)
        {
            return ViewCentralSolicitacaoServicoDomainService.BuscarSolicitacoesServicoLista(filtro.Transform<ViewCentralSolicitacaoServicoFilterSpecification>()).Transform<SMCPagerData<SolicitacaoServicoListarData>>();
        }
    }
}