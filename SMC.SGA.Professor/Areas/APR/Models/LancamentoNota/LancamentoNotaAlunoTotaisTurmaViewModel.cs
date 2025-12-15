using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Professor.Areas.APR.Models
{
    public class LancamentoNotaAlunoTotaisTurmaModel : SMCViewModelBase
    {
        public string SeqAlunoHistorico { get; set; }
        public string TotalParcial { get; set; }
        public short? TotalFinal { get; set; }
        public SituacaoHistoricoEscolar SituacaoFinal { get; set; }
        public string DescricaoSituacaoFinal { get; set; }
        public bool TodasApuracoesDivisaoLancadas { get; set; }
        public bool TodasApuracoesVazias { get; set; }
    }
}