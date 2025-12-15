using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Academico.Common.Areas.TUR.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMC.Academico.Common.Areas.TUR.Enums;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TipoTurmaDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCReadOnly(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        [SMCSortable(true)]
        public override long Seq { get; set; }

        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCDescription]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid11_24)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCIgnoreProp(SMCViewMode.Filter)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        [SMCSelect]
        public AssociacaoOfertaMatriz AssociacaoOfertaMatriz { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .EditInModal()
                .Tokens(tokenInsert: UC_TUR_001_03_01.MANTER_TIPO_TURMA,
                           tokenEdit: UC_TUR_001_03_01.MANTER_TIPO_TURMA,
                           tokenRemove: UC_TUR_001_03_01.MANTER_TIPO_TURMA,
                           tokenList: UC_TUR_001_03_01.MANTER_TIPO_TURMA);
        }
    }
}