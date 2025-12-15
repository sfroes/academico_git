using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.CAM.Interfaces
{
    public interface IEventoLetivoService : ISMCService
    {
        long SalvarEventoLetivo(EventoLetivoData modelo);

        SMCPagerData<EventoLetivoListaData> BuscarEventosLetivos(EventoLetivoFiltroData filtros);

        EventoLetivoData BuscarEventoLetivo(long seq);

        DatasEventoLetivoData BuscarEventoLetivoAtual(long seqAluno);

        DatasEventoLetivoData BuscarDatasEventoLetivo(long seqCicloLetivo, long seqAluno, string tokenTipoEvento);
    }
}