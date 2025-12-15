using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class UmPeriodoVigenciaException : SMCApplicationException
    {
        public UmPeriodoVigenciaException() 
            : base(Resources.ExceptionsResource.ERR_UmPeriodoVigenciaException)
        { }
    }
}

