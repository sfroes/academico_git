using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class CopiarProcessoDetalheEtapaViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public string DescricaoEtapa { get; set; }

        [SMCHidden]
        public long SeqEtapaSgf { get; set; }

        [SMCSelect]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid2_24, SMCSize.Grid2_24)]
        public bool Obrigatoria { get; set; }

        [SMCSelect]
        [SMCRequired]
        [SMCConditionalReadonly(nameof(Obrigatoria), true, PersistentValue = true)]
        [SMCDependency(nameof(Obrigatoria), nameof(ProcessoController.PreencherCampoAssociar), "Processo", true)]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid2_24, SMCSize.Grid2_24)]
        public bool? Associar { get; set; }

        [SMCConditionalReadonly(nameof(Obrigatoria), true, PersistentValue = true)]
        [SMCSelect]
        [SMCRequired]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        public bool CopiarConfiguracoes { get; set; }

        [SMCSelect]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public TipoPrazoEtapa TipoPrazoEtapa { get; set; }

        [SMCConditionalDisplay(nameof(TipoPrazoEtapa), SMCConditionalOperation.Equals, TipoPrazoEtapa.DiasUteis, RuleName = "CD1")]
        [SMCConditionalDisplay(nameof(TipoPrazoEtapa), SMCConditionalOperation.Equals, TipoPrazoEtapa.DiasCorridos, RuleName = "CD2")]
        [SMCConditionalRule("CD1 || CD2")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public short? NumeroDiasPrazoEtapa { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCConditionalDisplay(nameof(TipoPrazoEtapa), SMCConditionalOperation.Equals, TipoPrazoEtapa.PeriodoVigencia)]
        [SMCConditionalRequired(nameof(TipoPrazoEtapa), SMCConditionalOperation.Equals, TipoPrazoEtapa.PeriodoVigencia)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public DateTime? DataInicio { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCConditionalDisplay(nameof(TipoPrazoEtapa), SMCConditionalOperation.Equals, TipoPrazoEtapa.PeriodoVigencia)]
        [SMCConditionalRequired(nameof(TipoPrazoEtapa), SMCConditionalOperation.Equals, TipoPrazoEtapa.PeriodoVigencia)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public DateTime? DataFim { get; set; }

        [SMCSelect]
        [SMCReadOnly]
        [SMCHidden]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public SituacaoEtapa SituacaoEtapa { get; set; }
    }
}