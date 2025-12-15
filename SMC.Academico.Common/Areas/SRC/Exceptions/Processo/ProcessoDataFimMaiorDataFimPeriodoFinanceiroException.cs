using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoDataFimMaiorDataFimPeriodoFinanceiroException : SMCApplicationException
    {
        public ProcessoDataFimMaiorDataFimPeriodoFinanceiroException()
            : base(ExceptionsResource.ERR_ProcessoDataFimMaiorDataFimPeriodoFinanceiroException)
        {
        }
    }
}