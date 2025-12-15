using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoAssociacaoPrazoReabrirSolicitacaoEncerradoException : SMCApplicationException
    {
        public GrupoEscalonamentoAssociacaoPrazoReabrirSolicitacaoEncerradoException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoAssociacaoPrazoReabrirSolicitacaoEncerradoException)
        {
        }
    }
}