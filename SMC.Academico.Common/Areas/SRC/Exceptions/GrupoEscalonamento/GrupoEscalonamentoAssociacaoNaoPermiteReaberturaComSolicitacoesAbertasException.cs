using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoAssociacaoNaoPermiteReaberturaComSolicitacoesAbertasException : SMCApplicationException
    {
        public GrupoEscalonamentoAssociacaoNaoPermiteReaberturaComSolicitacoesAbertasException(string descricao)
            : base(string.Format(ExceptionsResource.ERR_GrupoEscalonamentoAssociacaoNaoPermiteReaberturaComSolicitacoesAbertasException, descricao))
        {
        }
    }
}