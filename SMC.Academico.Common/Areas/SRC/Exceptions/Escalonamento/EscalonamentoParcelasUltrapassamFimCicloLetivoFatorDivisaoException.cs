using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class EscalonamentoParcelasUltrapassamFimCicloLetivoFatorDivisaoException : SMCApplicationException
    {
        public EscalonamentoParcelasUltrapassamFimCicloLetivoFatorDivisaoException()
            : base(ExceptionsResource.ERR_EscalonamentoParcelasUltrapassamFimCicloLetivoFatorDivisao)
        {
        }
    }
}