using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Models
{
    public class HomeCursoViewModel : SMCViewModelBase
    {
        public string DescricaoCursoLocalidadeTurno { get; set; }

        public List<HomeTurmaViewModel> Turmas { get; set; }
    }
}