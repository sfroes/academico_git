using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class TipoTermoIntercambioDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCSortable]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCMaxLength(50)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid13_24)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCSortable]
        public bool? PermiteAssociarAluno { get; set; }

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .EditInModal()
                .Tokens(tokenInsert: UC_ALN_004_02_01.TIPO_TERMO_INTERCAMBIO,
                        tokenEdit: UC_ALN_004_02_01.TIPO_TERMO_INTERCAMBIO,
                        tokenRemove: UC_ALN_004_02_01.TIPO_TERMO_INTERCAMBIO,
                        tokenList: UC_ALN_004_02_01.TIPO_TERMO_INTERCAMBIO);
        }

        #endregion Configurações
    }
}