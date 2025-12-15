using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.ALN.Resources;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class LiberacaoIngressaneInvalidaException : SMCApplicationException
    {
        public LiberacaoIngressaneInvalidaException()
            : base(ExceptionsResource.ERR_LiberacaoIngressaneInvalidaException)
        { }
    }
}