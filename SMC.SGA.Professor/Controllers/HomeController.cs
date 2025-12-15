using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Common.Exceptions;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.AvaliacaoPermanente.ServiceContract.Areas.QST.Data;
using SMC.AvaliacaoPermanente.ServiceContract.Areas.QST.Interfaces;
using SMC.Framework;
using SMC.Framework.Caching;
using SMC.Framework.DataFilters;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Logging;
using SMC.Framework.Model;
using SMC.Framework.Rest;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.UI.Mvc.Util;
using SMC.Seguranca.ServiceContract.Areas.APL.Interfaces;
using SMC.SGA.Professor.Extensions;
using SMC.SGA.Professor.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace SMC.SGA.Professor.Controllers
{
    public class HomeController : SMCControllerBase
    {
        private const string KEY_SESSION_SEQ_CICLO_LETIVO = "__SGA_HOME_PROFESSOR_SEQ_CICLO_LETIVO";

        #region [ Services ]

        private IPessoaAtuacaoAmostraPpaService PessoaAtuacaoAmostraPpaService => Create<IPessoaAtuacaoAmostraPpaService>();
        private IAmostraService AmostraService => Create<IAmostraService>();
        private IAplicacaoService AplicacaoService => Create<IAplicacaoService>();
        private IArquivoAnexadoService ArquivoAnexadoService => Create<IArquivoAnexadoService>();
        private IColaboradorService ColaboradorService => Create<IColaboradorService>();
        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();
        private IPessoaService PessoaService => Create<IPessoaService>();
        private ITurmaService TurmaService => Create<ITurmaService>();
        private Pessoas.ServiceContract.Areas.PES.Interfaces.IPessoaService IntegracaoPessoaService => Create<Pessoas.ServiceContract.Areas.PES.Interfaces.IPessoaService>();

        #endregion [ Services ]

        #region APIS

        public SMCApiClient APIQuestionarioRapidoPPA => SMCApiClient.Create("QuestionarioRapidoPPA");
        public ISMCApiClient AcompanhamentoReport => SMCApiClient.Create("AcompanhamentoReport");

        #endregion APIS

        //[SMCAuthorize(UC_DCT_003_01_01.HOME_PORTAL_ALUNO)]
        [SMCAllowAnonymous]
        public ActionResult Index()
        {
            // Caso o usuário tenha acesso apenas ao papel de acesso simulado, redireciona o mesmo para a tela do acesso simulado.
            if (AplicacaoService.VerificarUsuarioApenasPapelAcessoSimulado(User.SMCGetSequencialUsuario().Value, SMCContext.ApplicationId))
                return SMCRedirectToAction("AcessoSimulado/Index", "Seguranca");

            if (VerificarTermoCiencia(out var redirect))
            {
                return redirect;
            }

            var model = new HomeProfessorViewModel();
            model.SeqCicloLetivoInicio = (long?)Session[KEY_SESSION_SEQ_CICLO_LETIVO];

            try
            {
                ModalSelecionarInstituicao();
            }
            catch (SGAAuthenticationSuccessRedirectException)
            {
                return SMCRedirectToAction("Index");
            }

            SelecionarTurmaProfessorFiltros(model);

            SelecionarTurmaProfessor(model);

            // Carrega outros dados do professor
            var professorLogado = this.GetProfessorLogado();

            if (professorLogado != null && professorLogado.Seq > 0)
            {
                // Banner Avaliação CPA (Autoavaliacao)
                var codAmostraAutoavaliacao = PessoaAtuacaoAmostraPpaService.BuscarPessoaAtuacaoAmostraPpaNaoPreenchida(professorLogado.Seq, TipoAvaliacaoPpa.AutoavaliacaoProfessor);
                if (codAmostraAutoavaliacao.HasValue)
                {
                    var codigoPessoa = User.SMCGetCodigoPessoa().GetValueOrDefault();
                    var token = GerarHashMd5(codigoPessoa.ToString());
                    model.ExibirBannerAvaliacaoSemestralCpa = true;

                    // Parametros da url o codigo pessoa e token que é um MD5 do codigo pessoa                    
                    model.UrlAvaliacaoSemestralCpa = string.Format(ConfigurationManager.AppSettings["UrlAvaliacaoSemestralCpa"], codigoPessoa, token);

                    if (string.IsNullOrEmpty(model.UrlAvaliacaoSemestralCpa))
                        throw new UrlAvaliacaoCpaNaoConfiguradaException(TipoAvaliacaoPpa.AutoavaliacaoProfessor.SMCGetDescription());
                }
            }

            return View(model);
        }

        [SMCAllowAnonymous]
        [HttpPost]
        public ActionResult SelecaoTurmaPesquisa(HomeProfessorViewModel model)
        {
            SelecionarTurmaProfessor(model);

            return PartialView("_SelecaoTurmaPesquisar", model);
            //return View(model);
        }

        [HttpGet]
        [SMCAuthorize(UC_APR_001_17_01.EXIBIR_RELATORIO_ACOMPANHAMENTO_NOTA)]
        public ActionResult RelatorioAcompanhamento(SMCEncryptedLong seqOrigemAvaliacao)
        {
            long? seqProfessorLogado = HttpContext.GetEntityLogOn(FILTER.PROFESSOR)?.Value;
            var arq = AcompanhamentoReport.Download($"GerarRelatorio?seqOrigemAvaliacao={seqOrigemAvaliacao}&seqProfessorLogado={seqProfessorLogado}", method: Method.GET);
            return File(arq, "application/pdf");
        }

        private void SelecionarTurmaProfessor(HomeProfessorViewModel model)
        {
            // Se tiver professor logado realiza a consulta de turmas
            var professorLogado = this.GetProfessorLogado();

            if (professorLogado != null && professorLogado.Seq > 0)
            {
                Session[KEY_SESSION_SEQ_CICLO_LETIVO] = model.SeqCicloLetivoInicio;

                var listaTurmas = TurmaService.BuscarTurmasProfessor(professorLogado.Seq, true);

                //Preencher o seqOrigemAvaliação nas turmas divisão
                foreach (var item in listaTurmas)
                {
                    foreach (var turma in item.Turmas)
                    {
                        // Utilizar nas divisões as flag da turma pois as das divisões sempre serão nulas
                        turma.DivisoesTurma.SMCForEach(d =>
                        {
                            d.PermiteAvaliacaoParcial = turma.PermiteAvaliacaoParcial;
                        });

                        foreach (var divisoes in turma.TurmaDivisoes)
                        {
                            var turmaDivisaoComponente = turma.DivisoesTurma.FirstOrDefault(f => f.SeqDivisaoComponente == divisoes.SeqDivisaoComponente);
                            if (turmaDivisaoComponente != null)
                            {
                                divisoes.SeqOrigemAvaliacao = turmaDivisaoComponente.SeqOrigemAvaliacao;
                                divisoes.TipoEscalaApuracao = turmaDivisaoComponente.TipoEscalaApuracao;
                                divisoes.DiarioFechado = turmaDivisaoComponente.DiarioFechado;
                            }
                            divisoes.PermiteAvaliacaoParcial = turma.PermiteAvaliacaoParcial;//Utilizar nas divisões as flag da turma pois as das divisões sempre serão nulas
                        }
                    }
                }

                if (listaTurmas.Count > 0)
                {
                    var desagruparTurmas = listaTurmas.SelectMany(s => s.Turmas);

                    if (model.SeqCicloLetivoInicio.HasValue && model.SeqCicloLetivoInicio.Value > 0)
                        desagruparTurmas = desagruparTurmas.Where(w => w.SeqCicloLetivoInicio == model.SeqCicloLetivoInicio.Value).ToList();

                    if (model.SeqNivelEnsino.HasValue && model.SeqNivelEnsino.Value > 0)
                        desagruparTurmas = desagruparTurmas.Where(w => w.SeqNivelEnsino == model.SeqNivelEnsino.Value).ToList();

                    if (model.SeqLocalidade.HasValue && model.SeqLocalidade.Value > 0)
                        desagruparTurmas = desagruparTurmas.Where(w => w.SeqLocalidade == model.SeqLocalidade.Value).ToList();

                    if (model.SeqCurso.HasValue && model.SeqCurso.Value > 0)
                        desagruparTurmas = desagruparTurmas.Where(w => w.SeqCurso == model.SeqCurso.Value).ToList();

                    if (model.SeqTurno.HasValue && model.SeqTurno.Value > 0)
                        desagruparTurmas = desagruparTurmas.Where(w => w.SeqTurno == model.SeqTurno.Value).ToList();

                    List<TurmaListarGrupoCursoData> retorno = new List<TurmaListarGrupoCursoData>();
                    desagruparTurmas.GroupBy(g => g.DescricaoCursoLocalidadeTurno).SMCForEach(f =>
                    {
                        retorno.Add(new TurmaListarGrupoCursoData() { DescricaoCursoLocalidadeTurno = f.First().DescricaoCursoLocalidadeTurno, Turmas = f.ToList() });
                    });

                    model.Cursos = retorno.TransformList<HomeCursoViewModel>();
                }
            }
        }

        private void SelecionarTurmaProfessorFiltros(HomeProfessorViewModel model)
        {
            // Se tiver professor logado realiza a consulta de turmas
            var professorLogado = this.GetProfessorLogado();

            if (professorLogado != null && professorLogado.Seq > 0)
            {
                var listaFiltros = TurmaService.BuscarTurmasProfessorFiltros(professorLogado.Seq);

                model.CiclosLetivo = listaFiltros.OrderByDescending(c => c.AnoNumeroCicloLetivo).Select(s => new SMCDatasourceItem() { Seq = s.SeqCicloLetivoInicio, Descricao = s.DescricaoCicloLetivoInicio }).SMCDistinct(d => d.Seq).ToList();
                model.SeqCicloLetivoInicio = model.SeqCicloLetivoInicio ?? (model.CiclosLetivo.SMCCount() > 0 ? model.CiclosLetivo.First().Seq : (long?)null);
                model.NiveisEnsino = listaFiltros.Select(s => new SMCDatasourceItem() { Seq = s.SeqNivelEnsino, Descricao = s.DescricaoNivelEnsino }).SMCDistinct(d => d.Seq).ToList();
                model.Localidades = listaFiltros.Select(s => new SMCDatasourceItem() { Seq = s.SeqLocalidade, Descricao = s.DescricaoLocalidade }).SMCDistinct(d => d.Seq).ToList();
                model.CursosFiltro = listaFiltros.Select(s => new SMCDatasourceItem() { Seq = s.SeqCurso, Descricao = s.DescricaoCurso }).SMCDistinct(d => d.Seq).ToList();
                model.Turnos = listaFiltros.Select(s => new SMCDatasourceItem() { Seq = s.SeqTurno, Descricao = s.DescricaoTurno }).SMCDistinct(d => d.Seq).ToList();
            }
        }

        /// <summary>
        /// Logout da aplicação
        /// </summary>
        [SMCAllowAnonymous]
        public ActionResult Logout()
        {
            try
            {
                SMCDataFilterHelper.ClearGlobalFilter();
                this.ClearCookie();

                // Limpa o cache
                if (this.GetProfessorLogado() != null)
                {
                    var keyCacheTemplate = "__SGA_TURMAS_PROFESSOR_" + this.GetProfessorLogado().Seq;
                    SMCCacheManager.Remove(keyCacheTemplate);
                }
            }
            finally
            {
                // Desconecta o usuário
                SMCFederationHelper.SignOut("Index", "Home");
            }
            return new EmptyResult();
        }

        #region [ Modal para seleção da instituição ]

        [SMCAllowAnonymous]
        public ActionResult ModalSelecionarInstituicao()
        {
            try
            {
                var model = new ProfessorSeletorViewModel();

                // Se não tem professor logado, busca os possíveis para apresentar na modal.
                var professorLogado = this.GetProfessorLogado();
                if (professorLogado == null || professorLogado.Seq <= 0)
                {
                    // Desabilita o filtro global para selecionar as instituição de ensino que o usuário pode selecionar
                    this.EnabledFilterGlobal(false);
                    List<SMCDatasourceItem> instituicoesEnsino = PessoaService.BuscarInstituicoesEnsinoPessoaSelect(new PessoaFiltroData { SeqUsuarioSAS = User.SMCGetSequencialUsuario() });
                    model.InstituicoesEnsino = instituicoesEnsino;

                    // Seleciona a instituição automaticamente caso tenha somente uma
                    if (model.InstituicoesEnsino.Count == 1)
                    {
                        SelecionarInstituicao(model.InstituicoesEnsino.First().Seq);
                        model.HabilitarSelecao = false;

                        // Selecionou automaticamente o professor.
                        // Dispara a exception abaixo pois ela é capturada no Index com objetivo de recarregar a tela
                        // pois os cookies setados na seleção automática só passam a valer na próxima requisição.
                        throw new SGAAuthenticationSuccessRedirectException();
                    }
                    else
                        model.HabilitarSelecao = true;
                }
                else
                {
                    model.HabilitarSelecao = false;
                }

                return PartialView("_ModalSelecionarInstituicao", model);
            }
            finally
            {
                this.EnabledFilterGlobal(true);
            }
        }

        /// <summary>
        /// Realiza a troca de instituiçao de ensino do professor
        /// </summary>
        /// <returns>Redireciona para Index limpando o cookie</returns>
        [SMCAllowAnonymous]
        public ActionResult ModalTrocarInstituicao()
        {
            SMCDataFilterHelper.ClearGlobalFilter();
            this.ClearCookie();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        /// <summary>
        /// POST que recebe a instituição selecionada pelo usuário e armazena no cookie
        /// </summary>
        /// <param name="seqInstituicaoEnsino">Sequencial da instituição de ensino</param>
        [HttpPost]
        [SMCAllowAnonymous]
        public ActionResult SelecionarInstituicao(long seqInstituicaoEnsino)
        {
            try
            {
                this.EnabledFilterGlobal(false);

                // Recupera a instituição de ensino pra buscar o logo
                var instituicaoEnsino = InstituicaoEnsinoService.BuscarInstituicaoEnsino(seqInstituicaoEnsino);

                // Busca os dados do professor
                var professor = ColaboradorService.BuscarProfessorLogin(User.SMCGetSequencialUsuario().GetValueOrDefault(), seqInstituicaoEnsino);

                // Caso não tenha nenhuma pessoa para o sequencial logado
                if (professor == null)
                    throw new SMCApplicationException("Favor efetuar o login novamente.");

                // Cria o cookie
                this.SetCookie(new SMCEntityFilterGlobalModel()
                {
                    FilterKey = FILTER.PROFESSOR,
                    Value = professor.Seq,
                    Description = professor?.Nome,
                    SeqArquivoLogo = instituicaoEnsino?.SeqArquivoLogotipo
                });
            }
            finally
            {
                // Habilita o filtro global
                this.EnabledFilterGlobal(true);
            }

            return SMCRedirectToAction("Index", "Home", new { area = "" });
        }

        /// <summary>
        /// Buscar a logo da instituição do professor
        /// </summary>
        /// <param name="seq">Sequencial do arquivo</param>
        /// <returns>O arquivo de logo da instituição</returns>
        [SMCAllowAnonymous]
        public FileResult Logo(SMCEncryptedLong seq)
        {
            var arquivo = ArquivoAnexadoService.BuscarArquivoAnexado(seq);
            return File(arquivo.FileData, arquivo.Type);
        }

        /// <summary>
        /// Apresenta os dados do professor logado no topo da página
        /// </summary>
        [ChildActionOnly]
        [SMCAllowAnonymous]
        public ActionResult MarcaProfessor()
        {
            this.EnabledFilterGlobal(true);
            var professorLogado = this.HttpContext.GetProfessorLogado();
            return PartialView("_MarcaProfessor", professorLogado);
        }

        #endregion [ Modal para seleção da instituição ]

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

        [SMCAllowAnonymous]
        public ActionResult RedirectTermoCiencia()
        {
            return View();
        }

        [SMCAllowAnonymous]
        public ActionResult RedirectComprovanteVacinacao()
        {
            return View();
        }

        private bool VerificarTermoCiencia(out ActionResult redirectTemoCiencia)
        {
            if (!((bool?)Session["__SMC_IGNORAR_TERMO_CIENCIA_JA_REDIRECIONOU"]).GetValueOrDefault())
            {
                Session["__SMC_IGNORAR_TERMO_CIENCIA_JA_REDIRECIONOU"] = true;
                if (IntegracaoPessoaService.BuscarTermosCiencia(User.SMCGetCodigoPessoa().GetValueOrDefault(), "SGA", false).Any())
                {
                    redirectTemoCiencia = SMCRedirectToAction(nameof(RedirectTermoCiencia));
                    return true;
                }
            }
            redirectTemoCiencia = null;
            return false;
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