using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoDispensaOrigemNaoAgrupadaException : SMCApplicationException
    {
        public SolicitacaoDispensaOrigemNaoAgrupadaException()
            : base(ExceptionsResource.ERR_SolicitacaoDispensaOrigemNaoAgrupadaException)
        {

        }
    }
}