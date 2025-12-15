using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class LancamentoNotaFechamentoDiarioAlunoData : ISMCMappable
    {
        public long SeqAlunoHistorico { get; set; }

        public decimal? Nota { get; set; }

        public int Faltas { get; set; }

        public SituacaoHistoricoEscolar SituacaoHistoricoEscolar { get; set; }
    }
}
