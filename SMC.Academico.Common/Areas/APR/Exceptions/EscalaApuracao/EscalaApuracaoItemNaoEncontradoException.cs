using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class EscalaApuracaoItemNaoEncontradoException : SMCApplicationException
    {
        public EscalaApuracaoItemNaoEncontradoException(short nota)
            : base(string.Format(ExceptionsResource.ERR_EscalaApuracaoItemNaoEncontradoException, nota))
        { }
    }
}