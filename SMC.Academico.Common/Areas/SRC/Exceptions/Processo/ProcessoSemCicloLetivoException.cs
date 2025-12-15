using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoSemCicloLetivoException : SMCApplicationException
    {
        public ProcessoSemCicloLetivoException()
            : base(ExceptionsResource.ERR_ProcessoSemCicloLetivoException)
        { }
    }
}