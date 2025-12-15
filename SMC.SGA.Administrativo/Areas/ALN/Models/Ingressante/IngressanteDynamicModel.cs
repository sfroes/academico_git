using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Academico.UI.Mvc.Areas.SRC.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;
using SMC.SGA.Administrativo.Areas.PES.Models;
using SMC.SGA.Administrativo.Models;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    // Identificação Wizard0
    [SMCStepConfiguration(ActionStep = nameof(IngressanteController.Selecao), Partial = "_IdentificacaoPessoaExistenteFiltro", UseOnTabs = false)]
    // Dados Pessoais Tab0 Wizard1
    [SMCStepConfiguration(ActionStep = nameof(IngressanteController.DadosPessoais))]
    // Contatos Tab1 Wizard2
    [SMCStepConfiguration(ActionStep = nameof(IngressanteController.Contatos))]
    // Dados Acadêmicos Tab2 Wizard3
    [SMCStepConfiguration(ActionStep = nameof(IngressanteController.DadosAcademicos), Partial = "_DadosAcademicos")]
    // Grupo de escalonamento Tab3 Wizard4
    [SMCStepConfiguration(ActionStep = nameof(IngressanteController.Escalonamento), Partial = "_Escalonamento")]
    // Confirmação Wizard5
    [SMCStepConfiguration(ActionStep = nameof(IngressanteController.Confirmacao), Partial = "_DadosConfirmacao", UseOnTabs = false)]
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
    public class IngressanteDynamicModel : PessoaAtuacaoViewModel, ISMCWizardViewModel, ISMCStatefulView, ISMCMappable
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IProcessoSeletivoService), nameof(IProcessoSeletivoService.BuscarProcessosSeletivosPorCampanhaIngressoDiretoSelect), values: new[] { nameof(SeqCampanha) })]
        public List<SMCDatasourceItem> ProcessosSeletivos { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoPorProcessoSeletivoSelect), values: new[] { nameof(SeqProcessoSeletivo) })]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ICicloLetivoService), nameof(ICicloLetivoService.BuscarCiclosLetivosPorCampanhaNivelSelect), values: new[] { nameof(SeqCampanha), nameof(SeqNivelEnsino) })]
        public List<SMCDatasourceItem> CiclosLetivos { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ITipoVinculoAlunoService), nameof(ITipoVinculoAlunoService.BuscarTiposVinculoAlunoPorProcessoSeletivo), values: new[] { nameof(SeqProcessoSeletivo) })]
        public List<SMCDatasourceItem> TiposVinculoAluno { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IFormaIngressoService), nameof(IFormaIngressoService.BuscarFormasIngressoInstituicaoNivelVinculoSelect), values: new[] { nameof(SeqProcessoSeletivo), nameof(SeqTipoVinculoAluno) })]
        public List<SMCDatasourceItem> FormasIngresso { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IMatrizCurricularOfertaService), nameof(IMatrizCurricularOfertaService.BuscarMatrizesCurricularesOfertasPorCampanhaSelect), values: new[] { nameof(SeqNivelEnsino), nameof(SeqTipoVinculoAluno), nameof(SeqCicloLetivo), nameof(SeqsCampanhaOferta), nameof(SeqsTermoIntercambio) })]
        public List<SMCDatasourceItem> MatrizesCurricularesOferta { get; set; }

        [SMCDataSource(SMCStorageType.Session)]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoOrientacaoService), nameof(IInstituicaoNivelTipoOrientacaoService.BuscarTiposOrientacaoSelect), values: new[] { nameof(SeqNivelEnsino) })]
        public List<SMCDatasourceItem> TiposOrientacao { get; set; }

        //[SMCDataSource]
        //[SMCServiceReference(typeof(IInstituicaoNivelTipoOrientacaoService), nameof(IInstituicaoNivelTipoOrientacaoService.BuscarTiposOrientacaoSelect), values: new[] { nameof(SeqNivelEnsino), nameof(SeqTipoVinculoAluno), nameof(PossuiTipoIntercambio), nameof(CadastroOrientacoesIngressante) })]
        //public List<SMCDatasourceItem> TiposOrientacaoPessoaAtuacao { get; set; }

        // Ao corrigir o dynamic utilizar o método superior
        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoOrientacaoService), nameof(IInstituicaoNivelTipoOrientacaoService.BuscarTiposOrientacaoGambiarraBindDynamicIngressanteSelect), values: new[] { nameof(SeqNivelEnsino), nameof(SeqTipoVinculoAluno), nameof(PossuiTipoIntercambio), nameof(CadastroOrientacoesIngressante) })]
        public List<SMCDatasourceItem> TiposOrientacaoPessoaAtuacao { get; set; }

        #endregion [ DataSources ]

        #region [ Controles dynamic ]

        [SMCIgnoreProp]
        public int Step { get; set; }

        [SMCHidden]
        [SMCStep(2, 1)]
        public override TipoAtuacao TipoAtuacao { get => TipoAtuacao.Ingressante; }

        [SMCIgnoreProp]
        public override object BuscaPessoaExistenteRouteValues { get => new { Area = "ALN", Controller = "Ingressante" }; }

        #endregion [ Controles dynamic ]

        #region [ Dados Acadêmicos Wizard4 Tab2 ]

        [SMCHidden]
        public SituacaoIngressante SituacaoIngressante { get; set; }

        [SMCHidden]
        public OrigemIngressante OrigemIngressante { get; set; }

        [SMCIgnoreProp]
        public bool DadosAcademicosSomenteLeitura
        {
            get
            {
                return 
                    SituacaoNaoPermiteEdicao(SituacaoIngressante) ||
                    (
                        (
                            SituacaoIngressante == SituacaoIngressante.AguardandoLiberacaMatricula ||
                            SituacaoIngressante == SituacaoIngressante.AptoMatricula
                        ) &&
                        (
                            OrigemIngressante == OrigemIngressante.Convocacao ||
                            OrigemIngressante == OrigemIngressante.ImportacaoPlanilha ||
                            OrigemIngressante == OrigemIngressante.SelecionadoGPI
                        )
                    );
            }
        }

        [CampanhaLookup]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCStep(3, 2)]
        public CampanhaLookupViewModel Campanha { get; set; }

        [SMCDependency(nameof(Campanha) + "." + nameof(CampanhaLookupViewModel.SeqEntidadeResponsavel))]
        [SMCHidden]
        [SMCStep(3, 2)]
        public long? SeqEntidadeResponsavel { get => Campanha?.SeqEntidadeResponsavel; }

        [SMCDependency(nameof(Campanha) + "." + nameof(CampanhaLookupViewModel.Seq))]
        [SMCHidden]
        [SMCStep(3, 2)]
        public long? SeqCampanha { get => Campanha?.Seq; }

        [SMCDependency(nameof(SeqCampanha), nameof(IngressanteController.BuscarProcessosSeletivosIngressoDireto), "Ingressante", true)]
        [SMCRequired]
        [SMCSelect(nameof(ProcessosSeletivos), AutoSelectSingleItem = true, NameDescriptionField = nameof(DescricaoProcessoSeletivo))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCStep(3, 2)]
        public long SeqProcessoSeletivo { get; set; }

        [SMCIgnoreProp]
        public string DescricaoProcessoSeletivo { get; set; }

        [SMCHidden]
        [SMCStep(5, 3)]
        public long? SeqCampanhaCicloLetivo { get; set; }

        [SMCHidden]
        [SMCStep(5, 3)]
        public long SeqProcesso { get; set; }

        [SMCDependency(nameof(SeqProcessoSeletivo), nameof(IngressanteController.BuscarNiveisEnsino), "Ingressante", true)]
        [SMCRequired]
        [SMCSelect(nameof(NiveisEnsino), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCStep(3, 2)]
        public long SeqNivelEnsino { get; set; }

        [SMCDependency(nameof(SeqNivelEnsino), nameof(IngressanteController.BuscarCiclosLetivos), "Ingressante", true, nameof(SeqCampanha))]
        [SMCRequired]
        [SMCSelect(nameof(CiclosLetivos), NameDescriptionField = nameof(DescricaoCicloLetivo), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCStep(3, 2)]
        public long SeqCicloLetivo { get; set; }

        [SMCIgnoreProp]
        public string DescricaoCicloLetivo { get; set; }

        [SMCDependency(nameof(SeqProcessoSeletivo), nameof(IngressanteController.BuscarTiposVinculoAluno), "Ingressante", true)]
        [SMCRequired]
        [SMCSelect(nameof(TiposVinculoAluno), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCStep(3, 2)]
        public long SeqTipoVinculoAluno { get; set; }

        [SMCDependency(nameof(SeqTipoVinculoAluno), nameof(IngressanteController.BuscarFormasIngresso), "Ingressante", true, nameof(SeqProcessoSeletivo))]
        [SMCRequired]
        [SMCSelect(nameof(FormasIngresso), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCStep(3, 2)]
        public long SeqFormaIngresso { get; set; }

        [InstituicaoExternaLookup]
        [SMCConditionalDisplay(nameof(SeqFormaIngresso), "true", DataAttribute = "data-transferencia")]
        [SMCConditionalRequired(nameof(SeqFormaIngresso), "true", DataAttribute = "data-transferencia")]
        [SMCSize(SMCSize.Grid12_24)]
        public InstituicaoExternaLookupViewModel SeqInstituicaoTransferenciaExterna { get; set; }

        [SMCConditionalDisplay(nameof(SeqFormaIngresso), "true", DataAttribute = "data-transferencia")]
        [SMCConditionalRequired(nameof(SeqFormaIngresso), "true", DataAttribute = "data-transferencia")]
        [SMCSize(SMCSize.Grid6_24)]
        public string CursoTransferenciaExterna { get; set; }

        [SMCHidden]
        [SMCStep(3, 2)]
        public bool Ativas { get; set; } = true;

        [SMCDependency(nameof(SeqNivelEnsino), nameof(IngressanteController.TipoVinculoAlunoExigeOfertaMatrizCurricular), "Ingressante", true, new[] { nameof(SeqTipoVinculoAluno) })]
        [SMCDependency(nameof(SeqTipoVinculoAluno), nameof(IngressanteController.TipoVinculoAlunoExigeOfertaMatrizCurricular), "Ingressante", true, new[] { nameof(SeqNivelEnsino) })]
        [SMCHidden]
        [SMCStep(3, 2)]
        public bool? ExigeOfertaMatrizCurricular { get; set; }

        [SMCDependency(nameof(SeqNivelEnsino), nameof(IngressanteController.BuscarQuantidadeOfertaCampanhaIngresso), "Ingressante", true, new[] { nameof(SeqTipoVinculoAluno) })]
        [SMCDependency(nameof(SeqTipoVinculoAluno), nameof(IngressanteController.BuscarQuantidadeOfertaCampanhaIngresso), "Ingressante", true, new[] { nameof(SeqNivelEnsino) })]
        [SMCHidden]
        [SMCStep(3, 2)]
        public int QuantidadeOfertaCampanhaIngresso { get; set; }

        [SMCDetail(min: 1)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(3, 2)]
        public SMCMasterDetailList<IngressanteOfertaDetailViewModel> Ofertas { get; set; }

        /// <summary>
        /// Sequencial da campanha oferta representada pela Oferta acima e utilizado pelo datasource MatrizesCurricularesOferta
        /// </summary>
        [SMCIgnoreProp]
        public long? SeqCampanhaOferta => Ofertas?.FirstOrDefault()?.SeqCampanhaOferta?.Seq;

        [SMCConditionalDisplay(nameof(ExigeOfertaMatrizCurricular), true)]
        [SMCDependency(nameof(OfertasMatrizDependencyOfertas), nameof(IngressanteController.BuscarOfertasMatrizCurricular), "Ingressante", false, nameof(OfertasMatrizDependencyTermos), nameof(SeqNivelEnsino), nameof(SeqTipoVinculoAluno), nameof(SeqCicloLetivo))]
        [SMCDependency(nameof(OfertasMatrizDependencyTermos), nameof(IngressanteController.BuscarOfertasMatrizCurricular), "Ingressante", false, nameof(OfertasMatrizDependencyOfertas), nameof(SeqNivelEnsino), nameof(SeqTipoVinculoAluno), nameof(SeqCicloLetivo))]
        [SMCSelect(nameof(MatrizesCurricularesOferta), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(3, 2)]
        public long? SeqMatrizCurricularOferta { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(3, 2)]
        public List<PessoaAtuacaoCondicaoObrigatoriedadeViewModel> CondicoesObrigatoriedade { get; set; }

        [SMCDependency(nameof(SeqNivelEnsino), nameof(IngressanteController.TipoVinculoAlunoExigeParceriaIntercambioIngresso), "Ingressante", true, new[] { nameof(SeqTipoVinculoAluno) })]
        [SMCDependency(nameof(SeqTipoVinculoAluno), nameof(IngressanteController.TipoVinculoAlunoExigeParceriaIntercambioIngresso), "Ingressante", true, new[] { nameof(SeqNivelEnsino) })]
        [SMCHidden]
        [SMCStep(3, 2)]
        public bool? ExigeParceriaIntercambioIngresso { get; set; }

        [SMCHidden]
        [SMCStep(3, 2)]
        public TipoMobilidade TipoMobilidade { get; set; } = TipoMobilidade.IngressoEmNossaInstituicao;

        [SMCDetail(SMCDetailType.Modal, min: 1, windowSize: SMCModalWindowSize.Large)]
        [SMCReadOnly(SMCViewMode.ReadOnly)] //FIX: Remover ao corrigir o ReadOnly
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(3, 2)]
        public SMCMasterDetailList<PessoaAtuacaoTermoIntercambioViewModel> TermosIntercambio { get; set; }

        [SMCDependency(nameof(SeqNivelEnsino), nameof(IngressanteController.ValidarOrientacaoPermitida), "Ingressante", true, includedProperties: new[] { nameof(SeqTipoVinculoAluno) })]
        [SMCDependency(nameof(SeqTipoVinculoAluno), nameof(IngressanteController.ValidarOrientacaoPermitida), "Ingressante", true, includedProperties: new[] { nameof(SeqNivelEnsino) })]
        [SMCHidden]
        [SMCStep(3, 2)]
        public bool? PermiteOrientacaoPessoaAtuacao { get; set; }

        [SMCDependency(nameof(SeqNivelEnsino), nameof(IngressanteController.ValidarOrientacaoRequerida), "Ingressante", true, includedProperties: new[] { nameof(SeqTipoVinculoAluno) })]
        [SMCDependency(nameof(SeqTipoVinculoAluno), nameof(IngressanteController.ValidarOrientacaoRequerida), "Ingressante", true, includedProperties: new[] { nameof(SeqNivelEnsino) })]
        [SMCHidden]
        [SMCStep(3, 2)]
        public bool? RequerOrientacaoPessoaAtuacao { get; set; }

        [SMCIgnoreProp]
        public bool PossuiTipoIntercambio { get => false; }

        [SMCIgnoreProp]
        public CadastroOrientacao[] CadastroOrientacoesIngressante { get => new CadastroOrientacao[] { CadastroOrientacao.Exige, CadastroOrientacao.Permite }; }

        [SMCConditionalDisplay(nameof(PermiteOrientacaoPessoaAtuacao), true)]
        [SMCConditionalRequired(nameof(RequerOrientacaoPessoaAtuacao), true)]
        [SMCDependency(nameof(SeqNivelEnsino), nameof(IngressanteController.BuscarTiposOrientacao), "Ingressante", true, includedProperties: new[] { nameof(SeqTipoVinculoAluno) })]
        [SMCDependency(nameof(SeqTipoVinculoAluno), nameof(IngressanteController.BuscarTiposOrientacao), "Ingressante", true, includedProperties: new[] { nameof(SeqNivelEnsino) })]
        [SMCSelect(nameof(TiposOrientacaoPessoaAtuacao))]
        [SMCSize(SMCSize.Grid6_24)]
        public long? SeqTipoOrientacao { get; set; }

        [SMCConditionalDisplay(nameof(PermiteOrientacaoPessoaAtuacao), true, RuleName = "OrientacaoPermitida")]
        [SMCConditionalDisplay(nameof(SeqTipoOrientacao), SMCConditionalOperation.NotEqual, "", RuleName = "TipoOrientacaoPreenchida")]
        [SMCConditionalRule("OrientacaoPermitida && TipoOrientacaoPreenchida")]
        [SMCDetail]
        [SMCReadOnly(SMCViewMode.ReadOnly)] //FIX: Remover ao corrigir o ReadOnly
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(3, 2)]
        public SMCMasterDetailList<IngressanteOrientacaoPessoaAtuacaoDetailViewModel> OrientacaoParticipacoesColaboradores { get; set; }

        [SMCIgnoreProp]
        public List<long> SeqsCampanhaOferta => Ofertas?.Where(w => w.SeqCampanhaOferta?.Seq != null).Select(s => s.SeqCampanhaOferta.Seq.Value).ToList();

        [SMCIgnoreProp]
        public List<long> SeqsTermoIntercambio => TermosIntercambio?.Where(w => w.SeqTermoIntercambio != null && w.SeqTermoIntercambio.Seq.HasValue).Select(s => s.SeqTermoIntercambio.Seq.Value).ToList();

        [SMCHidden]
        public string OfertasMatrizDependencyTermos { get; set; }

        [SMCHidden]
        public string OfertasMatrizDependencyOfertas { get; set; }

        #endregion [ Dados Acadêmicos Wizard4 Tab2 ]

        #region [ Grupo de escalonamento Wizard5 Tab3 ]

        [SMCHidden]
        public bool? GrupoEscalonamentoAtivo { get; set; } = true;

        [GrupoEscalonamentoLookup]
        [SMCDependency(nameof(GrupoEscalonamentoAtivo))]
        [SMCDependency(nameof(SeqProcesso))]
        [SMCOrder(60)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(5, 3)]
        public GrupoEscalonamentoLookupViewModel SeqGrupoEscalonamento { get; set; }

        #endregion [ Grupo de escalonamento Wizard5 Tab3 ]

        #region [ Confirmação Wizard6 ]

        [SMCStep(5)]
        public string NomeConfirmacao { get; set; }

        [SMCStep(5)]
        public string VinculoConfirmacao { get; set; }

        [SMCStep(5)]
        public string NumeroPassaporteConfirmacao { get; set; }

        [SMCStep(5)]
        public string EmailConfirmacao { get; set; }

        [SMCStep(5)]
        public string MatrizCurricularOfertaConfirmacao { get; set; }

        [SMCStep(5)]
        public List<IngressanteCondicaoObrigatoriedadeConfirmacaoViewModel> CondicoesObrigatoriedadeConfirmacao { get; set; }

        #endregion [ Confirmação Wizard6 ]

        #region [ Configuracao ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.CssClass(insert: "smc-sga-wizard-ingressante")
                   .Ajax()
                   .Javascript("Areas/ALN/Ingressante/IngressanteDynamic")
                   .Detail<IngressanteListarDynamicModel>("_DetailList")
                   .Wizard(SMCDynamicWizardEditMode.Tab)
                   .DisableInitialListing(true)
                   .Service<IIngressanteService>(index: nameof(IIngressanteService.BuscarIngressantes),
                                                 insert: nameof(IIngressanteService.BuscarConfiguracaoIngressante),
                                                 save: nameof(IIngressanteService.SalvarIngressante),
                                                 edit: nameof(IIngressanteService.BuscarIngressante),
                                                 delete: nameof(IIngressanteService.ExcluirIngressante))
                   .Tokens(tokenInsert: UC_ALN_002_01_02.MANTER_INGRESSANTE,
                           tokenEdit: UC_ALN_002_01_02.MANTER_INGRESSANTE,
                           tokenList: UC_ALN_002_01_01.PESQUISAR_INGRESSANTE,
                           tokenRemove: UC_ALN_002_01_02.MANTER_INGRESSANTE)
                   .ModalSize(SMCModalWindowSize.Largest);
            
            this.TipoAtuacaoAuxiliar = TipoAtuacao.Ingressante;
        }

        #endregion [ Configuracao ]

        #region [ Helpers ]

        /// <summary>
        /// Valida se a situação de ingressante não permite edição
        /// </summary>
        /// <param name="situacao">Situacao a ser validada</param>
        /// <returns>True caso a situação informada não permita edição</returns>
        public static bool SituacaoNaoPermiteEdicao(SituacaoIngressante situacao) =>
            situacao == SituacaoIngressante.Matriculado ||
            situacao == SituacaoIngressante.Desistente ||
            situacao == SituacaoIngressante.Cancelado;

        #endregion [ Helpers ]
    }
}