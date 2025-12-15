using SMC.Framework.UI.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.CNC.Models
{
    public class IntegralizacaoMatrizHistoricoEscolarViewModel : SMCViewModelBase
    {
        public string CodigoConfiguracao { get; set; }

        public string DescricaoConfiguracao { get; set; }

        public string SiglaComponente { get; set; }

        public bool ExibirAssunto { get; set; }

        public bool ExibirTurma { get; set; }

        public bool ExibirProfessores {
            get
            {
                if (HistoricosEmCurso != null && HistoricosEmCurso.Any(a => a.Professores != null))
                    return true;

                if (HistoricosSemApuracao != null && HistoricosSemApuracao.Any(a => a.Professores != null))
                    return true;

                if (HistoricosNotas != null && HistoricosNotas.Any(a => a.Professores != null))
                    return true;

                if (HistoricosDispensa != null && HistoricosDispensa.Any(a => a.Professores != null))
                    return true;

                return false;
            }
        }

        public List<IntegralizacaoMatrizHistoricoEmCursoViewModel> HistoricosEmCurso { get; set; }

        public List<IntegralizacaoMatrizHistoricoSemApuracaoViewModel> HistoricosSemApuracao { get; set; }

        public List<IntegralizacaoMatrizHistoricoEscolarNotaViewModel> HistoricosNotas { get; set; }

        public List<IntegralizacaoMatrizHistoricoEscolarDispensaViewModel> HistoricosDispensa { get; set; }
    }
}
