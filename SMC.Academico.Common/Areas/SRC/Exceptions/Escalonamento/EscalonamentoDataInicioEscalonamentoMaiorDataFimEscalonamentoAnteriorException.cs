using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class EscalonamentoDataFimMaiorDataFimPeriodoFinanceiroException : SMCApplicationException
    {
        public EscalonamentoDataFimMaiorDataFimPeriodoFinanceiroException()
            : base(ExceptionsResource.ERR_EscalonamentoDataFimMaiorDataFimPeriodoFinanceiroException)
        {
        }
    }
}