using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class SituacaoMatriculaCanceladaException : SMCApplicationException
    {
        public SituacaoMatriculaCanceladaException()
            : base(ExceptionsResource.ERR_SituacaoMatriculaCanceladaException)
        {
        }
    }
}