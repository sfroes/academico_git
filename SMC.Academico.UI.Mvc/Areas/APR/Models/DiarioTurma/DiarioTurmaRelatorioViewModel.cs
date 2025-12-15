using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class DiarioTurmaRelatorioViewModel : SMCViewModelBase
    {
        public List<DiarioTurmaCabecalhoViewModel> Cabecalho { get; set; }
        public List<DiarioTurmaProfessorViewModel> Professores { get; set; }
        public List<DiarioTurmaAlunoViewModel> Alunos { get; set; }
    }
}
