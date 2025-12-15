using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class PeriodoIntercambioDataFimMenorConclusaoInvalidaException : SMCApplicationException
    {
        public PeriodoIntercambioDataFimMenorConclusaoInvalidaException() : base (ExceptionsResource.ERR_PeriodoIntercambioDataFimMenorQueConclusaoInvalidaException)
        {}
    }
}
