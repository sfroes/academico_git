using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoUnidadeResponsavelRepetidaException : SMCApplicationException
    {
        public ProcessoUnidadeResponsavelRepetidaException()
            : base(ExceptionsResource.ERR_ProcessoUnidadeResponsavelRepetidaException)
        { }
    }
}