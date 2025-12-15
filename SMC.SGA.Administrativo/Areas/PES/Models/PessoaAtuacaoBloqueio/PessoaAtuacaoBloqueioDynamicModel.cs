using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.UI.Mvc.Areas.PES.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.PES.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "Identificacao", Size = SMCSize.Grid24_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "Desbloqueio", Size = SMCSize.Grid24_24)]
    public class PessoaAtuacaoBloqueioDynamicModel : SMCDynamicViewModel
    {
        #region DataSources

        [SMCIgnoreProp]
        [SMCDataSource("TipoBloqueio")]
        [SMCInclude(ignore: true)]
        [SMCServiceReference(typeof(ITipoBloqueioService), nameof(ITipoBloqueioService.BuscarTiposBloqueiosSelect))]
        public List<SMCDatasourceItem> TiposBloqueio { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource("MotivoBloqueio")]
        public List<SMCDatasourceItem> MotivosBloqueio { get; set; }

        #endregion DataSources

        #region Propriedades de apoio

        [SMCHidden]
        [SMCParameter]
        public long SeqPessoaAtuacaoBloqueio { get { return this.Seq; } }

        /*Ao abrir a tela para edição de um bloqueio, deverão ser avaliados os seguintes critérios:*/
        /* Se o cadastro foi feito por uma integração: 
         *  Todos os campos da tela de bloqueio deverão ser exibidos como somente leitura, exceto o campo Observação.*/
        [SMCHidden]
        public bool CadastroIntegracao { get; set; }

        /*Se o cadastro não foi feito por uma integração, deverá ser avaliada a situação: 
         * Se a situação for igual a Desbloqueado e o tipo do desbloqueio igual a Efetivo: 
         *  Todos os campos da tela de bloqueio deverão ser exibidos como somente leitura.*/
        [SMCHidden]
        //public bool PossuiDesbloqueio { get { return this.SituacaoBloqueio == SituacaoBloqueio.Desbloqueado; } }
        public bool PossuiDesbloqueio { get { return SituacaoBloqueio == SituacaoBloqueio.Desbloqueado && this.TipoDesbloqueio == TipoDesbloqueio.Efetivo; } }

        /*Se o motivo do bloqueio permite o cadastro de itens, todos os campos da tela de item de bloqueio deverão ser 
          exibidos como somente leitura e, não deverá ser permitido associar, excluir ou editar um item de bloqueio. Caso 
          contrário, a seção de itens do bloqueio, não deverá ser exibida, conforme a NV06.*/
        /*NV06
      A seção Itens do bloqueio deverá ser exibida de acordo com a parametrização do motivo do bloqueio: 
      · Se não permite o cadastro de itens, a seção não deverá ser exibida. 
      · Se permite o cadastro de itens, a seção deverá ser exibida e deverá ser associado pelo menos um item.*/
        [SMCHidden]
        [SMCDependency(nameof(SeqMotivoBloqueioAuxiliar), nameof(PessoaAtuacaoBloqueioController.MotivoBloqueioPermiteItens), "PessoaAtuacaoBloqueio", true)]
        public bool? PermiteItens { get; set; }

        [SMCHidden]
        public bool? BloqueioSomenteLeitura { get => CadastroIntegracao || PossuiDesbloqueio; }

        #endregion Propriedades de apoio

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        [SMCReadOnly]
        [SMCOrder(0)]
        [SMCKey]
        [SMCGroupedProperty("Identificacao")]
        public override long Seq { get; set; }

        [SMCOrder(1)]
        [PessoaAtuacaoLookup]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid17_24, SMCSize.Grid20_24)]
        [SMCRequired]
        [SMCInclude(ignore: true)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCGroupedProperty("Identificacao")]
        public PessoaAtuacaoLookupViewModel SeqPessoaAtuacao { get; set; }

        [SMCOrder(2)]
        [SMCRequired]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCGroupedProperty("Identificacao")]
        [SMCSelect]
        public SituacaoBloqueio SituacaoBloqueio { get; set; }

        [SMCOrder(3)]
        [SMCSelect(nameof(TiposBloqueio), SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid8_24)]
        [SMCRequired]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCGroupedProperty("Identificacao")]
        public long? SeqTipoBloqueio { get; set; }

        //[SMCOrder(4)]
        //[SMCSelect(nameof(MotivosBloqueio), NameDescriptionField = nameof(DescricaoMotivoBloqueio))]
        //[SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid17_24, SMCSize.Grid12_24)]
        //[SMCRequired]
        //[SMCReadOnly(SMCViewMode.Edit)]
        //[SMCGroupedProperty("Identificacao")]
        //[SMCDependency(nameof(SeqTipoBloqueio), nameof(MotivoBloqueioController.BuscarMotivosBloqueioSelect), "MotivoBloqueio", true)]
        //[SMCDependency(nameof(SeqTipoBloqueio), nameof(MotivoBloqueioController.BuscarMotivosBloqueioInstituicaoEditSelect), "MotivoBloqueio", true)]
        //public long? SeqMotivoBloqueio { get; set; }

        //FORAM CRIADAS AS VARIÁVEIS SeqMotivoBloqueioAuxiliar, SeqMotivoBloqueioAuxiliarEdita, SeqMotivoBloqueioAuxiliarSalvar
        //PORQUE POR ALGUM MOTIVO SE TIVER O SEQMOTIVOBLOQUEIO NO PESSOAATUACAOBLOQUEIODATA, O DEPENDENCY NÃO FUNCIONA,
        //E AO SELECIONAR UM TIPO DE BLOQUEIO O CAMPO MOTIVO DE BLOQUEIO PERMANECE READONLY
        //DESSA FORMA, SeqMotivoBloqueioAuxiliar SÓ EXISTE NO DYNAMIC, E PARA SALVAR O VALOR SELECIONADO É UTILIZADA A 
        //SeqMotivoBloqueioAuxiliarSalvar, E PARA CARREGAR O VALOR SALVO É UTILIZADA A SeqMotivoBloqueioAuxiliarEditar
        [SMCOrder(4)]
        [SMCSelect(nameof(MotivosBloqueio), NameDescriptionField = nameof(DescricaoMotivoBloqueio))]
        [SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid17_24, SMCSize.Grid12_24)]
        [SMCRequired]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCGroupedProperty("Identificacao")]
        [SMCDependency(nameof(SeqTipoBloqueio), nameof(MotivoBloqueioController.BuscarMotivosBloqueioFormatoManualAmbosSelect), "MotivoBloqueio", true)]
        public long? SeqMotivoBloqueioAuxiliar { get; set; }

        [SMCHidden]
        public long SeqMotivoBloqueioAuxiliarEditar { get; set; }

        [SMCHidden]
        public long? SeqMotivoBloqueioAuxiliarSalvar
        {
            get
            {
                return SeqMotivoBloqueioAuxiliar;
            }
        }

        [SMCHidden]
        [SMCIgnoreProp]
        public string DescricaoMotivoBloqueio { get; set; }

        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        [SMCGroupedProperty("Identificacao")]
        [SMCRequired]
        [SMCConditionalReadonly(nameof(TipoDesbloqueio), SMCConditionalOperation.Equals, TipoDesbloqueio.Efetivo, PersistentValue = true)]
        public DateTime DataBloqueio { get; set; }

        [SMCOrder(6)]
        [SMCReadOnly]
        [SMCGroupedProperty("Identificacao")]
        [SMCDependency(nameof(SeqPessoaAtuacao) + ".Descricao")]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid17_24, SMCSize.Grid24_24)]
        public string DescricaoReferenciaAtuacao { get; set; }

        [SMCOrder(7)]
        [SMCDescription]
        [SMCMaxLength(1000)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCMultiline]
        [SMCConditionalRequired(nameof(PermiteItens), SMCConditionalOperation.Equals, false)]
        [SMCConditionalDisplay(nameof(PermiteItens), SMCConditionalOperation.Equals, false)]
        [SMCConditionalReadonly(nameof(CadastroIntegracao), SMCConditionalOperation.Equals, true, RuleName = "RContemIntegracao")]
        [SMCConditionalReadonly(nameof(Seq), SMCConditionalOperation.NotEqual, 0, RuleName = "REdicao")]
        [SMCConditionalReadonly(nameof(TipoDesbloqueio), SMCConditionalOperation.Equals, TipoDesbloqueio.Efetivo, PersistentValue = true, RuleName = "RDesbloqueioEfetivo")]
        [SMCConditionalRule("(RContemIntegracao && REdicao) || RDesbloqueioEfetivo")]
        [SMCGroupedProperty("Identificacao")]
        public string Descricao { get; set; }

        [SMCOrder(8)]
        [SMCMaxLength(1000)]
        [SMCMultiline]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCGroupedProperty("Identificacao")]
        [SMCConditionalReadonly(nameof(TipoDesbloqueio), SMCConditionalOperation.Equals, TipoDesbloqueio.Efetivo, PersistentValue = true)]
        public string Observacao { get; set; }

        [SMCOrder(9)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        [SMCGroupedProperty("Identificacao")]
        public DateTime? DataCriacao { get; set; }

        [SMCOrder(10)]
        [SMCReadOnly]
        [SMCHidden(SMCViewMode.Insert)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        [SMCGroupedProperty("Identificacao")]
        public DateTime? UltimaAtualizacao { get; set; }

        [SMCOrder(11)]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid16_24)]
        [SMCReadOnly]
        [SMCGroupedProperty("Identificacao")]
        public string ResponsavelBloqueio { get; set; }

        #region [Informações do Desbloqueio]

        /*NV09 A seção de Informações de Desbloqueio deverá ser exibida somente se a situação for igual a Desbloqueado. 
         * E todos os dados deverão ser exibidos como somente leitura.*/
        [SMCHidden]
        public bool? ExibirDesbloqueio { get => SituacaoBloqueio == SituacaoBloqueio.Desbloqueado; }

        [SMCOrder(12)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCHidden(SMCViewMode.Insert)]
        [SMCGroupedProperty("Desbloqueio")]
        [SMCSelect]
        [SMCConditionalDisplay(nameof(ExibirDesbloqueio), SMCConditionalOperation.Equals, true)]
        public TipoDesbloqueio TipoDesbloqueio { get; set; }

        [SMCOrder(13)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCCssClass("smc-clear")]
        [SMCHidden(SMCViewMode.Insert)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCGroupedProperty("Desbloqueio")]
        [SMCConditionalDisplay(nameof(ExibirDesbloqueio), SMCConditionalOperation.Equals, true)]
        public DateTime? DataDesbloqueioTemporario { get; set; }

        [SMCOrder(14)]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCHidden(SMCViewMode.Insert)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCGroupedProperty("Desbloqueio")]
        [SMCConditionalDisplay(nameof(ExibirDesbloqueio), SMCConditionalOperation.Equals, true)]
        public string UsuarioDesbloqueioTemporario { get; set; }

        [SMCOrder(15)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCHidden(SMCViewMode.Insert)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCGroupedProperty("Desbloqueio")]
        [SMCConditionalDisplay(nameof(ExibirDesbloqueio), SMCConditionalOperation.Equals, true)]
        public DateTime? DataDesbloqueioEfetivo { get; set; }

        [SMCOrder(16)]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCHidden(SMCViewMode.Insert)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCGroupedProperty("Desbloqueio")]
        [SMCConditionalDisplay(nameof(ExibirDesbloqueio), SMCConditionalOperation.Equals, true)]
        public string UsuarioDesbloqueioEfetivo { get; set; }

        [SMCOrder(17)]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCMultiline]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCHidden(SMCViewMode.Insert)]
        [SMCGroupedProperty("Desbloqueio")]
        [SMCConditionalDisplay(nameof(ExibirDesbloqueio), SMCConditionalOperation.Equals, true)]
        public string JustificativaDesbloqueio { get; set; }

        #endregion [Informações do Desbloqueio]

        [SMCOrder(18)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(ExibirDesbloqueio), SMCConditionalOperation.Equals, true)]
        [SMCDetail(SMCDetailType.Block, DisplayAsGrid = true, HideMasterDetailButtons = true)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCHidden(SMCViewMode.Insert)]
        public SMCMasterDetailList<PessoaAtuacaoBloqueioComprovanteViewModel> Comprovantes { get; set; }

        [SMCOrder(19)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCDetail(type: SMCDetailType.Modal, DisplayAsGrid = true)]
        [SMCConditionalDisplay(nameof(PermiteItens), SMCConditionalOperation.Equals, true)]
        [SMCReadOnly(SMCViewMode.Edit)]
        public SMCMasterDetailList<PessoaAtuacaoBloqueioItemViewModel> Itens { get; set; }

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Service<IPessoaAtuacaoBloqueioService>(index: nameof(IPessoaAtuacaoBloqueioService.BuscarPessoasAtuacoesBloqueios),
                                                           insert: nameof(IPessoaAtuacaoBloqueioService.PreencherModeloInserirPessoaAtuacaoBloqueio),
                                                           edit: nameof(IPessoaAtuacaoBloqueioService.PreencherModeloAlterarPessoaAtuacaoBloqueio),
                                                           save: nameof(IPessoaAtuacaoBloqueioService.SalvarPessoaAtuacaoBloqueio))
                   .Tokens(tokenInsert: UC_PES_004_03_02.MANTER_BLOQUEIO,
                           tokenEdit: UC_PES_004_03_02.MANTER_BLOQUEIO,
                           tokenRemove: UC_PES_004_03_02.MANTER_BLOQUEIO,
                           tokenList: UC_PES_004_03_01.PESQUISAR_BLOQUEIO)
                   .Detail<PessoaAtuacaoBloqueioListarDynamicModel>("_DetailList")
                   .DisableInitialListing(true);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);

            if (viewMode == SMCViewMode.Edit)
                SeqMotivoBloqueioAuxiliar = SeqMotivoBloqueioAuxiliarEditar;
        }

        #endregion Configurações
    }
}