using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class TokenSituacaoDocumentoAcademicoNaoEncontradoException : SMCApplicationException
    {
        public TokenSituacaoDocumentoAcademicoNaoEncontradoException(string token)
            : base(string.Format(ExceptionsResource.ERR_TokenSituacaoDocumentoAcademicoNaoEncontradoException,token))
        { }
    }
}
