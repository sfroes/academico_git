using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ServicoSemConfiguracoesException : SMCApplicationException
    {
        public ServicoSemConfiguracoesException() : base(ExceptionsResource.ERR_ServicoSemConfiguracoesException)
        {
        }
    }
}