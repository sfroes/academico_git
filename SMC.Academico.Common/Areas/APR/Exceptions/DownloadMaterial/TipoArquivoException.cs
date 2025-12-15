using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class TipoArquivoException : SMCApplicationException
    {
        public TipoArquivoException(string nome)
            : base(string.Format(ExceptionsResource.ERR_TipoArquivoException, nome))
        {
        }
    }
}