using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class AssuntoNormativoDynamicModel : SMCDynamicViewModel
    {
        [SMCFilter(true, true)]
        [SMCKey]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid2_24)]
        [SMCSortable(true)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCMaxLength(255)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid11_24)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCIgnoreProp(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid11_24)]
        public string Token { get; set; }

        [SMCIgnoreProp(SMCViewMode.Filter)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCSortable(true)]
        public bool? HabilitaEmissaoDocumentoConclusao { get; set; }        
        
        [SMCFilter(true, true)]
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