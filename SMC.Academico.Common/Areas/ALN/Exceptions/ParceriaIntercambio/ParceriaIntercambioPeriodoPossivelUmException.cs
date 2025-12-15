using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class ParceriaIntercambioPeriodoPossivelUmException : SMCApplicationException
    {
        public ParceriaIntercambioPeriodoPossivelUmException() 
            : base(Resources.ExceptionsResource.ERR_ParceriaIntercambioPeriodoPossivelUmException)
        { }
    }
}

