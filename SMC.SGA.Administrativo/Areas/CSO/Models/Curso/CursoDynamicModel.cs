using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.DadosMestres.Common.Areas.PES.Enums;
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
using SMC.SGA.Administrativo.Areas.CSO.Controllers;
using SMC.SGA.Administrativo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    [SMCStepConfiguration(ActionStep = nameof(CursoController.NivelEnsino))]
    [SMCStepConfiguration(ActionStep = nameof(CursoController.DadosGerais))]
    [SMCStepConfiguration(ActionStep = nameof(CursoController.VerificarPasso), UseOnTabs = false)]
    [SMCStepConfiguration(ActionStep = nameof(CursoController.Classificacoes), Partial = "_ClassificacoesEntidade")]
    [SMCStepConfiguration(ActionStep = nameof(CursoController.Confirmacao), Partial = "_DadosConfirmacaoCurso", UseOnTabs = false)]
    [SMCGroupedPropertyConfiguration(GroupId = "situacaoEntidade", Size = SMCSize.Grid24_24)]
    public class CursoDynamicModel : EntidadeViewModel, ISMCWizardViewModel, ISMCStatefulView, ISMCMappable, ISMCStep
    {
        public CursoDynamicModel()
        {
            this.NivelEnsinoComClassificacao = false;
            this.DataInicioSituacaoAtual = DateTime.Now;
        }

        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IHierarquiaEntidadeService),
                     nameof(IHierarquiaEntidadeService.BuscarHierarquiaOrganizacionalSuperiorSelect),
                     values: new string[] { nameof(SeqTipoEntidade), nameof(ApenasAtivas) })]
        public List<SMCDatasourceItem> EntidadesSuperior { get; set; } = new List<SMCDatasourceItem>();

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoCursoService), nameof(ITipoCursoService.BuscarTiposCursoPorNivelEnsinoSelect),
             values: new string[] { nameof(SeqNivelEnsino) })]
        public List<TipoCurso> TiposCurso { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICursoService), nameof(ICursoService.BuscarSituacoesCursoSelect))]
        public List<SMCDatasourceItem> Situacoes { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<TipoEndereco> TiposEnderecos { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem<string>> TiposTelefone { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        [SMCMapForceFromTo]
        public List<SMCDatasourceItem<string>> TiposEnderecoEletronico { get; set; }

        #endregion [ DataSources ]

        #region [ Nível de Ensino ]

        [SMCConditionalDisplay(nameof(NivelEnsinoComClassificacao), SMCConditionalOperation.Equals, false)]
        [SMCOrder(3)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSelect("NiveisEnsino", NameDescriptionField = "DescricaoNivelEnsino")]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCStep(0, 0)]
        public long SeqNivelEnsino { get; set; }

        [SMCConditionalDisplay(nameof(NivelEnsinoComClassificacao), SMCConditionalOperation.Equals, true)]
        [SMCHidden(SMCViewMode.Edit)]
        [SMCOrder(3)]
        [SMCReadOnly]
        [SMCSelect("NiveisEnsino")]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCStep(0, 0)]
        public long SeqNivelEnsinoReadOnly { get { return SeqNivelEnsino; } }

        [SMCHidden]
        [SMCStep(0, 0)]
        public bool NivelEnsinoComClassificacao { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCIgnoreMetadata]
        public string DescricaoNivelEnsino { get; set; }

        #endregion [ Nível de Ensino ]

        #region [ Dados Gerais ]

        [SMCKey]
        [SMCOrder(2)]
        [SMCReadOnly]
        [SMCRequired]
        [SMCSize(SMCSize.Grid2_24)]
        [SMCStep(1, 0)]
        public override long Seq { get; set; }

        [SMCDependency(nameof(TipoCurso), "RecuperarNomeCurso", "Curso", false, nameof(Nome))]
        [SMCDescription]
        [SMCMaxLength(100)]
        [SMCOrder(4)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(1, 0)]
        public override string Nome { get; set; }

        //Utilizado pelo navigation group
        [SMCHidden]
        [SMCMapProperty("Seq")]
        [SMCSize(SMCSize.Grid2_24)]
        public long SeqEntidade { get; set; }

        [SMCHidden]
        [SMCStep(0, 0)]
        [SMCSize(SMCSize.Grid8_24)]
        public override long SeqTipoEntidade { get; set; }

        [SMCHidden]
        [SMCStep(1, 0)]
        public bool ApenasAtivas { get { return true; } }

        [SMCOrder(8)]
        [SMCStep(1, 0)]
        [SMCConditionalDisplay("UnidadeSeoVisivel", true)]
        [SMCConditionalRequired("UnidadeSeoObrigatorio", true)]
        [SMCInclude(true)] // O Dynamic gera include automático dos lookups, ignorado por ser uma entidade externa
        [UnidadeLookup]
        [SMCSize(SMCSize.Grid14_24)]
        public virtual UnidadeLookupViewModel CodigoUnidadeSeo { get; set; }

        [SMCOrder(11)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSelect("TiposCurso", SortBy = SMCSortBy.Description, AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCStep(1, 0)]
        public TipoCurso TipoCurso { get; set; }

        [SMCGroupedProperty("situacaoEntidade")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCInclude("HistoricoSituacoes.SituacaoEntidade")]
        [SMCOrder(13)]
        [SMCRequired]
        [SMCSelect("Situacoes")]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCStep(1, 0)]
        public long SeqSituacaoAtual { get; set; }

        [SMCGroupedProperty("situacaoEntidade")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCOrder(14)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCStep(1, 0)]
        public DateTime DataInicioSituacaoAtual { get; set; }

        [SMCGroupedProperty("situacaoEntidade")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCMinDate("DataInicioSituacaoAtual")]
        [SMCOrder(15)]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCStep(1, 0)]
        public DateTime? DataFimSituacaoAtual { get; set; }

        [SMCDetail(min: 1)]
        [SMCMapForceFromTo]
        [SMCOrder(16)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(1, 0)]
        public SMCMasterDetailList<CursoEntidadeViewModel> HierarquiasEntidades { get; set; }

        #endregion [ Dados Gerais ]

        #region [ Dados de Contato ]

        [Address(Correspondence = true)]
        [SMCConditionalDisplay("HabilitaEnderecos", SMCConditionalOperation.Equals, true)]
        [SMCMapForceFromTo]
        [SMCOrder(12)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCCssClass("smc-sga-detalhe-editavel-blocos-endereco-responsivo")]
        [SMCStep(2, 1)]
        public AddressList Enderecos { get; set; }

        [SMCHidden]
        [SMCStep(2, 1)]
        public bool HabilitaEnderecos { get; set; }

        [SMCDetail]
        [SMCConditionalDisplay("HabilitaTelefones", SMCConditionalOperation.Equals, true)]
        [SMCMapForceFromTo]
        [SMCOrder(13)]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(2, 1)]
        public SMCMasterDetailList<TelefoneCategoriaViewModel> Telefones { get; set; }

        [SMCHidden]
        [SMCStep(2, 1)]
        public bool HabilitaTelefones { get; set; }

        [SMCConditionalDisplay("HabilitaEnderecosEletronicos", SMCConditionalOperation.Equals, true)]
        [SMCDetail]
        [SMCMapForceFromTo]
        [SMCOrder(14)]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(2, 1)]
        public SMCMasterDetailList<EnderecoEletronicoCategoriaViewModel> EnderecosEletronicos { get; set; }

        [SMCHidden]
        [SMCStep(2, 1)]
        public bool HabilitaEnderecosEletronicos { get; set; }

        #endregion [ Dados de Contato ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Wizard(editMode: SMCDynamicWizardEditMode.Tab)
                   .Detail<CursoListarDynamicModel>("_DetailList", allowSort: true)
                   .HeaderIndexList("Legenda")
                   .DisableInitialListing(true)
                   .Tokens(
                             tokenEdit: UC_CSO_001_01_02.MANTER_CURSO,
                           tokenInsert: UC_CSO_001_01_02.MANTER_CURSO,
                           tokenRemove: UC_CSO_001_01_02.MANTER_CURSO,
                             tokenList: UC_CSO_001_01_01.PESQUISAR_CURSO)
                   .Service<ICursoService>(index: nameof(ICursoService.BuscarCursos),
                                           insert: nameof(ICursoService.BuscarConfiguracaoDoCurso),
                                           edit: nameof(ICursoService.BuscarCursoComConfiguracao),
                                           save: nameof(ICursoService.SalvarCurso))
                   .Button("AssociacaoCursoUnidade", "Index", "CursoUnidade",
                           UC_CSO_001_02_01.PESQUISAR_ASSOCIACAO_CURSO_UNIDADE,
                           i => new { seqEntidade = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq) })
                   .Button("Curriculo", "Index", "Curriculo",
                           UC_CUR_001_01_01.PESQUISAR_CURRICULO,
                           i => new
                           {
                               area = "CUR",
                               seqCurso = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq)
                           })
                   .Button("AlterarSituacaoCurso", "Index", "CursoHistoricoSituacao",
                           UC_ORG_001_10_01.MANTER_SITUACAO_ENTIDADE,
                           i => new
                           {
                               seqEntidade = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq),
                               seqTipoEntidade = SMCDESCrypto.EncryptNumberForURL(((CursoListarDynamicModel)i).SeqTipoEntidade),
                           })
                   .Button("PostagemMaterial", "Index", "Material",
                                UC_APR_004_01_01.PESQUISAR_MATERIAL,
                                i => new { seqOrigem = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq), tipoOrigemMaterial = TipoOrigemMaterial.Entidade, Area = "APR" })
                   .Javascript("Curso");
            //

            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("seqPrograma"))
                options.ButtonBackIndex("Index", "Programa", x => new { area = "CSO" });
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            if (viewMode == SMCViewMode.Insert)
            {
                this.HierarquiasEntidades = new SMCMasterDetailList<CursoEntidadeViewModel>();
                this.HierarquiasEntidades.DefaultModel = new CursoEntidadeViewModel { SeqEntidade = SeqEntidade, SeqTipoEntidade = SeqTipoEntidade };
            }
            else if (viewMode == SMCViewMode.Edit)
            {
                this.HierarquiasEntidades.DefaultModel = new CursoEntidadeViewModel { SeqEntidade = SeqEntidade, SeqTipoEntidade = SeqTipoEntidade };
            }
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new CursoNavigationGroup(this);
        }
    }
}