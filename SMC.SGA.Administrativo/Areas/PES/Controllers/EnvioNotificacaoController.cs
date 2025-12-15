using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Domain.Areas.FIN.Jobs;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Data;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using SMC.Notificacoes.UI.Mvc.Models;
using SMC.SGA.Administrativo.Areas.PES.Models;
using SMC.SGA.Administrativo.Areas.PES.Models.EnvioNotificacao;

namespace SMC.SGA.Administrativo.Areas.PES.Controllers
{
    public class EnvioNotificacaoController : SMCControllerBase
    {
        #region [Constantes]
        private const string SESSION_ENVIO_NOTIFICACAO_FILTRO_KEY = "EnvioNotificacaoFiltro";
        private const string TEMPDATA_ACTION_VOLTAR_NAVEGADOR_KEY = "isActioVoltarNavegador";
        private const string SESSION_ACTION_LIMPAR_CAMPOS_FILTRO_KEY = "isActioLimparCamposFiltro";
        private const string SESSION_WIZARD_KEY = "EnvioNotificacaoWizard";
        #endregion [Constantes]
        #region [ Services ]

        private IEnvioNotificacaoService EnvioNotificacaoService => Create<IEnvioNotificacaoService>();
        private ITipoVinculoAlunoService TipoVinculoAlunoService => Create<ITipoVinculoAlunoService>();
        private IFormaIngressoService FormaIngressoService => Create<IFormaIngressoService>();

        private ISituacaoMatriculaService SituacaoMatriculaService => Create<ISituacaoMatriculaService>();

        internal ICursoOfertaLocalidadeService CursoOfertaLocalidadeService => Create<ICursoOfertaLocalidadeService>();
        internal IEntidadeService EntidadeService => Create<IEntidadeService>();
        internal IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();
        internal IInstituicaoNivelService InstituicaoNivelService => Create<IInstituicaoNivelService>();
        internal IInstituicaoNivelTipoDocumentoAcademicoService InstituicaoNivelTipoDocumentoAcademico => Create<IInstituicaoNivelTipoDocumentoAcademicoService>();
        internal IInstituicaoNivelTipoDocumentoAcademicoService InstituicaoNivelTipoDocumentoAcademicoService => Create<IInstituicaoNivelTipoDocumentoAcademicoService>();
        internal IInstituicaoNivelTipoAtividadeColaboradorService InstituicaoNivelTipoAtividadeColaboradorService => Create<IInstituicaoNivelTipoAtividadeColaboradorService>();
        internal INotificacaoService NotificacaoService => Create<INotificacaoService>();
        private ITipoVinculoColaboradorService TipoVinculoColaboradorService => Create<ITipoVinculoColaboradorService>();


        #endregion [ Services ]


        [SMCAuthorize(UC_PES_008_01_01.PESQUISAR_NOTIFICACAO)]
        public ActionResult Index(string voltarNavegador = null, string limparCamposFiltro = null)
        {
            var filtro = CarregarFiltroDaSession();

            if (IsActionVoltarDoNavegador(voltarNavegador))
                return View(filtro);

            if (IsActionLimparCamposFiltro(limparCamposFiltro))
                LimparFiltroDaSession();

            return View(filtro);
        }

        private bool IsActionVoltarDoNavegador(string voltarNavegador)
        {
            bool voltar = !string.IsNullOrEmpty(voltarNavegador) && voltarNavegador != "false";
            TempData[TEMPDATA_ACTION_VOLTAR_NAVEGADOR_KEY] = voltar;
            return voltar;
        }

        private bool IsActionLimparCamposFiltro(string limparCamposFiltro)
        {
            bool limpar = !string.IsNullOrEmpty(limparCamposFiltro) && limparCamposFiltro != "false";
            TempData[SESSION_ACTION_LIMPAR_CAMPOS_FILTRO_KEY] = limpar;
            return limpar;
        }

        private void LimparFiltroDaSession()
        {
            SalvarFiltrosSessionSeAlterado(new EnvioNotificacaoFiltroViewModel());
        }

        private EnvioNotificacaoWizardViewModel GetWizardSession()
        {
            return (EnvioNotificacaoWizardViewModel)Session[SESSION_WIZARD_KEY] ?? new EnvioNotificacaoWizardViewModel();
        }

