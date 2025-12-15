using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IPosicaoConsolidadaService : ISMCService
    {        
        SMCPagerData<PosicaoConsolidadaListarData> ListarPosicoesConsolidadas(PosicaoConsolidadaFiltroData filtro);         
    }
}
