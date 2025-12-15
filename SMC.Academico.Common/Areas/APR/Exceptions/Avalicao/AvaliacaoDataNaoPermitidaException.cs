using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class AvaliacaoDataNaoPermitidaException : SMCApplicationException
    {
        public AvaliacaoDataNaoPermitidaException(string status)
            : base(string.Format(ExceptionsResource.ERR_AvaliacaoDataNaoPermitidaException, status))
        {
        }
    }
}