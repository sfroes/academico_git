using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class ApuracaoAvaliacaoAulasNaoApuradasException : SMCApplicationException
    {
        public ApuracaoAvaliacaoAulasNaoApuradasException()
            : base(ExceptionsResource.ERR_ApuracaoAvaliacaoAulasNaoApuradasException)
        {
        }
    }
}