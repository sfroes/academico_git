using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.GRD.ValueObjects
{
    public class HistoricoDivisaoTurmaConfiguracaoGradeListarVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqDivisaoTurma { get; set; }

        public DateTime DataInicio { get; set; }

        public TipoDistribuicaoAula TipoDistribuicaoAula { get; set; }

        public TipoPulaFeriado TipoPulaFeriado { get; set; }

        public bool AulaSabado { get; set; }

        public TipoPagamentoAula TipoPagamentoAula { get; set; }
    }
}
