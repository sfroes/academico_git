using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.GRD.Data
{
    public class HistoricoDivisaoTurmaConfiguracaoGradeData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqDivisaoTurma { get; set; }

        public DateTime DataInicio { get; set; }

        public TipoDistribuicaoAula TipoDistribuicaoAula { get; set; }

        public TipoPulaFeriado TipoPulaFeriado { get; set; }

        public bool? AulaSabado { get; set; }

        public TipoPagamentoAula TipoPagamentoAula { get; set; }
    }
}
