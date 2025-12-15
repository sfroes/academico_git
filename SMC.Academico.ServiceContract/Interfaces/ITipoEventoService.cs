using SMC.Academico.Common.Constants;
using SMC.Calendarios.ServiceContract.Areas.CLD.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface ITipoEventoService : ISMCService
    {
        List<SMCDatasourceItem> BuscarTiposEventosAGDSelect(TipoEventoFiltroData filtros);

        SMCPagerData<TipoEventoData> BuscarTiposEventosAGD(TipoEventoFiltroData filtros);

        List<SMCDatasourceItem> BuscarTiposEventosCalendarioAGD(long seqCalendarioAgd);

        string BuscarTokenTipoEventoAGD(long seqTipoEventoAgd);
    }
}