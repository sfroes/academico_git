using SMC.Academico.Common.Areas.DCT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.DCT.Exceptions
{
    public class ExclusaoColaboradorVinculoNaoPermitidaException : SMCApplicationException
    {
        public ExclusaoColaboradorVinculoNaoPermitidaException()
            : base(ExceptionsResource.ERR_ExclusaoColaboradorVinculoNaoPermitidaException)
        {
        }
    }
}