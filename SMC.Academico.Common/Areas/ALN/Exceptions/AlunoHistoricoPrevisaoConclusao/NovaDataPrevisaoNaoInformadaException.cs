using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class NovaDataPrevisaoNaoInformadaException : SMCApplicationException
    {
        public NovaDataPrevisaoNaoInformadaException()
            : base(ExceptionsResource.ERR_NovaDataPrevisaoNaoInformadaException)
        { }
    }
}