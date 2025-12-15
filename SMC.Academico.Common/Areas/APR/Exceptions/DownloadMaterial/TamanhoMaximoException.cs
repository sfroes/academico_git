using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class TamanhoMaximoException : SMCApplicationException
    {
        public TamanhoMaximoException()
            : base(ExceptionsResource.ERR_TamanhoMaximoException)
        {
        }
    }
}