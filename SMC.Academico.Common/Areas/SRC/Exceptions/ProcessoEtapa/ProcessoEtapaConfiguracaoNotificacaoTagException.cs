using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaConfiguracaoNotificacaoTagException : SMCApplicationException
    {
        public ProcessoEtapaConfiguracaoNotificacaoTagException(string tag, string tipoNotificao)
            : base(string.Format(ExceptionsResource.ERR_ProcessoEtapaConfiguracaoNotificacaoTagException, tipoNotificao, tag))
        {
        }
    }
}