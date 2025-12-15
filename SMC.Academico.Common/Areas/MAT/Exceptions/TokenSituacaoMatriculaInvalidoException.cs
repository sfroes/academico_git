using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class TokenSituacaoMatriculaInvalidoException : SMCApplicationException
    {
        public TokenSituacaoMatriculaInvalidoException()
            : base(ExceptionsResource.ERR_TokenSituacaoMatriculaInvalidoException)
        { }
    }
}