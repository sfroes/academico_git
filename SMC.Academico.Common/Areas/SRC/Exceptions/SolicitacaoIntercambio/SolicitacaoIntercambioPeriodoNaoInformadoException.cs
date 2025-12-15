using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoIntercambioPeriodoNaoInformadoException : SMCApplicationException
    {
        public SolicitacaoIntercambioPeriodoNaoInformadoException()
            : base(ExceptionsResource.ERR_SolicitacaoIntercambioPeriodoNaoInformadoException)
        { }
    }
}