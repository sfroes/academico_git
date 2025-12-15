using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class EscalonamentoEdicaoDataFimMaiorDataFimAnteriorException : SMCApplicationException
    {
        public EscalonamentoEdicaoDataFimMaiorDataFimAnteriorException(string grupos)
            : base(string.Format(ExceptionsResource.ERR_EscalonamentoEdicaoDataFimMaiorDataFimAnteriorException, grupos))
        {
        }
    }
}