using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CUR.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    [SMCStepConfiguration(UseOnTabs = false)]
    [SMCStepConfiguration(ActionStep = nameof(InstituicaoNivelTipoComponenteCurricularController.StepTipoDivisaoComponente))]
    [SMCStepConfiguration]
    public class InstituicaoNivelTipoComponenteCurricularDynamicModel : SMCDynamicViewModel, ISMCWizardViewModel, ISMCStatefulView
    {
        #region [ DataSource ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCDataSource("TipoComponenteCurricular")]
        [SMCServiceReference(typeof(ICURDynamicService))]
        public List<SMCDatasourceItem> TiposComponenteCurricular { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> TiposDivisao { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoTrabalhoService), nameof(ITipoTrabalhoService.BuscarTiposTrabalhoInstituicaoNivelEnsinoSelect), values: new string[] { nameof(SeqInstituicaoNivel) })]
        public List<SMCDatasourceItem> TiposTrabalho { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeService), nameof(IInstituicaoTipoEntidadeService.BuscarTipoEntidadesDaInstituicaoSelect))]
        public List<SMCDatasourceItem> TiposEntidade { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelCriterioAprovacaoService), nameof(IInstituicaoNivelCriterioAprovacaoService.BuscarCriteriosAprovacaoDaInstituicaoNivelSelect), values: new string[] { nameof(SeqInstituicaoNivel) })]
        public List<SMCDatasourceItem> CriteriosAprovacaoPadrao { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelCalendarioService), nameof(IInstituicaoNivelCalendarioService.BuscarTiposEventosCalendarioInstituicaoNivel), values: new string[] { nameof(SeqInstituicaoNivel) })]
        public List<SMCDatasourceItem> TiposEventoAGD { get; set; }

        #endregion [ DataSource ]

        #region [ Tipo de Componente ]

        [SMCIgnoreProp]
        public int Step { get; set; }

        [SMCHidden]
        [SMCKey]
        [SMCOrder(0)]
        [SMCRequired]
        [SMCStep(0)]
        public override long Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(1)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSelect(nameof(InstituicaoNiveis))]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCStep(0, 0)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCOrder(1)]
        [SMCSortable(true, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        public string DescricaoInstituicaoNivel { get; set; }

        [SMCHidden]
        [SMCStep(2, 1)]
        [SMCInclude("InstituicaoNivel")]
        [SMCMapProperty("InstituicaoNivel.PermiteCreditoComponenteCurricular")]
        public bool InstituicaoNivelExigeCredito { get; set; }

        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(2)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSelect(nameof(TiposComponenteCurricular), "Seq", "Descricao", SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCStep(0, 0)]
        public long SeqTipoComponenteCurricular { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("TipoComponenteCurricular")]
        [SMCMapProperty("TipoComponenteCurricular.Descricao")]
        [SMCOrder(2)]
        [SMCSortable(true, true, "TipoComponenteCurricular.Descricao")]
        public string DescricaoTipoComponenteCurricular { get; set; }

        #endregion [ Tipo de Componente ]

        #region [ Tipo Divisão e Entidade ]

        [SMCDetail(SMCDetailType.Tabular, min: 1)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCMapForceFromTo]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(1, 0)]
        [SMCInclude("TiposDivisaoComponente.TipoDivisaoComponente")]
        public SMCMasterDetailList<InstituicaoNivelTipoDivisaoComponenteViewModel> TiposDivisaoComponente { get; set; }

        [SMCGroupedProperty("Entidade")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(4)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid15_24, SMCSize.Grid8_24)]
        [SMCStep(1, 0)]
        public bool EntidadeResponsavelObrigatoria { get; set; }

        [SMCGroupedProperty("Entidade")]
        [SMCConditionalRequired(nameof(EntidadeResponsavelObrigatoria), SMCConditionalOperation.Equals, true)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCMask("999")]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid5_24)]
        [SMCStep(1, 0)]
        public short? QuantidadeMaximaEntidadeResponsavel { get; set; }

        [SMCGroupedProperty("Entidade")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(6)]
        [SMCSelect("TiposEntidade")]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid11_24)]
        [SMCStep(1, 0)]
        public long? TipoEntidadeResponsavel { get; set; }

        #endregion [ Tipo Divisão e Entidade ]

        #region [ Parâmetros ]

        [SMCGroupedProperty("Parametro")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(7)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        [SMCStep(2, 1)]
        public bool PermiteConfiguracaoComponente { get; set; }

        [SMCGroupedProperty("Parametro")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(8)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        [SMCStep(2, 1)]
        public bool PermiteCadastroRequisito { get; set; }

        [SMCGroupedProperty("Parametro")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(9)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        [SMCStep(2, 1)]
        public bool PermiteCadastroDispensa { get; set; }

        [SMCGroupedProperty("Parametro")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(10)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        [SMCStep(2, 1)]
        public bool PermiteAssuntoComponente { get; set; }

        [SMCGroupedProperty("Parametro")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(11)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        [SMCStep(2, 1)]
        public bool PermiteSubdivisaoOrganizacao { get; set; }

        [SMCGroupedProperty("Parametro")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(12)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        [SMCStep(2, 1)]
        public bool NomeReduzidoObrigatorio { get; set; }

        [SMCGroupedProperty("Parametro")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(13)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        [SMCStep(2, 1)]
        public bool SiglaObrigatoria { get; set; }

        [SMCGroupedProperty("Parametro")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(14)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        [SMCStep(2, 1)]
        public bool QuantidadeVagasPrevistasObrigatorio { get; set; }

        [SMCGroupedProperty("Parametro")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(15)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        [SMCStep(2, 1)]
        public bool CriterioAprovacaoObrigatorio { get; set; }

        [SMCOrder(16)]
        [SMCGroupedProperty("Parametro")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSelect(nameof(CriteriosAprovacaoPadrao))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        [SMCStep(2, 1)]
        public long? SeqCriterioAprovacaoPadrao { get; set; }

        [SMCGroupedProperty("Ementa")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(17)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid10_24)]
        [SMCStep(2, 1)]
        public bool PermiteEmenta { get; set; }

        [SMCConditionalRequired(nameof(PermiteEmenta), SMCConditionalOperation.Equals, true)]
        [SMCGroupedProperty("Ementa")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCMask("99999")]
        [SMCMaxValue(32000)]
        [SMCMinValue(20)]
        [SMCOrder(18)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid6_24)]
        [SMCStep(2, 1)]
        public short? QuantidadeMinimaCaracteresEmenta { get; set; }

        [SMCConditionalRequired(nameof(ExibeCargaHoraria), SMCConditionalOperation.Equals, true)]
        [SMCConditionalReadonly(nameof(ExibeCargaHoraria), SMCConditionalOperation.Equals, false)]
        [SMCGroupedProperty("Carga")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(19)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid10_24)]
        [SMCStep(2, 1)]
        public bool ExigeCargaHoraria { get; set; }

        [SMCConditionalRequired(nameof(ExibeCredito), SMCConditionalOperation.Equals, true)]
        [SMCConditionalReadonly(nameof(ExibeCredito), SMCConditionalOperation.Equals, false)]
        [SMCGroupedProperty("Carga")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(20)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        [SMCStep(2, 1)]
        [SMCConditionalDisplay(nameof(InstituicaoNivelExigeCredito), SMCConditionalOperation.Equals, true)]
        public bool ExigeCredito { get; set; }

        [SMCConditionalRequired(nameof(ExibeCargaHoraria), SMCConditionalOperation.Equals, true)]
        [SMCConditionalReadonly(nameof(ExibeCargaHoraria), SMCConditionalOperation.Equals, false)]
        [SMCGroupedProperty("Carga")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(21)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid10_24)]
        [SMCStep(2, 1)]
        public FormatoCargaHoraria FormatoCargaHoraria { get; set; }

        [SMCConditionalRequired(nameof(ExibeCargaHoraria), SMCConditionalOperation.Equals, true, RuleName = "R1")]
        [SMCConditionalRequired(nameof(ExibeCredito), SMCConditionalOperation.Equals, true, RuleName = "R2")]
        [SMCConditionalRule("R1 && R2")]
        [SMCGroupedProperty("Carga")]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCMask("999")]
        [SMCOrder(22)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid6_24)]
        [SMCStep(2, 1)]
        public short? QuantidadeHorasPorCredito { get; set; }

        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid10_24)]
        [SMCOrder(23)]
        [SMCRequired]
        [SMCStep(2, 1)]
        [SMCRadioButtonList]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCGroupedProperty("Parametro")]
        public bool ExibeCargaHoraria { get; set; }

        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        [SMCOrder(24)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRequired]
        [SMCStep(2, 1)]
        [SMCRadioButtonList]
        [SMCGroupedProperty("Parametro")]
        public bool ExibeCredito { get; set; }

        #endregion [ Parâmetros ]

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .CssClass(insert: "smc-sga-wizard-tipo-comp-curricular", edit: "smc-sga-wizard-tipo-comp-curricular")

                .Detail<InstituicaoNivelTipoComponenteCurricularDynamicModel>("_DetailList", x => x.DescricaoInstituicaoNivel, "_DetailHeader")

                .Service<IInstituicaoNivelTipoComponenteCurricularService>(edit: nameof(IInstituicaoNivelTipoComponenteCurricularService.BuscarInstituicaoNivelTipoComponenteCurricular),
                                                                           save: nameof(IInstituicaoNivelTipoComponenteCurricularService.SalvarInstituicaoNivelTipoComponenteCurricular))

                .Wizard(editMode: SMCDynamicWizardEditMode.Tab)

                .Ajax()

                .Tokens(tokenList: UC_CUR_004_01_01.PESQUISAR_TIPO_COMPONENTE_INSTITUICAO_NIVEL_ENSINO,
                        tokenInsert: UC_CUR_004_01_02.MANTER_TIPO_COMPONENTE_INSTITUICAO_NIVEL_ENSINO,
                        tokenEdit: UC_CUR_004_01_02.MANTER_TIPO_COMPONENTE_INSTITUICAO_NIVEL_ENSINO,
                        tokenRemove: UC_CUR_004_01_02.MANTER_TIPO_COMPONENTE_INSTITUICAO_NIVEL_ENSINO);
        }

        #endregion [ Configurações ]
    }
}