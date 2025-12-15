using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoDataFimMaiorDataFimPeriodoFinanceiroCopiarProcessoException : SMCApplicationException
    {
        public ProcessoDataFimMaiorDataFimPeriodoFinanceiroCopiarProcessoException()
            : base(ExceptionsResource.ERR_ProcessoDataFimMaiorDataFimPeriodoFinanceiroCopiarProcessoException)
        {
        }
    }
}