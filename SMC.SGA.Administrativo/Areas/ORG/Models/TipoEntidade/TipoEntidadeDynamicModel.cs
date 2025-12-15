using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class TipoEntidadeDynamicModel : SMCDynamicViewModel
    {
        public TipoEntidadeDynamicModel()
        {
            this.EntidadeExternada = false;
        }

        [SMCKey]
        [SMCSortable(true)]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCSortable(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(Framework.SMCSize.Grid13_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid18_24, Framework.SMCSize.Grid9_24)]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCRequired]
        [SMCIgnoreProp(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCMaxLength(100)]
        [SMCSize(Framework.SMCSize.Grid7_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid6_24)]
        [SMCConditionalReadonly("EntidadeExternada", SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public string Token { get; set; }

        [SMCOrder(3)]
        [SMCIgnoreProp(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRadioButtonList]
        [SMCSize(Framework.SMCSize.Grid8_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid6_24)]
        public bool? PermiteAtoNormativo { get; set; }

        [SMCOrder(4)]
        [SMCIgnoreProp(SMCViewMode.Filter)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRadioButtonList]
        [SMCSize(Framework.SMCSize.Grid8_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid6_24)]
        public bool? EntidadeExternada { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Tokens(tokenInsert: UC_ORG_001_03_02.MANTER_NIVEL_ENSINO,
                           tokenEdit: UC_ORG_001_03_02.MANTER_NIVEL_ENSINO,
                           tokenRemove: UC_ORG_001_03_02.MANTER_NIVEL_ENSINO,
                           tokenList: UC_ORG_001_03_02.MANTER_NIVEL_ENSINO);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            if (viewMode == SMCViewMode.Insert)
                this.PermiteAtoNormativo = false;
        }

    }
}