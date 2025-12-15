using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class EscalonamentoDataInicioEscalonamentoMaiorDataFimEscalonamentoAnteriorException : SMCApplicationException
    {
        public EscalonamentoDataInicioEscalonamentoMaiorDataFimEscalonamentoAnteriorException()
            : base(ExceptionsResource.ERR_EscalonamentoDataInicioEscalonamentoMaiorDataFimEscalonamentoAnteriorException)
        {
        }
    }
}