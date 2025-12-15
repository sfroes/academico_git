using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class SemVigenciaComDataValidaException : SMCApplicationException
    {
        public SemVigenciaComDataValidaException() 
            : base(Resources.ExceptionsResource.ERR_SemVigenciaComDataValidaException)
        { }
    }
}

