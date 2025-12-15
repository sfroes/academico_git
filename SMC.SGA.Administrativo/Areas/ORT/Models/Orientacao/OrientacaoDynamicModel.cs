using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ORT.Controllers;
using SMC.SGA.Administrativo.Areas.ORT.Views.Orientacao.App_LocalResources;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    [SMCStepConfiguration(ActionStep = nameof(OrientacaoController.Passo1), UseOnTabs = true)]
    [SMCStepConfiguration(ActionStep = nameof(OrientacaoController.Passo2), Partial = "_Alunos", UseOnTabs = false)]
    [SMCStepConfiguration(ActionStep = nameof(OrientacaoController.Passo3), Partial = "_Professores", UseOnTabs = false)]
    [SMCStepConfiguration(ActionStep = nameof(OrientacaoController.Passo4), Partial = "_DadosConfirmacao", UseOnTabs = false)]
    public class OrientacaoDynamicModel : SMCDynamicViewModel, ISMCWizardViewModel, ISMCStatefulView, ISMCMappable
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarUnidadesResponsaveisGPILocalSelect))]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICursoOfertaLocalidadeService), nameof(ICursoOfertaLocalidadeService.BuscarLocalidadesAtivasSelect))]
        public List<SMCDatasourceItem> Localidades { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoVinculoAlunoService), nameof(IInstituicaoNivelTipoVinculoAlunoService.BuscarTipoOrientacaoPorNivelEnsinoPermiteInclusaoManual), values: new[] { nameof(SeqNivelEnsino), nameof(SeqTipoVinculoAluno), nameof(SeqTipoIntercambio) })]
        public List<SMCDatasourceItem> TiposOrientacoes { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoOrientacaoParticipacaoService), nameof(IInstituicaoNivelTipoOrientacaoParticipacaoService.BuscarInstituicaoNivelTipoOrientacaoParticipacaoSelect), values: new string[] { nameof(SeqNivelEnsino), nameof(SeqTipoOrientacao), nameof(SeqTipoVinculo), nameof(SeqTipoIntercambio) })]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> TiposParticipacao { get; set; }

        [SMCHidden]
        [SMCDataSource]
        [SMCServiceReference(typeof(IColaboradorService), nameof(IColaboradorService.BuscarColaboradoresOrientacaoSelect), values: new[] { nameof(SeqNivelEnsino), nameof(SeqsAlunos) })]
        public List<SMCDatasourceItem> Colaboradores { get; set; }

        [SMCHidden]
        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoVinculoAlunoService), nameof(IInstituicaoNivelTipoVinculoAlunoService.BuscarTipoVinculoPorNivelEnsinoPermiteManutencaoManual), values: new[] { nameof(SeqNivelEnsino) })]
        public List<SMCDatasourceItem> TiposVinculos { get; set; }

        [SMCHidden]
        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoVinculoAlunoService), nameof(IInstituicaoNivelTipoVinculoAlunoService.BuscarTermoIntercambioPorNivelEnsinoPermiteManutencaoManual), values: new[] { nameof(SeqNivelEnsino), nameof(SeqTipoVinculoAluno) })]
        public List<SMCDatasourceItem> TiposIntercambio { get; set; }

        #endregion [ DataSources ]

        #region Campos Hidden

        [SMCHidden]
        public int Step { get; set; }

        [SMCHidden]
        [SMCStep(0, 0)]
        public int StepOrigem { get; set; }

        [SMCHidden]
        [SMCStep(1, 0)]
        public int StepOrigemAluno { get; set; }

        [SMCKey]
        [SMCHidden]
        [SMCStep(0, 0)]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCStep(0, 0)]
        public long SeqEntidadeInstituicao { get; set; }

        [SMCHidden]
        [SMCStep(1, 0)]
        public TipoAtuacao TipoAtuacao { get; set; }

        [SMCHidden]
        public long SeqNivelEnsinoComparacao { get; set; }

        [SMCHidden]
        public long SeqTipoVinculoComparacao { get; set; }

        [SMCHidden]
        public long? SeqTipoIntercambioComparacao { get; set; }

        [SMCHidden]
        public long SeqTipoOrientacaoComparacao { get; set; }

        [SMCHidden]
        public int NumeroMaximoAlunos { get; set; }

        [SMCIgnoreProp]
        public long SeqTipoVinculo { get { return this.SeqTipoVinculoAluno; } }

        public long[] SeqsAlunos { get { return this.OrientacoesPessoaAtuacao?.Select(s => s.SeqPessoaAtuacao?.Seq ?? 0).ToArray(); } }

        [SMCHidden]
        public List<long> OrientacoesPessoaAtuacaoComparacao { get; set; }

        [SMCHidden]
        [SMCIgnoreProp]
        public string TipoTermoIntercambioNameDescriptionField { get; set; }

        /// <summary>
        /// Criado para não causar impacto nas rotinas que utlizam o campo com este nome
        /// </summary>
        [SMCHidden]
        [SMCIgnoreProp]
        public long? SeqTipoIntercambio { get => this.SeqTipoTermoIntercambio; }

        #endregion Campos Hidden

        #region Passo 1 - Dados da Orientação

        [SMCConditionalDisplay(nameof(StepOrigem), SMCConditionalOperation.GreaterThen, 1)]
        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-alerta")]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCStep(0)]
        public string MensagemAlerta { get { return UIResource.MensagemProximoPasso; } }

        [SMCFilter(true, true)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSelect(nameof(NiveisEnsino), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCStep(0, 0)]
        public long SeqNivelEnsino { get; set; }

        [SMCDependency(nameof(SeqNivelEnsino), nameof(OrientacaoController.BuscarTipoVinculoSelect), "Orientacao", true)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSelect(nameof(TiposVinculos))]
        [SMCStep(0, 0)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        public long SeqTipoVinculoAluno { get; set; }

        [SMCDependency(nameof(SeqNivelEnsino), nameof(OrientacaoController.BuscarTipoIntecambioSelect), "Orientacao", true, new string[] { nameof(SeqTipoVinculoAluno) })]
        [SMCDependency(nameof(SeqTipoVinculoAluno), nameof(OrientacaoController.BuscarTipoIntecambioSelect), "Orientacao", true, new string[] { nameof(SeqNivelEnsino), nameof(Seq) })]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCSelect(nameof(TiposIntercambio), NameDescriptionField = nameof(TipoTermoIntercambioNameDescriptionField))]
        [SMCStep(0, 0)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public long? SeqTipoTermoIntercambio { get; set; }

        //[SMCDependency(nameof(SeqNivelEnsino), nameof(OrientacaoController.BuscarTipoOperacaoSelect), "Orientacao", false, new string[] { nameof(SeqTipoVinculoAluno), nameof(SeqTipoTermoIntercambio) })]
        [SMCDependency(nameof(SeqTipoVinculoAluno), nameof(OrientacaoController.BuscarTipoOrientacaoSelect), "Orientacao", true, new string[] { nameof(SeqNivelEnsino), nameof(SeqTipoTermoIntercambio) })]
        [SMCDependency(nameof(SeqTipoTermoIntercambio), nameof(OrientacaoController.BuscarTipoOrientacaoSelect), "Orientacao", false, new string[] { nameof(SeqNivelEnsino), nameof(SeqTipoVinculoAluno) })]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSelect(nameof(TiposOrientacoes), autoSelectSingleItem: true, NameDescriptionField = nameof(SeqTipoOrientacaoDescriptionField))]
        [SMCStep(0, 0)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public long SeqTipoOrientacao { get; set; }

        [SMCIgnoreProp]
        public string SeqTipoOrientacaoDescriptionField { get; set; }

        //[SMCRequired]
        //[SMCStep(0, 0)]
        //[SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid5_24)]
        //public DateTime DataInicioOrientacao { get; set; }

        //[SMCMinDate(nameof(DataInicioOrientacao))]
        //[SMCStep(0, 0)]
        //[SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid5_24)]
        //public DateTime? DataFimOrientacao { get; set; }

        #endregion Passo 1 - Dados da Orientação

        #region Passo 2 - Aluno(s)

        [SMCConditionalDisplay(nameof(StepOrigemAluno), SMCConditionalOperation.GreaterThen, 1)]
        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-alerta")]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCStep(1)]
        public string MensagemAlertaAluno { get { return UIResource.MensagemAlertaAluno; } }

        [SMCDetail(min: 1)]
        [SMCInclude(nameof(OrientacoesPessoaAtuacao))]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(1, 0)]
        public SMCMasterDetailList<OrientacaoPessoaAtuacaoMasterDetailViewModel> OrientacoesPessoaAtuacao { get; set; }

        #endregion Passo 2 - Aluno(s)

        #region Passo 3 - Professor(es)

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-informativa smc-sga-mensagem-multiplas-linhas")]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCStep(2, 0)]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemIformativa { get; set; }

        [SMCDetail(min: 1)]
        [SMCInclude(nameof(OrientacoesColaborador))]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(2, 0)]
        public SMCMasterDetailList<OrientacoesColaboradorMasterDetailViewModel> OrientacoesColaborador { get; set; }

        #endregion Passo 3 - Professor(es)

        #region Passo4 -Confirmação

        [SMCStep(3)]
        public string DescricaoNivelEnsino { get; set; }

        [SMCStep(3)]
        public string DescricaoTipoVinculoAluno { get; set; }

        [SMCStep(3)]
        public string DescricaoTipoIntercambio { get; set; }

        [SMCStep(3)]
        public string DescricaoTipoOrientacao { get; set; }

        [SMCStep(3)]
        public List<string> ListaNomeProfessores { get; set; }

        #endregion Passo4 -Confirmação

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.CssClass(insert: "smc-sga-wizard-orientacao", edit: "smc-sga-wizard-orientacao").Ajax()
            .Wizard(editMode: SMCDynamicWizardEditMode.Tab)
            .Detail<OrientacaoListarDynamicModel>("_DetailList")
            .DisableInitialListing(true)
           .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                        ((OrientacaoListarDynamicModel)x).NomesAlunosExclucao))
            .Tokens(tokenInsert: UC_ORT_001_02_02.MANTER_ORIENTACAO,
                    tokenEdit: UC_ORT_001_02_02.MANTER_ORIENTACAO,
                    tokenRemove: UC_ORT_001_02_02.MANTER_ORIENTACAO,
                    tokenList: UC_ORT_001_02_01.PESQUISAR_ORIENTACAO)
            .Service<IOrientacaoService>(index: nameof(IOrientacaoService.BuscarOrientacoes),
                                         save: nameof(IOrientacaoService.SalvarOrientacao),
                                         edit: nameof(IOrientacaoService.AlterarOrientacao),
                                         delete: nameof(IOrientacaoService.ExcluirOrientacao));
        }
    }
}