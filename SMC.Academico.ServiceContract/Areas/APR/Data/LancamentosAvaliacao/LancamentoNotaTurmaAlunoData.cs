using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class LancamentoNotaTurmaAlunoData : ISMCMappable
    {
        public long SeqAlunoHistorico { get; set; }

        public string TotalParcial { get; set; }

        public decimal? TotalFinal { get; set; }

        public SituacaoHistoricoEscolar? SituacaoHistoricoEscolar { get; set; }

        public bool TodasApuracoesDivisaoLancadas { get; set; }

        public bool TodasApuracoesVazias { get; set; }
    }
}
