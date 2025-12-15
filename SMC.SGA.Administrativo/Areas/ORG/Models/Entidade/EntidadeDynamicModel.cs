using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.ORG.InterfaceBlocks;
using SMC.Academico.UI.Mvc.Areas.ORG.Models;
using SMC.EstruturaOrganizacional.UI.Mvc.Areas.ESO.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Localidades.Common.Areas.LOC.Enums;
using SMC.Localidades.UI.Mvc.DataAnnotation;
using SMC.Localidades.UI.Mvc.Models;
using SMC.SGA.Administrativo.Models;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    [SMCStepConfiguration(ActionStep = "Step1", UseOnTabs = false)]
    [SMCStepConfiguration(ActionStep = "Step2")]
    [SMCStepConfiguration(ActionStep = "Step3")]
    [SMCStepConfiguration(Partial = "_ClassificacoesEntidade", ActionStep = "Step4")]
    [SMCStepConfiguration(Partial = "_DadosConfirmacaoEntidade", ActionStep = "Step5", UseOnTabs = false)]
    [SMCStepConfiguration(Partial = "_AtoNormativo", UseOnWizards = false)]
    [SMCGroupedPropertyConfiguration(GroupId = "situacaoEntidade", Size = SMCSize.Grid24_24)]
    public class EntidadeDynamicModel : EntidadeViewModel, ISMCWizardViewModel, ISMCStatefulView, IAtoNormativoBI
    {
        public const string KEY_DATASOURCE_TIPOENTIDADE = "TipoEntidade";
        private object get;

        public EntidadeDynamicModel()
        {
            this.DataInicioSituacaoAtual = DateTime.Now;
        }

        [SMCStep(1, 0)]
        [SMCKey]
        [SMCReadOnly]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCStep(0)]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCSelect(nameof(TiposEntidade), NameDescriptionField = nameof(NomeTipoEntidadeSelecionada))]
        [SMCConditionalReadonly(nameof(Seq), SMCConditionalOperation.NotEqual, "0", PersistentValue = true)]
        [SMCRequired]
        public override long SeqTipoEntidade { get; set; }

        [SMCDataSource(KEY_DATASOURCE_TIPOENTIDADE, StorageType = SMCStorageType.TempData)]
        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeService), nameof(IInstituicaoTipoEntidadeService.BuscarTipoEntidadesNaoExternadaDaInstituicaoSelect))]
        public List<SMCDatasourceItem> TiposEntidade { get; set; }

        public string NomeTipoEntidadeSelecionada { get; set; }

        //Propriedades idênticas as da classe pai. Duplicadas apenas para alterar o size no componente na tela
        [SMCOrder(4)]
        [SMCStep(1, 0)]
        [SMCDescription]
        [SMCRequired]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid9_24)]
        [SMCMaxLength(100)]
        public new virtual string Nome { get; set; }

        [SMCOrder(5)]
        [SMCStep(1, 0)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCMaxLength(15)]
        [SMCConditionalDisplay(nameof(SiglaVisivel), true)]
        [SMCConditionalRequired(nameof(SiglaObrigatoria), true)]
        public new string Sigla { get; set; }

        [SMCOrder(6)]
        [SMCStep(1, 0)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCMaxLength(50)]
        [SMCConditionalDisplay(nameof(NomeReduzidoVisivel), true)]
        [SMCConditionalRequired(nameof(NomeReduzidoObrigatorio), true)]
        public new string NomeReduzido { get; set; }

        [SMCConditionalDisplay(nameof(UnidadeSeoVisivel), true)]
        [SMCConditionalRequired(nameof(UnidadeSeoObrigatorio), true)]
        [SMCOrder(8)]
        [SMCStep(1, 0)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCInclude(true)]
        [UnidadeLookup]
        public new UnidadeLookupViewModel CodigoUnidadeSeo { get; set; }

        // Fim duplicação.

        [SMCOrder(9)]
        [SMCSelect(nameof(Situacoes))]
        [SMCStep(1, 0)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCRequired]
        [SMCInclude("HistoricoSituacoes.SituacaoEntidade")]
        [SMCGroupedProperty("situacaoEntidade")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        public long SeqSituacaoAtual { get; set; }

        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeService), nameof(IInstituicaoTipoEntidadeService.BuscarSituacoesTipoEntidadeDaInstituicaoSelect), values: new string[] { nameof(SeqTipoEntidade) })]
        [SMCDataSource]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> Situacoes { get; set; }

        [SMCOrder(10)]
        [SMCRequired]
        [SMCStep(1, 0)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCGroupedProperty("situacaoEntidade")]
        [SMCMapForceFromTo]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        public DateTime DataInicioSituacaoAtual { get; set; }

        [SMCOrder(11)]
        [SMCStep(1, 0)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCGroupedProperty("situacaoEntidade")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCMinDate(nameof(DataInicioSituacaoAtual))]
        public DateTime? DataFimSituacaoAtual { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCOrder(12)]
        [SMCStep(2, 1)]
        [Address(Correspondence = true)]
        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(HabilitaEnderecos), SMCConditionalOperation.Equals, true)]
        [SMCCssClass("smc-sga-detalhe-editavel-blocos-endereco-responsivo")]
        public AddressList Enderecos { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<TipoEndereco> TiposEnderecos { get; set; }

        [SMCHidden]
        [SMCStep(2, 1)]
        public bool HabilitaEnderecos { get; set; }

        [SMCDetail]
        [SMCOrder(13)]
        [SMCStep(2, 1)]
        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCConditionalDisplay(nameof(HabilitaTelefones), SMCConditionalOperation.Equals, true)]
        public SMCMasterDetailList<TelefoneCategoriaViewModel> Telefones { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem<string>> TiposTelefone { get; set; }

        [SMCHidden]
        [SMCStep(2, 1)]
        public bool HabilitaTelefones { get; set; }

        [SMCOrder(14)]
        [SMCStep(2, 1)]
        [SMCMapForceFromTo]
        [SMCDetail]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCConditionalDisplay(nameof(HabilitaEnderecosEletronicos), SMCConditionalOperation.Equals, true)]
        public SMCMasterDetailList<EnderecoEletronicoCategoriaViewModel> EnderecosEletronicos { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem<string>> TiposEnderecoEletronico { get; set; }

        [SMCHidden]
        [SMCStep(2, 1)]
        public bool HabilitaEnderecosEletronicos { get; set; }

        public long SeqHierarquiaClassificacao { get; set; }

        #region [BI_ORG_002 - Atos Normativos da Entidade]

        [SMCHidden]
        [SMCStep(0)]
        public bool AtivaAbaAtoNormativo { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool HabilitaColunaGrauAcademico { get; set; }

        [SMCStep(3)]
        [SMCIgnoreProp(SMCViewMode.Insert)]
        [SMCConditional(SMCConditionalBehavior.Visibility, nameof(AtivaAbaAtoNormativo), SMCConditionalOperation.Equals, true)]
        public List<AtoNormativoVisualizarViewModel> AtoNormativo { get; set; }

        #endregion [BI_ORG_002 - Atos Normativos da Entidade]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Service<IEntidadeService>(index: nameof(IEntidadeService.BuscarEntidades),
                                              save: nameof(IEntidadeService.SalvarEntidade),
                                              edit: nameof(IEntidadeService.BuscarEntidade),
                                              delete: nameof(IEntidadeService.ExcluirEntidade))
                   .DisableInitialListing(true)
                   .RegisterControls(RegisterHelperControls.Lookup,
                                     RegisterHelperControls.Fields,
                                     RegisterHelperControls.DataSelector,
                                     RegisterHelperControls.Upload)
                   .Tokens(tokenEdit: UC_ORG_001_06_02.MANTER_ENTIDADE,
                           tokenInsert: UC_ORG_001_06_02.MANTER_ENTIDADE,
                           tokenRemove: UC_ORG_001_06_02.MANTER_ENTIDADE,
                           tokenList: UC_ORG_001_06_01.PESQUISAR_ENTIDADE)
                   .Wizard(editMode: SMCDynamicWizardEditMode.Tab)
                   .Button("EntidadeHistoricoSituacao", "Index", "EntidadeHistoricoSituacao",
                                UC_ORG_001_10_01.MANTER_SITUACAO_ENTIDADE,
                                i => new { seqEntidade = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq), seqTipoEntidade = SMCDESCrypto.EncryptNumberForURL(((EntidadeListarDynamicModel)i).SeqTipoEntidade) })
                   .Button("PostagemMaterial", "Index", "Material",
                                UC_APR_004_01_01.PESQUISAR_MATERIAL,
                                i => new { seqOrigem = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq), tipoOrigemMaterial = TipoOrigemMaterial.Entidade, Area = "APR" });
        }
    }
}