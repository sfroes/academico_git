using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class TipoAtoNormativoDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid2_24)]
        [SMCSortable(true)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCMaxLength(255)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid20_24)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        [SMCMaxLength(100)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(Framework.SMCSize.Grid10_24)]
        public string DescricaoXSD { get; set; }

        [SMCRequired]
        [SMCIgnoreProp(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid8_24)]
        public string Token { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCSortable(true)]
        public bool? Ativo { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .ModalSize(SMCModalWindowSize.Medium)
                   .Tokens(tokenInsert: UC_ORG_003_02_02.MANTER_TIPO_ATO_NORMATIVO,
                           tokenEdit: UC_ORG_003_02_02.MANTER_TIPO_ATO_NORMATIVO,
                           tokenRemove: UC_ORG_003_02_02.MANTER_TIPO_ATO_NORMATIVO,
                           tokenList: UC_ORG_003_02_01.PESQUISAR_TIPO_ATO_NORMATIVO);
        }
    }
}