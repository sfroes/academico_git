using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaParametrosPrazoInvalidoException : SMCApplicationException
    {
        public ProcessoEtapaParametrosPrazoInvalidoException()
            : base(ExceptionsResource.ERR_ProcessoEtapaParametrosPrazoInvalidoException)
        {

        }
    }
}