        private void SetWizardSession(EnvioNotificacaoWizardViewModel model)
        {
            Session[SESSION_WIZARD_KEY] = model;
        }
        [SMCAuthorize(UC_PES_008_01_01.PESQUISAR_NOTIFICACAO)]
        public ActionResult BuscarEnvioNotificacoes(EnvioNotificacaoFiltroViewModel filtros)
        {
            if (IsTempDataActionVoltarDoNavegador())
                SalvarFiltrosSessionSeAlterado(filtros);
            EnvioNotificacaoFiltroViewModel filtroViewModel = CarregarFiltroDaSession();
            SMCPagerData<EnvioNotificacaoListarData> notificacoes = this.EnvioNotificacaoService.BuscarEnvioNotificacoes(filtroViewModel.Transform<EnvioNotificacaoFiltroData>());
            var viewModelLista = notificacoes.Transform<SMCPagerData<EnvioNotificacaoListarViewModel>>();
            var model = new SMCPagerModel<EnvioNotificacaoListarViewModel>(viewModelLista, filtroViewModel.PageSettings, filtroViewModel);

            return PartialView("_DetailList", model);
        }

        private bool IsTempDataActionVoltarDoNavegador()
        {
            return TempData[TEMPDATA_ACTION_VOLTAR_NAVEGADOR_KEY] == null || !((bool)TempData[TEMPDATA_ACTION_VOLTAR_NAVEGADOR_KEY]);
        }

        private EnvioNotificacaoFiltroViewModel CarregarFiltroDaSession()
        {
            if (Session[SESSION_ENVIO_NOTIFICACAO_FILTRO_KEY] is string filtroJson)
            {
                return JsonConvert.DeserializeObject<EnvioNotificacaoFiltroViewModel>(filtroJson)
                       ?? new EnvioNotificacaoFiltroViewModel();
            }
            return new EnvioNotificacaoFiltroViewModel();
        }

        /// <summary>
        /// Armazena filtros da tela no tempData, para recuperá-los quando o usuário clica em Voltar
        /// </summary>
        /// <param name="filtros">ViewModel do filtro da tela inicial de Envio De Notificações</param>
        private void SalvarFiltrosSessionSeAlterado(EnvioNotificacaoFiltroViewModel filtros)
        {
            string filtrosJson = JsonConvert.SerializeObject(filtros);
            if (!filtrosJson.Equals(Session[SESSION_ENVIO_NOTIFICACAO_FILTRO_KEY]))
                Session[SESSION_ENVIO_NOTIFICACAO_FILTRO_KEY] = filtrosJson;
        }

        [SMCAuthorize(UC_PES_008_01_02.ENVIAR_NOTIFICACAO)]
        public ActionResult Incluir(EnvioNotificacaoViewModel model)
        {
            return View(model);
        }

        [SMCAuthorize(UC_SRC_004_01_05.CONSULTAR_DADOS_NOTIFICACAO)]
        public ActionResult VisualizarNotificacao(SMCEncryptedLong seq)
        {
            VisualizarNotificacaoViewModel model = new VisualizarNotificacaoViewModel();
            EnvioNotificacaoListarData envioNotificacao = CarregarEnvioNotificacao(model, seq);
            string mensagemConfiguracaoEnvioNotificacao = CarregarConfiguracaoNotificacaoEmailService(model, envioNotificacao.SeqConfiguracaoTipoNotificacao);
            model.Mensagem = BuscarMensagemEmail(envioNotificacao.SeqLayoutMensagemEmail, mensagemConfiguracaoEnvioNotificacao);
            model.VisualizarDestinatariosNotificacaoViewModel = CarregarDestinatariosNotificacaoViewModel(envioNotificacao);
            return View("VisualizarNotificacao", model);
        }

        private string BuscarMensagemEmail(long? seqLayoutMensagemEmail, string mensagemConfiguracaoEnvioNotificacao)
        {
            if (!seqLayoutMensagemEmail.HasValue || seqLayoutMensagemEmail.Value == 0)
                return mensagemConfiguracaoEnvioNotificacao;

            var mensagemEmailMergeLayout = NotificacaoService.SimularMergeLayoutMensagemEmail(seqLayoutMensagemEmail.Value, null, mensagemConfiguracaoEnvioNotificacao);
            return string.IsNullOrEmpty(mensagemEmailMergeLayout)
                ? mensagemConfiguracaoEnvioNotificacao
                : mensagemEmailMergeLayout;
        }

