using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class TiposAutorizacaoBdpViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqPrograma { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCUnique]
        public TipoAutorizacao TipoAutorizacao { get; set; }

        [SMCMinValue(1)]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCConditionalRequired(nameof(TipoAutorizacao), SMCConditionalOperation.Equals, new object[] { TipoAutorizacao.Parcial })]
        [SMCConditionalReadonly(nameof(TipoAutorizacao), SMCConditionalOperation.NotEqual, new object[] { TipoAutorizacao.Parcial })]
        public short? NumeroDiasDuracaoAutorizacao { get; set; }
    }
}