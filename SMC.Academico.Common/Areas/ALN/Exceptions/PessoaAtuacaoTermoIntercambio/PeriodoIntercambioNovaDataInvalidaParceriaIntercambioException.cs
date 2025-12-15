using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class PeriodoIntercambioNovaDataInvalidaParceriaIntercambioException : SMCApplicationException
    {
        public PeriodoIntercambioNovaDataInvalidaParceriaIntercambioException() : base (ExceptionsResource.ERR_PeriodoIntercambioDatasParceriaInvalidasException)
        {}
    }
}
