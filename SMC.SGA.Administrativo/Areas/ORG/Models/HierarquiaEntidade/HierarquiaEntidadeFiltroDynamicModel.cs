using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class HierarquiaEntidadeFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter(true, true)]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public long? Seq { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCOrder(1)]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid11_24)]
        public string Descricao { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        [SMCMapForceFromTo]
        public Nullable<DateTime> DataInicioVigencia { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        [SMCMinDate("DataInicioVigencia")]
        public Nullable<DateTime> DataFimVigencia { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(4)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public bool? SomenteAtivas { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            SomenteAtivas = true;
        }
    }
}