using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Interfaces
{
    public interface IApuracaoFrequenciaGradeService : ISMCService
    {
        LancamentoFrequenciaData BuscarLancamentoFrequencia(long seqOrigemAvaliacao);
        TimeSpan? BuscarHorarioLimiteTurno(long seqOrigemAvaliacao);
        void SalvarLancamentoFrequencia(List<ApuracaoFrequenciaGradeData> data);
    }
}
