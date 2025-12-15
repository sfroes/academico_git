using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoItemParcelaUnicaSemMotivoBloqueioException : SMCApplicationException
    {
        public GrupoEscalonamentoItemParcelaUnicaSemMotivoBloqueioException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoItemParcelaUnicaSemMotivoBloqueio)
        {
        }
    }
}