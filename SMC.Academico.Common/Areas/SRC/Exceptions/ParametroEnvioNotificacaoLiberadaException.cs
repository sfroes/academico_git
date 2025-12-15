using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ParametroEnvioNotificacaoLiberadaException : SMCApplicationException
    {
        public ParametroEnvioNotificacaoLiberadaException()
            : base(ExceptionsResource.ERR_ParametroEnvioNotificacaoLiberadaException)
        {
        }
    }
}