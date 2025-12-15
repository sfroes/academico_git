using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data.SolicitacaoMatricula;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.AvaliacaoPermanente.ServiceContract.Areas.QST.Data;
using SMC.AvaliacaoPermanente.ServiceContract.Areas.QST.Interfaces;
using SMC.Framework;
using SMC.Framework.DataFilters;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Logging;
using SMC.Framework.Model;
using SMC.Framework.Rest;
using SMC.Framework.Security;
using SMC.Framework.Security.Util;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.UI.Mvc.Util;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Interfaces;
using SMC.Seguranca.ServiceContract.Areas.APL.Interfaces;
using SMC.SGA.Aluno.Areas.ALN.Models;
using SMC.SGA.Aluno.Extensions;
using SMC.SGA.Aluno.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace SMC.SGA.Aluno.Controllers
{
    public class HomeController : SMCControllerBase
    {
        private const string KEY_SESSION_SEQ_CICLO_LETIVO = "__SGA_HOME_ALUNO_SEQ_CICLO_LETIVO";

        #region Serviços

        private IAplicacaoService AplicacaoService => Create<IAplicacaoService>();

        private ISolicitacaoMatriculaService SolicitacaoMatriculaService => Create<ISolicitacaoMatriculaService>();

        private IPlanoEstudoItemService PlanoEstudoItemService => Create<IPlanoEstudoItemService>();

        private IHistoricoEscolarService HistoricoEscolarService => Create<IHistoricoEscolarService>();

        private IAlunoService AlunoService => Create<IAlunoService>();

        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        private IArquivoAnexadoService ArquivoAnexadoService => Create<IArquivoAnexadoService>();

        private Pessoas.ServiceContract.Areas.PES.Interfaces.IPessoaService IntegracaoPessoaService => Create<Pessoas.ServiceContract.Areas.PES.Interfaces.IPessoaService>();

        private IPessoaService PessoaService => Create<IPessoaService>();

        private IIngressanteService IngressanteService => Create<IIngressanteService>();

        private ITurmaService TurmaService => Create<ITurmaService>();

        private ISolicitacaoServicoService SolicitacaoServicoService => Create<ISolicitacaoServicoService>();

        private IPublicacaoBdpService PublicacaoBdpService => Create<IPublicacaoBdpService>();

        private ICicloLetivoService CicloLetivoService => Create<ICicloLetivoService>();

        private IMensagemService MensagemService => Create<IMensagemService>();

        private IPessoaAtuacaoAmostraPpaService PessoaAtuacaoAmostraPpaService => Create<IPessoaAtuacaoAmostraPpaService>();

        private IPessoaAtuacaoService PessoaAtuacaoService => Create<IPessoaAtuacaoService>();

        private IIntegracaoAcademicoService IntegracaoAcademicoService => this.Create<IIntegracaoAcademicoService>();

        private IAmostraService AmostraService => this.Create<IAmostraService>();

        #endregion Serviços

        #region APIS

        public SMCApiClient APIQuestionarioRapidoPPA => SMCApiClient.Create("QuestionarioRapidoPPA");

        #endregion APIS

        [SMCAllowAnonymous]
        //[SMCAuthorize(UC_ALN_005_01_02.HOME_PORTAL_ALUNO, UC_ALN_005_01_01.SELECIONAR_VINCULO)]
        public ActionResult Index(bool desativarSelecaoAutomatica = false)
        {
            try
            {
                // Dados para seleção de vinculo
                (bool HabilitarSelecao, List<SMCDatasourceItem> InstituicoesEnsino, List<SMCDatasourceItem> Alunos) dadosPreselecao = (false, null, null);
                var alunoLogado = this.GetAlunoLogado();

                // Verifica se está tentando acesso a outra URL
                string returnUrl = string.Empty;
                if (SMCFederationHelper.GetUrl(out returnUrl))
                {
                    // Seleciona o vinculo
                    if (alunoLogado == null || alunoLogado.Seq <= 0)
                        dadosPreselecao = PreSelecaoAluno(desativarSelecaoAutomatica);

                    // Redireciona para a URL
                    return Redirect(returnUrl);
                }

                // Caso o usuário tenha acesso apenas ao papel de acesso simulado, redireciona o mesmo para a tela do acesso simulado.
                if (AplicacaoService.VerificarUsuarioApenasPapelAcessoSimulado(User.SMCGetSequencialUsuario().Value, SMCContext.ApplicationId))
                    return SMCRedirectToAction("AcessoSimulado/Index", "Seguranca");

                // Caso seja um acesso simulado, não pede para responder a questionários
                bool acessoSimulado = SMCContext.User.Identity.Name.Contains("(por ");
                if (acessoSimulado)
                {
                    Session["__SMC_IGNORAR_TERMO_CIENCIA_JA_REDIRECIONOU"] = true;
                    Session["__SMC_IGNORAR_QUESTIONARIO_JA_REDIRECIONOU"] = true;
                }

                // Efetua a seleção de vinculo
                if (alunoLogado == null || alunoLogado.Seq <= 0)
                {
                    dadosPreselecao = PreSelecaoAluno(desativarSelecaoAutomatica);
                    if (!dadosPreselecao.HabilitarSelecao)
                    {
                        // quando o PreSelecaoAluno retorna o !HabilitarSelecao o cookie de login foi atualizado
                        alunoLogado = this.GetAlunoLogado();

                        // Se após o login automático não conseguir resolver o cookie, faz o redirect para home para que na próxima requisição reconheça
                        if (alunoLogado == null || alunoLogado.Seq == 0)
                            return SMCRedirectToAction("Index");
                    }
                }
                else
                    dadosPreselecao = PreSelecaoAluno(true);

                // Monta o model para a página home
                var model = new HomeAlunoViewModel();
                model.SeqPublicacaoBdp = (long?)TempData["SeqPublicacaoBdp"];
                model.SeqCicloLetivo = (long?)Session[KEY_SESSION_SEQ_CICLO_LETIVO];

                // Verifica termos de ciência
                // Caso não tenha definido a session ainda, busca as informações no pessoas
                if (!((bool?)Session["__SMC_IGNORAR_TERMO_CIENCIA_JA_REDIRECIONOU"]).GetValueOrDefault())
                {
                    Session["__SMC_IGNORAR_TERMO_CIENCIA_JA_REDIRECIONOU"] = true;
                    model.RedirecionarTermoCiencia = IntegracaoPessoaService.BuscarTermosCiencia(User.SMCGetCodigoPessoa().GetValueOrDefault(), "SGA", false).Any();
                    model.UrlTermoCiencia = ConfigurationManager.AppSettings["UrlTermoCiencia"];
                }

                if (alunoLogado != null && alunoLogado.Seq > 0)
                {

                    if (!((bool?)Session["__SMC_IGNORAR_QUESTIONARIO_JA_REDIRECIONOU"]).GetValueOrDefault() && !model.RedirecionarTermoCiencia)
                    {
                        var aluno = AlunoService.BuscarAluno(alunoLogado.Seq);
                        if (aluno.CodigoAlunoMigracao.HasValue)
                        {
                            Session["__SMC_IGNORAR_QUESTIONARIO_JA_REDIRECIONOU"] = true;
                            model.RedirecionarQuestionarioSurveyMonkey = IntegracaoAcademicoService.ExibeQuestionarioSurveyMonkey(aluno.CodigoAlunoMigracao.Value);
                            model.CodigoAlunoMigracao = aluno.CodigoAlunoMigracao.Value;
                            model.UrlQuestionarioSurveyMonkey = ConfigurationManager.AppSettings["UrlQuestionarioSurveyMonkey"];
                        }
                    }

                    // Recupera os ciclo letivos
                    model.CiclosLetivos = CicloLetivoService.BuscarCiclosLetivosPorAluno(alunoLogado.Seq);
                    model.SeqCicloLetivo = model.SeqCicloLetivo ?? model.CiclosLetivos?.FirstOrDefault()?.Seq;
                    model.SeqPessoaAtuacao = alunoLogado.Seq;
                    model.ExibirIntegralizacao = HistoricoEscolarService.VisualizarConsultaConcederFormacaoIntegralizacao(alunoLogado.Seq);

                    // Busca as turmas do primeiro ciclo letivo
                    model.Cursos = TurmaService.BuscarTurmasAluno(alunoLogado.Seq, model.SeqCicloLetivo, null).TransformList<HomeCursoViewModel>();
                    model.Atividades = PlanoEstudoItemService.BuscarAtividadesAcademicasAluno(alunoLogado.Seq, model.SeqCicloLetivo).TransformList<PlanoEstudoItemViewModel>();

                    // Buscar todas as publicações BDP
                    var publicacoes = PublicacaoBdpService.BuscarPublicacoesBdpsAluno(alunoLogado.Seq);
                    model.Publicacoes = publicacoes.TransformList<HomePublicacaoBdpViewModel>();

                    var totalizadoresSolicitacoes = SolicitacaoServicoService.BuscarTotalizadorSolicitacoesServico(alunoLogado.Seq);
                    model.TotalSolicitacoesConcluidas = totalizadoresSolicitacoes.Concluidas;
                    model.TotalSolicitacoesNovas = totalizadoresSolicitacoes.Novas;
                    model.TotalSolicitacoesEmAndamento = totalizadoresSolicitacoes.EmAndamento;
                    model.TotalSolicitacoesEncerradas = totalizadoresSolicitacoes.Encerradas;

                    var dadosRematricula = SolicitacaoMatriculaService.BuscarDadosRematricula(new DadosSolicitacaoMatriculaRenovacaoFiltroData { SeqPessoaAtuacao = alunoLogado.Seq });
                    model.ExisteRematriculaAberta = dadosRematricula.ExisteRematriculaAberta;
                    model.DataInicioRematricula = dadosRematricula.DataInicioRematricula;
                    model.DataFimRematricula = dadosRematricula.DataFimRematricula;
                    model.SeqSolicitacaoRematricula = dadosRematricula.SeqSolicitacaoServico;
                    model.TipoMatricula = dadosRematricula.TipoMatricula;

                    // Banner Avaliação CPA (Concluinte)
                    var codAmostraConcluinte = PessoaAtuacaoAmostraPpaService.BuscarPessoaAtuacaoAmostraPpaNaoPreenchida(alunoLogado.Seq, TipoAvaliacaoPpa.Concluinte);
                    if (codAmostraConcluinte.HasValue)
                    {
                        model.ExibirBannerAvaliacaoCpa = true;
                        model.CodigoAmostraPpa = codAmostraConcluinte.Value;
                        model.UrlAvaliacaoCpa = ConfigurationManager.AppSettings["UrlAvaliacaoCpa"];

                        if (string.IsNullOrEmpty(model.UrlAvaliacaoCpa))
                            throw new UrlAvaliacaoCpaNaoConfiguradaException(TipoAvaliacaoPpa.Concluinte.SMCGetDescription());

                        if (model.CodigoAmostraPpa == 0)
                            throw new CodigoAmostraPpaNaoEncontradoException();
                    }

                    // Banner Avaliação CPA (Semestral de Disciplina + Autoavaliacao)
                    var codAmostraSemestral = PessoaAtuacaoAmostraPpaService.BuscarPessoaAtuacaoAmostraPpaNaoPreenchida(alunoLogado.Seq, TipoAvaliacaoPpa.SemestralDisciplina);
                    var codAmostraAutoavaliacao = PessoaAtuacaoAmostraPpaService.BuscarPessoaAtuacaoAmostraPpaNaoPreenchida(alunoLogado.Seq, TipoAvaliacaoPpa.AutoavaliacaoAluno);
                    if (codAmostraSemestral.HasValue || codAmostraAutoavaliacao.HasValue)
                    {
                        var codigoPessoa = User.SMCGetCodigoPessoa().GetValueOrDefault();
                        var token = GerarHashMd5(codigoPessoa.ToString()); 
                        model.ExibirBannerAvaliacaoSemestralCpa = true;                          
                        
                        // Parametros da url o codigo pessoa e token que é um MD5 do codigo pessoa                    
                        model.UrlAvaliacaoSemestralCpa = string.Format(ConfigurationManager.AppSettings["UrlAvaliacaoSemestralCpa"], codigoPessoa, token);

                        if (string.IsNullOrEmpty(model.UrlAvaliacaoSemestralCpa))
                            throw new UrlAvaliacaoCpaNaoConfiguradaException(TipoAvaliacaoPpa.SemestralDisciplina.SMCGetDescription());
                    }

                    // Validação de exibição com tokens de segurança
                    model.PermitiPesquisarSolicitacaoAluno = SMCSecurityHelper.Authorize(UC_ALN_005_02_01.PESQUISAR_SOLICITACAO_ALUNO);
                    model.PermitirVisualizarBannerTurmasLinhaTempo = SMCSecurityHelper.Authorize(UC_ALN_005_01_02.PERMITIR_VISUALIZAR_BANNER_TURMAS_LINHA_DO_TEMPO);
                    model.PermitirExibirIntegralizacaoCurricular = SMCSecurityHelper.Authorize(UC_CNC_001_02_01.EXIBIR_INTEGRALIZACAO_CURRICULAR);
                }
                else
                {
                    var pessoas = PessoaService.BuscarPessoas(new PessoaFiltroData { SeqUsuarioSAS = User.SMCGetSequencialUsuario().GetValueOrDefault() });

                    // Caso não tenha nenhuma pessoa para o sequencial logado
                    if (pessoas == null || !pessoas.Any())
                        throw new SMCApplicationException("Favor efetuar o login novamente.");

                    // Busca os alunos disponíveis para seleção
                    var alunos = dadosPreselecao.Alunos;// AlunoService.BuscarAlunosSelect(new AlunoFiltroData { SeqPessoa = pessoa.Seq }, false);

                    // Caso não tenha nenhuma pessoa para o sequencial logado
                    if (alunos == null || !alunos.Any())
                    {
                        // Não existe aluno para este login. Verifica se possui ingressante
                        var ingressantes = IngressanteService.BuscarIngressantesPessoa(new IngressanteFiltroData { SeqUsuarioSAS = User.SMCGetSequencialUsuario().GetValueOrDefault() });

                        if (ingressantes == null || !ingressantes.Any())
                            throw new NaoExisteAlunoAssociadoException();
                        else
                            throw new NaoExisteIngressanteAssociadoException();
                    }
                }

                return View(model);
            }
            catch (NaoExisteIngressanteAssociadoException e)
            {
                return SMCRedirectToAction("Index", "Home", new { @area = "MAT" });
            }
        }

        [SMCAuthorize(UC_ALN_005_01_02.HOME_PORTAL_ALUNO)]
        public ActionResult ListarTimeline(SMCPagerModel filtro)
        {
            var data = MensagemService.ListarMensagens(new MensagemFiltroData()
            {
                SeqPessoaAtuacao = this.GetAlunoLogado().Seq,
                MensagensValidas = true,
                PageSettings = filtro.PageSettings,
                CategoriaMensagem = CategoriaMensagem.LinhaDoTempo
            });
            var lista = data.TransformList<SMCTimelineItem>();
            var model = new SMCPagerModel<SMCTimelineItem>(lista, filtro.PageSettings);
            return PartialView("_ListarTimeline", model);
        }

        [SMCAuthorize(UC_ALN_005_01_02.HOME_PORTAL_ALUNO)]
        public ActionResult ListarTurmas(long? seqCicloLetivo)
        {
            Session[KEY_SESSION_SEQ_CICLO_LETIVO] = seqCicloLetivo;

            var modelLista = new HomeAlunoViewModel();
            var alunoLogado = this.GetAlunoLogado();
            modelLista.Cursos = TurmaService.BuscarTurmasAluno(alunoLogado.Seq, seqCicloLetivo, null).TransformList<HomeCursoViewModel>();
            modelLista.Atividades = PlanoEstudoItemService.BuscarAtividadesAcademicasAluno(alunoLogado.Seq, seqCicloLetivo).TransformList<PlanoEstudoItemViewModel>();

            return PartialView("_ListarTurmas", modelLista);
        }

        [SMCAuthorize(UC_ALN_005_01_02.HOME_PORTAL_ALUNO)]
        public ActionResult ComprovanteMatricula()
        {
            var alunoLogado = this.GetAlunoLogado();
            if (alunoLogado == null || alunoLogado.Seq == 0)
                throw new AlunoLogadoSemVinculoException();

            var dados = AlunoService.BuscarDadosIngressanteAluno(alunoLogado.Seq);
            return SMCRedirectToUrl(Url.Action("ComprovanteMatriculaRelatorio", "ComprovanteMatricula", new { SeqIngressante = new SMCEncryptedLong(dados.SeqIngressante), SeqSolicitacaoMatricula = new SMCEncryptedLong(dados.SeqSolicitacaoMatricula), @area = "" }));
        }

        [SMCAuthorize(UC_ALN_005_01_03.VISUALIZAR_NOTAS_FREQUENCIA)]
        public ActionResult NotasFrequencias(SMCEncryptedLong seqTurma)
        {
            var alunoLogado = this.GetAlunoLogado();
            if (alunoLogado == null || alunoLogado.Seq == 0)
                throw new AlunoLogadoSemVinculoException();

            var historico = HistoricoEscolarService.BuscarHistoricoEscolarTurma(seqTurma, alunoLogado.Seq);

            var model = historico.Transform<NotaFrequenciaViewModel>();
            return PartialView(model);
        }

        /// <summary>
        /// Logout da aplicação
        /// </summary>
        [SMCAllowAnonymous]
        public ActionResult Logout()
        {
            SMCDataFilterHelper.ClearGlobalFilter();
            this.ClearCookie();

            // Desconecta o usuário
            SMCFederationHelper.SignOut("Index", "Home");

            return new EmptyResult();
        }

        #region Modal para seleção de vínculo

        [SMCAuthorize(UC_ALN_005_01_01.SELECIONAR_VINCULO)]
        [SMCAllowAnonymous]
        public ActionResult ModalSelecionarAluno(bool desativarSelecaoAutomatica = false)
        {
            try
            {
                // Caso seja a URL do SAS para selecionar acesso simulado, não renderiza a modal.
                string controllerName = Request.RequestContext.RouteData.Values["controller"]?.ToString();
                if (controllerName.ToLower() == "acessosimulado")
                    return new EmptyResult();

                var model = new AlunoSeletorViewModel();

                // Se não tem aluno logado, busca os possíveis para apresentar na modal.
                var alunoLogado = this.GetAlunoLogado();
                if (alunoLogado == null || alunoLogado.Seq <= 0)
                {
                    var dadosPreSelecao = PreSelecaoAluno(desativarSelecaoAutomatica);
                    model.HabilitarSelecao = dadosPreSelecao.HabilitarSelecao;
                    model.Alunos = dadosPreSelecao.Alunos;
                    model.InstituicoesEnsino = dadosPreSelecao.InstituicoesEnsino;

                    Session["__SMC_IGNORAR_QUESTIONARIO_JA_REDIRECIONOU"] = false;
                }
                else
                {
                    model.HabilitarSelecao = false;
                }

                return PartialView("_ModalSelecionarAluno", model);
            }
            catch (NaoExisteAlunoAssociadoException e)
            {
                return PartialView("_ModalSelecionarAlunoErro", e);
            }
        }

        private (bool HabilitarSelecao, List<SMCDatasourceItem> InstituicoesEnsino, List<SMCDatasourceItem> Alunos) PreSelecaoAluno(bool desativarSelecaoAutomatica = false)
        {
            List<SMCDatasourceItem> instituicoesEnsino = PessoaService.BuscarInstituicoesEnsinoPessoaSelect(new PessoaFiltroData { SeqUsuarioSAS = User.SMCGetSequencialUsuario() });
            List<SMCDatasourceItem> alunos = null;
            bool habilitarSelecao = false;

            // Seleciona a instituição automaticamente caso tenha somente uma
            if (instituicoesEnsino.Count == 1)
            {
                // Recupera todos os vínculos de aluno da pessoa, na entidade selecionada
                alunos = BuscarAlunosLista(instituicoesEnsino.First().Seq, !desativarSelecaoAutomatica);

                if (desativarSelecaoAutomatica)
                {
                    habilitarSelecao = true;
                }
                else
                {
                    // Falso pra forçar sempre exibir a janela
                    if (alunos.Count == 1)
                    {
                        SalvarSelecaoCookie(alunos.FirstOrDefault().Seq, instituicoesEnsino.First().Seq);
                        habilitarSelecao = false;
                    }
                    else if (alunos.Count(a => a.DataAttributes != null && a.DataAttributes.FirstOrDefault(at => at.Key == "ativo" && at.Value != string.Empty) != null) == 1)
                    {
                        SalvarSelecaoCookie(alunos.FirstOrDefault(a => a.DataAttributes != null && a.DataAttributes.FirstOrDefault(at => at.Key == "ativo" && at.Value != string.Empty) != null).Seq, instituicoesEnsino.First().Seq);
                        habilitarSelecao = false;
                    }
                    else
                        habilitarSelecao = true;
                }
            }
            else
                habilitarSelecao = true;

            return (habilitarSelecao, instituicoesEnsino, alunos);
        }

        private void SalvarSelecaoCookie(long seqAluno, long seqInstituicaoEnsino)
        {
            // Recupera a instituição de ensino pra buscar o logo
            var instituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicaoEnsino(seqInstituicaoEnsino);

            // Busca os dados do aluno selecionado
            var aluno = AlunoService.BuscarAluno(seqAluno);

            // Cria o cookie
            this.SetCookie(new SMCEntityFilterGlobalModel()
            {
                FilterKey = FILTER.ALUNO,
                Value = aluno.Seq,
                Description = aluno.Descricao,
                SeqArquivoLogo = instituicaoEnsino?.SeqArquivoLogotipo
            });
        }

        private List<SMCDatasourceItem> BuscarAlunosLista(long seqInstituicaoEnsino, bool carregarVinculoAtivo = false)
        {
            // Recupera os dados da pessoa de acordo com ousuário do SAS

            var pessoas = PessoaService.BuscarPessoas(new PessoaFiltroData { SeqUsuarioSAS = User.SMCGetSequencialUsuario().GetValueOrDefault() });

            // Caso não tenha nenhuma pessoa para o sequencial logado
            if (pessoas == null || !pessoas.Any())
                throw new SMCApplicationException("Favor efetuar o login novamente.");

            // Busca os ingressantes disponíveis para seleção
            var alunos = AlunoService.BuscarAlunosSelect(new AlunoFiltroData { SeqInstituicaoEnsino = seqInstituicaoEnsino, SeqUsuarioSAS = User.SMCGetSequencialUsuario().GetValueOrDefault() }, true, carregarVinculoAtivo);

            // Caso não tenha nenhuma pessoa para o sequencial logado
            if (alunos == null || !alunos.Any())
            {
                // Não existe aluno para este login. Verifica se possui ingressante
                var ingressantes = IngressanteService.BuscarIngressantesPessoa(new IngressanteFiltroData { SeqUsuarioSAS = User.SMCGetSequencialUsuario().GetValueOrDefault() });

                if (ingressantes == null || !ingressantes.Any())
                    throw new NaoExisteAlunoAssociadoException();
                else
                    throw new NaoExisteIngressanteAssociadoException();
            }

            return alunos;
        }

        [SMCAuthorize(UC_ALN_005_01_01.SELECIONAR_VINCULO)]
        public ActionResult ModalTrocarAluno()
        {
            SMCDataFilterHelper.ClearGlobalFilter();
            this.ClearCookie();
            return RedirectToAction("Index", "Home", new { area = "", desativarSelecaoAutomatica = true });
        }

        [SMCAuthorize(UC_ALN_005_01_01.SELECIONAR_VINCULO)]
        public ActionResult BuscarAlunosInstituicao(long seqInstituicaoEnsino)
        {
            try
            {
                var alunos = BuscarAlunosLista(seqInstituicaoEnsino);
                return Json(alunos);
            }
            catch (NaoExisteAlunoAssociadoException e)
            {
                return PartialView("_ModalSelecionarAlunoErro", e);
            }
            catch (NaoExisteIngressanteAssociadoException)
            {
                return SMCRedirectToAction("Index", "Home", new { @area = "MAT" });
            }
        }

        /// <summary>
        /// POST que recebe o aluno selecionado pelo usuário e armazena no cookie
        /// </summary>
        /// <param name="seqAluno">Aluno selecionado</param>
        [HttpPost]
        [SMCAuthorize(UC_ALN_005_01_01.SELECIONAR_VINCULO)]
        public ActionResult SelecionarAluno(long seqAluno, long seqInstituicaoEnsino)
        {
            SalvarSelecaoCookie(seqAluno, seqInstituicaoEnsino);

            return SMCRedirectToAction("Index", "Home", new { area = "" });
        }

        [SMCAuthorize(UC_ALN_005_01_01.SELECIONAR_VINCULO)]
        public FileResult Logo(SMCEncryptedLong seq)
        {
            var arquivo = ArquivoAnexadoService.BuscarArquivoAnexado(seq);
            return File(arquivo.FileData, arquivo.Type);
        }

        /// <summary>
        /// Apresenta os dados do aluno logado no topo da página
        /// </summary>
        [ChildActionOnly]
        [SMCAuthorize(UC_ALN_005_01_01.SELECIONAR_VINCULO)]
        public ActionResult MarcaAluno()
        {
            var alunoLogado = HttpContext.GetAlunoLogado();
            return PartialView("_MarcaAluno", alunoLogado);
        }

        #endregion Modal para seleção de vínculo

        /// <summary>
        /// Realiza download de arquivos anexados (esse método será descontinuado)
        /// </summary>
        [SMCAllowAnonymous]
        public FileResult DownloadFile(string guidFile, string name, string type, string seqEntity = null)
        {
            //Não permite que seja feito o download se foi enviado o sequencial aberto 
            if (Int64.TryParse(guidFile, out long seq) || Int64.TryParse(seqEntity, out long seqEntidade))
            {
                throw new SMCFileNotFoundException(name);
            }

            byte[] retData = null;
            string retName = null;
            string retType = null;

            Guid guidParsed = Guid.Empty;

            // Caso tenha passado um guid, significa que o arquivo ainda está no TEMP e não foi salvo no BD
            if (Guid.TryParse(guidFile, out guidParsed))
            {
                var data = SMCUploadHelper.GetFileData(new SMCUploadFile { GuidFile = guidFile });
                if (data != null)
                {
                    retData = data;
                    retName = name;
                    retType = type;
                }
            }

            // Caso não tenha achado no temp...
            if (retData == null)
            {
                // Caso tenha passado um EncryptedLong, faz o parse, ou busca o SeqEntity
                try
                {
                    seq = new SMCEncryptedLong(guidFile);
                }
                catch (Exception)
                {
                    if (seqEntity != null)
                    {
                        seq = new SMCEncryptedLong(seqEntity);
                    }
                }

                // Caso não achou nada...
                if (guidParsed != Guid.Empty)
                {
                    return DownloadFileGuid(guidParsed, name, type);
                }
                else if (seq != 0)
                {
                    // Busca o arquivo do seq informado
                    var arq = ExecuteService<SMCUploadFile, SMCUploadFile>(ArquivoAnexadoService.BuscarArquivoAnexado, seq);
                    if (arq != null)
                    {
                        retData = arq.FileData;
                        retName = arq.Name;
                        retType = arq.Type;
                    }
                }
                else
                {
                    throw new SMCFileNotFoundException(name);
                }
            }

            // Caso seja PDF, retorna inline.
            if (retName.EndsWith(".pdf") || retType == "application/pdf")
            {
                Response.AddHeader("Content-Disposition", $"inline; filename=\"${retName}\"");
                return File(retData, "application/pdf");
            }

            return File(retData, retType, retName);
        }

        /// <summary>
        /// Realiza download de arquivos anexados 
        /// </summary>
        [SMCAllowAnonymous]
        public FileResult DownloadFileGuid(Guid guidFile, string name, string type)
        {
            byte[] retData = null;
            string retName = null;
            string retType = null;

            //Verifica se o arquivo está no TEMP e não foi salvo no BD
            var data = SMCUploadHelper.GetFileData(new SMCUploadFile { GuidFile = guidFile.ToString() });
            if (data != null)
            {
                retData = data;
                retName = name;
                retType = type;
            }
            else
            {
                //Busca o arquivo do guid informado no banco caso não tenha achado no temp 
                var arq = ArquivoAnexadoService.BuscarArquivoAnexadoPorGuid(guidFile);
                if (arq != null)
                {
                    retData = arq.FileData;
                    retName = arq.Name;
                    retType = arq.Type;
                }
                else
                {
                    throw new SMCFileNotFoundException(name);
                }
            }

            //Caso seja PDF, retorna inline.
            if (retName.EndsWith(".pdf") || retType == "application/pdf")
            {
                Response.AddHeader("Content-Disposition", $"inline; filename=\"${retName}\"");
                return File(retData, "application/pdf");
            }

            return File(retData, retType, retName);
        }

        /// <summary>
        /// Para atender a encriptação do silver código da pessoa
        /// </summary>
        /// <param name="codigoPessoa">Codigo pessoa</param>
        /// <returns>Um hash que atende ao silver referente ao código da pessoa</returns>
        private static string GerarHashMd5(string codigoPessoa)
        {

            string currentTime = DateTime.Now.ToString("yyyy-MM-dd_HH:mm");// 896146+2019-04-25_13:49
            var md5Dados = string.Format("{0}+{1}", codigoPessoa, currentTime); // 0b7ac50214a00f0c7bcc3eb6f2d887a8

            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(md5Dados));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2")); // HEXADECIMAL
            }

            return sBuilder.ToString();
        }
    }
}