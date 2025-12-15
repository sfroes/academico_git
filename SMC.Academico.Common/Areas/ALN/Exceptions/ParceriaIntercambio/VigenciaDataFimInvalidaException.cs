using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class VigenciaDataFimInvalidaException : SMCApplicationException
    {
        public VigenciaDataFimInvalidaException() 
            : base(Resources.ExceptionsResource.ERR_VigenciaDataFimInvalidaException)
        { }
    }
}

