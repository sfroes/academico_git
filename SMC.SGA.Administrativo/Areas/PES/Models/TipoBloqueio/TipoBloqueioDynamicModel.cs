using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class TipoBloqueioDynamicModel : SMCDynamicViewModel
    {

        [SMCKey]
        [SMCOrder(0)]
        [SMCSortable(true)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid8_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid6_24, Framework.SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCOrder(1)]
        [SMCDescription]
        [SMCSortable(true, true)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(Framework.SMCSize.Grid16_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid18_24, Framework.SMCSize.Grid14_24)]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCSortable(true)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(Framework.SMCSize.Grid8_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid6_24)]
        [SMCRegularExpression(REGEX.TOKEN)]
        public string Token { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Tokens(tokenInsert: UC_PES_004_01_01.MANTER_TIPO_BLOQUEIO,
                           tokenEdit: UC_PES_004_01_01.MANTER_TIPO_BLOQUEIO,
                           tokenRemove: UC_PES_004_01_01.MANTER_TIPO_BLOQUEIO,
                           tokenList: UC_PES_004_01_01.MANTER_TIPO_BLOQUEIO);
        }
    }
}