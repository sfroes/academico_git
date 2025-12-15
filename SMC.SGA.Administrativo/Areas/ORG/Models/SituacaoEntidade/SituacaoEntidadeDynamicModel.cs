using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class SituacaoEntidadeDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]        
        [SMCSortable(true)]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCDescription]       
        [SMCSortable(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(Framework.SMCSize.Grid14_24)]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCRequired]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSelect(SortBy =SMCSortBy.Value)]
        [SMCSize(SMCSize.Grid6_24)]
        public CategoriaAtividade CategoriaAtividade { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Tokens(tokenInsert: UC_ORG_001_08_01.MANTER_SITUACAO,
                           tokenEdit: UC_ORG_001_08_01.MANTER_SITUACAO,
                           tokenRemove: UC_ORG_001_08_01.MANTER_SITUACAO,
                           tokenList: UC_ORG_001_08_01.MANTER_SITUACAO);
        }
    }
}