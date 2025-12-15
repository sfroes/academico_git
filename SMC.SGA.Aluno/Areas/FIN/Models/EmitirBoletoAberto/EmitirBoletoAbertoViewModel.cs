using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.FIN.Models
{
    public class EmitirBoletoAbertoViewModel : SMCViewModelBase
    {
        public List<EmitirBoletoAbertoCursoViewModel> Cursos { get; set; }
    }
}