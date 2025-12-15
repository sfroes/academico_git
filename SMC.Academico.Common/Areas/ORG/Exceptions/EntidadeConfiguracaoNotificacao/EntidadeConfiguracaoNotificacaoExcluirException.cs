using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.ORG.Resources;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class EntidadeConfiguracaoNotificacaoExcluirException : SMCApplicationException
    {
        public EntidadeConfiguracaoNotificacaoExcluirException()
            : base(ExceptionsResource.ERR_EntidadeConfiguracaoNotificacaoExcluirException)
        {
        }
    }
}
