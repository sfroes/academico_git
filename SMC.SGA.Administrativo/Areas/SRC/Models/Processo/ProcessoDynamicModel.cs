using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Common.Enums;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.ORG.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "AgendamentoSAT", Size = SMCSize.Grid24_24)]
    public class ProcessoDynamicModel : SMCDynamicViewModel
    {
        #region Data Sources

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IProcessoUnidadeResponsavelService), nameof(IProcessoUnidadeResponsavelService.BuscarUnidadesResponsaveisVinculadasProcessoSelect))]
        public List<SMCDatasourceItem> EntidadesSuperiores { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoServicoService), nameof(ITipoServicoService.BuscarTiposServicosPorInstituicaoNivelEnsinoSelect))]
        public List<SMCDatasourceItem> TiposServico { get; set; } = new List<SMCDatasourceItem>();

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IServicoService), nameof(IServicoService.BuscarServicosPorTipoServicoSelect), values: new string[] { nameof(SeqTipoServico) })]
        public List<SMCDatasourceItem> Servicos { get; set; }

        #endregion Data Sources

        #region Propriedades Auxiliares

        [SMCHidden]
        public bool ApenasAtivas { get { return true; } }

        [SMCHidden]
        public bool UsarSeqEntidade { get { return true; } }

        [SMCHidden]
        [SMCDependency(nameof(SeqTipoServico), nameof(ProcessoController.BuscarTokenTipoServico), "Processo", true)]
        public string TokenTipoServico { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqServico), nameof(ProcessoController.BuscarTokenServico), "Processo", true)]
        public string TokenServico { get; set; }

        [SMCHidden]
        public TipoUnidadeResponsavel TipoUnidadeResponsavel { get { return TipoUnidadeResponsavel.EntidadeResponsavel; } }

        [SMCHidden]
        public bool ProcessoEncerrado { get; set; }

        [SMCParameter]
        [SMCHidden]
        public long SeqProcesso { get { return Seq; } }

        [SMCHidden]
        public long? SeqAgendamentoSat { get; set; }

        [SMCHidden]
        public SituacaoAgendamento? SituacaoAgendamento { get; set; }


        #endregion Propriedades Auxiliares

        [SMCReadOnly]
        [SMCKey]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCOrder(1)]
        [SMCDescription]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid20_24)]
        [SMCRequired]
        [SMCConditionalReadonly(nameof(ProcessoEncerrado), true, PersistentValue = true)]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCSelect(nameof(TiposServico), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCRequired]
        [SMCMapProperty("Servico.SeqTipoServico")]
        [SMCInclude("Servico")]
        [SMCConditionalReadonly(nameof(Seq), SMCConditionalOperation.NotEqual, 0, PersistentValue = true)]
        public long SeqTipoServico { get; set; }

        [SMCOrder(3)]
        [SMCSelect(nameof(Servicos), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid10_24)]
        [SMCRequired]
        [SMCDependency(nameof(SeqTipoServico), nameof(ProcessoController.BuscarServicosPorTipoServicoSelect), "Processo", true)]
        [SMCConditionalReadonly(nameof(Seq), SMCConditionalOperation.NotEqual, 0, PersistentValue = true)]
        public long SeqServico { get; set; }

        [SMCOrder(4)]
        [SMCMaxValue(100)]
        [SMCMinValue(0)]
        [SMCCurrency]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCIgnoreProp(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCConditionalDisplay(nameof(TokenTipoServico), SMCConditionalOperation.Equals, TOKEN_TIPO_SERVICO.RENOVACAO_MATRICULA, RuleName = "R1")]
        [SMCConditionalDisplay(nameof(TokenTipoServico), SMCConditionalOperation.Equals, TOKEN_TIPO_SERVICO.MATRICULA_REABERTURA, RuleName = "R2")]
        [SMCConditionalDisplay(nameof(TokenServico), SMCConditionalOperation.Equals,TOKEN_SERVICO.PRORROGACAO_PRAZO_CONCLUSAO, RuleName = "R3")]
        [SMCConditionalRule("R1 || R2 || R3")]
        [SMCConditionalReadonly(nameof(ProcessoEncerrado), true, PersistentValue = true)]
        public decimal? ValorPercentualServicoAdicional { get; set; }

        [SMCOrder(5)]
        [CicloLetivoLookup]
        [SMCMapProperty("CicloLetivo.Seq")]
        [SMCInclude("CicloLetivo")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCConditionalReadonly(nameof(ProcessoEncerrado), true, PersistentValue = true)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCOrder(6)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCRequired]
        [SMCConditionalReadonly(nameof(ProcessoEncerrado), true, PersistentValue = true)]
        public DateTime DataInicio { get; set; }

        [SMCOrder(7)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCMinDateNow]
        [SMCMinDate(nameof(DataInicio))]
        [SMCConditionalReadonly(nameof(ProcessoEncerrado), true, PersistentValue = true)]
        public DateTime? DataFim { get; set; }

        [SMCOrder(8)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCReadOnly]
        public DateTime? DataEncerramento { get; set; }

        [SMCOrder(9)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCReadOnly]
        [SMCDisplay]
        [SMCIgnoreProp(SMCViewMode.Insert | SMCViewMode.List | SMCViewMode.Filter)]
        [SMCConditionalDisplay(nameof(TokenTipoServico), TOKEN_TIPO_SERVICO.RENOVACAO_MATRICULA)]
        [SMCGroupedProperty("AgendamentoSAT")]
        [SMCValueEmpty("-")]
        public long? SeqAgendamentoSatExibir { get { return SeqAgendamentoSat; } }

        [SMCOrder(10)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCReadOnly]
        [SMCDisplay]
        [SMCIgnoreProp(SMCViewMode.Insert | SMCViewMode.List | SMCViewMode.Filter)]
        [SMCConditionalDisplay(nameof(TokenTipoServico), TOKEN_TIPO_SERVICO.RENOVACAO_MATRICULA)]
        [SMCGroupedProperty("AgendamentoSAT")]
        [SMCValueEmpty("-")]
        public SituacaoAgendamento? SituacaoAgendamentoExibir { get { return SituacaoAgendamento; } }

        [SMCOrder(11)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCIgnoreProp(SMCViewMode.Edit | SMCViewMode.List | SMCViewMode.Filter)]
        public List<ProcessoEtapaSGFViewModel> EtapasSGF { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        public List<long> SeqsEntidadesCompartilhadas { get; set; }
        
        [SMCHidden]
        public List<long> SeqsEntidadesResponsaveis { get; set; }
        
        [EntidadeSelecaoMultiplaLookup]
        [SMCDependency(nameof(SeqInstituicaoEnsino))]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCDescription]
        [SMCOrder(12)]
        [SMCConditionalReadonly(nameof(ProcessoEncerrado), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid24_24)]
        public List<EntidadeSelecaoMultiplaLookupViewModel> EntidadesResponsaveis { get; set; }

        [EntidadeSelecaoMultiplaLookup]
        [SMCDependency(nameof(SeqInstituicaoEnsino))]
        [SMCDependency(nameof(SeqsEntidadesCompartilhadas))]
        [SMCDescription]
        [SMCOrder(13)]
        [SMCConditionalReadonly(nameof(ProcessoEncerrado), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid24_24)]
        public List<EntidadeSelecaoMultiplaLookupViewModel> EntidadesCompartilhadas { get; set; }

        //FOI NECESSÁRIO REMOVER ESTE CAMPO POIS MESMO HIDDEN NÃO POSSIBILITAVA SALVAR O PROCESSO
        //[SMCOrder(14)]
        //[SMCDetail(min: 1)]
        //[SMCSize(SMCSize.Grid24_24)]
        //[SMCConditionalReadonly(nameof(ProcessoEncerrado), true, PersistentValue = true)]
        //public SMCMasterDetailList<ProcessoUnidadeResponsavelDetalheViewModel> UnidadesResponsaveis { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.ViewPartialInsert("_Inserir")
                   .Service<IProcessoService>(index: nameof(IProcessoService.BuscarProcessos),
                                              insert: nameof(IProcessoService.BuscarProcessoInserir),
                                              edit: nameof(IProcessoService.BuscarProcessoEditar),
                                              save: nameof(IProcessoService.SalvarProcesso))

                   .Button("CopiarProcesso", "CopiarProcesso", "Processo",
                           UC_SRC_002_01_04.COPIAR_PROCESSO,
                           i => new { seqProcesso = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq) })

                   .Button("ConfiguracaoProcesso", "Index", "ConfiguracaoProcesso",
                           UC_SRC_002_02_01.PESQUISAR_CONFIGURACAO_PROCESSO,
                           i => new { seqProcesso = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq) })

                    .Button("ConfiguracaoEtapa", "Index", "ConfiguracaoEtapa",
                           UC_SRC_002_04_01.PESQUISAR_CONFIGURACAO_ETAPA,
                           i => new { seqProcesso = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq) })

                   .Button("EscalonamentoEtapa", "Index", "Escalonamento",
                           UC_SRC_002_05_01.PESQUISAR_ESCALONAMENTO_ETAPA,
                           i => new { seqProcesso = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq) },
                           displayButton: (model) =>
                           {
                               var listaModel = model as ProcessoListarDynamicModel;
                               return listaModel.ExibeBtnEscalonamentosEtapa;
                           })

                   .Button("GruposEscalonamento", "Index", "GrupoEscalonamento",
                           UC_SRC_002_06_01.PESQUISAR_GRUPO_ESCALONAMENTO_PROCESSO,
                           i => new { seqProcesso = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq) },
                           displayButton: (model) =>
                           {
                               var listaModel = model as ProcessoListarDynamicModel;
                               return listaModel.ExibeBtnGrupoEscalonamento;
                           })

                   .Button("ProcessoEtapaConfiguracaoNotificacao", "Index", "ProcessoEtapaConfiguracaoNotificacao",
                           UC_SRC_003_02_01.PESQUISAR_CONFIGURACAO_NOTIFICACAO,
                           i => new { seqProcesso = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq) })

                   //.Button("Solicitacoes", "Index", "SolicitacaoServico",
                   //        UC_SRC_004_01_01.PESQUISAR_SOLICITACAO,
                   //        i => new { seqProcesso = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq) })

                   .Button("PosicaoConsolidada", "Index", "PosicaoConsolidada",
                           UC_SRC_005_01_01.CONSULTAR_POSICAO_CONSOLIDADA,
                           i => new { seqProcesso = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq) })

                   .Button("PreparacaoRematricula", "Index", "PreparacaoRematricula",
                           UC_SRC_002_01_01.PREPARAR_RENOVACAO_MATRICULA,
                           i => new { seqProcesso = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq) },
                           displayButton: (model) =>
                           {
                               var listaModel = model as ProcessoListarDynamicModel;
                               return listaModel.TokenServico == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU || listaModel.TokenServico == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_LATO_SENSU;
                           })

                   .Button("EncerrarProcesso", "EncerrarProcesso", "Processo",
                            UC_SRC_002_01_01.ENCERRAR_PROCESSO,
                            i => new { seqProcesso = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq) },
                            confirm: x => new Framework.Dynamic.SMCDynamicConfirm
                            {
                                Message = Views.Processo.App_LocalResources.UIResource.MSG_Confirmacao_EncerrarProcesso,
                                Title = Views.Processo.App_LocalResources.UIResource.TITLE_Confirmacao_EncerrarProcesso
                            },
                            // Somente se o tipo de prazo das etapas que compõem o processo for igual a Período de Vigência
                            // ou Escalonamento
                            displayButton: (model) =>
                            {
                                var listaModel = model as ProcessoListarDynamicModel;
                                return listaModel.ExibeBtnEncerrarProcesso;
                            })

                   .Button("ReabrirProcesso", "ReabrirProcesso", "Processo",
                            UC_SRC_002_01_01.REABRIR_PROCESSO,
                            i => new { seqProcesso = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq) },
                            confirm: x => new Framework.Dynamic.SMCDynamicConfirm
                            {
                                Message = Views.Processo.App_LocalResources.UIResource.MSG_Confirmacao_ReabrirProcesso,
                                Title = Views.Processo.App_LocalResources.UIResource.TITLE_Confirmacao_ReabrirProcesso
                            },
                            displayButton: (model) =>
                            {
                                var listaModel = model as ProcessoListarDynamicModel;

                                return listaModel.HabilitaBtnReabrirProcesso;
                            })

                   // Fix: Permitir a configuração dos botões direto no fluent do Button.
                   .ConfigureButton((button, model, action) =>
                    {
                        if (button.Options.SecurityTokens.SMCContains(UC_SRC_002_01_04.COPIAR_PROCESSO))
                        {
                            var listaModel = model as ProcessoListarDynamicModel;

                            if (!listaModel.HabilitaBtnCopiarProcesso || !listaModel.HabilitaBtnComPermissaoManutencaoProcesso)
                            {
                                button.Enabled(false);

                                if (!listaModel.HabilitaBtnCopiarProcesso)
                                    button.ButtonInstructions(listaModel.InstructionCopiarProcesso);
                            }
                        }
                        if (button.Options.SecurityTokens.SMCContains(UC_SRC_002_02_01.PESQUISAR_CONFIGURACAO_PROCESSO))
                        {
                            var listaModel = model as ProcessoListarDynamicModel;
                            if (!listaModel.HabilitaBtnComPermissaoManutencaoProcesso)
                                button.Enabled(false);
                        }
                        if (button.Options.SecurityTokens.SMCContains(UC_SRC_002_04_01.PESQUISAR_CONFIGURACAO_ETAPA))
                        {
                            var listaModel = model as ProcessoListarDynamicModel;

                            if (!listaModel.HabilitaBtnConfigurarEtapa || !listaModel.HabilitaBtnComPermissaoManutencaoProcesso)
                            {
                                button.Enabled(false);

                                if (!listaModel.HabilitaBtnConfigurarEtapa)
                                    button.ButtonInstructions(listaModel.InstructionConfigurarEtapa);
                            }
                        }
                        #region Validação do token 'Token de acesso para permissão de manutenção de processos' comentado conforme Task 40346
                        //if (button.Options.SecurityTokens.SMCContains(UC_SRC_002_05_01.PESQUISAR_ESCALONAMENTO_ETAPA))
                        //{
                        //    var listaModel = model as ProcessoListarDynamicModel;
                        //    if (!listaModel.HabilitaBtnComPermissaoManutencaoProcesso)
                        //        button.Enabled(false);
                        //}
                        #endregion
                        if (button.Options.SecurityTokens.SMCContains(UC_SRC_002_06_01.PESQUISAR_GRUPO_ESCALONAMENTO_PROCESSO))
                        {
                            var listaModel = model as ProcessoListarDynamicModel;
                            //Validação do token 'Token de acesso para permissão de manutenção de processos' comentado conforme Task 40346
                            if (!listaModel.HabilitaBtnGrupoEscalonamento /*|| !listaModel.HabilitaBtnComPermissaoManutencaoProcesso*/)
                            {
                                button.Enabled(false);

                                if (!listaModel.HabilitaBtnGrupoEscalonamento)
                                    button.ButtonInstructions(Views.Processo.App_LocalResources.UIResource.MSG_BtnGrupoEscalonamentoDesabilitado);
                            }
                        }
                        #region Validação do token 'Token de acesso para permissão de manutenção de processos' comentado conforme Task 40346
                        //if (button.Options.SecurityTokens.SMCContains(UC_SRC_003_02_01.PESQUISAR_CONFIGURACAO_NOTIFICACAO))
                        //{
                        //    var listaModel = model as ProcessoListarDynamicModel;
                        //    if (!listaModel.HabilitaBtnComPermissaoManutencaoProcesso)
                        //        button.Enabled(false);
                        //}
                        #endregion
                        if (button.Options.SecurityTokens.SMCContains(UC_SRC_002_01_01.PREPARAR_RENOVACAO_MATRICULA))
                        {
                            var listaModel = model as ProcessoListarDynamicModel;

                            if (!listaModel.HabilitaBtnComPermissaoManutencaoProcesso)
                                button.Enabled(false);

                            // Verifica se o processo está vigente.
                            if (listaModel.DataFim < DateTime.Now)
                            {
                                button.Enabled(false);
                                button.ButtonInstructions(Views.Processo.App_LocalResources.UIResource.MSG_ProcessoEncerrado);
                            }
                        }
                        if (button.Options.SecurityTokens.SMCContains(UC_SRC_002_01_01.ENCERRAR_PROCESSO))
                        {
                            var listaModel = model as ProcessoListarDynamicModel;

                            if (!listaModel.HabilitaBtnEncerrarProcesso || !listaModel.HabilitaBtnComPermissaoManutencaoProcesso)
                            {
                                button.Enabled(false);

                                if (!listaModel.HabilitaBtnEncerrarProcesso)
                                    button.ButtonInstructions(listaModel.InstructionEncerrarProcesso);
                            }
                        }
                        if (button.Options.SecurityTokens.SMCContains(UC_SRC_002_01_01.ENCERRAR_PROCESSO))
                        {
                            var listaModel = model as ProcessoListarDynamicModel;

                            if (!listaModel.HabilitaBtnEncerrarProcesso || !listaModel.HabilitaBtnComPermissaoManutencaoProcesso)
                            {
                                button.Enabled(false);

                                if (!listaModel.HabilitaBtnEncerrarProcesso)
                                    button.ButtonInstructions(listaModel.InstructionEncerrarProcesso);
                            }
                        }

                        if (button.Options.SecurityTokens.SMCContains(UC_SRC_002_01_01.REABRIR_PROCESSO))
                        {
                            var listaModel = model as ProcessoListarDynamicModel;

                            if (!listaModel.DataEncerramento.HasValue)
                            {
                                button.Enabled(false);

                            }
                        }
                    })

                   .Tokens(tokenInsert: UC_SRC_002_01_02.MANTER_PROCESSO,
                           tokenEdit: UC_SRC_002_01_02.MANTER_PROCESSO,
                           tokenRemove: UC_SRC_002_01_02.MANTER_PROCESSO,
                           tokenList: UC_SRC_002_01_01.PESQUISAR_PROCESSOS)

                   .Detail<ProcessoListarDynamicModel>("_DetailList", allowSort: false);

            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("Seq") || (HttpContext.Current.Request.QueryString.AllKeys.Contains("SeqServico") && HttpContext.Current.Request.QueryString.AllKeys.Contains("SeqTipoServico")))
                options.DisableInitialListing(false);
            else
                options.DisableInitialListing(true);
        }
    }
}