using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ServicoSemMotivoSGFConfiguradoException : SMCApplicationException
    {
        public ServicoSemMotivoSGFConfiguradoException() : 
            base(ExceptionsResource.ERR_ServicoSemMotivoSGFConfiguradoException)
        {
        }
    }
}