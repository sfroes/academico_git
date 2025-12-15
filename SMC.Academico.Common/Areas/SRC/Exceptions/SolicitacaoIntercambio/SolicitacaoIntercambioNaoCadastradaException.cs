using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoIntercambioNaoCadastradaException : SMCApplicationException
    {
        public SolicitacaoIntercambioNaoCadastradaException()
            : base(ExceptionsResource.ERR_SolicitacaoIntercambioNaoCadastradaException)
        {
        }
    }
}