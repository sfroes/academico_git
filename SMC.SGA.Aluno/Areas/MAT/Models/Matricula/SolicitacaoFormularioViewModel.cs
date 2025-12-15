using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;

namespace SMC.SGA.Aluno.Areas.MAT.Models.Matricula
{
    public class SolicitacaoFormularioViewModel : MatriculaPaginaViewModelBase
    {
        public override string Token => MatriculaTokens.SOLICITACAO_FORMULARIO;

        public FormularioPadraoDadoFormularioViewModel DadoFormulario { get; set; }
    }
}