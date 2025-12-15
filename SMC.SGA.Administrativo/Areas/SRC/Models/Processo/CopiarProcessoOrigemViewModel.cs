using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class CopiarProcessoOrigemViewModel : SMCViewModelBase
    {
        [SMCReadOnly]
        [SMCKey]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public long Seq { get; set; }

        [SMCReadOnly]
        [SMCDescription]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid20_24)]
        public string Descricao { get; set; }

        [SMCHidden]
        public long SeqServico { get; set; }

        [SMCReadOnly]        
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public string DescricaoCicloLetivo { get; set; }

        [SMCReadOnly]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public DateTime DataInicio { get; set; }

        [SMCReadOnly]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public DateTime? DataFim { get; set; }


        [SMCCurrency]
        [SMCReadOnly]
        [SMCConditionalDisplay(nameof(CopiarProcessoViewModel.TokenTipoServico), SMCConditionalOperation.Equals, TOKEN_TIPO_SERVICO.RENOVACAO_MATRICULA, RuleName = "R1")]
        [SMCConditionalDisplay(nameof(CopiarProcessoViewModel.TokenTipoServico), SMCConditionalOperation.Equals, TOKEN_TIPO_SERVICO.MATRICULA_REABERTURA, RuleName = "R2")]
        [SMCConditionalDisplay(nameof(CopiarProcessoViewModel.TokenServico), SMCConditionalOperation.Equals, TOKEN_SERVICO.PRORROGACAO_PRAZO_CONCLUSAO, RuleName = "R3")]
        [SMCConditionalRule("R1 || R2 || R3")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public decimal? ValorPercentualServicoAdicional { get; set; }
    }
}