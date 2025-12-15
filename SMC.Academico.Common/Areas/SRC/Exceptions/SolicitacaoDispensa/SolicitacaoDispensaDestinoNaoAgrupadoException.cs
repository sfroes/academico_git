using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoDispensaDestinoNaoAgrupadoException : SMCApplicationException
    {
        public SolicitacaoDispensaDestinoNaoAgrupadoException()
            : base(ExceptionsResource.ERR_SolicitacaoDispensaDestinoNaoAgrupadoException)
        {
        }
    }
}