using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaTipoPrazoEscalonamentoException : SMCApplicationException
    {
        public ProcessoEtapaTipoPrazoEscalonamentoException()
            : base(ExceptionsResource.ERR_ProcessoEtapaTipoPrazoEscalonamento)
        {
        }
    }
}