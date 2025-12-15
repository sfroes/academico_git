using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class PeriodoIntercambioProcessoRenovacaoEncerradoException : SMCApplicationException
    {
        public PeriodoIntercambioProcessoRenovacaoEncerradoException() : base(ExceptionsResource.ERR_PeriodoIntercambioProcessoRenovacaoEncerradoException)
        {}
    }
}
