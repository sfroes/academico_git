using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class SituacaoMatriculaInvalidaException : SMCApplicationException
    {
        public SituacaoMatriculaInvalidaException()
            : base(ExceptionsResource.ERR_SituacaoMatriculaInvalidaException)
        { }
    }
}