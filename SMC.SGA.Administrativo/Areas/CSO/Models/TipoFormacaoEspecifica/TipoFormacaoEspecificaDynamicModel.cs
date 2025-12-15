using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CSO.Controllers;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class TipoFormacaoEspecificaDynamicModel : SMCDynamicViewModel
    {
        #region [ DadosGerais ]

        [SMCGroupedProperty("DadosGerais")]
        [SMCKey]
        [SMCOrder(0)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCSortable(true)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCGroupedProperty("DadosGerais")]
        [SMCMaxLength(100)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid20_24)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        [SMCGroupedProperty("DadosGerais")]
        [SMCMinLength(3)]
        [SMCMaxLength(255)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid18_24)]
        [SMCRegularExpression(REGEX.TOKEN)]
        public string Token { get; set; }

        [SMCGroupedProperty("DadosGerais")]
        [SMCMapForceFromTo]
        [SMCOrder(3)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCSortable(true)]
        public bool Ativo { get; set; }

        #endregion [ DadosGerais ]

        #region [ Parametros ]

        [SMCGroupedProperty("Parametros")]
        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCOrder(4)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public ClasseTipoFormacao? ClasseTipoFormacao { get; set; }

        [SMCGroupedProperty("Parametros")]
        [SMCHorizontalAlignment(SMCHorizontalAlignment.Left)]
        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCOrder(5)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public bool? ExigeGrau { get; set; }

        [SMCGroupedProperty("Parametros")]
        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCOrder(6)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        [SMCConditional(SMCConditionalBehavior.ReadOnly, nameof(ExigeGrau), SMCConditionalOperation.Equals, true)]
        [SMCConditional(SMCConditionalBehavior.Required, nameof(ExigeGrau), SMCConditionalOperation.Equals, false)]
        [SMCDependency(nameof(ExigeGrau), nameof(TipoFormacaoEspecificaController.SetaValorPermiteTitulacao), "TipoFormacaoEspecifica", false)]
        public bool? PermiteTitulacao { get; set; }

        [SMCGroupedProperty("Parametros")]
        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCOrder(7)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid7_24)]
        [SMCConditional(SMCConditionalBehavior.Visibility | SMCConditionalBehavior.Required, nameof(ExigeGrau), SMCConditionalOperation.Equals, true)]
        [SMCDependency(nameof(ExigeGrau), nameof(TipoFormacaoEspecificaController.SetaValorGrauDescricaoFormacao), "TipoFormacaoEspecifica", false)]
        public bool? ExibeGrauDescricaoFormacao { get; set; }

        [SMCGroupedProperty("Parametros")]
        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCMapForceFromTo]
        [SMCOrder(8)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid9_24)]
        public bool? PermiteEmitirDocumentoConclusao { get; set; }

        [SMCGroupedProperty("Parametros")]
        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCMapForceFromTo]
        [SMCOrder(9)]
        [SMCRadioButtonList]
        [SMCConditionalDisplay(nameof(PermiteEmitirDocumentoConclusao), true)]
        [SMCConditionalRequired(nameof(PermiteEmitirDocumentoConclusao), true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        public bool? GeraCarimbo { get; set; }

        #endregion [ Parametros ]

        #region [ Tipo de curso ]

        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCOrder(9)]
        [SMCDetail(SMCDetailType.Tabular, min: 1)]
        public SMCMasterDetailList<TipoFormacaoEspecificaTipoCursoViewModel> TiposCurso { get; set; }

        #endregion [ Tipo de curso ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Service<ITipoFormacaoEspecificaService>(edit: nameof(ITipoFormacaoEspecificaService.BuscarTipoFormacaoEspecifica),
                                                            save: nameof(ITipoFormacaoEspecificaService.Salvar))
                   .Javascript("Areas/CSO/TipoFormacaoEspecifica/TipoFormacaoEspecifica")
                   .EditInModal()
                   .ModalSize(SMCModalWindowSize.Large)
                   .Tokens(tokenInsert: UC_CSO_001_08_01.MANTER_TIPO_FORMACAO_ESPECIFICA,
                           tokenEdit: UC_CSO_001_08_01.MANTER_TIPO_FORMACAO_ESPECIFICA,
                           tokenRemove: UC_CSO_001_08_01.MANTER_TIPO_FORMACAO_ESPECIFICA,
                           tokenList: UC_CSO_001_08_01.MANTER_TIPO_FORMACAO_ESPECIFICA);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);

            if (viewMode == SMCViewMode.Insert)
                Ativo = true;
        }
    }
}