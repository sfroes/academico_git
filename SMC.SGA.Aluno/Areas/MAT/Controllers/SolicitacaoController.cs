using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Academico.UI.Mvc;
using SMC.Academico.UI.Mvc.Areas.SRC.Controllers;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Academico.UI.Mvc.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Seguranca.ServiceContract.Areas.APL.Interfaces;
using SMC.SGA.Aluno.Areas.MAT.Models;
using SMC.SGA.Aluno.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Aluno.Areas.MAT.Controllers
{
    public class SolicitacaoController : SolicitacaoServicoFluxoBaseController
    {
        #region Serviços

        private IAplicacaoService AplicacaoService => Create<IAplicacaoService>();

        private ISGFHelperService SGFHelperService
        {
            get { return this.Create<ISGFHelperService>(); }
        }

        private IIngressanteService IngressanteService
        {
            get { return this.Create<IIngressanteService>(); }
        }

        private IPessoaService PessoaService
        {
            get { return this.Create<IPessoaService>(); }
        }

        private ISolicitacaoMatriculaService SolicitacaoMatriculaService
        {
            get { return this.Create<ISolicitacaoMatriculaService>(); }
        }

        private IRegistroDocumentoService RegistroDocumentoService => Create<IRegistroDocumentoService>();
        private IGrupoEscalonamentoItemService GrupoEscalonamentoItemService => Create<IGrupoEscalonamentoItemService>();
        private ISolicitacaoServicoService SolicitacaoServico => Create<ISolicitacaoServicoService>();

        #endregion Serviços

        #region Modal Seleção Solicitação

        [ChildActionOnly]
        public ActionResult ModalSelecionarSolicitacao()
        {
            try
            {
                // Cria o modelo
                var model = new SolicitacaoMatriculaSeletorViewModel();

                // Verifica se já selecionou o ingressante para continuar a inscrição
                var solicitacaoSelecionada = Session[SolicitacaoMatriculaConstants.KEY_SESSION_PESSOA_ATUACAO_SELECIONADA] as SolicitacaoMatriculaSelecionadaViewModel;

                // Caso não tenha selecionado, faz a carga dos ingressantes
                if (solicitacaoSelecionada == null)
                {
                    // Recupera os dados da pessoa de acordo com ousuário do SAS
                    var pessoas = PessoaService.BuscarPessoas(new PessoaFiltroData { SeqUsuarioSAS = User.SMCGetSequencialUsuario().GetValueOrDefault() });

                    // Caso não tenha nenhuma pessoa para o sequencial logado
                    if (pessoas == null || !pessoas.Any())
                        throw new SMCApplicationException("Favor efetuar o login novamente.");

                    //// Verifica se já possui pessoa atuação selecionada.. Se sim, filtra as solicitações de reabertura/rematrícula
                    //var alunoLogado = this.GetAlunoLogado();
                    //if (alunoLogado != null && alunoLogado.Seq > 0)
                    //{
                    //    // Verifica se possui alguma matrícula de reabertura ou renovação em aberto. Se tiver, utiliza ela.
                    //    var dadosRematricula = SolicitacaoMatriculaService.BuscarDadosRematricula(new DadosSolicitacaoMatriculaRenovacaoFiltroData { SeqPessoaAtuacao = alunoLogado.Seq });
                    //    if (dadosRematricula.SeqSolicitacaoServico.HasValue)
                    //    {
                    //        model.SeqSolicitacaoServico = dadosRematricula.SeqSolicitacaoServico;
                    //        model.DescricaoProcesso = dadosRematricula.DescricaoProcesso;

                    //        SelecionarSolicitacao(model);
                    //        ViewBag.HabilitarSelecao = false;
                    //        ViewBag.SelecaoAutomatica = true;
                    //        Session[SolicitacaoMatriculaConstants.KEY_SESSION_DESABILITAR_SELECAO_SERVICO] = true;

                    //        return PartialView("_ModalSelecionarSolicitacao", model);
                    //    }
                    //}

                    // Busca os ingressantes disponíveis para seleção
                    var solicitacoes = SolicitacaoMatriculaService.BuscarSolicitacoesMatriculaLista(new SolicitacaoMatriculaFiltroData { SeqUsuarioSAS = User.SMCGetSequencialUsuario().GetValueOrDefault() }).TransformList<SolicitacaoMatriculaListaViewModel>();
                    //var ingressantes = IngressanteService.BuscarIngressantesLista(new IngressanteFiltroData { SeqPessoa = pessoa.Seq }).TransformList<IngressanteListaViewModel>();

                    // Caso não tenha nenhuma pessoa para o sequencial logado
                    if (solicitacoes == null || solicitacoes.Count == 0)
                        throw new NaoExisteIngressanteAssociadoException();

                    // Seleciona o ingressante automaticamente caso tenha somente um
                    // GLENIA, comente o if abaixo e descomente esse if comentado
                    //if (false && ingressantes?.Count == 1)
                    if (solicitacoes?.Count == 1)
                    {
                        var solicitacaoUnica = solicitacoes.FirstOrDefault();
                        model.SeqSolicitacaoServico = solicitacaoUnica.SeqSolicitacaoServico;
                        model.DescricaoProcesso = solicitacaoUnica.DescricaoProcesso;

                        SelecionarSolicitacao(model);
                        ViewBag.HabilitarSelecao = false;
                        ViewBag.SelecaoAutomatica = true;
                        Session[SolicitacaoMatriculaConstants.KEY_SESSION_DESABILITAR_SELECAO_SERVICO] = true;
                    }
                    else
                    {
                        model.Solicitacoes = solicitacoes;
                        ViewBag.HabilitarSelecao = true;
                    }
                }
                else
                {
                    // Atribui o sequencial selecionado
                    model.SeqSolicitacaoServico = solicitacaoSelecionada.SeqSolicitacaoServico;
                    model.DescricaoProcesso = solicitacaoSelecionada.DescricaoProcesso;

                    ViewBag.HabilitarSelecao = false;
                }

                return PartialView("_ModalSelecionarSolicitacao", model);
            }
            catch (System.Exception ex)
            {
                var seqAplicacao = AplicacaoService.BuscarAplicacaoPelaSigla(SMCContext.ApplicationId).Seq;
                var permiteAcessoSimulado = AplicacaoService.VerificarUsuarioPapel(User.SMCGetSequencialUsuario().Value, seqAplicacao, TOKENS_PAPEIS_APLICACAO.ACESSO_SIMULADO);
                if (permiteAcessoSimulado)
                    return PartialView("_ModalSelecionarSolicitacaoAcessoSimulado", ex);

                return PartialView("_ModalSelecionarSolicitacaoErro", ex);
            }
        }

        /// <summary>
        /// POST que recebe o ingressante selecionado pelo usuário e armazena no cookie
        /// </summary>
        /// <param name="model">Modelo com os dados postados</param>
        [HttpPost]
        [SMCAllowAnonymous]
        public ActionResult SelecionarSolicitacao(SolicitacaoMatriculaSeletorViewModel model)
        {
            if (model == null || model.SeqSolicitacaoServico == null)
                throw new SMCApplicationException("Obrigatório selecionar um processo.");

            // Recupera do POST a descrição do processo. Enviamos todas as descrições no POST para não precisar buscar do banco de dados novamente
            var descricaoProcesso = Request.Params["DescricaoProcesso_" + model.SeqSolicitacaoServico] ?? model.DescricaoProcesso;

            // Adiciona na session do usuário o processo que foi previamente selecionado
            Session[SolicitacaoMatriculaConstants.KEY_SESSION_PESSOA_ATUACAO_SELECIONADA] = new SolicitacaoMatriculaSelecionadaViewModel { SeqSolicitacaoServico = model.SeqSolicitacaoServico.Value, DescricaoProcesso = descricaoProcesso };
            return SMCRedirectToAction("Index", "Solicitacao", new { area = "MAT" });
        }

        /// <summary>
        /// Apresenta os dados do processo no topo da página
        /// </summary>
        [ChildActionOnly]
        public ActionResult MarcaSolicitacao(bool? automatico)
        {
            ViewBag.SelecaoAutomatica = automatico;
            var solicitacaoSelecionada = Session[SolicitacaoMatriculaConstants.KEY_SESSION_PESSOA_ATUACAO_SELECIONADA] as SolicitacaoMatriculaSelecionadaViewModel;
            return PartialView("_MarcaSolicitacao", solicitacaoSelecionada);
        }

        [SMCAllowAnonymous]
        public ActionResult ModalTrocarSolicitacao()
        {
            Session[SolicitacaoMatriculaConstants.KEY_SESSION_PESSOA_ATUACAO_SELECIONADA] = null;
            return SMCRedirectToAction("Index", "Solicitacao", new { area = "MAT" });
        }

        #endregion Modal Seleção Solicitação

        #region Index

        /// <summary>
        /// Index do Ingressante
        /// </summary>
        /// <returns>Tela inicial do ingressante</returns>
        public ActionResult Index()
        {
            // Verifica se já selecionou o ingressante para continuar a inscrição. Caso não tenha selecionado, retorna o usuário para Home para poder fazer a seleção do ingressante
            var solicitacaoSelecionada = Session[SolicitacaoMatriculaConstants.KEY_SESSION_PESSOA_ATUACAO_SELECIONADA] as SolicitacaoMatriculaSelecionadaViewModel;
            if (solicitacaoSelecionada == null)
                return SMCRedirectToAction("Index", "Home", new { area = "MAT" });

            solicitacaoSelecionada.SeqPessoaAtuacao = this.GetAlunoLogado().Seq;
            solicitacaoSelecionada.Etapas = SGFHelperService.BuscarEtapas(solicitacaoSelecionada.SeqSolicitacaoServico).TransformList<EtapaListaViewModel>();

            solicitacaoSelecionada.ExibirEntregaDocumentos = ExibirBotaoEntregaDocumentos(solicitacaoSelecionada.SeqSolicitacaoServico);

            return View(solicitacaoSelecionada);
        }

        private bool ExibirBotaoEntregaDocumentos(long seqSolicitacaoServico)
        {
           // Este botão deverá estar visível somente se:
           // -A solicitação em questão tiver código de adesão
           // - Não estiver na situação atual com categoria "Concluído" e "Encerrado"
           // E
           // O prazo para de entrega de pelo menos um documento estiver preenchido e vigente E a solicitação possuir documento, que permite upload, indeferido ou pendente.
           // OU
           // A solicitação possuir documento na situação "Aguardando (nova) entrega" em etapas diferentes da primeira.
            bool temCodigoAdesao = SolicitacaoMatriculaService.TemCodigoAdesao(seqSolicitacaoServico);
            bool documentoPendenteIndefinido = false;
            bool documentoAguardandoEntrega = false;
            bool solicitacaoConcluidaOuEncerrada = false;

            if (temCodigoAdesao)
            { 
                SolicitacaoServicoListarData solicitacaoServico = SolicitacaoServico.ListarSolicitacoesServico(new SolicitacaoServicoFiltroData() { Seq = seqSolicitacaoServico }).FirstOrDefault();

                solicitacaoConcluidaOuEncerrada = solicitacaoServico.SituacaoAtualSolicitacaoEncerradaOuConcluida;               

                // Se por ventura não atender as condições não se faz necessario buscar arquivos
                if (!solicitacaoConcluidaOuEncerrada)
                {
                    documentoPendenteIndefinido = this.RegistroDocumentoService.BuscarDocumentosRegistro(seqSolicitacaoServico, null, true)
                                        /*.Where(w => w.Obrigatorio || w.Grupos.SMCAny())*/
                                        .SelectMany(s => s.Documentos)
                                        .Any(a => a.DataPrazoEntrega.HasValue &&
                                                  a.DataPrazoEntrega.Value.Date >= DateTime.Now.Date &&
                                                  (a.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente ||
                                                   a.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Indeferido));

                    // Validar se a solicitação esta vigente
                    List<EtapaListaViewModel> etapas = BuscarEtapas(seqSolicitacaoServico, true);

                    // Valida se a ultima etapa esta vigente
                    EtapaListaViewModel etapaFim = etapas.OrderByDescending(o => o.OrdemEtapaSGF).FirstOrDefault();

                    //TASK 61308 - alteração da regra para não validar mais data fim/hora do escalonamento da ultima etapa
                    //solicitacaoVigente = GrupoEscalonamentoItemService.ValidarEscalonmentoDaEtapaVigentePorGrupoEscalonamento(solicitacaoServico.SeqGrupoEscalonamento, etapaFim.SeqEtapaSGF, true);


                    // Buscar todas as etapas diferentes da primeira
                    EtapaListaViewModel etapaInicial = etapas.OrderBy(o => o.OrdemEtapaSGF).FirstOrDefault();
                    List<EtapaListaViewModel> etapasDiferentesDaPrimeira = etapas.SMCRemove(r => r.SeqEtapaSGF == etapaInicial.SeqEtapaSGF).ToList();

                    // Buscar todos os documentos aguardando entrega que não sejam da primeira etapa
                    List<DocumentoData> arquivosDasEtapasDiferentesDaPrimeira = new List<DocumentoData>();
                    foreach (var etapa in etapasDiferentesDaPrimeira)
                    {
                        var arquivos = RegistroDocumentoService.BuscarDocumentosRegistro(seqSolicitacaoServico, etapa.SeqConfiguracaoEtapa);
                        arquivosDasEtapasDiferentesDaPrimeira.AddRange(arquivos);
                    }

                    documentoAguardandoEntrega = arquivosDasEtapasDiferentesDaPrimeira/*.Where(c => c.Obrigatorio || c.Grupos.SMCAny())*/.SelectMany(s => s.Documentos).Any(a => a.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega);
                }
            }

            return (temCodigoAdesao && !solicitacaoConcluidaOuEncerrada) && (documentoPendenteIndefinido || documentoAguardandoEntrega);
        }

        [ChildActionOnly]
        public ActionResult CabecalhoSolicitacao(long seqSolicitacaoServico)
        {
            var processo = SolicitacaoMatriculaService.BuscarCabecalhoMatricula(seqSolicitacaoServico).Transform<CabecalhoSolicitacaoViewModel>();
            return PartialView("_Cabecalho", processo);
        }

        #endregion Index

        public ActionResult EntregarDocumentos(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqPessoaAtuacao)
        {
           // Listar, em ordem alfabética, os documentos da solicitação de matrícula que são obrigatórios, ou que pertencem a grupos de documentos, que permitem upload:
           // - Cuja situação é "Pendente" ou "Indeferido", E
           // - Cuja situação em etapas diferentes da primeira é "Aguardando (nova) entrega".

            var aplicacaoAluno = SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ALUNO;
            var etapas = BuscarEtapas(seqSolicitacaoServico, true);
            var etapaEmAndamento = etapas.OrderBy(o => o.OrdemEtapaSGF).Where(s => s.SituacaoEtapaIngressante == SituacaoEtapaSolicitacaoMatricula.EmAndamento || s.SituacaoEtapaIngressante == SituacaoEtapaSolicitacaoMatricula.NaoIniciada).FirstOrDefault();
            var seqConfiguracaoEtapaEmAndamento = etapaEmAndamento?.SeqConfiguracaoEtapa;
            var primeiraEtapa = etapas.OrderBy(o => o.OrdemEtapaSGF).First();
            var ehPrimeiraEtapa = primeiraEtapa.SeqConfiguracaoEtapa == seqConfiguracaoEtapaEmAndamento;

            var situacoesAguardandoEntrega = new List<SituacaoEntregaDocumento> { SituacaoEntregaDocumento.AguardandoEntrega };
            var situacoesPendenteIndeferido = new List<SituacaoEntregaDocumento> { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.Indeferido };
            List<DocumentoData> documentos = new List<DocumentoData>();

            // Buscar todas as etapas diferentes da primeira
            EtapaListaViewModel etapaInicial = etapas.OrderBy(o => o.OrdemEtapaSGF).FirstOrDefault();
            List<EtapaListaViewModel> etapasDiferentesDaPrimeira = etapas.SMCRemove(r => r.SeqEtapaSGF == etapaInicial.SeqEtapaSGF).ToList();

            foreach (var etapa in etapasDiferentesDaPrimeira)
            {
                var documentosAnteriores = this.RegistroDocumentoService.BuscarDocumentosRegistroPorStatus(seqSolicitacaoServico, etapa.SeqConfiguracaoEtapa, situacoesAguardandoEntrega, true)
                                                                        /*.Where(w => w.Obrigatorio || w.Grupos.SMCAny())*/.ToList();
                documentos.AddRange(documentosAnteriores);
            }

            // Buscar todos os documentos que tem a situação de pendentes e indefidos que pertencem a grupos
            var docuemtosPendentesOuIndefinido = this.RegistroDocumentoService.BuscarDocumentosRegistroPorStatus(seqSolicitacaoServico,null, situacoesPendenteIndeferido, true)
                                                                             /*.Where(w => w.Obrigatorio || w.Grupos.SMCAny())*/.ToList();

            documentos.AddRange(docuemtosPendentesOuIndefinido);

            documentos = documentos.OrderBy(o => o.DescricaoTipoDocumento).ToList();

            var registroDocumento = PreencheModeloSolicitacaoRegistroDocumento(seqSolicitacaoServico,
                                                                               null,
                                                                               seqPessoaAtuacao,
                                                                               etapaEmAndamento,
                                                                               ehPrimeiraEtapa,
                                                                               null,
                                                                               null,
                                                                               documentos.TransformList<SolicitacaoDocumentoViewModel>());

            var view = GetExternalView(AcademicoExternalViews._REGISTRAR_DOCUMENTOS);
            return PartialView(view, registroDocumento);
        }

        public ActionResult SalvarEntregarDocumentos(SolicitacaoRegistroDocumentoViewModel model)
        {
            var etapas = BuscarEtapas(model.SeqSolicitacaoServico, true);
            var etapaEmAndamento = etapas.Where(s => s.SituacaoEtapaIngressante == SituacaoEtapaSolicitacaoMatricula.EmAndamento).FirstOrDefault();
            var seqConfiguracaoEtapaEmAndamento = etapaEmAndamento?.SeqConfiguracaoEtapa;
            var primeiraEtapa = etapas.OrderBy(o => o.OrdemEtapaSGF).First();
            var ehPrimeiraEtapa = primeiraEtapa.SeqConfiguracaoEtapa == seqConfiguracaoEtapaEmAndamento;

            model.Documentos.SMCForEach(sf => sf.Documentos.SMCForEach(f =>
            {
                if (f.ArquivoAnexado != null)
                {
                    f.EntregaPosterior = null;
                }
            }));

            ValidarDocumentosMesmoTipoPermitemVarios(model.Documentos);
            //"ValidarUpload" Validação não é feita pois tem uma regra para validar só na primeira etapa e o botão da home habilita só para usuario que tem código adesão

            var registros = model.Transform<RegistroDocumentoData>();
            registros.Documentos.SMCForEach(sf => sf.Documentos.SMCForEach(f =>
            {
                if (SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ALUNO)
                {
                    f.Observacao = f.Observacao;
                }
                else
                {
                    f.ObservacaoSecretaria = f.Observacao;
                }
            }));
            //somente envia notificacao se for diferente da primeira etapa
            RegistroDocumentoService.AnexarDocumentosSolicitacao(registros, !ehPrimeiraEtapa, ehPrimeiraEtapa);

            return SMCRedirectToUrl(model.BackUrl);
        }
    }
}