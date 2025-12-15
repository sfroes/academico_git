using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class RestauracaoNaoPermitidaException : SMCApplicationException
    {
        public RestauracaoNaoPermitidaException()
            : base(ExceptionsResource.ERR_RestauracaoNaoPermitidaException)
        { }
    }
}
