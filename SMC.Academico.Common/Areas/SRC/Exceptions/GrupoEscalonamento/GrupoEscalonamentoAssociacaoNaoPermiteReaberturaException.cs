using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoAssociacaoNaoPermiteReaberturaException : SMCApplicationException
    {
        public GrupoEscalonamentoAssociacaoNaoPermiteReaberturaException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoAssociacaoNaoPermiteReaberturaException)
        {
        }
    }
}