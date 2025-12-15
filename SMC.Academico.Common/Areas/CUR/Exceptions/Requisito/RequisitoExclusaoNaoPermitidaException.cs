using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class RequisitoExclusaoNaoPermitidaException : SMCApplicationException
    {
        public RequisitoExclusaoNaoPermitidaException()
            : base(ExceptionsResource.ERR_ExcluirRequisitoMatrizCurricularAssociada)
        {
        }
    }
}