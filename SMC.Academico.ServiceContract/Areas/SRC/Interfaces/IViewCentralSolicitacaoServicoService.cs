using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IViewCentralSolicitacaoServicoService : ISMCService
    {
        SMCPagerData<SolicitacaoServicoListarData> BuscarSolicitacoesServicoLista(SolicitacaoServicoFiltroData filtro);
    }
}