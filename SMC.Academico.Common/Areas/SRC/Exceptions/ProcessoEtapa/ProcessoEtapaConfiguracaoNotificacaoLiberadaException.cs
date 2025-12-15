using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaConfiguracaoNotificacaoLiberadaException : SMCApplicationException
    {
        public ProcessoEtapaConfiguracaoNotificacaoLiberadaException(string status)
            : base(string.Format(ExceptionsResource.ERR_ProcessoEtapaConfiguracaoNotificacaoLiberadaException, status))
        {
        }
    }
}