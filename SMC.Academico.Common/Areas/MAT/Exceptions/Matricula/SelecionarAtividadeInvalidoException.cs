using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class SelecionarAtividadeInvalidoException : SMCApplicationException
    {
        public SelecionarAtividadeInvalidoException()
            : base(ExceptionsResource.ERR_SelecionarAtividadeInvalidoException)
        {
        }
    }
}
