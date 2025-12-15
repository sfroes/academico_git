using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class EscalonamentoDataVencimentoParcelaMaiorDataFimException : SMCApplicationException
    {
        public EscalonamentoDataVencimentoParcelaMaiorDataFimException()
            : base(ExceptionsResource.ERR_EscalonamentoDataVencimentoParcelaMaiorDataFim)
        {
        }
    }
}