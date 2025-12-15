using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoItemParcelaDataVencimentoCicloLetivoException : SMCApplicationException
    {
        public GrupoEscalonamentoItemParcelaDataVencimentoCicloLetivoException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoItemParcelaDataVencimentoCicloLetivo)
        {
        }
    }
}