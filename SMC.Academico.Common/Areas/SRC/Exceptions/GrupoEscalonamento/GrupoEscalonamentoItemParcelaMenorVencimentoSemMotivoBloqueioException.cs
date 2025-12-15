using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoItemParcelaMenorVencimentoSemMotivoBloqueioException : SMCApplicationException
    {
        public GrupoEscalonamentoItemParcelaMenorVencimentoSemMotivoBloqueioException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoItemParcelaMenorVencimentoSemMotivoBloqueio)
        {
        }
    }
}