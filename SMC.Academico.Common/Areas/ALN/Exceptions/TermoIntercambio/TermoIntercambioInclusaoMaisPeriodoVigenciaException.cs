using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class TermoIntercambioInclusaoMaisPeriodoVigenciaException : SMCApplicationException
    {
        public TermoIntercambioInclusaoMaisPeriodoVigenciaException()
            : base(Resources.ExceptionsResource.ERR_TermoIntercambioInclusaoMaisPeriodoVigenciaException)
        { }
    }
}

