using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class AtendimentoParecerViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.ATENDIMENTO_PADRAO_ATENDIMENTO_PARECER;

        [SMCHidden]
        public override string ChaveTextoBotaoProximo => "Botao_Concluiratendimento";

        [SMCSelect]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid6_24)]
        public bool? Situacao { get; set; }

        [SMCMultiline]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string Parecer { get; set; }
    }
}