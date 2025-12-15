using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class CriacaoAmostraPPAException : SMCApplicationException
    {
        public CriacaoAmostraPPAException()
            : base(ExceptionsResource.ERR_CriacaoAmostraPPAException)
        {
        }
    }
}