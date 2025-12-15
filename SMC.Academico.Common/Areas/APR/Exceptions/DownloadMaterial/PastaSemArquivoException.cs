using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class PastaSemArquivoException : SMCApplicationException
    {
        public PastaSemArquivoException()
            : base(ExceptionsResource.ERR_PastaSemArquivoException)
        {
        }
    }
}