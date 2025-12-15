using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IJustificativaSolicitacaoServicoService : ISMCService
    {
        List<SMCDatasourceItem> BuscarJustificativasSolicitacaoServicoSelect(JustificativaSolicitacaoServicoFiltroData filter);
    }
}