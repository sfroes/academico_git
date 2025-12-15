using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class ProcessoNaoRematriculaException : SMCApplicationException
    {
        public ProcessoNaoRematriculaException()
            : base (ExceptionsResource.ERR_ProcessoNaoRematriculaException)
        { }
    }
}