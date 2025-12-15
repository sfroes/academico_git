using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.FIN.Controllers;
using SMC.SGA.Administrativo.Areas.FIN.Views.PessoaAtuacaoBeneficio.App_LocalResources;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class PessoaAtuacaoBeneficioDynamicModel : SMCDynamicViewModel
    {
        #region DataSource

        [SMCDataSource(dataSource: "Beneficio")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IPessoaAtuacaoBeneficio), nameof(IPessoaAtuacaoBeneficio.BuscarsBeneficiosSelect), values: new string[] { nameof(SeqPessoaAtuacao) })]
        public List<SMCDatasourceItem> Beneficios { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IPessoaAtuacaoBeneficio), nameof(IPessoaAtuacaoBeneficio.BuscarConfiguracoesBeneficiosSelect), values: new string[] { nameof(SeqBeneficio) })]
        public List<SMCDatasourceItem> ConfiguracaoBeneficios { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> SituacoesChancelaBeneficio { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IPessoaAtuacaoBeneficio), nameof(IPessoaAtuacaoBeneficio.BuscarTipoResponsavelFinanceiroSelect), values: new string[] { nameof(SeqBeneficio) })]
        public List<SMCSelectListItem> ListaTipoResponsavelFinanceiro { get; set; }

        #endregion DataSource

        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public bool Aluno { get; set; }

        [SMCRequired]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCConditionalReadonly(nameof(CamposSomenteLeituraSituacaoBeneficio), true, PersistentValue = true, RuleName = "SBR1")]
        [SMCConditionalReadonly(nameof(Seq), SMCConditionalOperation.GreaterThen, 0, PersistentValue = true, RuleName = "SBR2")]
        [SMCConditionalRule("SBR1 || SBR2")]
        [SMCSelect(nameof(Beneficios), SortBy = SMCSortBy.Description, AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public long? SeqBeneficio { get; set; }

        [SMCDependency(nameof(SeqBeneficio), nameof(PessoaAtuacaoBeneficioController.BuscarConfiguracoesBeneficiosSelect), "PessoaAtuacaoBeneficio", true, new string[] { nameof(SeqPessoaAtuacao) })]
        [SMCConditionalReadonly(nameof(IdDeducaoValorParcelaTitular), SMCConditionalOperation.Equals, false, PersistentValue = true, RuleName = "SCBR1")]
        [SMCConditionalReadonly(nameof(CamposSomenteLeituraSituacaoBeneficio), true, PersistentValue = true, RuleName = "SCBR2")]
        [SMCConditionalRule("SCBR1 || SCBR2")]
        [SMCConditionalRequired(nameof(IdDeducaoValorParcelaTitular), SMCConditionalOperation.Equals, true)]
        [SMCSelect(nameof(ConfiguracaoBeneficios), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public long? SeqConfiguracaoBeneficio { get; set; }

        [SMCDependency(nameof(IncideParcelaMatricula), nameof(PessoaAtuacaoBeneficioController.BuscarDataAdmissaoIngressante), "PessoaAtuacaoBeneficio", false, new string[] { nameof(SeqPessoaAtuacao) })]
        [SMCConditionalReadonly(nameof(IncideParcelaMatricula), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "DIVR1")]
        [SMCConditionalReadonly(nameof(CamposSomenteLeituraSituacaoBeneficio), true, PersistentValue = true, RuleName = "DIVR2")]
        [SMCConditionalRule("DIVR1 || DIVR2")]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        public DateTime DataInicioVigencia { get; set; }

        [SMCMinDate(nameof(DataInicioVigencia))]
        [SMCConditionalReadonly(nameof(CamposSomenteLeituraSituacaoBeneficio), true, PersistentValue = true)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        public DateTime? DataFimVigencia { get; set; }

        [SMCDependency(nameof(SeqConfiguracaoBeneficio), nameof(PessoaAtuacaoBeneficioController.BuscarDescricaoTipoDeducao), "PessoaAtuacaoBeneficio", true)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid6_24)]
        public string TipoDeducaoDescricao { get; set; }

        [SMCDependency(nameof(SeqConfiguracaoBeneficio), nameof(PessoaAtuacaoBeneficioController.BuscarIdTipoDeducao), "PessoaAtuacaoBeneficio", true)]
        [SMCHidden]
        public int IdTipoDeducao { get; set; }

        [SMCDependency(nameof(SeqConfiguracaoBeneficio), nameof(PessoaAtuacaoBeneficioController.BuscarDescricaoFormaDeducao), "PessoaAtuacaoBeneficio", true)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid5_24)]
        public string FormaDeducaoDescricao { get; set; }

        [SMCDependency(nameof(SeqConfiguracaoBeneficio), nameof(PessoaAtuacaoBeneficioController.BuscarIdFormaDeducao), "PessoaAtuacaoBeneficio", true)]
        [SMCHidden]
        public int IdFormaDeducao { get; set; }

        [SMCDependency(nameof(SeqConfiguracaoBeneficio), nameof(PessoaAtuacaoBeneficioController.BuscarFormaDeducaoEnum), "PessoaAtuacaoBeneficio", true)]
        [SMCHidden]
        public FormaDeducao FormaDeducao { get; set; }

        [SMCDependency(nameof(SeqConfiguracaoBeneficio), nameof(PessoaAtuacaoBeneficioController.BuscarTipoDeducaoEnum), "PessoaAtuacaoBeneficio", true)]
        [SMCHidden]
        public TipoDeducao TipoDeducao { get; set; }

        [SMCIgnoreProp(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.ReadOnly)]
        public decimal? ValorBeneficio
        {
            get { return this.ValorBeneficioMoeda ?? this.ValorBeneficioPercentual; }
            set
            {
                if (this.IdFormaDeducao == (int)SMC.Academico.Common.Areas.FIN.Enums.FormaDeducao.PercentualBolsa)
                {
                    this.ValorBeneficioPercentual = value;
                }
                else
                {
                    this.ValorBeneficioMoeda = value;
                }
            }
        }

        [SMCDependency(nameof(SeqConfiguracaoBeneficio), nameof(PessoaAtuacaoBeneficioController.BuscarValorConfiguracaoBeneficio), "PessoaAtuacaoBeneficio", false, new string[] { nameof(Seq) })]
        [SMCConditionalDisplay(nameof(IdFormaDeducao), SMCConditionalOperation.Equals, (int)FormaDeducao.PercentualBolsa)]
        [SMCConditionalReadonly(nameof(CamposSomenteLeituraSituacaoBeneficio), true, PersistentValue = true, RuleName = "VBPR1")]
        [SMCConditionalReadonly(nameof(TipoDeducao), SMCConditionalOperation.Equals, TipoDeducao.Fixo, PersistentValue = true, RuleName = "VBPR2")]
        [SMCConditionalRule("VBPR1 || VBPR2")]
        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCMapForceFromTo]
        [SMCMinValue(0)]
        [SMCMaxValue(100)]
        [SMCCurrency]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid5_24)]
        public decimal? ValorBeneficioPercentual { get; set; }

        [SMCDependency(nameof(SeqConfiguracaoBeneficio), nameof(PessoaAtuacaoBeneficioController.BuscarValorConfiguracaoBeneficio), "PessoaAtuacaoBeneficio", false, new string[] { nameof(Seq) })]
        [SMCConditionalDisplay(nameof(IdFormaDeducao), SMCConditionalOperation.NotEqual, (int)SMC.Academico.Common.Areas.FIN.Enums.FormaDeducao.PercentualBolsa)]
        [SMCConditionalReadonly(nameof(CamposSomenteLeituraSituacaoBeneficio), true, PersistentValue = true, RuleName = "VBMR1")]
        [SMCConditionalReadonly(nameof(TipoDeducao), SMCConditionalOperation.Equals, TipoDeducao.Fixo, PersistentValue = true, RuleName = "VBMR2")]
        [SMCConditionalRule("VBMR1 || VBMR2")]
        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCMapForceFromTo]
        [SMCMinValue(0)]
        [SMCCurrency]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid5_24)]
        public decimal? ValorBeneficioMoeda { get; set; }

        [SMCFocus]
        [SMCConditionalReadonly(nameof(CamposSomenteLeituraTokenChancela), true, PersistentValue = true, RuleName = "SCR1")]
        [SMCConditionalRule("SCR1")]
        [SMCSelect(nameof(SituacoesChancelaBeneficio), autoSelectSingleItem: true)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid6_24)]
        public int SeqSituacaoChancelaBeneficioAtual { get; set; }

        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        public bool ExibeValoresTermoAdesao { get; set; }

        [SMCHidden(SMCViewMode.Insert)]
        [SMCDependency(nameof(DesabilitarJustificativa), nameof(PessoaAtuacaoBeneficioController.ConteudoJustificativa), "PessoaAtuacaoBeneficio", false, new string[] { nameof(JustificativaBanco) })]
        [SMCConditionalReadonly(nameof(DesabilitarJustificativa), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCConditionalRequired(nameof(SeqSituacaoChancelaBeneficioAtual), SMCConditionalOperation.Equals, (int)SituacaoChancelaBeneficio.AguardandoChancela, RuleName = "RJR1")]
        [SMCConditionalRequired(nameof(SeqSituacaoChancelaBeneficioAtual), SMCConditionalOperation.Equals, (int)SituacaoChancelaBeneficio.Indeferido, RuleName = "RJR2")]
        [SMCConditionalRequired(nameof(DesabilitarJustificativa), SMCConditionalOperation.Equals, false, RuleName = "RJR3")]
        [SMCConditionalRule("(RJR1 || RJR2) && RJR3")]
        [SMCSize(SMCSize.Grid24_24)]
        public string Justificativa { get; set; }

        [SMCHidden]
        public string JustificativaBanco
        {
            get
            {
                return Justificativa;

            }
        }

        [SMCHidden]
        [SMCDependency(nameof(SeqSituacaoChancelaBeneficioAtual), nameof(PessoaAtuacaoBeneficioController.DesabilitarJustificativa), "PessoaAtuacaoBeneficio", false, new string[] { nameof(SeqSituacaoChancelaBeneficioAtualBanco) })]
        public bool? DesabilitarJustificativa { get; set; }

        [SMCConditionalReadonly(nameof(CamposSomenteLeituraSituacaoBeneficio), true, PersistentValue = true, RuleName = "IPR1")]
        [SMCConditionalRule("IPR1")]
        [SMCDependency(nameof(SeqBeneficio), nameof(PessoaAtuacaoBeneficioController.BuscarIncideParcelaSelect), "PessoaAtuacaoBeneficio", false, new string[] { nameof(SeqPessoaAtuacao), nameof(Aluno) })]
        [SMCConditionalRequired(nameof(Aluno), false)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid5_24)]
        public bool? IncideParcelaMatricula { get; set; }

        [SMCDependency(nameof(SeqBeneficio), nameof(PessoaAtuacaoBeneficioController.VerificarExisteConfiguracaoBeneficio), "PessoaAtuacaoBeneficio", true, new string[] { nameof(SeqPessoaAtuacao) })]
        [SMCHidden]
        public bool IncideParcelaMatriculaBanco { get; set; }

        /// <summary>
        /// NV11
        /// </summary>
        [SMCDependency(nameof(SeqBeneficio), nameof(PessoaAtuacaoBeneficioController.VerificarExisteConfiguracaoBeneficio), "PessoaAtuacaoBeneficio", true, new string[] { nameof(SeqPessoaAtuacao) })]
        [SMCHidden]
        public bool ExisteConfiguracaoBeneficio { get; set; }

        [SMCConditionalDisplay(nameof(IncideParcelaMatricula), SMCConditionalOperation.Equals, false, RuleName = "R3")]
        [SMCConditionalDisplay(nameof(IncideParcelaMatriculaBanco), SMCConditionalOperation.Equals, true, RuleName = "R4")]
        [SMCConditionalRule("R3 && R4")]
        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-alerta")]
        [SMCDisplay]
        [SMCHidden(SMCViewMode.List)]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemInformativa { get; set; } = UIResource.Texto_MensagemInformativa;

        [SMCDetail]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<PessoaAtuacaoBeneficioBeneficioHistoricoSituacaoViewModel> HistoricoSituacoes { get; set; }

        [SMCConditionalDisplay(nameof(IdAssociarResponsavelFinanceiro), SMCConditionalOperation.NotEqual, (int)SMC.Academico.Common.Areas.FIN.Enums.AssociarResponsavelFinanceiro.NaoPermite)]
        [SMCConditionalReadonly(nameof(CamposSomenteLeituraSituacaoBeneficio), true, PersistentValue = true, RuleName = "RFR1")]
        [SMCConditionalRule("RFR1")]
        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<PessoaAtuacaoBeneficioResponsavelFinanceiroViewModel> ResponsaveisFinanceiro { get; set; }

        [SMCConditionalReadonly(nameof(CamposSomenteLeituraSituacaoBeneficio), true, PersistentValue = true, RuleName = "CFR1")]
        [SMCConditionalRule("CFR1")]
        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<PessoaAtaucaoBeneficioControleFinanceiroViewModel> ControlesFinanceiros { get; set; }

        [SMCConditionalReadonly(nameof(CamposSomenteLeituraSituacaoBeneficio), true, PersistentValue = true)]
        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<PessoaAtuacaoBeneficioAnexoViewModel> ArquivosAnexo { get; set; }

        #region Campos validação

        [SMCDependency(nameof(SeqBeneficio), nameof(PessoaAtuacaoBeneficioController.BuscarIdAssociarResponsavelFinanceiro), "PessoaAtuacaoBeneficio", true)]
        [SMCHidden]
        public int? IdAssociarResponsavelFinanceiro { get; set; }

        [SMCDependency(nameof(SeqBeneficio), nameof(PessoaAtuacaoBeneficioController.VerificarExisteResponsaveisFinanceiros), "PessoaAtuacaoBeneficio", true)]
        [SMCHidden]
        public bool? IdExisteResponsaveisFinanceiros { get; set; }

        [SMCDependency(nameof(SeqBeneficio), nameof(PessoaAtuacaoBeneficioController.BuscarIdDeducaoValorParcelaTitular), "PessoaAtuacaoBeneficio", true)]
        [SMCHidden]
        public bool? IdDeducaoValorParcelaTitular { get; set; }

        [SMCHidden]
        public bool CamposSomenteLeituraSituacaoBeneficio
        {
            ///RN_FIN_003 - Pessoa Atuação x Benefício - Pré-condições
            get
            {
                /* - Se a situação for igual a “Aguardando Chancela”:
                Todos os campos deverão ser exibidos / habilitados de acordo com suas respectivas regras de navegação e regras de negócio.
                - Senão, se a situação for igual a “Cancelado”:
                Todos os campos deverão ser exibidos como somente leitura(desabilitados).
                - Senão, se a situação for igual a “Deferido”:
                Todos os campos deverão ser exibidos como somente leitura(desabilitados).  
                - Senão, se a situação for igual a “Indeferido”:
                Todos os campos deverão ser exibidos como somente leitura(desabilitados).*/
                switch (this.SeqSituacaoChancelaBeneficioAtual)
                {
                    //case (int)SituacaoChancelaBeneficio.Cancelado:
                    //    return true;
                    case (int)SituacaoChancelaBeneficio.Deferido:
                        return true;
                    case (int)SituacaoChancelaBeneficio.Indeferido:
                        return true;
                    default:
                        return false;
                }
            }
        }

        [SMCHidden]
        public bool ParametroSetorBolsa { get; set; }

        [SMCHidden]
        public bool CamposSomenteLeituraTokenChancela
        {
            get
            {
                if (SMCAuthorizationHelper.Authorize(new string[] { UC_FIN_001_03_02.PERMITE_ALTERAR_CHANCELA_BENEFICIO }) && ParametroSetorBolsa)
                    return false;

                if (SMCAuthorizationHelper.Authorize(new string[] { UC_FIN_001_03_02.CHANCELA_SETOR_FINANCEIRO }) && !ParametroSetorBolsa)
                    return false;

                return true;
            }
        }

        [SMCDependency(nameof(SeqBeneficio), nameof(PessoaAtuacaoBeneficioController.DesabilitarDataFimBeneficio), "PessoaAtuacaoBeneficio", true)]
        [SMCHidden]
        public bool? DesablilitarDataFim { get; set; }

        [SMCDependency(nameof(SeqConfiguracaoBeneficio), nameof(PessoaAtuacaoBeneficioController.DesabilitaCampoValorPorConfiguracaoBeneficio), "PessoaAtuacaoBeneficio", false)]
        [SMCHidden]
        public bool? DesabilitaCampoValorPorConfiguracaoBeneficio { get; set; }

        /// <summary>
        /// Sequencial situação incial do beneficio atual
        /// </summary>
        [SMCHidden]
        public int SeqSituacaoChancelaBeneficioAtualBanco
        {
            get
            {
                return this.SeqSituacaoChancelaBeneficioAtual;
            }
        }

        [SMCIgnoreProp]
        public string MensagemConfirmacaoAssert { get; set; }

        #endregion

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .EditInModal(allowSaveNew: false, refreshIndexPageOnSubmit: true)
                .ModalSize(SMCModalWindowSize.Largest)
                .Assert("_ConfirmacaoAssertPessoaAtuacaoBeneficio", (service, model) =>
                 {
                     ///[CONSISTÊNCIA DO NRO DE PARCELAS DE ACORDO COM A CONFIGURAÇÃO DO BENEFÍCIO]
                     ///Se posssui condição de pagamento preenchido, verificar se a quantidade de parcelas da condição
                     ///de pagamento da solicitação de matrícula é diferente do número de parcelas padrão definido no
                     ///benefício que está sendo associado.
                     ///Se for diferente a quantidade de parcelas, exibir mensagem de confirmação: "Para associar esse
                     ///benefício será necessário alterar a condição de pagamento que o ingressante selecionou.Confirma a
                     ///alteração ? "
                     var modelPessoaAtuacao = (model as PessoaAtuacaoBeneficioDynamicModel);
                     var servicePessoaAtaucao = service.Create<IPessoaAtuacaoBeneficio>();

                     if (servicePessoaAtaucao.ValidarNumeroParcelasParametrizadosConfiguracaoSaoDiferentes(modelPessoaAtuacao.SeqPessoaAtuacao, (long)modelPessoaAtuacao.SeqBeneficio))
                     {
                         return true;
                     }

                     return false;
                 },
                (service, model) =>
                {
                    var modelPessoaAtuacao = (model as PessoaAtuacaoBeneficioDynamicModel);
                    var servicePessoaAtaucao = service.Create<IPessoaAtuacaoBeneficio>();
                    var retorno = new PessoaAtuacaoBeneficioDynamicModel() { MensagemConfirmacaoAssert = string.Empty };

                    if (modelPessoaAtuacao.Aluno)
                    {
                        retorno.MensagemConfirmacaoAssert = UIResource.MensagemConfirmacaoAluno;
                    }
                    else
                    {
                        retorno.MensagemConfirmacaoAssert = UIResource.MensagemConfirmacaoIngressante;
                    }

                    return retorno;
                })
                .RequiredIncomingParameters(nameof(SeqPessoaAtuacao))
                .HeaderIndex("CabecalhoPessoaAtuacaoBeneficio")
                .Header("CabecalhoPessoaAtuacaoBeneficio")
                .ViewPartialInsert("_PessoaAtuacaoBeneficio")
                .ViewPartialEdit("_PessoaAtuacaoBeneficio")
                .ButtonBackIndex(nameof(PessoaAtuacaoBeneficioController.Voltar), "PessoaAtuacaoBeneficio",
                    x => new { seqPessoaAtuacao = SMCDESCrypto.EncryptNumberForURL(((PessoaAtuacaoBeneficioFiltroDynamicModel)x).SeqPessoaAtuacao) })
                .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                            ((PessoaAtuacaoBeneficioListarDynamicModel)x).DescricaoBeneficio,
                                            ((PessoaAtuacaoBeneficioListarDynamicModel)x).NomePessoa))
                .Detail<PessoaAtuacaoBeneficioListarDynamicModel>("_DetailList", allowSort: false, hideNavigator: true)
                .Tokens(tokenList: UC_FIN_001_03_01.PESQUISAR_ASSOCIACAO_BENEFICIO_PESSOA_ATUACAO,
                        tokenInsert: UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO,
                        tokenEdit: UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO,
                        tokenRemove: UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)
                .Service<IPessoaAtuacaoBeneficio>(index: nameof(IPessoaAtuacaoBeneficio.BuscarPesssoasAtuacoesBeneficios),
                                                  save: nameof(IPessoaAtuacaoBeneficio.SalvarPessoaAtuacaoBeneficio),
                                                  edit: nameof(IPessoaAtuacaoBeneficio.AlterarPessoaAtuacaoBeneficio),
                                                  insert: nameof(IPessoaAtuacaoBeneficio.BuscarAssociacaoBeneficio),
                                                  delete: nameof(IPessoaAtuacaoBeneficio.ExcluirPesssoaAtuacaoBeneficio));
        }
    }
}