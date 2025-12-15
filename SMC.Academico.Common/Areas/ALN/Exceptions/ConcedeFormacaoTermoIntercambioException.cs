using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class ConcedeFormacaoTermoIntercambioException : SMCApplicationException
    {
        public ConcedeFormacaoTermoIntercambioException()
        : base(ExceptionsResource.ERR_ConcedeFormacaoTermoIntercambioException)
        {
        }
    }
}
 