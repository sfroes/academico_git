using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Formularios.Common.Areas.FRM.Enums;
using SMC.Formularios.UI.Mvc.Attributes;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class FormularioSolicitacaoPadraoPaginaViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_PADRAO_SOLICITACAO_FORMULARIO;

        [SMCSGF(RenderMode = FormaExibicaoSecao.Nenhum)]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public FormularioPadraoDadoFormularioViewModel DadoFormulario { get; set; }
    }
}