        private static VisualizarDestinatariosNotificacaoViewModel CarregarDestinatariosNotificacaoViewModel(EnvioNotificacaoListarData envioNotificacao)
        {
            return new VisualizarDestinatariosNotificacaoViewModel()
            {
                TipoAtuacao = envioNotificacao.TipoAtuacao,
                SeqsDestinatarios = envioNotificacao.SeqsDestinatarios,
                SeqNotificacao = envioNotificacao.Seq
            };
        }

        private string CarregarConfiguracaoNotificacaoEmailService(VisualizarNotificacaoViewModel model, long seqConfiguracaoTipoNotificacao)
        {
            var configuracaoEnvioNotificacao = this.NotificacaoService.BuscarConfiguracaoNotificacaoEmail(seqConfiguracaoTipoNotificacao);
            model.EmailRemetente = configuracaoEnvioNotificacao.EmailOrigem;
            model.NomeRemetente = configuracaoEnvioNotificacao.NomeOrigem;
            model.EmailResposta = configuracaoEnvioNotificacao.EmailResposta;
            model.Mensagem = configuracaoEnvioNotificacao.Mensagem;
            model.Assunto = configuracaoEnvioNotificacao.Assunto;
            return configuracaoEnvioNotificacao.Mensagem;
        }

        private EnvioNotificacaoListarData CarregarEnvioNotificacao(VisualizarNotificacaoViewModel model, long seqEnvioNotificacao)
        {
            var envioNotificacao = this.EnvioNotificacaoService.BuscarEnvioNotificacoes(new EnvioNotificacaoFiltroData() { Seq = seqEnvioNotificacao }).FirstOrDefault();
            model.UsuarioEnvio = envioNotificacao.UsuarioEnvio;
            model.DataEnvio = envioNotificacao.DataEnvio;
            model.SeqsDestinatarios = envioNotificacao.SeqsDestinatarios;
            model.SeqLayoutMensagemEmail = envioNotificacao.SeqLayoutMensagemEmail;
            return envioNotificacao;
        }

        [SMCAuthorize(UC_PES_008_01_01.PESQUISAR_NOTIFICACAO)]
        public ActionResult VoltarParaHomeEnvioNotificacao()
        {
            TempData[TEMPDATA_ACTION_VOLTAR_NAVEGADOR_KEY] = false;
            TempData.Keep(TEMPDATA_ACTION_VOLTAR_NAVEGADOR_KEY);
            EnvioNotificacaoFiltroViewModel envioNotificacaoFiltroViewModel = new EnvioNotificacaoFiltroViewModel();

            if (Session[SESSION_ENVIO_NOTIFICACAO_FILTRO_KEY] is string filtroJson)
            {
                envioNotificacaoFiltroViewModel = JsonConvert.DeserializeObject<EnvioNotificacaoFiltroViewModel>(filtroJson);
            }
            return View("Index", envioNotificacaoFiltroViewModel);
        }

