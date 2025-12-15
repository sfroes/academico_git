using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class LancamentoNotaFechamentoDiarioAlunoVO : ISMCMappable
    {
        public long SeqAlunoHistorico { get; set; }

        public decimal? Nota { get; set; }

        public int Faltas { get; set; }

        public SituacaoHistoricoEscolar SituacaoHistoricoEscolar { get; set; }
    }
}
