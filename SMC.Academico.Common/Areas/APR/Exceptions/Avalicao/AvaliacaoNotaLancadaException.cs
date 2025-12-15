using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class AvaliacaoNotaLancadaException : SMCApplicationException
    {
        public AvaliacaoNotaLancadaException()
            : base(ExceptionsResource.ERR_AvaliacaoNotaLancadaException)
        {
        }
    }
}