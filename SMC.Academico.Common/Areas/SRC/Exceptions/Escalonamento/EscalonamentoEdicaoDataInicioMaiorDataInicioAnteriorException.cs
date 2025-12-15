using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class EscalonamentoEdicaoDataInicioMaiorDataInicioAnteriorException : SMCApplicationException
    {
        public EscalonamentoEdicaoDataInicioMaiorDataInicioAnteriorException(string grupos)
            : base(string.Format(ExceptionsResource.ERR_EscalonamentoEdicaoDataInicioMaiorDataInicioAnteriorException, grupos))
        {
        }
    }
}