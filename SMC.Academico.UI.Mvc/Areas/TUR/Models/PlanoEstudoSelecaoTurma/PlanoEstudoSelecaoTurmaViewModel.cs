using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Models
{
    public class PlanoEstudoSelecaoTurmaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqSolicitacaoMatricula { get; set; }

        [SMCHidden]
        public string backUrl { get; set; }

        [SMCHidden]
        public string CicloLetivoDescricao { get; set; }

        [SMCHidden]
        public int Ano { get; set; }

        [SMCHidden]
        public int Periodo { get; set; }

        public string DescricaoTurma { get; set; }

        [SMCHidden]
        public List<long?> SelectedValues { get; set; }

        [SMCHidden]
        public List<PlanoEstudoTurmaViewModel> TurmasOferta { get; set; }
    }
}
