using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class NovaDataPrevisaoAnteriorPrevisaoAtualException : SMCApplicationException
    {
        public NovaDataPrevisaoAnteriorPrevisaoAtualException()
            : base (ExceptionsResource.ERR_NovaDataPrevisaoAnteriorPrevisaoAtualException)
        { }
    }
}