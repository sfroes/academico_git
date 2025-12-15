using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Constants;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class TipoFuncionarioDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCSortable(true)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCMaxLength(100)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid20_24)]
        [SMCSortable(true, true)]
        public string DescricaoMasculino { get; set; }

        [SMCHidden(SMCViewMode.List)]
        //[SMCDescription]
        [SMCMaxLength(100)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid14_24)]
        public string DescricaoFeminino { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        public TipoRegistroProfissional? TipoRegistroProfissionalObrigatorio { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCMaxLength(100)]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid14_24)]
        public string Token { get; set; }

        [SMCSelect]
        [SMCRequired]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24)]
        public bool? ObrigatorioVinculoUnico { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .ModalSize(SMCModalWindowSize.Medium)
                   .Tokens(tokenInsert: UC_PES_006_01_01.MANTER_TIPO_VINCULO_FUNCIONARIO,
                           tokenEdit: UC_PES_006_01_01.MANTER_TIPO_VINCULO_FUNCIONARIO,
                           tokenRemove: UC_PES_006_01_01.MANTER_TIPO_VINCULO_FUNCIONARIO,
                           tokenList: UC_PES_006_01_01.MANTER_TIPO_VINCULO_FUNCIONARIO);
        }
    }
}