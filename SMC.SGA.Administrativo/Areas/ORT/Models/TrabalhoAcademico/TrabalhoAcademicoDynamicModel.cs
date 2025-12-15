using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ORT.Controllers;
using SMC.SGA.Administrativo.Areas.ORT.Views.TrabalhoAcademico.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "PotencialRegistro", Size = SMCSize.Grid24_24 )]
    [SMCStepConfiguration(ActionStep = nameof(TrabalhoAcademicoController.Passo1), UseOnTabs = true)]
    [SMCStepConfiguration(ActionStep = nameof(TrabalhoAcademicoController.Passo2), Partial = "_Alunos", UseOnTabs = false)]
    [SMCStepConfiguration(ActionStep = nameof(TrabalhoAcademicoController.Passo3), Partial = "_ComponentesCurriculares", UseOnTabs = false)]
    [SMCStepConfiguration(ActionStep = nameof(TrabalhoAcademicoController.Passo4), Partial = "_DadosConfirmacao", UseOnTabs = false)]
    public class TrabalhoAcademicoDynamicModel : SMCDynamicViewModel, ISMCWizardViewModel, ISMCStatefulView, ISMCMappable
    {
        #region [ Hidden ]

        [SMCHidden]
        public bool? AvaliacaoCadastrada { get; set; }

        //Vem da interface ISMCStep, implementada por ISMCWizardViewModel (ajuda a controlar e identificar os passos)
        [SMCIgnoreProp]
        public int Step { get; set; }

        [SMCHidden]
        public bool LimparRegistrosAutorizacao { get; set; }

        [SMCHidden]
        [SMCStep(0, 0)]
        public bool ExibirAlertaNivelEnsinoAlterado { get; set; }

        [SMCHidden]
        [SMCStep(0, 0)]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public long NumeroSequencial { get; set; }

        [SMCHidden]
        public long? SeqNivelEnsinoComparacao { get; set; }

        [SMCHidden]
        public long? SeqTipoTrabalhoComparacao { get; set; }

        [SMCHidden]
        [SMCStep(1, 0)]
        public bool? AlterarDataDepositoSecretaria { get; set; }

        [SMCHidden]
        public long? SeqAluno { get => Autores?.FirstOrDefault()?.SeqAluno.Seq; }

        [SMCHidden]
        public long? SeqAlunoComparacao { get; set; }

        public TipoGestaoDivisaoComponente TipoGestaoDivisaoComponente { get => TipoGestaoDivisaoComponente.Trabalho; }

        /// <summary>
        /// <NV10> Na alteração, se já houver avaliação cadastrada para um dos componentes
        /// curriculares associados ao trabalho, não será mais possível alterar o aluno
        /// e os componetes, ficando assim desabilitados os campos.</NV10>
        /// </summary>
        [SMCHidden]
        [SMCStep(0, 0)]
        public bool ExisteAvaliacaoCadastrada { get; set; }

        /// <summary>
        /// Utilizado para não permitir alterar o título do trabalho caso já exista publicação no BDP
        /// </summary>
        [SMCHidden]
        [SMCStep(0, 0)]
        public bool ExistePublicacaoBdp { get; set; }

        [SMCHidden]
        [SMCStep(0, 0)]
        [SMCDependency(nameof(SeqTipoTrabalho), nameof(TrabalhoAcademicoController.BuscarGeraFinanceiroEntregaTrabalho), "TrabalhoAcademico", true, nameof(SeqInstituicaoEnsino), nameof(SeqNivelEnsino))]
        public bool GeraFinanceiroEntregaTrabalho { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool? ExibeDuracao { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool? HabilitaDuracao { get; set; }


        [SMCHidden]
        [SMCStep(0)]
        public bool? PermiteInclusaoManual { get; set; }

        #endregion [ Hidden ]

        #region [ DataSources ]

        [SMCIgnoreProp] 
        [SMCDataSource()]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoTrabalhoService), nameof(ITipoTrabalhoService.BuscarTiposTrabalhoInstituicaoNivelEnsinoSelect), values: new[] { nameof(SeqNivelEnsino), nameof(SeqInstituicaoEnsino), nameof(PermiteInclusaoManual) })]
        public List<SMCDatasourceItem> TiposTrabalho { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(IDivisaoMatrizCurricularComponenteService), nameof(IDivisaoMatrizCurricularComponenteService.BuscarDivisaoComponenteCurricularSelect), values: new[] { nameof(SeqAluno), nameof(SeqTipoTrabalho), nameof(SeqNivelEnsino), nameof(SeqInstituicaoEnsino) })]
        public List<SMCDatasourceItem> DivisoesComponentes { get; set; }

        #endregion [ DataSources ]

        #region [ Wizard0 Aba 1 - Informações do Trabalho]

        [SMCConditionalDisplay(nameof(ExibirAlertaNivelEnsinoAlterado), SMCConditionalOperation.Equals, true)]
        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-alerta")]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        [SMCStep(0, 0)]
        public string MensagemAlerta { get { return UIResource.MensagemAlteracaoNivelEnsino; } }

        [SMCOrder(0)]
        [SMCRequired]
        [SMCMaxLength(500)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        [SMCConditionalReadonly(nameof(ExistePublicacaoBdp), true, PersistentValue = true)]
        [SMCStep(0, 0)]
        public string Titulo { get; set; }

        [SMCOrder(1)]
        [SMCRequired]
        [SMCSelect(nameof(NiveisEnsino), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid9_24)]
        [SMCHidden(SMCViewMode.List)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCStep(0, 0)]
        public long? SeqNivelEnsino { get; set; }

        [SMCOrder(2)]
        [SMCRequired]
        [SMCDependency(nameof(SeqNivelEnsino), nameof(TrabalhoAcademicoController.BuscarTiposTrabalho), "TrabalhoAcademico",false, new string[] { nameof(SeqInstituicaoEnsino) })]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid9_24)]
        [SMCSelect(nameof(TiposTrabalho), autoSelectSingleItem: true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCStep(0, 0)]
        public long? SeqTipoTrabalho { get; set; }

        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        [SMCHidden(SMCViewMode.List)]
        [SMCConditionalReadonly(nameof(AlterarDataDepositoSecretaria), SMCConditionalOperation.Equals, false, PersistentValue = true, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(ExisteAvaliacaoCadastrada), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R2")]
        [SMCConditionalRule("(R1 || R2)")]
        [SMCConditionalRequired(nameof(GeraFinanceiroEntregaTrabalho), SMCConditionalOperation.Equals, true)]
        [SMCConditionalDisplay(nameof(GeraFinanceiroEntregaTrabalho), SMCConditionalOperation.Equals, true)]
        [SMCDependency(nameof(SeqTipoTrabalho), nameof(TrabalhoAcademicoController.PreencherDataDepositoSecretaria), "TrabalhoAcademico", true, includedProperties: new string[] { nameof(GeraFinanceiroEntregaTrabalho), nameof(DataDepositoSecretaria) })]
        [SMCStep(0, 0)]
        [SMCMaxDateNow]
        public DateTime? DataDepositoSecretaria { get; set; }

        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCStep(0, 0)]
        [SMCMinValue(1)]
        [SMCHidden(SMCViewMode.Insert)]
        [SMCConditionalDisplay(nameof(ExibeDuracao), SMCConditionalOperation.Equals, true)]
        [SMCConditionalReadonly(nameof(HabilitaDuracao), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        public short? NumeroDiasDuracaoAutorizacaoParcial { get; set; }

        #endregion [ Wizard0 Aba 1 - Informações do Trabalho]

        #region [ Wizard1 Aba 2 - Aluno(s)]

        [SMCHidden]
        [SMCCssClass("smc-breakline")]
        [SMCKey]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCStep(1, 0)]
        [SMCOrder(4)]
        public override long Seq { get; set; }

        #region[Potencial de Registro]

        [SMCSelect]
        [SMCGroupedProperty("PotencialRegistro")]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCStep(0, 0)]
        [SMCOrder(5)]
        [SMCConditionalDisplay(nameof(ExibirPotencial), SMCConditionalOperation.Equals, true)]
        [SMCConditionalReadonly(nameof(HabilitaPotencial), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCIgnoreProp(SMCViewMode.Insert | SMCViewMode.Filter | SMCViewMode.List)]
        public bool? PotencialPatente { get; set; }

        [SMCSelect]
        [SMCGroupedProperty("PotencialRegistro")]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCStep(0, 0)]
        [SMCOrder(6)]        
        [SMCConditionalDisplay(nameof(ExibirPotencial), SMCConditionalOperation.Equals, true)]
        [SMCConditionalReadonly(nameof(HabilitaPotencial), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCIgnoreProp(SMCViewMode.Insert | SMCViewMode.Filter | SMCViewMode.List)]
        public bool? PotencialRegistroSoftware { get; set; }

        [SMCSelect]
        [SMCGroupedProperty("PotencialRegistro")]        
        [SMCSize(SMCSize.Grid8_24)]
        [SMCStep(0, 0)]
        [SMCOrder(7)]
        [SMCConditionalDisplay(nameof(ExibirPotencial), SMCConditionalOperation.Equals, true)]
        [SMCConditionalReadonly(nameof(HabilitaPotencial), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCIgnoreProp(SMCViewMode.Insert | SMCViewMode.Filter | SMCViewMode.List)]
        public bool? PotencialNegocio { get; set; }
        
        [SMCStep(0, 0)]
        [SMCHidden]
        public bool HabilitaPotencial { get; set; }

        [SMCStep(0, 0)]
        [SMCHidden]
        public bool ExibirPotencial { get; set; }


                       

        #endregion


        /// <summary>
        /// RN_USG_005 - Filtro por Entidade Responsável
        /// Listar Entidades Responsáveis de acordo com usuário logado ou filtrar
        /// dados por Entidade Responsável pelo Curso de acordo com o usuário logado.
        /// </summary>
        [SMCDetail(min: 1, max: 1, HideMasterDetailButtons = true)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(1, 0)]
        [SMCOrder(8)]
        [SMCInclude(nameof(Autores))]
        [SMCConditionalReadonly(nameof(ExisteAvaliacaoCadastrada), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(Seq), SMCConditionalOperation.NotEqual, 0, PersistentValue = true, RuleName = "R2")]
        [SMCConditionalRule("R1 || R2")]
        public SMCMasterDetailList<TrabalhoAcademicoAutoriaViewModel> Autores { get; set; }

        #endregion [ Wizard1 Aba 2 - Aluno(s)]

        #region [ Wizard2 Aba 3 - Compnente(s) Curricular(es) ]

        /// <summary>
        /// SeqComponenteCurricular
        /// </summary>
        [SMCDetail(min: 1, max: 1, HideMasterDetailButtons = true)]
        [SMCStep(2, 0)]
        [SMCOrder(9)]
        [SMCHidden(SMCViewMode.List)]
        [SMCDependency(nameof(Autores) + "." + nameof(TrabalhoAcademicoAutoriaViewModel.SeqAluno))]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        [SMCConditionalReadonly(nameof(ExisteAvaliacaoCadastrada), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public SMCMasterDetailList<TrabalhoAcademicoDivisaoComponenteViewModel> DivisoesComponente { get; set; }

        #endregion [ Wizard2 Aba 3 - Compnente(s) Curricular(es) ]

        #region [ Wizard3 Aba 4 - Confirmação ]

        [SMCStep(3)]
        public string DescricaoNivelEnsino { get; set; }

        [SMCStep(3)]
        public string DescricaoTipoTrabalho { get; set; }

        #endregion [ Wizard3 Aba 4 - Confirmação ]
   
        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            Dictionary<string, List<string>> assertPreRequisitos = new Dictionary<string, List<string>>();

            options.CssClass(insert: "smc-sga-wizard-trabalho-academico", edit: "smc-sga-wizard-trabalho-academico").Ajax()
                .Wizard(SMCDynamicWizardEditMode.Tab)
                .Detail<TrabalhoAcademicoListarDynamicModel>("_DetailList")
                .Header("HeaderEdit")
                .DisableInitialListing(true)
                .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                            ((TrabalhoAcademicoListarDynamicModel)x).NomesAutores.FirstOrDefault()))
                .Assert("_AssertPreRequisito", (controller, model) =>
                {
                    if (assertPreRequisitos.Any())
                        return true;

                    return false;
                }, (controller, model) =>
                {
                    var modelParsed = (model as TrabalhoAcademicoDynamicModel);

                    var service = controller.Create<IRequisitoService>();

                    foreach (var aluno in modelParsed.Autores)
                    {
                        var validacao = service.ValidarPreRequisitos(aluno.SeqAluno.Seq.Value, modelParsed.DivisoesComponente.Select(d => d.SeqDivisaoComponente).ToList(), null, null, null);
                        if (!validacao.Valido)
                            assertPreRequisitos.Add(aluno.NomeAutor, validacao.MensagensErro);
                    }

                    return new RequisitosInvalidos { Requisitos = assertPreRequisitos };
                })
                 .Assert("MSG_Confirmacao_SituacaoTrabalho", (controller, model) =>
                 {
                     var data = (model as TrabalhoAcademicoDynamicModel);

                     // Se for ação de incluir, sair do Assert e prosseguir para o metodo de SalvarTrabalhoAcademico
                     if (data.Seq == 0)
                         return false;

                     var trabalhoAcademicoService = controller.Create<ITrabalhoAcademicoService>();

                     var trabalhoAcademico = data.Transform<TrabalhoAcademicoData>();

                     bool exibirConfirmacao = trabalhoAcademicoService.ValidarSituacaoTrabalho(trabalhoAcademico);
                     
                     // Se valido para exibir caixa de dialogo, marca a opcao de limpar registros de autorização
                     if (exibirConfirmacao)
                     {
                         data.LimparRegistrosAutorizacao = true;
                     }
                     
                     return exibirConfirmacao;
                 })
                .Tokens(tokenInsert: UC_ORT_002_02_01.PERMITIR_INCLUIR_TRABALHO_ACADEMICO,
                           tokenEdit: UC_ORT_002_02_02.MANTER_TRABALHO_ACADEMICO,
                           tokenRemove: UC_ORT_002_02_02.MANTER_TRABALHO_ACADEMICO,
                           tokenList: UC_ORT_002_02_01.PESQUISAR_TRABALHO_ACADEMICO)
                .Service<ITrabalhoAcademicoService>(index: nameof(ITrabalhoAcademicoService.BuscarTrabalhosAcademicos),
                                                     edit: nameof(ITrabalhoAcademicoService.AlterarTrabalhoAcademico),
                                                     save: nameof(ITrabalhoAcademicoService.SalvarTrabalhoAcademico),
                                                     delete: nameof(ITrabalhoAcademicoService.ExcluirTrabalhoAcademico));
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);
            if (viewMode == SMCViewMode.Edit)
                PermiteInclusaoManual = null;
            else
                PermiteInclusaoManual = true;
        }

        #endregion [ Configurações ]
    }

    public class RequisitosInvalidos : SMCViewModelBase
    {
        public Dictionary<string, List<string>> Requisitos { get; set; }
    }
}