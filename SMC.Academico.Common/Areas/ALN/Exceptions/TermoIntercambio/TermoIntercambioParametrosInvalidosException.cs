using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class TermoIntercambioParametrosInvalidosException : SMCApplicationException
    {
        public TermoIntercambioParametrosInvalidosException()
            : base(Resources.ExceptionsResource.ERR_TermoIntercambioParametrosInvalidosException)
        { }
    }
}