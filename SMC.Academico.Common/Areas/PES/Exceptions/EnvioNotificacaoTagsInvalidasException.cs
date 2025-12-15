using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.PES.Resources;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class EnvioNotificacaoTagsInvalidasException : SMCApplicationException
    {
        public EnvioNotificacaoTagsInvalidasException(string tag, string tipoNotificacao)
            : base(string.Format(ExceptionsResource.ERR_EnvioNotificacaoTagsInvalidasException, tag, tipoNotificacao))
        {     
        }
    }
}
