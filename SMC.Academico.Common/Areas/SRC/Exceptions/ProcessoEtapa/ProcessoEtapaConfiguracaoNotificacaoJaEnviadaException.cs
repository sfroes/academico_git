using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaConfiguracaoNotificacaoJaEnviadaException : SMCApplicationException
    {
        public ProcessoEtapaConfiguracaoNotificacaoJaEnviadaException()
            : base(ExceptionsResource.ERR_ProcessoEtapaConfiguracaoNotificacaoJaEnviadaException)
        {
        }
    }
}