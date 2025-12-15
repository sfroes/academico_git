using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.ORG.InterfaceBlocks;
using SMC.Academico.UI.Mvc.Areas.ORG.Models;
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
using SMC.SGA.Administrativo.Areas.ORG.Models;
using SMC.SGA.Administrativo.Models;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    [SMCStepConfiguration]
    [SMCStepConfiguration(Partial = "_ClassificacoesEntidade")]
    [SMCStepConfiguration]
    [SMCStepConfiguration(Partial = "_AtoNormativo")]
    [SMCGroupedPropertyConfiguration(GroupId = "situacaoEntidade", Size = SMCSize.Grid24_24)]
    public class ProgramaDynamicModel : EntidadeViewModel, IAtoNormativoBI
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IRegimeLetivoService), nameof(IRegimeLetivoService.BuscarRegimesLetivosStrictoSelect))]
        public List<SMCDatasourceItem> RegimesLetivos { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IHierarquiaEntidadeService),
                             nameof(IHierarquiaEntidadeService.BuscarHierarquiaOrganizacionalSuperiorSelect),
                             values: new string[] { nameof(SeqTipoEntidade), nameof(ApenasAtivas) })]
        public List<SMCDatasourceItem> EntidadesResponsavel { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IProgramaService), nameof(IProgramaService.BuscarSituacoesPrograma), values: new string[] { nameof(ListarSituacoesInativas) })]
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

        [SMCIgnoreProp]
        private bool ListarSituacoesInativas { get; set; } = false;

        [SMCIgnoreProp]
        public bool ApenasAtivas { get; set; } = true;

        #endregion [ DataSources ]

        #region [ Aba0 Dados Gerais ]

        [SMCStep(0)]
        [SMCKey]
        [SMCReadOnly]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        [SMCRequired]
        public override long Seq { get; set; }

        //Utilizado pelo navigation group
        [SMCStep(0)]
        [SMCHidden]
        [SMCMapProperty("Seq")]
        public long SeqEntidade { get; set; }

        [SMCStep(0)]
        [SMCHidden]
        public override long SeqTipoEntidade { get; set; }

        [SMCStep(0)]
        [SMCHidden]
        public long SeqHierarquiaEntidadeItem { get; set; }

        [SMCOrder(4)]
        [SMCStep(1, 0)]
        [SMCDescription]
        [SMCRequired]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid10_24)]
        [SMCMaxLength(100)]
        public new virtual string Nome { get; set; }

        [SMCOrder(5)]
        [SMCStep(1, 0)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCMaxLength(15)]
        [SMCConditionalDisplay(nameof(SiglaVisivel), true)]
        [SMCConditionalRequired(nameof(SiglaObrigatoria), true)]
        public new string Sigla { get; set; }

        [SMCOrder(6)]
        [SMCStep(1, 0)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid7_24)]
        [SMCMaxLength(50)]
        [SMCConditionalDisplay(nameof(NomeReduzidoVisivel), true)]
        [SMCConditionalRequired(nameof(NomeReduzidoObrigatorio), true)]
        public new string NomeReduzido { get; set; }

        [SMCOrder(8)]
        [SMCStep(1, 0)]
        [SMCConditionalDisplay(nameof(UnidadeSeoVisivel), true)]
        [SMCConditionalRequired(nameof(UnidadeSeoObrigatorio), true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid9_24)]
        [SMCInclude(true)] // O Dynamic gera include automático dos lookups, ignorado por ser uma entidade externa
        [UnidadeLookup]
        public new virtual UnidadeLookupViewModel CodigoUnidadeSeo { get; set; }

        [SMCMaxLength(20)]
        [SMCOrder(9)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCStep(0)]
        [SMCConditionalRequired(nameof(CodigoCapesObrigatorio), SMCConditionalOperation.Equals, true)]
        public string CodigoCapes { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        [SMCDependency(nameof(SeqSituacaoAtual), nameof(ProgramaController.PreencherCodigoCapesObrigatorio), "Programa", true)]
        public bool CodigoCapesObrigatorio { get; set; }

        [SMCStep(0)]
        [SMCOrder(10)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCSelect(nameof(RegimesLetivos), AutoSelectSingleItem = true, SortBy = SMCSortBy.Description, NameDescriptionField = nameof(DescricaoRegimeLetivo))]
        [SMCRequired]
        public long SeqRegimeLetivo { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public string DescricaoRegimeLetivo { get; set; }

        [SMCOrder(11)]
        [SMCRadioButtonList]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid7_24)]
        [SMCStep(0)]
        public TipoPrograma TipoPrograma { get; set; }

        [SMCIgnoreProp]
        public string DescricaoEntidadeResponsavel { get; set; }

        [SMCOrder(12)]
        [SMCRequired]
        [SMCSelect(nameof(EntidadesResponsavel), SortBy = SMCSortBy.Description, AutoSelectSingleItem = true, NameDescriptionField = nameof(DescricaoEntidadeResponsavel))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid13_24)]
        [SMCStep(0)]
        public long SeqHierarquiaEntidadeItemSuperior { get; set; }

        [SMCOrder(13)]
        [SMCStep(0)]
        [SMCMapForceFromTo]
        [SMCDetail(min: 1)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<TiposAutorizacaoBdpViewModel> TiposAutorizacaoBdp { get; set; }

        [SMCGroupedProperty("situacaoEntidade")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCInclude("HistoricoSituacoes.SituacaoEntidade")]
        [SMCOrder(14)]
        [SMCRequired]
        [SMCSelect(nameof(Situacoes))]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCStep(0)]
        public long SeqSituacaoAtual { get; set; }

        [SMCGroupedProperty("situacaoEntidade")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCOrder(15)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCStep(0)]
        public DateTime DataInicioSituacaoAtual { get; set; } = DateTime.Today;

        [SMCGroupedProperty("situacaoEntidade")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCMinDate(nameof(DataInicioSituacaoAtual))]
        [SMCOrder(16)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCStep(0)]
        public DateTime? DataFimSituacaoAtual { get; set; }

        [SMCOrder(17)]
        [SMCStep(0)]
        [SMCMapForceFromTo]
        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<EntidadeIdiomaViewModel> DadosOutrosIdiomas { get; set; }

        [SMCOrder(18)]
        [SMCStep(0)]
        [SMCMapForceFromTo]
        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<ProgramaHistoricoNotaViewModel> HistoricoNotas { get; set; }

        #endregion [ Aba0 Dados Gerais ]

        #region [ Aba1 Classifiações ]

        //Classificações que estão na EntidadeViewModel

        #endregion [ Aba1 Classifiações ]

        #region [ Aba2 Dados de Contato ]

        [SMCOrder(18)]
        [SMCStep(2)]
        [Address(Correspondence = true)]
        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay("HabilitaEnderecos", SMCConditionalOperation.Equals, true)]
        [SMCCssClass("smc-sga-detalhe-editavel-blocos-endereco-responsivo")]
        public AddressList Enderecos { get; set; }

        [SMCHidden]
        [SMCStep(2)]
        public bool HabilitaEnderecos { get; set; }

        [SMCOrder(19)]
        [SMCStep(2)]
        [SMCDetail]
        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCConditionalDisplay("HabilitaTelefones", SMCConditionalOperation.Equals, true)]
        public SMCMasterDetailList<TelefoneCategoriaViewModel> Telefones { get; set; }

        [SMCHidden]
        [SMCStep(2)]
        public bool HabilitaTelefones { get; set; }

        [SMCOrder(20)]
        [SMCStep(2)]
        [SMCMapForceFromTo]
        [SMCDetail]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCConditionalDisplay("HabilitaEnderecosEletronicos", SMCConditionalOperation.Equals, true)]
        public SMCMasterDetailList<EnderecoEletronicoCategoriaViewModel> EnderecosEletronicos { get; set; }

        [SMCHidden]
        [SMCStep(2)]
        public bool HabilitaEnderecosEletronicos { get; set; }

        #endregion [ Aba2 Dados de Contato ]

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

        #region [ Configuração ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Tokens(tokenEdit: UC_CSO_002_01_02.MANTER_PROGRAMA,
                           tokenInsert: UC_CSO_002_01_02.MANTER_PROGRAMA,
                           tokenRemove: UC_CSO_002_01_02.MANTER_PROGRAMA,
                           tokenList: UC_CSO_002_01_01.PESQUISAR_PROGRAMA)
                   .Tab()
                   .Detail<ProgramaListarDynamicModel>("_DetailList")
                   .Service<IProgramaService>(index: nameof(IProgramaService.BuscarProgramas),
                                              insert: nameof(IProgramaService.BuscaConfiguracoesEntidadePrograma),
                                              edit: nameof(IProgramaService.BuscarPrograma),
                                              save: nameof(IProgramaService.SalvarPrograma))
                   .Button("FormacoesEspecificasPrograma", "Index", "FormacaoEspecifica",
                           UC_CSO_002_01_03.PESQUISAR_FORMACAO_ESPECIFICA_PROGRAMA, 
                           i => new
                           {
                               seqEntidadeResponsavel = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq),
                               seqTipoEntidade = SMCDESCrypto.EncryptNumberForURL(((ProgramaListarDynamicModel)i).SeqTipoEntidade),
                               seqHierarquiaEntidadeItem = SMCDESCrypto.EncryptNumberForURL(((ProgramaListarDynamicModel)i).SeqHierarquiaEntidadeItem)
                           })
                   .Button("CursosPrograma", "Index", "Curso",
                           UC_CSO_001_01_01.PESQUISAR_CURSO,
                           i => new
                           {
                               seqPrograma = SMCDESCrypto.EncryptNumberForURL(((ProgramaListarDynamicModel)i).SeqHierarquiaEntidadeItem)
                           })
                   .Button("PropostasPrograma", "Index", "ProgramaProposta",
                           UC_CSO_002_01_05.PESQUISAR_PROPOSTA,
                           i => new
                           {
                               seqEntidade = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq),
                               seqTipoEntidade = SMCDESCrypto.EncryptNumberForURL(((ProgramaListarDynamicModel)i).SeqTipoEntidade),
                               seqHierarquiaEntidadeItem = SMCDESCrypto.EncryptNumberForURL(((ProgramaListarDynamicModel)i).SeqHierarquiaEntidadeItem)
                           })
                   .Button("AlterarSituacaoPrograma", "Index", "ProgramaHistoricoSituacao",
                           UC_ORG_001_10_01.MANTER_SITUACAO_ENTIDADE,
                           i => new
                           {
                               seqEntidade = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq),
                               seqTipoEntidade = SMCDESCrypto.EncryptNumberForURL(((ProgramaListarDynamicModel)i).SeqTipoEntidade),
                               seqHierarquiaEntidadeItem = SMCDESCrypto.EncryptNumberForURL(((ProgramaListarDynamicModel)i).SeqHierarquiaEntidadeItem)
                           })
                   .Button("PostagemMaterial", "Index", "Material",
                                UC_APR_004_01_01.PESQUISAR_MATERIAL,
                                i => new { seqOrigem = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq), tipoOrigemMaterial = TipoOrigemMaterial.Entidade, Area = "APR" });
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new ProgramaNavigationGroup(this);
        }

        #endregion [ Configuração ]
    }
}