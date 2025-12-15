using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class TipoTrabalhoDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCSortable(true)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid4_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid4_24, Framework.SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCOrder(1)]
        [SMCDescription]
        [SMCSortable(true, true)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(Framework.SMCSize.Grid20_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid20_24, Framework.SMCSize.Grid20_24)]
        public string Descricao { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Tokens(tokenInsert: UC_ORT_002_01_01.MANTER_TIPO_TRABALHO,
                           tokenEdit: UC_ORT_002_01_01.MANTER_TIPO_TRABALHO,
                           tokenRemove: UC_ORT_002_01_01.MANTER_TIPO_TRABALHO,
                           tokenList: UC_ORT_002_01_01.MANTER_TIPO_TRABALHO);
        }
    }
}