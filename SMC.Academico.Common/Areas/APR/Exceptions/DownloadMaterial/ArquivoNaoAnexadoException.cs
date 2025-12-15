using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class ArquivoNaoAnexadoException : SMCApplicationException
    {
        public ArquivoNaoAnexadoException()
            : base(ExceptionsResource.ERR_ArquivoNaoAnexadoException)
        {
        }
    }
}