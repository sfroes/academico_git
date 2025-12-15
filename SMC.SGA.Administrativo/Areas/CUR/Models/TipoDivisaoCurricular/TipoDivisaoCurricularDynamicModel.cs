using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class TipoDivisaoCurricularDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCFilter(true, true)]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSortable(true)]
        [SMCSize(SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSortable(true, true)]
        [SMCSize(Framework.SMCSize.Grid14_24)]
        public string Descricao { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Tokens(tokenInsert: UC_CUR_001_03_01.MANTER_TIPO_DIVISAO_CURRICULAR,
                           tokenEdit: UC_CUR_001_03_01.MANTER_TIPO_DIVISAO_CURRICULAR,
                           tokenRemove: UC_CUR_001_03_01.MANTER_TIPO_DIVISAO_CURRICULAR,
                           tokenList: UC_CUR_001_02_01.PESQUISAR_DIVISAO_CURRICULAR);
        }
    }
}