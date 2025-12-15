using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class AlunosNotaViewModel : SMCViewModelBase, ISMCMappable
    {
        public long SeqAlunoHistorico { get; set; }
        public string Nome { get; set; }
        public string RA { get; set; }
        public List<ApuracoesNotaViewModel> Apuracoes { get; set; }
        public decimal? TotalDT { get; set; }
        public short? Faltas { get; set; }
        public bool ExibirFaltas { get; set; }
        public decimal? TotalParcial { get; set; }
        public decimal? TotalFinal { get; set; }
        public int TotalFaltas { get; set; }
        public decimal? TotalNotasAvaliacao { get; set; }
        public string Situacao { get; set; }
        public bool FezTodasAvaliacoes { get; set; }
        public bool AptoCalcularSituacaoFinal { get; set; }
        public string SituacaoHistoricoEscolcar { get; set; }

    }
}