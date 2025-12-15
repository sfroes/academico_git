using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class NovaDataPrevisaoAnteriorNaoUltimoDiaMesException : SMCApplicationException
    {
        public NovaDataPrevisaoAnteriorNaoUltimoDiaMesException()
            : base(ExceptionsResource.ERR_NovaDataPrevisaoAnteriorNaoUltimoDiaMesException)
        { }
    }
}