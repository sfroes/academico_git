using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class ArquivoInvalidoException : SMCApplicationException
    {
        public ArquivoInvalidoException()
            : base(ExceptionsResource.ERR_ArquivoInvalidoException)
        {
        }
    }
}