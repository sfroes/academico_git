using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.GRD.Models
{
    public class HistoricoDivisaoTurmaConfiguracaoGradeListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqDivisaoTurma { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public DateTime DataInicio { get; set; }

        public TipoDistribuicaoAula TipoDistribuicaoAula { get; set; }

        public TipoPagamentoAula TipoPagamentoAula { get; set; }

        public TipoPulaFeriado TipoPulaFeriado { get; set; }

        public bool AulaSabado { get; set; }   
    }
}