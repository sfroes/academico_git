using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoFinalizacaoSimDataInicioMaiorDataFimAnteriorException : SMCApplicationException
    {
        public GrupoEscalonamentoFinalizacaoSimDataInicioMaiorDataFimAnteriorException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoFinalizacaoSimDataInicioMaiorDataFimAnteriorException)
        {
        }
    }
}