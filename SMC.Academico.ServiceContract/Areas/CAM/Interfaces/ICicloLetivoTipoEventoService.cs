using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.CAM.Interfaces
{
    public interface ICicloLetivoTipoEventoService : ISMCService
    {
        long SalvarCicloLetivoTipoEvento(CicloLetivoTipoEventoData modelo);

        SMCPagerData<CicloLetivoTipoEventoListaData> BuscarCiclosLetivosTiposEventos(CicloLetivoTipoEventoFiltroData filtros);

        CicloLetivoTipoEventoData BuscarCicloLetivoTipoEvento(long seq);
    }
}