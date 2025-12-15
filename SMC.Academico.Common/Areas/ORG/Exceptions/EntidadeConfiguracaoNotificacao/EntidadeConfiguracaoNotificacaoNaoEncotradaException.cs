using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.ORG.Resources;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class EntidadeConfiguracaoNotificacaoNaoEncotradaException : SMCApplicationException
    {
        public EntidadeConfiguracaoNotificacaoNaoEncotradaException()
            : base(ExceptionsResource.ERR_EntidadeConfiguracaoNotificacaoNaoEncotradaException)
        {
        }
    }
}
