using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ParametroEnvioNotificacaoComNotificacaoJaEnviadaException : SMCApplicationException
    {
        public ParametroEnvioNotificacaoComNotificacaoJaEnviadaException()
            : base(ExceptionsResource.ERR_ParametroEnvioNotificacaoComNotificacaoJaEnviadaException)
        {
        }
    }
}