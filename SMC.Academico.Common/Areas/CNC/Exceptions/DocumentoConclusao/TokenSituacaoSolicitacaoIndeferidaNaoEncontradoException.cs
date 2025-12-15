using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class TokenMotivoSolicitacaoIndeferidaNaoEncontradoException : SMCApplicationException
    {
        public TokenMotivoSolicitacaoIndeferidaNaoEncontradoException(string token)
            : base(string.Format(ExceptionsResource.ERR_TokenMotivoSolicitacaoIndeferidaNaoEncontradoException, token))
        { }
    }
}
