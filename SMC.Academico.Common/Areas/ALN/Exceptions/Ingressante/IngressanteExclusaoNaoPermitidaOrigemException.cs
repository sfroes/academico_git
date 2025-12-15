using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class IngressanteExclusaoNaoPermitidaOrigemException : SMCApplicationException
    {
        public IngressanteExclusaoNaoPermitidaOrigemException()
            : base(ExceptionsResource.ERR_IngressanteExclusaoNaoPermitidaOrigemException)
        { }
    }
}