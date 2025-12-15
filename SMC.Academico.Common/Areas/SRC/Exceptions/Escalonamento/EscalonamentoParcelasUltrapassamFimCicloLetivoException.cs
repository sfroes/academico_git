using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class EscalonamentoParcelasUltrapassamFimCicloLetivoException : SMCApplicationException
    {
        public EscalonamentoParcelasUltrapassamFimCicloLetivoException()
            : base(ExceptionsResource.ERR_EscalonamentoParcelasUltrapassamFimCicloLetivo)
        {
        }
    }
}