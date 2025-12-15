using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class ProcessoAnteriorNaoEncerradoException : SMCApplicationException
    {
        public ProcessoAnteriorNaoEncerradoException()
            : base(ExceptionsResource.ERR_ProcessoAnteriorNaoEncerradoException)
        { }
    }
}