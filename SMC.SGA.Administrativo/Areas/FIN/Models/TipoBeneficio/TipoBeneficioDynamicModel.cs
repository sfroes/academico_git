using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class TipoBeneficioDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCReadOnly(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCFilter]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCSortable(true)]
        public override long Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCDescription]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid12_24)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        [SMCFilter(true, true)]
        [SMCRequired]
        [SMCSelect(autoSelectSingleItem: true)]
        [SMCSortable(true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        public ClassificacaoBeneficio ClassificacaoBeneficio { get; set; }

        [SMCRequired]
        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid5_24)]
        public bool ChancelaSetorBolsas { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .EditInModal()
                .Tokens(tokenInsert: UC_FIN_001_01_01.MANTER_TIPO_BENEFICIO,
                           tokenEdit: UC_FIN_001_01_01.MANTER_TIPO_BENEFICIO,
                           tokenRemove: UC_FIN_001_01_01.MANTER_TIPO_BENEFICIO,
                           tokenList: UC_FIN_001_01_01.MANTER_TIPO_BENEFICIO);
        }
    }
}