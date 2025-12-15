using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class IntegracaoDadosMestresException : SMCApplicationException
    {
        public IntegracaoDadosMestresException(string mensagem)
            : base(string.Format(ExceptionsResource.ERR_IntegracaoDadosMestresException, mensagem))
        {
        }
    }
}