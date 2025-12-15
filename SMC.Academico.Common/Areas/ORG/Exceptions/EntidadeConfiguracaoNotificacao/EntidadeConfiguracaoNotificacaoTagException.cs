using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.ORG.Resources;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class EntidadeConfiguracaoNotificacaoTagException : SMCApplicationException
    {
        public EntidadeConfiguracaoNotificacaoTagException(string tag, string tipoNotificacao)
            : base(string.Format(ExceptionsResource.ERR_EntidadeConfiguracaoNotificacaoTagException, tag, tipoNotificacao))
        {
        }    
    }
}
