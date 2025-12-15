using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoForaPeriodoVigenciaException : SMCApplicationException
    {
        public ProcessoForaPeriodoVigenciaException(string status)
            : base(string.Format(ExceptionsResource.ERR_ProcessoForaPeriodoVigencia, status))
        {
        }
    }
}