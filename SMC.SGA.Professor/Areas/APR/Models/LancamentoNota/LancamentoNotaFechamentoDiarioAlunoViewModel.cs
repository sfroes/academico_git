using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Professor.Areas.APR.Models
{
    public class LancamentoNotaFechamentoDiarioAlunoViewModel : SMCViewModelBase
    {
        public long SeqAlunoHistorico { get; set; }

        public decimal? Nota { get; set; }

        public int Faltas { get; set; }

        public SituacaoHistoricoEscolar SituacaoHistoricoEscolar { get; set; }
    }
}