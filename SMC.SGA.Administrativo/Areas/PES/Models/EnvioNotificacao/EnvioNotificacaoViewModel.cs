using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.SGA.Administrativo.Areas.PES.Views.EnvioNotificacao.App_LocalResources;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using SMC.Notificacoes.UI.Mvc.Models;
using SMC.SGA.Administrativo.Areas.CSO.Controllers;
using SMC.SGA.Administrativo.Areas.DCT.Controllers;
using SMC.SGA.Administrativo.Areas.PES.Controllers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Http;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class EnvioNotificacaoViewModel : SMCPagerViewModel, ISMCWizardViewModel, ISMCMappable, ISMCStep
    {
        #region [ Hidden ]

        [SMCIgnoreProp]
        public int Step { get; set; }

        [SMCHidden]
        public bool RetornarInstituicaoEnsinoLogada { get => true; }

        #endregion [ Hidden ]

        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(ITipoVinculoAlunoService), nameof(ITipoVinculoAlunoService.BuscarTipoVinculoAlunoNaInstituicaoSelect))]
        public List<SMCDatasourceItem> TiposVinculoAluno { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ISituacaoMatriculaService), nameof(ISituacaoMatriculaService.BuscarSituacoesMatriculasDaInstiuicaoSelect))]
        public List<SMCDatasourceItem> SituacoesMatricula { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoAtividadeColaboradorService), nameof(IInstituicaoNivelTipoAtividadeColaboradorService.BuscarTiposAtividadeColaboradorSelect))]
        public List<SMCDatasourceItem> TiposAtividadeColaborador { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ICursoOfertaLocalidadeService), nameof(ICursoOfertaLocalidadeService.BuscarEntidadesSuperioresSelect), values: new string[] { nameof(LocalidadesAtivas) })]
        public List<SMCDatasourceItem> Localidades { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        #endregion [ DataSources ]

        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect(IgnoredEnumItems = new object[] { TipoAtuacao.Ingressante, TipoAtuacao.Funcionario, TipoAtuacao.Nenhum })]
        [SMCFilter(true)]
        public TipoAtuacao TipoAtuacao { get; set; }

        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCDependency(nameof(TipoAtuacao), nameof(EnvioNotificacaoController.BuscarEntidadeResponsavel), "EnvioNotificacao", "PES", true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid8_24)]
        [SMCFilter(true)]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ITipoVinculoColaboradorService), nameof(ITipoVinculoColaboradorService.BuscarTipoVinculoColaboradorPorEntidadesSelect), values: new[] { nameof(SeqsEntidadesResponsaveis) })]
        public List<SMCDatasourceItem> TiposVinculoColaborador { get; set; }

        [SMCHidden]
        public bool LocalidadesAtivas => true;

        [SMCSelect(nameof(Localidades), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24)]
        [SMCConditionalDisplay(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Aluno)]
        [SMCFilter(true)]
        public long? SeqLocalidade { get; set; }

        [SMCSelect(nameof(NiveisEnsino), AutoSelectSingleItem = true)]
        [SMCConditionalDisplay(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Aluno)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        [SMCFilter(true)]
        public long? SeqNivelEnsino { get; set; }

        [CursoOfertaLookup]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCDependency(nameof(SeqLocalidade))]
        [SMCDependency(nameof(SeqNivelEnsino))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        [SMCConditionalDisplay(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Aluno)]
        [SMCFilter(true)]
        public CursoOfertaLookupViewModel SeqCursoOferta { get; set; }

        [SMCConditionalReadonly(nameof(SeqCursoOferta), SMCConditionalOperation.Equals, "", RuleName = "Rule1")]
        [SMCConditionalReadonly(nameof(SeqLocalidade), SMCConditionalOperation.Equals, "", RuleName = "Rule2")]
        [SMCConditionalRule("Rule1 && Rule2")]
        [SMCDependency(nameof(SeqCursoOferta), nameof(TurnoController.BuscarTurnosPorCursoOfertaOuLocalidadeSelect), "Turno", "CSO", false, includedProperties: new[] { nameof(SeqLocalidade) })]
        [SMCDependency(nameof(SeqLocalidade), nameof(TurnoController.BuscarTurnosPorCursoOfertaOuLocalidadeSelect), "Turno", "CSO", false, includedProperties: new[] { nameof(SeqCursoOferta) })]
        [SMCSelect]
        [SMCConditionalDisplay(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Aluno)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        [SMCFilter(true)]
        public long? SeqTurno { get; set; }

        [SMCDependency(nameof(SeqNivelEnsino), nameof(EnvioNotificacaoController.BuscarTipoVinculoAluno), "EnvioNotificacao", false)]
        [SMCSelect(nameof(TiposVinculoAluno))]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        [SMCConditionalDisplay(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Aluno)]
        [SMCFilter(true)]
        public long? SeqTipoVinculoAluno { get; set; }

        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCConditionalDisplay(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Aluno)]
        [SMCFilter(true)]
        public CicloLetivoLookupViewModel SeqCicloLetivoSituacaoMatricula { get; set; }

        [SMCConditionalReadonly(nameof(SeqCicloLetivoSituacaoMatricula), SMCConditionalOperation.Equals, "")]
        [SMCConditionalRequired(nameof(SeqCicloLetivoSituacaoMatricula), SMCConditionalOperation.NotEqual, "", RuleName = "R1")]
        [SMCConditionalRequired(nameof(SeqsSituacaoMatriculaCicloLetivo), SMCConditionalOperation.LessThen, 1, RuleName = "R2")]
        [SMCConditionalRule("R1 && R2")]
        [SMCDependency(nameof(SeqNivelEnsino), nameof(EnvioNotificacaoController.BuscarSituacoesMatricula), "EnvioNotificacao", false, nameof(SeqTipoVinculoAluno))]
        [SMCDependency(nameof(SeqTipoVinculoAluno), nameof(EnvioNotificacaoController.BuscarSituacoesMatricula), "EnvioNotificacao", false, nameof(SeqNivelEnsino))]
        [SMCSelect(nameof(SituacoesMatricula))]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid6_24)]
        [SMCConditionalDisplay(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Aluno)]
        [SMCFilter(true)]
        public List<long> SeqsSituacaoMatriculaCicloLetivo { get; set; }

        [FormacaoEspecificaLookup]
        [SMCConditionalReadonly(nameof(SeqCursoOferta), "")]
        [SMCDependency(nameof(SeqCursoOferta))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid8_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCConditionalDisplay(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Aluno)]
        [SMCFilter(true)]
        public FormacaoEspecificaLookupViewModel SeqFormacaoEspecifica { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        [SMCConditionalDisplay(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Aluno)]
        [SMCRegularExpression(@"^[0-9]{0,}[.]{0,1}[0-9]{0,}$", FormatErrorResourceKey = "ERR_Expression")]
        [SMCFilter(true)]
        public string Turma { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCConditionalDisplay(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Aluno)]
        [SMCFilter(true)]
        public long? NumeroRegistroAcademico { get; set; }

        [SMCMaxDate("DataFim")]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        [SMCConditionalDisplay(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Colaborador)]
        [SMCConditionalRequired(nameof(DataFim), SMCConditionalOperation.NotEqual, "")]
        [SMCFilter(true)]
        public DateTime? DataInicio { get; set; }

        [SMCMinDate("DataInicio")]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        [SMCConditionalDisplay(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Colaborador)]
        [SMCConditionalRequired(nameof(DataInicio), SMCConditionalOperation.NotEqual, "")]
        [SMCFilter(true)]
        public DateTime? DataFim { get; set; }

        [CursoOfertaLocalidadeLookup]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid6_24)]
        [SMCConditionalDisplay(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Colaborador)]
        [SMCFilter(true)]
        public CursoOfertaLocalidadeLookupViewModel SeqCursoOfertaLocalidade { get; set; }

        [SMCSelect(nameof(TiposAtividadeColaborador))]
        [SMCDependency(nameof(SeqCursoOfertaLocalidade), nameof(ColaboradorController.BuscarTiposAtividadeColaboradorSelect), "Colaborador", "DCT", true)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        [SMCConditionalDisplay(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Colaborador)]
        [SMCConditionalReadonly(nameof(SeqCursoOfertaLocalidade), SMCConditionalOperation.Equals, "")]
        [SMCFilter(true)]
        public TipoAtividadeColaborador? TipoAtividade { get; set; }

        [InstituicaoExternaLookup]
        [SMCDependency(nameof(RetornarInstituicaoEnsinoLogada))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid6_24)]
        [SMCConditionalDisplay(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Colaborador)]
        [SMCFilter(true)]
        public InstituicaoExternaLookupViewModel SeqInstituicaoExterna { get; set; }

        [SMCConditionalDisplay(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Colaborador)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid6_24)]
        [SMCFilter(true)]
        public long? SeqColaborador { get; set; }

        [SMCDependency(nameof(SeqsEntidadesResponsaveis), nameof(EnvioNotificacaoController.BuscarTipoVinculoColaboradorPorEntidadesSelect), "EnvioNotificacao", "PES", true)]
        [SMCSelect(nameof(TiposVinculoColaborador))]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid10_24)]
        [SMCConditionalDisplay(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Colaborador)]
        [SMCFilter(true)]
        public long? SeqTipoVinculoColaborador { get; set; }

        #region Configuracao - Step 2

        public Notificacoes.UI.Mvc.Models.ConfiguracaoNotificacaoEmailViewModel ConfiguracaoNotificacao { get; set; }

        [SMCDataSource("", "", "", SMCStorageType.HttpContext)]
        [SMCServiceReference(typeof(INotificacaoService), "BuscarLayoutNotificacaoEmailPorSiglaGrupoAplicacao", null, new string[] { "SGA" }, false)]
        public List<SMCDatasourceItem> LayoutEmail { get; set; }

        [SMCSelect("LayoutEmail", AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid24_24)]
        public long? SeqLayoutEmail { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCCssClass("smc-sga-mensagem-informativa smc-sga-mensagem smc-sga-display-mensagem")]
        [SMCHideLabel(true)]
        public string Observacao => UIResource.Mensagem_Observacao;

        public string Token { get; set; }

        #endregion Configuracao - Step 2

        [SMCHideLabel(true)]
        public List<long> SelectedValues { get; set; }

        #region Confirmação - Step 3

        public SMCPagerModel<EnvioNotificacaoPessoasListarViewModel> Destinatarios { get; set; }

        #endregion Confirmação - Step 3

        public bool UtilizarAutoSelect { get; set; }

        [SMCHidden]
        public bool AlterouTipoAtuacao { get; set; }
        [SMCHidden]
        public bool RetornouStep { get; set; }

    }
}