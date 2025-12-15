using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class TermoIntercambioPeriodoVigenciaExatamenteIguaisException : SMCApplicationException
    {
        public TermoIntercambioPeriodoVigenciaExatamenteIguaisException()
            : base(Resources.ExceptionsResource.ERR_TermoIntercambioPeriodoVigenciaExatamenteIguaisException)
        { }
    }
}

