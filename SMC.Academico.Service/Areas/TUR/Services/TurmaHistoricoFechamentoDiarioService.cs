using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.TUR.Services
{
    public class TurmaHistoricoFechamentoDiarioService : SMCServiceBase, ITurmaHistoricoFechamentoDiarioService
    {
        #region [ DomainService ]

        private TurmaHistoricoFechamentoDiarioDomainService TurmaHistoricoFechamentoDiarioDomainService
        {
            get { return this.Create<TurmaHistoricoFechamentoDiarioDomainService>(); }
        }

        #endregion

        public long ReabrirDiario(TurmaHistoricoFechamentoDiarioData data)
        {
            return TurmaHistoricoFechamentoDiarioDomainService.ReabrirDiario(data.Transform<TurmaHistoricoFechamentoDiarioVO>());
        }
    }
}
