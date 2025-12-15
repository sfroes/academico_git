using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoFinalizacaoNaoDataFimMaiorDataFimPosteriorException : SMCApplicationException
    {
        public GrupoEscalonamentoFinalizacaoNaoDataFimMaiorDataFimPosteriorException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoFinalizacaoNaoDataFimMaiorDataFimPosteriorException)
        {
        }
    }
}