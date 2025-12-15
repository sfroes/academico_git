using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CNC.Controllers;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class TipoApostilamentoDynamicModel : SMCDynamicViewModel
    {
        #region Campos Auxiliares

        [SMCHidden]
        public bool RetornarInstituicaoEnsinoLogada { get; set; } = true;

        #endregion

        [SMCKey]
        [SMCFilter(true, true)]
        [SMCSortable(true)]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(RetornarInstituicaoEnsinoLogada), nameof(TipoApostilamentoController.BuscarInstituicaoLogada), "TipoApostilamento", false)]
        public long? SeqInstituicaoEnsino { get; set; }

        [SMCDescription]
        [SMCMaxLength(255)]
        [SMCFilter(true, true)]
        [SMCSortable(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid14_24)]
        public string Descricao { get; set; }

        [SMCMinLength(3)]
        [SMCMaxLength(255)]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCOrder(2)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public string Token { get; set; }

        [SMCRadioButtonList]
        [SMCOrder(3)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24)]
        public bool Ativo { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Tokens(tokenInsert: UC_CNC_004_03_01.MANTER_TIPO_APOSTILAMENTO,
                           tokenEdit: UC_CNC_004_03_01.MANTER_TIPO_APOSTILAMENTO,
                           tokenRemove: UC_CNC_004_03_01.MANTER_TIPO_APOSTILAMENTO,
                           tokenList: UC_CNC_004_03_01.MANTER_TIPO_APOSTILAMENTO);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);

            if (viewMode == SMCViewMode.Insert)
                Ativo = true;
        }
    }
}