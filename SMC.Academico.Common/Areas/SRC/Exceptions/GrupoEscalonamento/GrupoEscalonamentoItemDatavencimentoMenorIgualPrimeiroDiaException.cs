using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoItemDatavencimentoMenorIgualPrimeiroDiaException : SMCApplicationException
    {
        public GrupoEscalonamentoItemDatavencimentoMenorIgualPrimeiroDiaException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoItemDatavencimentoMenorIgualPrimeiroDiaException)
        {
        }
    }
}