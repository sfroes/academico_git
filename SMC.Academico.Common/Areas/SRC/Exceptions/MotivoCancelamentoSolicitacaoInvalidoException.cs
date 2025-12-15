using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class MotivoCancelamentoSolicitacaoInvalidoException : SMCApplicationException
    {
        public MotivoCancelamentoSolicitacaoInvalidoException(string token)
            : base(string.Format(ExceptionsResource.ERR_MotivoCancelamentoSolicitacaoInvalidoException, token))
        { }
    }
}