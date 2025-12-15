using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using SMC.SGA.Administrativo.Areas.ALN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class TipoVinculoAlunoDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public override long Seq { get; set; }

        [SMCMaxLength(100)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid13_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid13_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        [SMCSelect]
        public TipoVinculoAlunoFinanceiro TipoVinculoAlunoFinanceiro { get; set; }

        [SMCDetail(min: 1)]
        [SMCRequired]
        public SMCMasterDetailList<FormaIngressoViewModel> FormasIngresso { get; set; }

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .Detail<TipoVinculoAlunoListarDynamicModel>("_DetailList")
                .Tokens(tokenInsert: UC_ALN_001_03_02.MANTER_VINCULO,
                        tokenEdit: UC_ALN_001_03_02.MANTER_VINCULO,
                        tokenRemove: UC_ALN_001_03_02.MANTER_VINCULO,
                        tokenList: UC_ALN_001_03_01.PESQUISAR_VINCULO);
        }

        #endregion Configurações
    }
}