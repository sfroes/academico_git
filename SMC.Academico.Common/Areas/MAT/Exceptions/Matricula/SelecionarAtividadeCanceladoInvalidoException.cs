using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class SelecionarAtividadeCanceladoInvalidoException : SMCApplicationException
    {
        public SelecionarAtividadeCanceladoInvalidoException()
            : base(ExceptionsResource.ERR_SelecionarAtividadeCanceladoInvalidoException)
        {
        }
    }   
}
