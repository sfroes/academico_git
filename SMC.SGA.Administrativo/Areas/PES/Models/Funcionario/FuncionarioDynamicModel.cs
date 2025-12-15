using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.PES.Controllers;
using SMC.SGA.Administrativo.Areas.PES.Views.Funcionario.App_LocalResources;
using SMC.SGA.Administrativo.Models;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    //Identificação step 1
    [SMCStepConfiguration(ActionStep = nameof(FuncionarioController.Selecao), Partial = "_IdentificacaoPessoaExistenteFiltro", UseOnTabs = false)]
    //Dados Pessoais step 2
    [SMCStepConfiguration(ActionStep = nameof(FuncionarioController.DadosPessoais))]
    //Contatos step 3
    [SMCStepConfiguration(ActionStep = nameof(FuncionarioController.Contatos))]
    //Vínculo step 4
    [SMCStepConfiguration(ActionStep = nameof(FuncionarioController.Vinculos), Partial = "_Vinculo", UseOnTabs = false)]
    //Confirmação step 5
    [SMCStepConfiguration(ActionStep = nameof(FuncionarioController.ConfirmacaoCadastroFuncionario), Partial = "_DadosConfirmacaoFuncionario", UseOnTabs = false)]
    [SMCGroupedPropertyConfiguration(GroupId = "Nacionalidade", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "Naturalidade", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "DocumentoPassaporte", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "DocumentoRg", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "DocumentoIdentidadeEstrangeira", Size = SMCSize.Grid24_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "DocumentoCnh", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "DocumentoTituloEleitor", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "DocumentoPisPasep", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "DocumentoMilitar", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "RegistroProfissional", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "NecessiadesEspeciais", Size = SMCSize.Grid12_24)]
    public class FuncionarioDynamicModel : PessoaAtuacaoViewModel, ISMCWizardViewModel, ISMCStatefulView, ISMCMappable
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoTipoFuncionarioService), nameof(IInstituicaoTipoFuncionarioService.BuscarTipoFuncionarioInstituicaoETipoEntidadePorFuncionario), values: new[] { nameof(SeqInstituicaoEnsino) })]
        public List<SMCDatasourceItem> TiposFuncionarios { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeTipoFuncionarioService), nameof(IInstituicaoTipoEntidadeTipoFuncionarioService.BuscarTipoEntidadePorTipoFuncionario), values: new[] { nameof(SeqTipoFuncionario) })]
        public List<SMCDatasourceItem> TipoEntidades { get; set; }


        [SMCDataSource]
        [SMCServiceReference(typeof(IFuncionarioVinculoService), nameof(IFuncionarioVinculoService.BuscarEntidadesPorVinculoFuncionario), values: new[] { nameof(SeqTipoFuncionario) })]
        public List<SMCDatasourceItem> Entidades { get; set; }



        #endregion [ DataSources ]

        #region [ Mensagens ]

        /// <summary>
        /// Mensagem passo 4 - Instituições
        /// </summary>
        [SMCCssClass("smc-sga-msg-instrucao-cadastro smc-sga-msg-info")]
        [SMCDisplay]
        [SMCOrder(0)]
        [SMCStep(3, -1)]
        public string MensagemStep4 { get => UIResource.Mensagem_Step_4; }

        /// <summary>
        /// Mensagem passo 5 - Vínculos
        /// </summary>
        [SMCCssClass("smc-sga-msg-instrucao-cadastro smc-sga-msg-info")]
        [SMCDisplay]
        [SMCOrder(0)]
        [SMCStep(4, -1)]
        public string MensagemStep5 { get => UIResource.Mensagem_Step_5; }

        /// <summary>
        /// Mensagem passo 6 - Confirmação
        /// </summary>
        [SMCCssClass("smc-sga-msg-instrucao-cadastro smc-sga-msg-confirmacao")]
        [SMCDisplay]
        [SMCStep(6, -1)]
        public string MensagemStep6 { get => UIResource.Mensagem_Step_6; }

        #endregion [ Mensagens ]

        #region [ Hidden ]

        [SMCIgnoreProp]
        public int Step { get; set; }

        [SMCHidden]
        [SMCStep(3, 0)]
        public bool RetornarInstituicaoEnsinoLogada { get => true; }

        [SMCHidden]
        [SMCStep(3, 2)]
        public long SeqFuncionario { get => this.Seq; }

        [SMCHidden]
        [SMCStep(0)]
        public override TipoAtuacao TipoAtuacao { get => TipoAtuacao.Funcionario; }

        [SMCIgnoreProp]
        public override object BuscaPessoaExistenteRouteValues { get => new { Area = "PES", Controller = "Funcionario" }; }

        #endregion [ Hidden ]

        #region [ Vínculo Wizard4 ]

        [SMCRequired]
        [SMCSelect(nameof(TiposFuncionarios), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCStep(3)]
        public long SeqTipoFuncionario { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCStep(3)]
        public DateTime DataInicioVinculo { get; set; }

        [SMCMinDate(nameof(DataInicioVinculo))]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCStep(3)]
        public DateTime? DataFimVinculo { get; set; }

        [SMCSelect(nameof(TipoEntidades),AutoSelectSingleItem =true)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCConditionalDisplay(nameof(ExibirCamposTipoEntidadesEEntidades), SMCConditionalOperation.Equals, true)]
        [SMCDependency(nameof(SeqTipoFuncionario), nameof(FuncionarioController.BuscarTiposEntidades), "Funcionario",true)]
        [SMCStep(4)]
        public long? SeqTipoEntidade { get; set; }

        [SMCSelect(nameof(Entidades))]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]        
        [SMCConditionalDisplay(nameof(ExibirCamposTipoEntidadesEEntidades), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(ExibirCamposTipoEntidadesEEntidades), SMCConditionalOperation.Equals, true)]
        [SMCDependency(nameof(SeqTipoEntidade), nameof(FuncionarioController.BuscarEntidades), "Funcionario", true)]
        [SMCStep(4)]
        public long? SeqEntidadeVinculo { get; set; }


        [SMCHidden]
        [SMCStep(4)]
        [SMCStep(5)]
        [SMCDependency(nameof(SeqTipoFuncionario), nameof(FuncionarioController.ExibirCampos), "Funcionario",true)]
        public bool ExibirCamposTipoEntidadesEEntidades { get; set; }

        #endregion [ Vínculo Wizard4 ]

        #region [ Confirmação Step 5 ]

        [SMCIgnoreProp]
        [SMCSize(SMCSize.Grid10_24)]
        public string NomeSocialConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(SMCSize.Grid10_24)]
        public string NumeroPassaporteConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(SMCSize.Grid12_24)]
        public string DescricaoVinculoConfirmacao { get; set; }
        
        [SMCIgnoreProp]
        [SMCSize(SMCSize.Grid12_24)]
        public string DescricaoEntidadeCadastrada { get; set; }



        #endregion [ Confirmação Stpe 5 ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Wizard(editMode: SMCDynamicWizardEditMode.Tab)
                   .Detail<FuncionarioListarDynamicModel>("_DetailList")
                   .DisableInitialListing(true)
                   .Tokens(tokenInsert: UC_PES_006_02_02.MANTER_FUNCIONARIO,
                             tokenEdit: UC_PES_006_02_02.MANTER_FUNCIONARIO,
                           tokenRemove: UC_PES_006_02_02.MANTER_FUNCIONARIO,
                             tokenList: UC_PES_006_02_01.PESQUISAR_FUNCIONARIO)
                   .Service<IFuncionarioService>(index: nameof(IFuncionarioService.BuscarFuncionarios),
                                                 insert: nameof(IFuncionarioService.BuscarConfiguracaoFuncionario),
                                                 save: nameof(IFuncionarioService.SalvarFuncionario),
                                                 edit: nameof(IFuncionarioService.BuscarFuncionario))
                   .Button("CadastroVinculoFuncionario", "Index", "FuncionarioVinculo",
                        UC_PES_006_02_01.PESQUISAR_FUNCIONARIO,
                        i => new
                        {
                            SeqFuncionario = SMCDESCrypto.EncryptNumberForURL(((FuncionarioListarDynamicModel)i).Seq)
                        });
        }
    }
}