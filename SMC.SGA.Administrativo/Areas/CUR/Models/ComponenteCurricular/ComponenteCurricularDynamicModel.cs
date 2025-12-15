using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CUR.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    [SMCStepConfiguration(UseOnTabs = false)]
    [SMCStepConfiguration(ActionStep = nameof(ComponenteCurricularController.StepTipoComponenteCurricular))]
    [SMCStepConfiguration(ActionStep = nameof(ComponenteCurricularController.StepOrganizacao))]
    public class ComponenteCurricularDynamicModel : SMCDynamicViewModel, ISMCWizardViewModel, ISMCStatefulView
    {
        #region [ DataSource ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoSelect))]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoComponenteCurricularService), nameof(IInstituicaoNivelTipoComponenteCurricularService.BuscarTipoComponenteCurricularSelect),
            values: new string[] { nameof(SeqInstituicaoNivelResponsavel) })]
        public List<SMCDatasourceItem> TiposComponenteCurricular { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoComponenteCurricularService), nameof(IInstituicaoNivelTipoComponenteCurricularService.BuscarEntidadesPorTipoComponenteSelect),
            values: new string[] { nameof(SeqInstituicaoNivelResponsavel), nameof(SeqTipoComponenteCurricular) })]
        public List<SMCDatasourceItem> EntidadesResponsavel { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IComponenteCurricularService), nameof(IComponenteCurricularService.BuscarQuantidadesSemanasSelect))]
        public List<SMCDatasourceItem> QtdSemanas { get; set; }

        #endregion [ DataSource ]

        #region [ Tipo de Componente ]

        [SMCIgnoreProp]
        public int Step { get; set; }

        [SMCKey]
        [SMCOrder(3)]
        [SMCReadOnly]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid2_24)]
        [SMCStep(1, 0)]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCOrder(0)]
        [SMCRequired]
        [SMCStep(0)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(1)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSelect(nameof(InstituicaoNiveis))]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(0, 0)]
        public long SeqInstituicaoNivelResponsavel { get; set; }

        [SMCDependency(nameof(SeqInstituicaoNivelResponsavel), nameof(ComponenteCurricularController.BuscarTipoComponenteCurricularSelect), "ComponenteCurricular", true, nameof(SeqInstituicaoEnsino))]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(2)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSelect(nameof(TiposComponenteCurricular))]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(0, 0)]
        public long SeqTipoComponenteCurricular { get; set; }

        #endregion [ Tipo de Componente ]

        #region [ Componente Curricular ]

        [SMCHidden]
        [SMCStep(1, 0)]
        public short? NumeroVersaoCarga { get; set; }

        [SMCOrder(3)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid2_24)]
        [SMCStep(1, 0)]
        public string Codigo { get; set; }

        [SMCMaxLength(100)]
        [SMCOrder(4)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid10_24)]
        [SMCStep(1, 0)]
        [SMCConditionalReadonly(nameof(PossuiAssociacaoDivisaoMatrizes), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(PossuiAssociacaoAssuntoConfiguracaoComponenteMatriz), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R2")]
        [SMCConditionalRule("R1 || R2")]

        public string Descricao { get; set; }

        [SMCConditionalRequired(nameof(DescricaoReduzidaObrigatorio), true)]
        [SMCMaxLength(50)]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCStep(1, 0)]
        public string DescricaoReduzida { get; set; }

        [SMCConditionalRequired(nameof(SiglaObrigatorio), true)]
        [SMCMaxLength(10)]
        [SMCOrder(6)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCStep(1, 0)]
        public string Sigla { get; set; }

        [SMCConditionalRequired(nameof(CargaHorariaObrigatorio), true)]
        [SMCConditionalDisplay(nameof(CargaHorariaDisplay), true)]
        [SMCMask("9999")]
        [SMCOrder(7)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCConditionalReadonly(nameof(PossuiAssociacaoDivisaoMatrizes), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(PossuiAssociacaoAssuntoConfiguracaoComponenteMatriz), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R2")]
        [SMCConditionalRule("R1 || R2")]
        [SMCStep(1, 0)]
        public short? CargaHoraria { get; set; }

        [SMCHidden]
        [SMCStep(1, 0)]
        public short? QuantidadeHorasCredito { get; set; }

        [SMCConditionalDisplay(nameof(CreditoDisplay), true)]
        [SMCConditionalReadonly(nameof(PossuiAssociacaoDivisaoMatrizes), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(PossuiAssociacaoAssuntoConfiguracaoComponenteMatriz), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R2")]
        [SMCConditionalReadonly(nameof(CargaHorariaDisplay), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R3")]
        [SMCConditionalReadonly(nameof(CreditoDisplay), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R4")]
        [SMCConditionalRule("(R1 || R2) || (R3 && R4)")]
        [SMCConditionalRequired(nameof(CreditoObrigatorio), true)]
        [SMCDependency(nameof(CargaHoraria), nameof(ComponenteCurricularController.CargaHorariaCredito), "ComponenteCurricular", false, new string[] { nameof(QuantidadeHorasCredito), nameof(CargaHorariaDisplay), nameof(CreditoDisplay), nameof(Credito) })]
        [SMCMask("9999")]
        [SMCOrder(8)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCStep(1, 0)]
        public short? Credito { get; set; }


        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCStep(1, 0)]
        [SMCOrder(9)]
        [SMCSelect(nameof(QtdSemanas))]
        [SMCConditionalDisplay(nameof(CargaHorariaDisplay), true)]
        public long? QuantidadeSemanas { get; set; }

        [SMCOrder(10)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCMapForceFromTo]
        [SMCStep(1, 0)]
        public bool Ativo { get; set; } = true;

        [SMCHidden]
        [SMCStep(1, 0)]
        public bool AtivoBanco { get; set; }

        [SMCStep(1, 0)]
        [SMCHidden]
        public bool PermiteAssuntoComponente { get; set; }

        [SMCStep(0, 0)]
        [SMCHidden]
        public bool PossuiAssociacaoDivisaoMatrizes { get; set; }

        [SMCStep(0, 0)]
        [SMCHidden]
        public bool PossuiAssociacaoAssuntoConfiguracaoComponenteMatriz { get; set; }


        [SMCOrder(11)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid6_24)]
        [SMCStep(1, 0)]
        [SMCConditionalReadonly(nameof(PermiteAssuntoComponente), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        public bool ExigeAssuntoComponente { get; set; }

        [SMCMultiline]
        [SMCOrder(12)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(1, 0)]
        public string Observacao { get; set; }

        [SMCConditionalDisplay(nameof(OrgaoReguladorDisplay), true)]
        [SMCDetail(SMCDetailType.Tabular)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(13)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(1, 0)]
        public SMCMasterDetailList<ComponenteCurricularOrgaoReguladorViewModel> OrgaosReguladores { get; set; }

        [SMCConditionalDisplay(nameof(EmentaDisplay), true)]
        [SMCDetail(SMCDetailType.Tabular)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(14)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(1, 0)]
        public SMCMasterDetailList<ComponenteCurricularEmentaViewModel> Ementas { get; set; }

        #endregion [ Componente Curricular ]

        #region [ Organização ]

        [SMCConditionalDisplay(nameof(TipoOrganizacaoDisplay), true)]
        [SMCOrder(15)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCConditionalReadonly(nameof(PossuiAssociacaoDivisaoMatrizes), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(PossuiAssociacaoAssuntoConfiguracaoComponenteMatriz), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R2")]
        [SMCConditionalRule("R1 || R2")]
        [SMCStep(2, 1)]
        public TipoOrganizacao? TipoOrganizacao { get; set; }

        [SMCConditionalDisplay(nameof(OrganizacoesDisplay), true)]
        [SMCDetail(SMCDetailType.Tabular)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCMapForceFromTo]
        [SMCOrder(16)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(2, 1)]
        public SMCMasterDetailList<ComponenteCurricularOrganizacaoViewModel> Organizacoes { get; set; }

        [SMCDetail(SMCDetailType.Tabular)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCMapForceFromTo]
        [SMCOrder(17)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalReadonly(nameof(PossuiAssociacaoDivisaoMatrizes), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(PossuiAssociacaoAssuntoConfiguracaoComponenteMatriz), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R2")]
        [SMCConditionalRule("R1 || R2")]
        [SMCStep(2, 1)]
        public SMCMasterDetailList<ComponenteCurricularNivelEnsinoViewModel> NiveisEnsino { get; set; }

        [SMCDetail(SMCDetailType.Tabular)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCMapForceFromTo]
        [SMCOrder(18)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(2, 1)]
        public SMCMasterDetailList<ComponenteCurricularEntidadeResponsavelViewModel> EntidadesResponsaveis { get; set; }

        #endregion [ Organização ]

        #region [ Obrigatórios e Displays ]

        [SMCHidden]
        [SMCStep(1, 0)]
        public bool DescricaoReduzidaObrigatorio { get; set; }

        [SMCHidden]
        [SMCStep(1, 0)]
        public bool SiglaObrigatorio { get; set; }

        [SMCHidden]
        [SMCStep(1, 0)]
        public bool CargaHorariaDisplay { get; set; }

        [SMCHidden]
        [SMCStep(1, 0)]
        public bool CargaHorariaObrigatorio { get; set; }

        [SMCHidden]
        [SMCStep(1, 0)]
        public bool CreditoDisplay { get; set; }

        [SMCHidden]
        [SMCStep(1, 0)]
        public bool CreditoObrigatorio { get; set; }

        [SMCHidden]
        [SMCStep(1, 0)]
        public bool OrgaoReguladorDisplay { get; set; }

        [SMCHidden]
        [SMCStep(1, 0)]
        public TipoOrgaoRegulador RegistroTipoOrgaoRegulador { get; set; }

        [SMCHidden]
        [SMCStep(1, 0)]
        public bool EmentaDisplay { get; set; }

        [SMCHidden]
        [SMCStep(2, 1)]
        public bool TipoOrganizacaoDisplay { get; set; }

        [SMCHidden]
        [SMCStep(2, 1)]
        public bool OrganizacoesDisplay { get; set; }

        [SMCHidden]
        [SMCStep(2, 1)]
        public bool EntidadesResponsaveisObrigatorio { get; set; }

        #endregion [ Obrigatórios e Displays ]

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Detail<ComponenteCurricularListarDynamicModel>("_DetailList")
                   .Button("ConfiguracaoComponente", "Index", "ConfiguracaoComponente",
                                UC_CUR_002_01_03.PESQUISAR_CONFIGURACAO_COMPONENTE_CURRICULAR,
                                i => new { seqComponenteCurricular = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq),  },
                                displayButton: x => (x as ComponenteCurricularListarDynamicModel).PermiteConfiguracaoComponente)
                   .Service<IComponenteCurricularService>(index: nameof(IComponenteCurricularService.BuscarComponentesCurriculares),
                                                           edit: nameof(IComponenteCurricularService.BuscarComponenteCurricular),
                                                           save: nameof(IComponenteCurricularService.SalvarComponenteCurricular))
                   .Assert("MSG_Asset_Desativar_ComponenteCurricular", x => (x as ComponenteCurricularDynamicModel).AtivoBanco && !(x as ComponenteCurricularDynamicModel).Ativo)
                   .HeaderIndexList("CabecalhoLista")
                   .Wizard(editMode: SMCDynamicWizardEditMode.Tab)
                   .Tokens(tokenList: UC_CUR_002_01_01.PESQUISAR_COMPONENTE_CURRICULAR,
                           tokenInsert: UC_CUR_002_01_02.MANTER_COMPONENTE_CURRICULAR,
                           tokenEdit: UC_CUR_002_01_02.MANTER_COMPONENTE_CURRICULAR,
                           tokenRemove: UC_CUR_002_01_02.MANTER_COMPONENTE_CURRICULAR);
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new ComponenteCurricularNavigationGroup(this);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);
            
            if (viewMode == SMCViewMode.Edit)
            {
                AtivoBanco = Ativo;
            }
        }

        #endregion [ Configurações ]
    }
}