using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.DCT.Controllers;
using SMC.SGA.Administrativo.Areas.DCT.Views.Colaborador.App_LocalResources;
using SMC.SGA.Administrativo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    // Identificação Wizard0
    [SMCStepConfiguration(ActionStep = nameof(ColaboradorController.Selecao), Partial = "_IdentificacaoPessoaExistenteFiltro", UseOnTabs = false)]
    // Dados Pessoais Tab0 Wizard1
    [SMCStepConfiguration(ActionStep = nameof(ColaboradorController.DadosPessoais))]
    // Contatos Tab1 Wizard2
    [SMCStepConfiguration(ActionStep = nameof(ColaboradorController.Contatos))]
    // Instituições Externas Tab2 Wizard3
    [SMCStepConfiguration]
    // Vínculo Wizard4
    [SMCStepConfiguration(ActionStep = nameof(ColaboradorController.Vinculos), Partial = "_Vinculo", UseOnTabs = false)]
    // Formação Acadêmica Wizard5
    [SMCStepConfiguration(ActionStep = nameof(ColaboradorController.FormacaoAcademica), UseOnTabs = false)]
    // Confirmação Wizard6
    [SMCStepConfiguration(ActionStep = nameof(ColaboradorController.ConfirmacaoCadastroColaborador), Partial = "_DadosConfirmacaoColaborador", UseOnTabs = false)]
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
    public class ColaboradorDynamicModel : PessoaAtuacaoViewModel, ISMCWizardViewModel, ISMCStatefulView, ISMCMappable
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarEntidadesVinculoColaboradorSelect))]
        public List<SMCSelectListItem> EntidadesColaborador { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeVinculoColaboradorService),
                             nameof(IInstituicaoTipoEntidadeVinculoColaboradorService.BuscarTiposVinculoColaboradorPorEntidadeSelect),
                             values: new[] { nameof(SeqEntidadeVinculo) })]
        public List<SMCSelectListItem> TiposVinculoColaborador { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IFormacaoEspecificaService),
                             nameof(IFormacaoEspecificaService.BuscarLinhasDePesquisaGrupoPrograma),
                             values: new[] { nameof(SeqEntidadeVinculo) })]
        public List<SMCSelectListItem> LinhasDePesquisaGrupoPrograma { get; set; }

        /// <summary>
        /// Ofertas de curso por localidade utilizadas no passo de vínculo do wizard
        /// </summary>
        [SMCDataSource]
        [SMCServiceReference(typeof(ICursoOfertaLocalidadeService),
                             nameof(ICursoOfertaLocalidadeService.BuscarCursoOfertasLocalidadeAtivasPorEntidadesResponsaveisSelect),
                             values: new[] { nameof(SeqEntidadeVinculo) })]
        public List<SMCSelectListItem> CursoOfertasLocalidades { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ITitulacaoService),
                             nameof(ITitulacaoService.BuscarTitulacoesSelect),
                             values: new[] { nameof(Sexo), nameof(SemCurso) })]
        public List<SMCSelectListItem> Titulacoes { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ITitulacaoDocumentoComprobatorioService),
                             nameof(ITitulacaoDocumentoComprobatorioService.BuscarTitulacaoDocumentosComprobatorios),
                             values: new[] { nameof(SeqTitulacao) })]
        public List<SMCSelectListItem> DocumentosComprobatorios { get; set; }

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
        [SMCStep(5, 4)]
        public bool RetornarInstituicaoEnsinoLogada { get => true; }

        [SMCHidden]
        [SMCStep(3, 2)]
        public long SeqColaborador { get => this.Seq; }

        [SMCHidden]
        [SMCStep(0)]
        public override TipoAtuacao TipoAtuacao { get => TipoAtuacao.Colaborador; }

        [SMCIgnoreProp]
        public override object BuscaPessoaExistenteRouteValues { get => new { Area = "DCT", Controller = "Colaborador" }; }

        [SMCIgnoreProp]
        public bool SemCurso => true;

        [SMCHidden]
        [SMCStep(5, 4)]
        public long SeqHierarquiaClassificacao { get; set; }

        #endregion [ Hidden ]

        #region [ Instituições Externas Tab2 Wizard3 ]

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(3, 2)]
        public SMCMasterDetailList<ColaboradorInstituicaoExternaViewModel> InstituicoesExternas { get; set; }

        #endregion [ Instituições Externas Tab2 Wizard3 ]

        #region [ Vínculo Wizard4 ]

        [SMCDependency(nameof(SeqEntidadeVinculo), nameof(ColaboradorVinculoController.ValidarTipoEntidadeVinculoGrupoPrograma), "ColaboradorVinculo", true)]
        [SMCHidden]
        [SMCStep(4, 3)]
        public bool EntidadeVinculoGrupoPrograma { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(EntidadesColaborador), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24)]
        [SMCStep(4, 3)]
        public long SeqEntidadeVinculo { get; set; }

        //Utilizado no cadastro de vinculo
        [SMCHidden]
        [SMCStep(4, 3)]
        public long SeqTipoFormacaoEspecifica { get; set; }

        [SMCDependency(nameof(SeqEntidadeVinculo), nameof(ColaboradorVinculoController.BuscarEntidadesFilhas), "ColaboradorVinculo", true)]
        [SMCHidden]
        [SMCStep(4, 3)]
        public long[] SeqsEntidadesResponsaveis { get; set; }

        [SMCDependency(nameof(SeqEntidadeVinculo), nameof(ColaboradorVinculoController.BuscarTiposVinculoColaboradorPorEntidadeSelect), "ColaboradorVinculo", true)]
        [SMCRequired]
        [SMCSelect(nameof(TiposVinculoColaborador), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCStep(4, 3)]
        public long SeqTipoVinculoColaborador { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCStep(4, 3)]
        public DateTime DataInicioVinculo { get; set; }

        [SMCMinDate(nameof(DataInicioVinculo))]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCStep(4, 3)]
        public DateTime? DataFimVinculo { get; set; }

        [SMCDependency(nameof(SeqTipoVinculoColaborador), nameof(ColaboradorVinculoController.RetornarTipoVinculoNecessitaAcompanhamento), "ColaboradorVinculo", false)]
        [SMCHidden]
        [SMCStep(4, 3)]
        public bool ExibirColaboradorResponsavel { get; set; }

        [SMCConditionalDisplay(nameof(ExibirColaboradorResponsavel), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(4, 3)]
        public string TituloPesquisa { get; set; }

        [SMCConditionalDisplay(nameof(ExibirColaboradorResponsavel), SMCConditionalOperation.Equals, true)]
        [SMCMultiline]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(4, 3)]
        public string Observacao { get; set; }

        [SMCConditionalDisplay(nameof(ExibirColaboradorResponsavel), SMCConditionalOperation.Equals, true)]
        [SMCDetail(type: SMCDetailType.Block, min: 1)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(4, 3)]
        public SMCMasterDetailList<ColaboradorResponsavelVinculoViewModel> ColaboradoresResponsaveis { get; set; }

        //[SMCDetail]
        //[SMCSize(SMCSize.Grid24_24)]
        //[SMCStep(4, 3)]
        //public SMCMasterDetailList<ColaboradorVinculoCursoViewModel> Cursos { get; set; }

        [SMCConditionalDisplay(nameof(EntidadeVinculoGrupoPrograma), SMCConditionalOperation.Equals, true)]
        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(4, 3)]
        public SMCMasterDetailList<ColaboradorVinculoFormacaoEspecificaViewModel> FormacoesEspecificas { get; set; }

        #endregion [ Vínculo Wizard4 ]

        #region [ Formação acadêmica Wizard5 ]

        [SMCRequired]
        [SMCSelect(nameof(Titulacoes))]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCStep(5, 4)]
        public long? SeqTitulacao { get; set; }

        [SMCMaxLength(100)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCStep(5, 4)]
        public string Descricao { get; set; }

        [SMCMask("9999")]
        [SMCSize(SMCSize.Grid2_24)]
        [SMCStep(5, 4)]
        public short? AnoInicio { get; set; }

        [SMCMask("9999")]
        [SMCMinValue(nameof(AnoInicio))]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCStep(5, 4)]
        public short? AnoObtencaoTitulo { get; set; }

        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCStep(5, 4)]
        public bool? TitulacaoMaxima { get; set; }

        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCStep(5, 4)]
        public string Curso { get; set; }

        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCStep(5, 4)]
        public string Orientador { get; set; }

        [InstituicaoExternaLookup]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCStep(5, 4)]
        [SMCDependency(nameof(RetornarInstituicaoEnsinoLogada))]
        public InstituicaoExternaLookupViewModel SeqInstituicaoExterna { get; set; }

        [ClassificacaoLookup]
        [SMCDependency(nameof(SeqHierarquiaClassificacao))]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCStep(5, 4)]
        public ClassificacaoLookupViewModel SeqClassificacao { get; set; }

        [SMCDependency(nameof(SeqTitulacao), nameof(ColaboradorController.BuscarDocumentosTitulacao), "Colaborador", true)]
        [SMCSelect(nameof(DocumentosComprobatorios))]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCStep(5, 4)]
        public List<long> SeqDocumentoApresentado { get; set; }

        #endregion [ Formação acadêmica Wizard5 ]

        #region [ Confirmação Wizard6 ]

        [SMCIgnoreProp]
        [SMCSize(SMCSize.Grid10_24)]
        public string NomeSocialConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(SMCSize.Grid10_24)]
        public string NumeroPassaporteConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoVinculoConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(SMCSize.Grid24_24)]
        public List<string> ColaboradoresResponsaveisConfirmacao { get; set; }

        //[SMCIgnoreProp]
        //[SMCSize(SMCSize.Grid24_24)]
        //public List<ColaboradorVinculoCursoConfirmacaoViewModel> CursosConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(SMCSize.Grid24_24)]
        public List<string> FormacoesEspecificasConfirmacao { get; set; }

        #endregion [ Confirmação Wizard6 ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Wizard(editMode: SMCDynamicWizardEditMode.Tab)
                   .CssClass(insert: "smc-sga-colaborador")
                   .Detail<ColaboradorListarDynamicModel>("_DetailList")
                   .DisableInitialListing(true)
                   .Tokens(tokenInsert: UC_DCT_001_06_02.MANTER_COLABORADOR,
                             tokenEdit: UC_DCT_001_06_02.MANTER_COLABORADOR,
                           tokenRemove: SMCSecurityConsts.SMC_DENY_AUTHORIZATION,
                             tokenList: UC_DCT_001_06_01.PESQUISAR_COLABORADOR)
                   .Service<IColaboradorService>(index: nameof(IColaboradorService.BuscarColaboradores),
                                                insert: nameof(IColaboradorService.BuscarConfiguracaoColaborador),
                                                  save: nameof(IColaboradorService.SalvarColaborador),
                                                  edit: nameof(IColaboradorService.BuscarColaborador))
                   .Button("CadastroComponenteAptoLecionar", "Index", "ColaboradorAptoComponente",
                       UC_DCT_001_08_01.PESQUISAR_COMPONENTE_APTO_LECIONAR,
                        i => new
                        {
                            SeqAtuacaoColaborador = SMCDESCrypto.EncryptNumberForURL(((ColaboradorListarDynamicModel)i).Seq)
                        },
                        displayButton: m => (m as ColaboradorListarDynamicModel).VinculosAtivos.SMCAny(a => a.Cursos.Any(c => c.TipoAtividadeColaborador.Any(t => t == TipoAtividadeColaborador.Aula))))
                   .Button("CadastroFormacaoAcademica", "Index", "FormacaoAcademica",
                        UC_PES_002_01_01.PESQUISAR_FORMACAO_ACADEMICA,
                        routes: i => new
                        {
                            SeqPessoaAtuacao = SMCDESCrypto.EncryptNumberForURL(((ColaboradorListarDynamicModel)i).Seq),
                            Area = "PES"
                        },
                        displayButton: m => !(m as ColaboradorListarDynamicModel).VinculosAtivos.SMCAny(a => a.InseridoPorCarga))
                    .Button("Notificacoes", "Index", "EnvioNotificacaoDestinatario",
                        UC_PES_008_02_01.VISUALIZAR_NOTIFICACAO_POR_PESSOA_ATUACAO,
                        i => new
                        {
                            seqPessoaAtuacao = SMCDESCrypto.EncryptNumberForURL(((ColaboradorListarDynamicModel)i).Seq),
                            Area = "PES"
                        })
                   .Button("CadastroVinculoColaborador", "Index", "ColaboradorVinculo",
                        UC_DCT_001_06_03.PESQUISAR_VINCULO_COLABORADOR,
                        i => new
                        {
                            SeqColaborador = SMCDESCrypto.EncryptNumberForURL(((ColaboradorListarDynamicModel)i).Seq)
                        });

            this.TipoAtuacaoAuxiliar = TipoAtuacao.Colaborador;             
        }
    }
}