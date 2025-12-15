using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Interfaces
{
    public interface IInstituicaoTipoEventoService : ISMCService
    {
        List<SMCDatasourceItem> BuscarTiposEventosAGDSelect(TipoEventoFiltroData filtros);

        SMCPagerData<InstituicaoTipoEventoListaData> BuscarInstituicoesTiposEventos(InstituicaoTipoEventoFiltroData filtros);

        InstituicaoTipoEventoData BuscarInstituicaoTipoEvento(InstituicaoTipoEventoFiltroData seqInstituicaoTipoEvento);

        long SalvarInstituicaoTipoEvento(InstituicaoTipoEventoData modelo);

        List<SMCDatasourceItem> BuscarParametrosInstituicaoTipoEventoSelect(InstituicaoTipoEventoFiltroData filtros);
    }
}