using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CUR.Controllers;
using SMC.SGA.Administrativo.Areas.CUR.Views.GrupoCurricular.App_LocalResources;
using SMC.SGA.Administrativo.Areas.CUR.Views.GrupoCurricularComponente.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "GrupoConfiguracao", Size = SMCSize.Grid24_24)]
    public class GrupoCurricularDynamicModel : SMCDynamicViewModel, ISMCTreeNode, ISMCSeq
    {
        #region [ Consts ]

        private const short FORMATO_CARGA_HORARIA = (short)Academico.Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.CargaHoraria;
        private const short FORMATO_CREDITO = (short)Academico.Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.Credito;
        private const short FORMATO_ITENS = (short)Academico.Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.Itens;

        #endregion [ Consts ]

        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoGrupoCurricularService), nameof(ITipoGrupoCurricularService.BuscarTiposGruposCurricularesInstituicaoNivelEnsinoSelect),
            values: new[] { nameof(SeqNivelEnsino) })]
        public List<SMCDatasourceItem> TiposGruposCurriculares { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoConfiguracaoGrupoCurricularService),
            nameof(ITipoConfiguracaoGrupoCurricularService.BuscarTiposConfiguracaoGrupoCurricularSelect),
            values: new[] { nameof(SeqTipoConfiguracaoGrupoCurricularSuperior) })]
        public List<SMCDatasourceItem> ConfiguracoesTiposGruposCurriculares { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoGrupoCurricularService),
            nameof(ITipoGrupoCurricularService.BuscarFormatosConfiguracaoGrupoPorNivelEnsinoDaInstituicaoSelect),
            values: new[] { nameof(SeqNivelEnsino) })]
        public List<SMCDatasourceItem> FormatosConfiguracao { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IBeneficioService),
            nameof(IBeneficioService.BuscarBeneficioPorNivelEnsinoSelect),
            values: new[] { nameof(SeqNivelEnsino) })]
        public List<SMCDatasourceItem> BeneficiosNivelEnsino { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICondicaoObrigatoriedadeService),
            nameof(ICondicaoObrigatoriedadeService.BuscarCondicoesObrigatoriedadePorNivelEnsino),
            values: new[] { nameof(SeqNivelEnsino) })]
        public List<SMCDatasourceItem> CondicoesObrigatoriedadeNivelEnsino { get; set; }

        #endregion [ DataSources ]

        [SMCKey]
        [SMCOrder(1)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqCurriculo { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long Index { get; set; }

        [SMCHidden]
        [SMCInclude("Curriculo")]
        [SMCMapProperty("Curriculo.SeqCurso")]
        public long SeqCurso { get; set; }

        [SMCHidden]
        [SMCInclude("Curriculo.Curso")]
        [SMCMapProperty("Curriculo.Curso.SeqNivelEnsino")]
        public long SeqNivelEnsino { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqGrupoCurricularSuperior { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqGrupoSuperior { get; set; }

        [SMCHidden]
        [SMCInclude("GrupoCurricularSuperior")]
        [SMCMapProperty("GrupoCurricularSuperior.SeqTipoConfiguracaoGrupoCurricular")]
        public long? SeqTipoConfiguracaoGrupoCurricularSuperior { get; set; }

        /// <summary>
        /// Sequencial do item superior com o nome esperado pelo modal do Tree
        /// </summary>
        [SMCHidden]
        [SMCParameter]
        public long? SeqPai { get; set; }

        [SMCConditionalDisplay(nameof(SeqPai), SMCConditionalOperation.GreaterThen, 0)]
        [SMCInclude("GrupoCurricularSuperior")]
        [SMCMapProperty("GrupoCurricularSuperior.Descricao")]
        [SMCOrder(0)]
        [SMCParameter(Name = "Description")] //Para passar a descrição do pai para o no filho (quando for novo)
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoItemSuperior { get; set; }

        [SMCDescription]
        [SMCMaxLength(255)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid10_24)]
        public string Descricao { get; set; }

        [SMCOrder(3)]
        [SMCRequired]
        [SMCSelect(nameof(TiposGruposCurriculares), NameDescriptionField = nameof(DescricaoTipoGrupoCurricular))]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public long SeqTipoGrupoCurricular { get; set; }

        [SMCHidden]
        [SMCInclude("TipoGrupoCurricular")]
        [SMCMapProperty("TipoGrupoCurricular.Descricao")]
        public string DescricaoTipoGrupoCurricular { get; set; }

        [FormacaoEspecificaLookup]
        [SMCDependency(nameof(SeqCurso))]
        [SMCInclude("FormacaoEspecifica")]
        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid8_24)]
        public FormacaoEspecificaLookupViewModel SeqFormacaoEspecifica { get; set; }

        [SMCDetail]
        [SMCInclude("Beneficios")]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<GrupoCurricularBeneficioViewModel> Beneficios { get; set; }

        [SMCDetail]
        [SMCInclude("CondicoesObrigatoriedade")]
        [SMCOrder(6)]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<GrupoCurricularCondicaoObrigatoriedadeViewModel> CondicoesObrigatoriedade { get; set; }

        [SMCGroupedProperty("GrupoConfiguracao")]
        [SMCOrder(7)]
        [SMCRequired]
        [SMCSelect(nameof(ConfiguracoesTiposGruposCurriculares))]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public long SeqTipoConfiguracaoGrupoCurricular { get; set; }

        [SMCDependency(nameof(SeqTipoConfiguracaoGrupoCurricular), nameof(GrupoCurricularController.VerificarTipoConfiguracaoFormatoExigido), "GrupoCurricular", true)]
        [SMCHidden]
        [SMCInclude("TipoConfiguracaoGrupoCurricular")]
        [SMCMapProperty("TipoConfiguracaoGrupoCurricular.ExigeFormato")]
        public bool TipoConfiguracaoExigeFormato { get; set; }

        [SMCConditionalDisplay(nameof(TipoConfiguracaoExigeFormato), SMCConditionalOperation.Equals, true)]
        [SMCConditionalReadonly(nameof(TipoConfiguracaoExigeFormato), SMCConditionalOperation.Equals, false)]
        [SMCConditionalRequired(nameof(TipoConfiguracaoExigeFormato), SMCConditionalOperation.Equals, true)]
        [SMCGroupedProperty("GrupoConfiguracao")]
        [SMCOrder(8)]
        [SMCSelect(nameof(FormatosConfiguracao))]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public FormatoConfiguracaoGrupo? FormatoConfiguracaoGrupo { get; set; }

        [SMCConditionalDisplay(nameof(FormatoConfiguracaoGrupo), SMCConditionalOperation.Equals, new[] { FORMATO_ITENS })]
        [SMCConditionalReadonly(nameof(FormatoConfiguracaoGrupo), SMCConditionalOperation.NotEqual, new[] { FORMATO_ITENS })]
        [SMCConditionalRequired(nameof(FormatoConfiguracaoGrupo), SMCConditionalOperation.Equals, new[] { FORMATO_ITENS })]
        [SMCGroupedProperty("GrupoConfiguracao")]
        [SMCMask("9999")]
        [SMCMinValue(1)]
        [SMCOrder(9)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public short? QuantidadeItens { get; set; }

        [SMCConditionalDisplay(nameof(FormatoConfiguracaoGrupo), SMCConditionalOperation.Equals, new[] { FORMATO_CARGA_HORARIA })]
        [SMCConditionalReadonly(nameof(FormatoConfiguracaoGrupo), SMCConditionalOperation.NotEqual, new[] { FORMATO_CARGA_HORARIA })]
        [SMCConditionalRequired(nameof(FormatoConfiguracaoGrupo), SMCConditionalOperation.Equals, new[] { FORMATO_CARGA_HORARIA })]
        [SMCDependency(nameof(QuantidadeHoraAula), nameof(GrupoCurricularController.CalcularHorasRelogio), "GrupoCurricular", false)]
        [SMCGroupedProperty("GrupoConfiguracao")]
        [SMCMask("9999")]
        [SMCOrder(10)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public short? QuantidadeHoraRelogio { get; set; }

        [SMCConditionalDisplay(nameof(FormatoConfiguracaoGrupo), SMCConditionalOperation.Equals, new[] { FORMATO_CARGA_HORARIA })]
        [SMCConditionalReadonly(nameof(FormatoConfiguracaoGrupo), SMCConditionalOperation.NotEqual, new[] { FORMATO_CARGA_HORARIA })]
        [SMCConditionalRequired(nameof(FormatoConfiguracaoGrupo), SMCConditionalOperation.Equals, new[] { FORMATO_CARGA_HORARIA })]
        [SMCDependency(nameof(QuantidadeHoraRelogio), nameof(GrupoCurricularController.CalcularHorasAula), "GrupoCurricular", false)]
        [SMCGroupedProperty("GrupoConfiguracao")]
        [SMCMask("9999")]
        [SMCOrder(11)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public short? QuantidadeHoraAula { get; set; }

        [SMCConditionalDisplay(nameof(FormatoConfiguracaoGrupo), SMCConditionalOperation.Equals, new[] { FORMATO_CREDITO })]
        [SMCConditionalReadonly(nameof(FormatoConfiguracaoGrupo), SMCConditionalOperation.NotEqual, new[] { FORMATO_CREDITO })]
        [SMCConditionalRequired(nameof(FormatoConfiguracaoGrupo), SMCConditionalOperation.Equals, new[] { FORMATO_CREDITO })]
        [SMCGroupedProperty("GrupoConfiguracao")]
        [SMCMask("9999")]
        [SMCOrder(12)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public short? QuantidadeCreditos { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .ModalSize(sizeNew: SMCModalWindowSize.Largest, sizeEdit: SMCModalWindowSize.Largest, sizeCustom: SMCModalWindowSize.Largest)
                   .IgnoreFilterGeneration()
                   .ButtonBackIndex("Index", "Curriculo")
                   .HeaderIndex(nameof(GrupoCurricularController.CabecalhoGrupoCurricular))
                   .Button(idResource: "NovoGrupoCurricularFilho",
                           action: "Incluir",
                           controller: "GrupoCurricular",
                           securityToken: UC_CUR_001_01_04.MANTER_GRUPO_CURRICULAR,
                           routes: model =>
                           {
                               var node = model as SMCTreeViewNode<GrupoCurricularListarDynamicModel>;
                               return new
                               {
                                   SeqPai = SMCDESCrypto.EncryptNumberForURL(node.Seq),
                                   Description = SMCDESCrypto.EncryptForURL(node.Description),
                                   SeqGrupoSuperior = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqGrupoCurricular ?? 0),
                                   SeqGrupoCurricularSuperior = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqGrupoCurricular ?? 0),
                                   SeqCurriculo = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqCurriculo),
                                   SeqNivelEnsino = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqNivelEnsino)
                               };
                           },
                           isModal: true,
                           buttonBehavior: SMCButtonBehavior.Novo,
                           displayButton: model =>
                           {
                               var item = (model as SMCTreeViewNode<GrupoCurricularListarDynamicModel>).Value;
                               return item.SeqGrupoCurricular.HasValue && item.PermiteGrupos && !item.ContemComponentes;
                           })
                   .Button(idResource: "AssociacaoComponenteCurricular",
                           action: "Incluir",
                           controller: "GrupoCurricularComponente",
                           securityToken: UC_CUR_001_01_07.ASSOCIAR_COMPONENTE_CURRICULAR_GRUPO,
                           routes: model =>
                           {
                               var node = model as SMCTreeViewNode<GrupoCurricularListarDynamicModel>;
                               return new
                               {
                                   SeqGrupoCurricular = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqGrupoCurricular ?? 0),
                                   SeqCurriculo = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqCurriculo),
                                   SeqNivelEnsino = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqNivelEnsino),
                                   FormatoConfiguracaoGrupo = node.Value.FormatoConfiguracaoGrupo,
                                   QuantidadeExigida = 1,
                                   originalController = "GrupoCurricular"
                               };
                           },
                           isModal: true,
                           buttonBehavior: SMCButtonBehavior.Novo,
                           displayButton: model =>
                           {
                               var item = (model as SMCTreeViewNode<GrupoCurricularListarDynamicModel>).Value;
                               return item.SeqGrupoCurricular.HasValue && !item.ContemGrupos;
                           })
                   .Button(idResource: "AlterarGrupoCurricular",
                           action: "Editar",
                           controller: "GrupoCurricular",
                           securityToken: UC_CUR_001_01_04.MANTER_GRUPO_CURRICULAR,
                           routes: model =>
                           {
                               var node = model as SMCTreeViewNode<GrupoCurricularListarDynamicModel>;
                               return new
                               {
                                   SeqPai = node.SeqPai == 0 ? null : SMCDESCrypto.EncryptNumberForURL(node.Seq),
                                   seq = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqGrupoCurricular ?? 0),
                                   originalController = "GrupoCurricular"
                               };
                           },
                           isModal: true,
                           buttonBehavior: SMCButtonBehavior.Alterar,
                           displayButton: model => (model as SMCTreeViewNode<GrupoCurricularListarDynamicModel>).Value.SeqGrupoCurricular.HasValue)
                   .Button(idResource: "ExcluirGrupoCurricular",
                           action: "Excluir",
                           controller: "GrupoCurricular",
                           securityToken: UC_CUR_001_01_04.MANTER_GRUPO_CURRICULAR,
                           routes: model =>
                           {
                               var node = model as SMCTreeViewNode<GrupoCurricularListarDynamicModel>;
                               return new
                               {
                                   Seq = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqGrupoCurricular ?? 0),
                                   SeqCurriculo = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqCurriculo)
                               };
                           },
                           buttonBehavior: SMCButtonBehavior.Excluir,
                           displayButton: model => (model as SMCTreeViewNode<GrupoCurricularListarDynamicModel>).Value.SeqGrupoCurricular.HasValue)
                   .Button(idResource: "VisualizarComponenteCurricular",
                           action: "VerDetalhesPartial",
                           controller: "ComponenteCurricular",
                           securityToken: UC_CUR_002_03_01.VISUALIZAR_DETALHES_COMPONENTE_CURRICULAR,
                           routes: model =>
                           {
                               var node = model as SMCTreeViewNode<GrupoCurricularListarDynamicModel>;
                               return new
                               {
                                   Seq = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqComponenteCurricular ?? 0),
                               };
                           },
                           isModal: true,
                           htmlAttributes: new { data_modal_title = Views.GrupoCurricular.App_LocalResources.UIResource.VisualizarComponenteCurricular_Title }, //data-modal-title
                           buttonBehavior: SMCButtonBehavior.Visualizar,
                           displayButton: model => (model as SMCTreeViewNode<GrupoCurricularListarDynamicModel>).Value.SeqGrupoComponenteCurricular.HasValue)
                   .Button(idResource: "AlterarComponenteCurricular",
                           action: "Editar",
                           controller: "GrupoCurricularComponente",
                           securityToken: UC_CUR_001_01_07.ASSOCIAR_COMPONENTE_CURRICULAR_GRUPO,
                           routes: model =>
                           {
                               var node = model as SMCTreeViewNode<GrupoCurricularListarDynamicModel>;
                               return new
                               {
                                   SeqPai = SMCDESCrypto.EncryptNumberForURL(node.Seq),
                                   seq = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqGrupoComponenteCurricular ?? 0),
                                   SeqCurriculo = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqCurriculo),
                                   SeqNivelEnsino = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqNivelEnsino),
                                   SeqTipoComponenteCurricular = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqTipoComponenteCurricular)
                               };
                           },
                           isModal: true,
                           buttonBehavior: SMCButtonBehavior.Alterar,
                           displayButton: model => (model as SMCTreeViewNode<GrupoCurricularListarDynamicModel>).Value.SeqGrupoComponenteCurricular.HasValue)
                   .Button(idResource: "ExcluirComponenteCurricular",
                           action: "ExcluirGrupoCurricularComponente",
                           controller: "GrupoCurricularComponente",
                           securityToken: UC_CUR_001_01_07.ASSOCIAR_COMPONENTE_CURRICULAR_GRUPO,
                           routes: model =>
                           {
                               var node = model as SMCTreeViewNode<GrupoCurricularListarDynamicModel>;
                               return new
                               {
                                   Seq = new SMCEncryptedLong(node.Value.SeqGrupoComponenteCurricular ?? 0),
                                   SeqCurriculo = new SMCEncryptedLong(node.Value.SeqCurriculo)
                               };
                           },
                           buttonBehavior: SMCButtonBehavior.Excluir,
                           displayButton: model => (model as SMCTreeViewNode<GrupoCurricularListarDynamicModel>).Value.SeqGrupoComponenteCurricular.HasValue,                           
                           confirm: model => new Framework.Dynamic.SMCDynamicConfirm
                           {
                               Title = Views.GrupoCurricularComponente.App_LocalResources.UIResource.Listar_Excluir_Titulo,
                               Message = string.Format(Views.GrupoCurricularComponente.App_LocalResources.UIResource.Listar_Excluir_Confirmacao, (model as SMCTreeViewNode<GrupoCurricularListarDynamicModel>).Value.DescricaoFormatada)
                           })
                   .ConfigureContextMenuItem((button, model, action) =>
                   {
                       if (action != SMCDynamicButtonAction.Custom)
                           button.Hide(true);
                   })
                   .TreeView(treeOpen: false, configureNode: x => (x as SMCTreeViewNode<GrupoCurricularListarDynamicModel>).Value.SeqGrupoCurricular.HasValue ? (x as SMCTreeViewNode<GrupoCurricularListarDynamicModel>).Options.CssClass = "smc-sga-treeview-item-destaque" : null)
                   .Service<IGrupoCurricularService>(index: nameof(IGrupoCurricularService.BuscarGruposCurricularesDescricaoSimplesTree),
                                                     insert: nameof(IGrupoCurricularService.BuscarConfiguracao),
                                                     save: nameof(IGrupoCurricularService.SalvarGrupoCurricular))
                   .Tokens(tokenInsert: UC_CUR_001_01_04.MANTER_GRUPO_CURRICULAR,
                           tokenEdit: UC_CUR_001_01_04.MANTER_GRUPO_CURRICULAR,
                           tokenRemove: UC_CUR_001_01_04.MANTER_GRUPO_CURRICULAR,
                           tokenList: UC_CUR_001_01_03.PESQUISAR_GRUPO_CURRICULAR);
        }

        #endregion [ Configurações ]
    }
}