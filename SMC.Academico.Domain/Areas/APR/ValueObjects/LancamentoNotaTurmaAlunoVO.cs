using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class LancamentoNotaTurmaAlunoVO : ISMCMappable
    {
        public long SeqAlunoHistorico { get; set; }

        public string TotalParcial { get; set; }

        public decimal? TotalFinal { get; set; }

        public SituacaoHistoricoEscolar? SituacaoHistoricoEscolar { get; set; }

        public bool TodasApuracoesDivisaoLancadas { get; set; }

        public bool TodasApuracoesVazias { get; set; }
    }
}
