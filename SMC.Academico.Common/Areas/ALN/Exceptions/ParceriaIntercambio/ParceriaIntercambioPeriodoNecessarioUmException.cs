using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class ParceriaIntercambioPeriodoNecessarioUmException : SMCApplicationException
    {
        public ParceriaIntercambioPeriodoNecessarioUmException() 
            : base(Resources.ExceptionsResource.ERR_ParceriaIntercambioPeriodoNecessarioUmException)
        { }
    }
}

