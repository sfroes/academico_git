using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoItemParcelaMotivoBloqueioObrigatorioException : SMCApplicationException
    {
        public GrupoEscalonamentoItemParcelaMotivoBloqueioObrigatorioException(string motivosBloqueio)
            : base(string.Format(ExceptionsResource.ERR_GrupoEscalonamentoItemParcelaMotivoBloqueioObrigatorio, motivosBloqueio))
        {
        }
    }
}