        [SMCAuthorize(UC_PES_008_01_02.ENVIAR_NOTIFICACAO)]
        public ActionResult WizardStepFiltroSelecao(EnvioNotificacaoViewModel model)
        {
            var wizardModel = GetWizardSession();
            wizardModel.Configuracao = model;

            var filtro = new EnvioNotificacaoFiltroSelecaoViewModel();

            filtro.TipoAtuacao = model.TipoAtuacao == TipoAtuacao.Nenhum ? TipoAtuacao.Aluno : model.TipoAtuacao;
            filtro.Localidades = CursoOfertaLocalidadeService.BuscarEntidadesSuperioresSelect(true);
            filtro.NiveisEnsino = InstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect();
            filtro.TiposVinculoAluno = TipoVinculoAlunoService.BuscarTiposVinculoAlunoSelect();

            // Se for retorno, popula os dados anteriores
            if (wizardModel.FiltroSelecao?.SelectedValues?.Any() == true && !model.AlterouTipoAtuacao)
            {
                filtro = wizardModel.FiltroSelecao;
                filtro.RetornouStep = true;

                filtro.TipoAtuacao = model.TipoAtuacao == TipoAtuacao.Nenhum ? TipoAtuacao.Aluno : model.TipoAtuacao;
                filtro.Localidades = CursoOfertaLocalidadeService.BuscarEntidadesSuperioresSelect(true);
                filtro.NiveisEnsino = InstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect();
                filtro.TiposVinculoAluno = TipoVinculoAlunoService.BuscarTiposVinculoAlunoSelect();


                // Recarrega combos
                if (filtro.SeqsEntidadesResponsaveis != null)
                {
                    if (filtro.TipoAtuacao == TipoAtuacao.Aluno)
                        filtro.EntidadesResponsaveis = EntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect(true);
                    else
                    {
                        filtro.TiposVinculoColaborador = TipoVinculoColaboradorService.BuscarTipoVinculoColaboradorPorEntidadesSelect(filtro.SeqsEntidadesResponsaveis);
                        filtro.EntidadesResponsaveis = EntidadeService.BuscarEntidadesVinculoColaboradorSelect(false);
                        filtro.TiposAtividadeColaborador = InstituicaoNivelTipoAtividadeColaboradorService.BuscarTiposAtividadeColaboradorSelect(new InstituicaoNivelTipoAtividadeColaboradorFiltroData());
                    }

                }

                if (filtro.SeqsSituacaoMatriculaCicloLetivo != null)
                {
                    filtro.SituacoesMatricula = SituacaoMatriculaService.BuscarSituacoesMatriculasDaInstiuicaoSelect(new SituacaoMatriculaFiltroData());
                }
            }


            wizardModel.FiltroSelecao = filtro;
            wizardModel.StepAtual = 0;
            SetWizardSession(wizardModel);


            return PartialView("_WizardStepFiltroSelecao", filtro);
        }
        [SMCAuthorize(UC_PES_008_01_02.ENVIAR_NOTIFICACAO)]
        public ActionResult WizardStepConfiguracao(EnvioNotificacaoFiltroSelecaoViewModel filtros)
        {
            var wizardModel = GetWizardSession();
            if (wizardModel.StepAtual == 0)
                wizardModel.FiltroSelecao = filtros;

            if (filtros.SelectedValues == null || !filtros.SelectedValues.Any())
                throw new EnvioNotificacaoObrigatoriedadeSelecaoPessoaAtuacaoException(filtros.TipoAtuacao.SMCGetDescription());


            //wizardModel.FiltroSelecao.PageSettings = filtros.PageSettings;

            var model = wizardModel.Configuracao;
            if (model == null)
                model = new EnvioNotificacaoViewModel();


            model.TipoAtuacao = filtros.TipoAtuacao;
            model.SelectedValues = filtros.SelectedValues;

            model.LayoutEmail = NotificacaoService.BuscarLayoutNotificacaoEmailPorSiglaGrupoAplicacao("SGA");
            if (model.TipoAtuacao == TipoAtuacao.Aluno)
                model.Token = "ENVIO_NOTIFICACAO_ALUNO";
            else
                model.Token = "ENVIO_NOTIFICACAO_PROFESSOR_PESQUISADOR";


            if (model.ConfiguracaoNotificacao == null)
                model.ConfiguracaoNotificacao = new ConfiguracaoNotificacaoEmailViewModel();

            model.ConfiguracaoNotificacao.SeqTipoNotificacao =
                NotificacaoService.BuscarSeqTipoNotificacaoPorTokenEGrupoAplicacao(model.Token, 2);

            model.Step = 1;

            wizardModel.Configuracao = model;

            wizardModel.StepAtual = 1;

            SetWizardSession(wizardModel);

            return PartialView("_WizardStepConfiguracao", model);
        }

