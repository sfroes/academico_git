using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class IngressanteExclusaoNaoPermitidaException : SMCApplicationException
    {
        public IngressanteExclusaoNaoPermitidaException()
            : base(ExceptionsResource.ERR_IngressanteExclusaoNaoPermitidaException)
        { }
    }
}