using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.UI.Mvc.Areas.MAT.Models;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Aluno.Areas.MAT.Models.Matricula;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.MAT.Models
{
    public class MontagemPlanoEstudosViewModel : MatriculaPaginaViewModelBase
    {
        public override string Token => MatriculaTokens.MONTAGEM_PLANO_ESTUDOS;

        [SMCHidden]
        public string backUrl { get; set; }

        [SMCHidden]
        public List<DivisaoTurmaSelecionadaItemViewModel> DivisaoTurmas { get; set; }
    }
}