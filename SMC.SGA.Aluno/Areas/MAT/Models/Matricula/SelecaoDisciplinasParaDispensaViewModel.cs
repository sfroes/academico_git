using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.SGA.Aluno.Areas.MAT.Models.Matricula;

namespace SMC.SGA.Aluno.Areas.MAT.Models
{
    public class SelecaoDisciplinasParaDispensaViewModel : MatriculaPaginaViewModelBase
    {
        public override string Token => MatriculaTokens.SOLICITACAO_DISPENSA_DISCIPLINA;

    }
}