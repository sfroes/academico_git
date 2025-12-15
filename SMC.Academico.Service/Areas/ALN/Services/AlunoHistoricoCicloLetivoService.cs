using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class AlunoHistoricoCicloLetivoService : SMCServiceBase, IAlunoHistoricoCicloLetivoService
    {
        #region [ DomainService ]

        private AlunoHistoricoCicloLetivoDomainService AlunoHistoricoCicloLetivoDomainService { get => Create<AlunoHistoricoCicloLetivoDomainService>(); }

        #endregion [ DomainService ]

        public List<RelatorioConsolidadoSituacaoData> BuscarDadosRelatorioConsolidadoSituacao(RelatorioConsolidadoSituacaoFiltroData filtro)
        {
            return AlunoHistoricoCicloLetivoDomainService.BuscarDadosRelatorioConsolidadoSituacao(filtro.Transform<RelatorioConsolidadoSituacaoFiltroVO>())
                   .TransformList<RelatorioConsolidadoSituacaoData>();
        }
    }
}
