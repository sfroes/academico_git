using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoIntercambioPeriodoCoincidenteException : SMCApplicationException
    {
        public SolicitacaoIntercambioPeriodoCoincidenteException()
            : base(ExceptionsResource.ERR_SolicitacaoIntercambioPeriodoCoincidenteException)
        { }
    }
}