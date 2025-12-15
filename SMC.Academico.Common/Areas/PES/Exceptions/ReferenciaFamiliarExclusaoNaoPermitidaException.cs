using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class ReferenciaFamiliarExclusaoNaoPermitidaException : SMCApplicationException
    {
        public ReferenciaFamiliarExclusaoNaoPermitidaException()
            : base(ExceptionsResource.ERR_ReferenciaFamiliarExclusaoNaoPermitidaException)
        {
        }
    }
}
