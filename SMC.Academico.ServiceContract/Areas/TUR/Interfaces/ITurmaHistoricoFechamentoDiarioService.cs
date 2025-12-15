using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Interfaces
{
    public interface ITurmaHistoricoFechamentoDiarioService : ISMCService
    {
        long ReabrirDiario(TurmaHistoricoFechamentoDiarioData data);
    }
}
