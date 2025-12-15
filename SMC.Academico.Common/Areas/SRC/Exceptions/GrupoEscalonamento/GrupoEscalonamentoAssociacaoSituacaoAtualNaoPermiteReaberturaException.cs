using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoAssociacaoSituacaoAtualNaoPermiteReaberturaException : SMCApplicationException
    {
        public GrupoEscalonamentoAssociacaoSituacaoAtualNaoPermiteReaberturaException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoAssociacaoSituacaoAtualNaoPermiteReaberturaException)
        {
        }
    }
}