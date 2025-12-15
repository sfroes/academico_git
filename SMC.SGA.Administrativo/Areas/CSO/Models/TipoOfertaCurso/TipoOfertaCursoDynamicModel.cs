using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class TipoOfertaCursoDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCFilter(true, true)]
        [SMCSortable(true)]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCSortable(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(Framework.SMCSize.Grid10_24)]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCRequired]
        [SMCIgnoreProp(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCMaxLength(100)]
        [SMCSize(Framework.SMCSize.Grid10_24)]
        public string Token { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Tokens(tokenInsert: UC_CSO_001_11_01.MANTER_TIPO_OFERTA_CURSO,
                           tokenEdit: UC_CSO_001_11_01.MANTER_TIPO_OFERTA_CURSO,
                           tokenRemove: UC_CSO_001_11_01.MANTER_TIPO_OFERTA_CURSO,
                           tokenList: UC_CSO_001_11_01.MANTER_TIPO_OFERTA_CURSO);
        }
    }
}