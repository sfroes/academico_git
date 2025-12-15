using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoFinalizacaoNaoDataFimMenorDataFimAnteriorException : SMCApplicationException
    {
        public GrupoEscalonamentoFinalizacaoNaoDataFimMenorDataFimAnteriorException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoFinalizacaoNaoDataFimMenorDataFimAnteriorException)
        {
        }
    }
}