using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ServicoSemTemplateSGFException : SMCApplicationException
    {
        public ServicoSemTemplateSGFException() : base(ExceptionsResource.ERR_ServicoSemTemplateSGFException)
        {
        }
    }
}