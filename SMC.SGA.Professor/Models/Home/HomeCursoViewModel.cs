using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Professor.Models
{
    public class HomeCursoViewModel : SMCViewModelBase
    {
        public string DescricaoCursoLocalidadeTurno { get; set; }

        public List<HomeTurmaViewModel> Turmas { get; set; }
    }
}