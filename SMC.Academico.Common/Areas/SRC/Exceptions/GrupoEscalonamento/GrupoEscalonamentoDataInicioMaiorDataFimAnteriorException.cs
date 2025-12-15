using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoDataInicioMaiorDataFimAnteriorException : SMCApplicationException
    {
        public GrupoEscalonamentoDataInicioMaiorDataFimAnteriorException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoDataInicioMaiorDataFimAnterior)
        {
        }
    }
}