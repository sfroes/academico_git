using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class AutorizacaoJaRealizadaException : SMCApplicationException
    {
        public AutorizacaoJaRealizadaException()
            : base(ExceptionsResource.ERR_AutorizacaoJaRealizadaException)
        {
        }
    }
}