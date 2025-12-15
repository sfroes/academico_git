using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class PeriodoIntercambioDataCoincideException : SMCApplicationException
    {
        public PeriodoIntercambioDataCoincideException() : base (ExceptionsResource.ERR_PeriodoIntercambioDataCoincideException)
        {}
    }
}
