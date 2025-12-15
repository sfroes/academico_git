using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoFinalizacaoNaoDataInicioMaiorDataInicioPosteriorException : SMCApplicationException
    {
        public GrupoEscalonamentoFinalizacaoNaoDataInicioMaiorDataInicioPosteriorException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoFinalizacaoNaoDataInicioMaiorDataInicioPosteriorException)
        {
        }
    }
}