using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class ConfirmacaoSolicitacaoPadraoPaginaViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_PADRAO_SOLICITACAO_CONFIRMACAO;

        public override string ChaveTextoBotaoProximo => "Botao_Concluirprocesso";

        public string DescricaoOriginal { get; set; }
    }
}