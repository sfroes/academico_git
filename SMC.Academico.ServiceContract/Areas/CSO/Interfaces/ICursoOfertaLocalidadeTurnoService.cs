using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface ICursoOfertaLocalidadeTurnoService : ISMCService
    {
        List<SMCDatasourceItem> BuscarTurnosPorLocalidadeCursoOfertaSelect(long? seqLocalidade, long? seqCursoOferta);
    }
}