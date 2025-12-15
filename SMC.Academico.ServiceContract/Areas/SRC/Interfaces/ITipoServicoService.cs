using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface ITipoServicoService : ISMCService
    {
        TipoServicoData BuscarTipoServico(long seqTipoServico);

        TipoServicoData BuscarTipoServicoPorToken(string token);

        List<SMCDatasourceItem> BuscarTiposServicosSelect();

        List<SMCDatasourceItem> BuscarTiposServicosPorInstituicaoNivelEnsinoSelect();

        List<SMCDatasourceItem> BuscarTiposServicosPorAlunoSelect(ServicoPorAlunoFiltroData filtro);

        List<SMCDatasourceItem> BuscarClassesTemplateProcessoSgfSelect();

    }
}