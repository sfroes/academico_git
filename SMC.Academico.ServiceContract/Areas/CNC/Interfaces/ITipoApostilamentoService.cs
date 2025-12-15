using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Interfaces
{
    public interface ITipoApostilamentoService : ISMCService
    {
        TipoApostilamentoData BuscarTipoApostilamento(long seq);

        List<SMCDatasourceItem> BuscarTiposApostilamentoSelect();

        List<SMCDatasourceItem> BuscarTiposApostilamentoSemTokenFormacaoSelect();
    }
}
