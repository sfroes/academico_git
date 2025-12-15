using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Interfaces
{
    public interface ITipoProcessoSeletivoService : ISMCService
    {
        List<SMCDatasourceItem> BuscarTiposProcessoSeletivoSelect();

        List<SMCDatasourceItem> BuscarTiposProcessoSeletivoPorCampanhaSelect(long SeqCampanha);

        List<SMCDatasourceItem> BuscarTiposProcessoSeletivoPorNivelEnsinoSelect(TipoProcessoSeletivoSelectFiltroData filtro);
    }
}