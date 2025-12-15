using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoEscalonamentoParcelasUltrapassamFimCicloLetivoException : SMCApplicationException
    {
        public GrupoEscalonamentoEscalonamentoParcelasUltrapassamFimCicloLetivoException(int qtdeMesesCicloLetivo)
            : base(string.Format(ExceptionsResource.ERR_GrupoEscalonamentoEscalonamentoParcelasUltrapassamFimCicloLetivo, qtdeMesesCicloLetivo))
        {
        }
    }
}
