using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class RegimeLetivoDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCFilter(true, true)]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCSortable(true)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCMaxLength(100)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid14_24)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCMinValue(1)]
        [SMCMaxValue(365)]
        [SMCMask("999")]
        [SMCSize(SMCSize.Grid7_24)]
        public short? NumeroItensAno { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Service<IRegimeLetivoService>(save: nameof(IRegimeLetivoService.SalvarRegimeLetivo))
                   .Tokens(tokenInsert: UC_CAM_002_02_01.MANTER_REGIME_LETIVO,
                           tokenEdit: UC_CAM_002_02_01.MANTER_REGIME_LETIVO,
                           tokenRemove: UC_CAM_002_02_01.MANTER_REGIME_LETIVO,
                           tokenList: UC_CAM_002_02_01.MANTER_REGIME_LETIVO);
        }
    }
}