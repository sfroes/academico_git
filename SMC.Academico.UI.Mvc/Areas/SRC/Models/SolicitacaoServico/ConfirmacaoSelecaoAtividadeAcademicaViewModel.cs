using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class ConfirmacaoSelecaoAtividadeAcademicaViewModel : SelecaoAtividadeAcademicaViewModel
    {
        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.CONFIRMACAO_SELECAO_ATIVIDADE_ACADEMICA;

        [SMCHidden]
        public override string ChaveTextoBotaoProximo
        {
            get
            {
                //if (TokenServico == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU)
                //    return "Botao_Confirmar_Renovacao";
                //else 
                if (TokenServico == TOKEN_SERVICO.MATRICULA_REABERTURA)
                    return "Botao_Navegacao_Proximo";
                    //return "Botao_Confirmar_Reabertura";
                else
                    return "Botao_Navegacao_Proximo";
            }
        }
    }
}