        [SMCAuthorize(UC_PES_008_01_02.ENVIAR_NOTIFICACAO)]
        public ActionResult WizardStepConfirmacao(EnvioNotificacaoViewModel model)
        {
            var wizardModel = GetWizardSession();

            wizardModel.Configuracao = model;
            model.Step = 2;

            EnvioNotificacaoService.ValidaTagsEnvioNotificacao(model.Transform<EnvioNotificacaoData>());

            string mensagemSimulada = model.SeqLayoutEmail.HasValue
                ? NotificacaoService.SimularMergeLayoutMensagemEmail(model.SeqLayoutEmail.Value, null, model.ConfiguracaoNotificacao.Mensagem)
                : model.ConfiguracaoNotificacao.Mensagem;

            TempData["__mensagemSimulada"] = HttpUtility.HtmlDecode(mensagemSimulada);

            wizardModel.StepAtual = 2;
            SetWizardSession(wizardModel);

            return PartialView("_WizardStepConfirmacao", model);
        }

        [SMCAuthorize(UC_PES_008_01_02.ENVIAR_NOTIFICACAO)]
        public ActionResult BuscarEntidadeResponsavel(TipoAtuacao tipoAtuacao)
        {
            //Necessário realizar o "auto select" dessa forma, uma vez que o componente do framework está selecionando automáticamente
            //mesmo com dois resultados
            if (tipoAtuacao == TipoAtuacao.Aluno)
            {
                var retorno = this.EntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect(true);
                if (retorno.Count() == 1)
                    retorno.First().Selected = true;

                return Json(retorno);
            }
            else
            {
                var retorno = this.EntidadeService.BuscarEntidadesVinculoColaboradorSelect(false);
                if (retorno.Count() == 1)
                    retorno.First().Selected = true;

                return Json(retorno);
            }
        }


        [SMCAuthorize(UC_PES_008_01_02.ENVIAR_NOTIFICACAO)]
        public ActionResult BuscarTipoVinculoAluno(SMCEncryptedLong seqNivelEnsino)
        {
            return Json(TipoVinculoAlunoService.BuscarTipoVinculoAlunoNaInstituicaoSelect(seqNivelEnsino));
        }

        [SMCAuthorize(UC_PES_008_01_02.ENVIAR_NOTIFICACAO)]
        public ActionResult BuscarFormasIngresso(SMCEncryptedLong seqNivelEnsino, SMCEncryptedLong seqTipoVinculoAluno)
        {
            return Json(FormaIngressoService.BuscarFormasIngressoInstituicaoNivelVinculoSelect(new FormaIngressoFiltroData()
            {
                SeqNivelEnsino = seqNivelEnsino,
                SeqTipoVinculoAluno = seqTipoVinculoAluno
            }));
        }

        [SMCAuthorize(UC_PES_008_01_02.ENVIAR_NOTIFICACAO)]
        public ActionResult BuscarSituacoesMatricula(SMCEncryptedLong seqNivelEnsino, SMCEncryptedLong seqTipoVinculoAluno)
        {
            return Json(SituacaoMatriculaService.BuscarSituacoesMatriculasDaInstiuicaoSelect(new SituacaoMatriculaFiltroData()
            {
                SeqNivelEnsino = seqNivelEnsino,
                SeqTipoVinculoAluno = seqTipoVinculoAluno
            }));
        }

        [SMCAuthorize(UC_PES_008_01_02.ENVIAR_NOTIFICACAO)]
        public ActionResult BuscarSituacaoMatricula(SMCEncryptedLong seqSituacaoMatricula)
        {
            return Json(SituacaoMatriculaService.BuscarSituacaoMatricula(seqSituacaoMatricula).Token);
        }

        [SMCAuthorize(UC_PES_008_01_02.ENVIAR_NOTIFICACAO)]
        public ActionResult BuscarPessoasEnvioNotificacao(EnvioNotificacaoFiltroSelecaoViewModel filtros)
        {
            var wizardModel = GetWizardSession();
            if (wizardModel != null && wizardModel.FiltroSelecao != null && wizardModel.FiltroSelecao.PageSettings != null && wizardModel.FiltroSelecao.RetornouStep)
            {
                //Seta o retornou step como false para controlar paginação corretamente.
                wizardModel.FiltroSelecao.RetornouStep = false;

                filtros.PageSettings = wizardModel.FiltroSelecao.PageSettings;
            }

            var notificacoes = this.EnvioNotificacaoService.BuscarPessoasEnvioNotificacoes(filtros.Transform<EnvioNotificacaoFiltroSelecaoData>());

            var viewModelLista = notificacoes.Transform<SMCPagerData<EnvioNotificacaoPessoasListarViewModel>>();
            var model = new SMCPagerModel<EnvioNotificacaoPessoasListarViewModel>(viewModelLista, filtros.PageSettings, filtros);

            if (filtros.TipoAtuacao == TipoAtuacao.Aluno)
                return PartialView("_ListaEnvioNotificacaoAtuacaoAluno", model);
            else
                return PartialView("_ListaEnvioNotificacaoAtuacaoProfessor", model);
        }

