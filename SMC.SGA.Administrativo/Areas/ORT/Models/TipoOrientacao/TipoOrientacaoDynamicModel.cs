using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class TipoOrientacaoDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCFilter(true, true)]
        [SMCSortable(true)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCSortable(true, true)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid9_24)]
        public string Token { get; set; }

        [SMCRequired]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public bool TrabalhoConclusaoCurso { get; set; }

        [SMCRequired]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public bool PermiteManutencaoManual { get; set; }

        [SMCRequired]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public bool PermiteInclusaoManual { get; set; }

        [SMCRequired]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public bool OrientacaoTurma { get; set; }

        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid9_24)]
        [SMCMinValue(0)]
        [SMCMask("9999")]
        public short? NumeroPrioridadeChancelaMatricula { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   //.ModalSize(SMCModalWindowSize.Small, SMCModalWindowSize.Small, SMCModalWindowSize.Small)
                   .Tokens(tokenInsert: UC_ORT_001_01_01.MANTER_TIPO_ORIENTACAO,
                           tokenEdit: UC_ORT_001_01_01.MANTER_TIPO_ORIENTACAO,
                           tokenRemove: UC_ORT_001_01_01.MANTER_TIPO_ORIENTACAO,
                           tokenList: UC_ORT_001_01_01.MANTER_TIPO_ORIENTACAO);
        }
    }
}