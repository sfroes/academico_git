using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class ModalidadeDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCFilter(true, true)]
        [SMCSortable(true)]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCSortable(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(Framework.SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCMaxLength(100)]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCSize(Framework.SMCSize.Grid10_24)]
        public string DescricaoXSD { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Tokens(tokenInsert: UC_CSO_001_05_01.MANTER_MODALIDADE,
                           tokenEdit: UC_CSO_001_05_01.MANTER_MODALIDADE,
                           tokenRemove: UC_CSO_001_05_01.MANTER_MODALIDADE,
                           tokenList: UC_CSO_001_05_01.MANTER_MODALIDADE);
        }
    }
}