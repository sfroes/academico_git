using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoItemSomatorioPercentualInvalidoException : SMCApplicationException
    {
        public GrupoEscalonamentoItemSomatorioPercentualInvalidoException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoItemSomatorioPercentualInvalido)
        {
        }
    }
}