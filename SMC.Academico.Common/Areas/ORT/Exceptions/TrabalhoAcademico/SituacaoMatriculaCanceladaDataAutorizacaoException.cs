using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class SituacaoMatriculaCanceladaDataAutorizacaoException : SMCApplicationException
    {
        public SituacaoMatriculaCanceladaDataAutorizacaoException()
            : base(ExceptionsResource.ERR_SituacaoMatriculaCanceladaDataAutorizacaoException)
        {
        }
    }
}