using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Professor.Areas.APR.Models
{
    public class LancamentoNotaAlunoTotaisDivisaoTurmaViewModel : SMCViewModelBase
    {
        public string SeqAlunoHistorico { get; set; }
        public decimal? Total { get; set; }
    }
}