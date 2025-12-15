using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.APR.Services
{
    public class ApuracaoFrequenciaGradeService : SMCServiceBase, IApuracaoFrequenciaGradeService
    {
        #region [ Domain Services ]

        private ApuracaoFrequenciaGradeDomainService ApuracaoFrequenciaGradeDomainService => Create<ApuracaoFrequenciaGradeDomainService>();

        #endregion [ Domain Services ]

        public LancamentoFrequenciaData BuscarLancamentoFrequencia(long seqOrigemAvaliacao)
        {
            return ApuracaoFrequenciaGradeDomainService.BuscarLancamentoFrequencia(seqOrigemAvaliacao).Transform<LancamentoFrequenciaData>();
        }

        public TimeSpan? BuscarHorarioLimiteTurno(long seqOrigemAvaliacao)
        {
            return ApuracaoFrequenciaGradeDomainService.BuscarHorarioLimiteTurno(seqOrigemAvaliacao);
        }

        public void SalvarLancamentoFrequencia(List<ApuracaoFrequenciaGradeData> data)
        {
            ApuracaoFrequenciaGradeDomainService.SalvarLancamentoFrequencia(data.TransformList<ApuracaoFrequenciaGradeVO>());
        }
    }
}
