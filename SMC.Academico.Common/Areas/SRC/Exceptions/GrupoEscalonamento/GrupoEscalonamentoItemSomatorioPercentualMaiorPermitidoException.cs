using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoItemSomatorioPercentualMaiorPermitidoException : SMCApplicationException
    {
        public GrupoEscalonamentoItemSomatorioPercentualMaiorPermitidoException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoItemSomatorioPercentualMaiorPermitidoException)
        {
        }
    }
}