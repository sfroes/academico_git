using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class DiarioTurmaAlunoViewModel : SMCViewModelBase
    {
        public long NumeroRegistroAcademico { get; set; }
        public string NomeAluno { get; set; }
        public int? Nota { get; set; }
        public string DescricaoEscalaApuracaoItem { get; set; }
        public int? Faltas { get; set; }
        public string DescricaoSituacaoHistoricoEscolar { get; set; }
    }
}