        [SMCAllowAnonymous]
        public ActionResult LimparEnvioNotificacaoFiltro()
        {
            Session.Remove(SESSION_WIZARD_KEY);
            return SMCRedirectToAction("Incluir");
        }

        [SMCAuthorize(UC_PES_008_01_02.ENVIAR_NOTIFICACAO)]
        public ActionResult BuscarTipoVinculoColaboradorPorEntidadesSelect(List<long> seqsEntidadesResponsaveis)
        {
            return Json(TipoVinculoColaboradorService.BuscarTipoVinculoColaboradorPorEntidadesSelect(seqsEntidadesResponsaveis));
        }


        [SMCAuthorize(UC_PES_008_01_02.ENVIAR_NOTIFICACAO)]
        public ActionResult SalvarEEnviarNotificacao(EnvioNotificacaoViewModel model)
        {
            var wizardModel = GetWizardSession();
            var modelSalvar = model.Transform<EnvioNotificacaoViewModel>(wizardModel.Configuracao);
            EnvioNotificacaoService.SalvarEEnviarNotificacao(modelSalvar.Transform<EnvioNotificacaoData>());

            Session.Remove(SESSION_WIZARD_KEY);

            return SMCRedirectToAction("Index");
        }

        [SMCAuthorize(UC_PES_008_01_02.ENVIAR_NOTIFICACAO)]
        public ActionResult BuscarPessoasEnvioNotificacoesConfirmacao(EnvioNotificacaoViewModel filtros)
        {
            var destinatarios = this.EnvioNotificacaoService.BuscarPessoasEnvioNotificacoesConfirmacao(filtros.Transform<EnvioNotificacaoData>());
            var viewModelLista = destinatarios.Transform<SMCPagerData<EnvioNotificacaoPessoasListarViewModel>>();
            var model = new SMCPagerModel<EnvioNotificacaoPessoasListarViewModel>(viewModelLista, filtros.PageSettings, filtros);

            if (filtros.TipoAtuacao == TipoAtuacao.Aluno)
                return PartialView("_ListaEnvioNotificacaoAtuacaoDestinatariosAluno", model);
            else
                return PartialView("_ListaEnvioNotificacaoAtuacaoDestinatariosProfessor", model);
        }

        [SMCAuthorize(UC_SRC_004_01_05.CONSULTAR_DADOS_NOTIFICACAO)]
        public ActionResult BuscarDestinatariosVisualizarNotificacao(VisualizarDestinatariosNotificacaoViewModel filtros)
        {

            var destinatarios = this.EnvioNotificacaoService.BuscarDestinatariosVisualizarNotificacao(filtros.Transform<VisualizarDestinatariosNotificacaoData>());

            var viewModelLista = destinatarios.Transform<SMCPagerData<EnvioNotificacaoPessoasListarViewModel>>();

            var model = new SMCPagerModel<EnvioNotificacaoPessoasListarViewModel>(viewModelLista, filtros.PageSettings, filtros);

            if (filtros.TipoAtuacao == TipoAtuacao.Aluno)
                return PartialView("_ListaVisualizarNotificacaoDestinatariosAluno", model);
            else
                return PartialView("_ListaVisualizarNotificacaoDestinatariosProfessor", model);
        }

        [SMCAllowAnonymous]
        public ActionResult LimparEnvioNotificacaoFiltroTipoAtuacao(TipoAtuacao tipoAtuacao)
        {
            var model = new EnvioNotificacaoViewModel() { TipoAtuacao = tipoAtuacao, AlterouTipoAtuacao = true };
            return SMCRedirectToAction("Incluir", routeValues: model);
        }
    }
}