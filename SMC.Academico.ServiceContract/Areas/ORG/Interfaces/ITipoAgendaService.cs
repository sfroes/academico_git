using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface ITipoAgendaService : ISMCService
    {
        List<SMCDatasourceItem> BuscarTiposAgendaSelect(TipoAgendaFiltroData filtros);

        long SalvarTipoAgenda(TipoAgendaData modelo);

        TipoAgendaData BuscarTipoAgenda(long seqTipoAgenda);
    }
}