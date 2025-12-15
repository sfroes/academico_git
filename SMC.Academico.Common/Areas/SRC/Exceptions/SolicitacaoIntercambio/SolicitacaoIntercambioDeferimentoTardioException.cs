using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoIntercambioDeferimentoTardioException : SMCApplicationException
    {
        public SolicitacaoIntercambioDeferimentoTardioException()
            : base(ExceptionsResource.ERR_SolicitacaoIntercambioDeferimentoTardioException)
        { }
    }
}