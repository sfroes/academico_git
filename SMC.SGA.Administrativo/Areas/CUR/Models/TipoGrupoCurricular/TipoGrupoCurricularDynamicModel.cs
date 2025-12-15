using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class TipoGrupoCurricularDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCFilter(true, true)]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSortable(true, true)]
        [SMCSize(SMCSize.Grid14_24)]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCDetail(min: 1)]
        public SMCMasterDetailList<TipoComponenteCurricularViewModel> TiposComponenteCurricular { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Tokens(tokenInsert: UC_CUR_001_04_01.MANTER_TIPO_GRUPO_CURRICULAR,
                           tokenEdit: UC_CUR_001_04_01.MANTER_TIPO_GRUPO_CURRICULAR,
                           tokenList: UC_CUR_001_04_01.MANTER_TIPO_GRUPO_CURRICULAR,
                           tokenRemove: UC_CUR_001_04_01.MANTER_TIPO_GRUPO_CURRICULAR);
        }
    }
}