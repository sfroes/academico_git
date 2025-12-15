using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Service.Areas.ORG.Services;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Rest;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using SMC.SGA.Administrativo.Areas.SRC.Views.SolicitacaoServico.App_LocalResources;
using SMC.SGA.Administrativo.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class SolicitacaoServicoController : SMCControllerBase
    {
        #region [ Services ]

        private ISolicitacaoServicoService SolicitacaoServicoService
        {
            get { return this.Create<ISolicitacaoServicoService>(); }
        }

        private ISolicitacaoDispensaService SolicitacaoDispensaService
        {
            get { return this.Create<ISolicitacaoDispensaService>(); }
        }

        private IServicoService ServicoService
        {
            get { return this.Create<IServicoService>(); }
        }

        private IViewCentralSolicitacaoServicoService ViewCentralSolicitacaoServicoService
        {
            get { return this.Create<IViewCentralSolicitacaoServicoService>(); }
        }

        private IProcessoService ProcessoService
        {
            get { return this.Create<IProcessoService>(); }
        }

        private IEntidadeService EntidadeService => this.Create<IEntidadeService>();

        private IGrupoEscalonamentoService GrupoEscalonamentoService
        {
            get { return this.Create<IGrupoEscalonamentoService>(); }
        }

        private ITipoVinculoAlunoService TipoVinculoAlunoService
        {
            get { return this.Create<ITipoVinculoAlunoService>(); }
        }

        private ISolicitacaoServicoBoletoTituloService SolicitacaoServicoBoletoTituloService
        {
            get { return this.Create<ISolicitacaoServicoBoletoTituloService>(); }
        }

        private IClassificacaoInvalidadeDocumentoService ClassificacaoInvalidadeDocumentoService => this.Create<IClassificacaoInvalidadeDocumentoService>();

        #endregion [ Services ]

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public JsonResult BuscarServicosDataSourceReadOnlySession()
        {
            var ret = ServicoService.BuscarServicosPorInstituicaoNivelEnsinoSelect();
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public JsonResult BuscarUsuariosResponsaveisDataSourceReadOnlySession()
        {
            var ret = SolicitacaoServicoService.BuscarUsuariosSolicitacoesAtribuidasSelect();
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public JsonResult BuscarTipoVinculoAlunoDataSourceReadOnlySession()
        {
            var ret = TipoVinculoAlunoService.BuscarTipoVinculoAlunoNaInstituicaoSelect(null);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public JsonResult BuscarEntidadesResponsaveisDataSourceReadOnlySession()
        {
            var ret = EntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect(usarNomeReduzido: true);
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public JsonResult BuscarProcessosDataSourceReadOnlySession(long[] seqsProcessos, long? seqServico)
        {
            List<SMCDatasourceItem> ret = new List<SMCDatasourceItem>();
            if (seqsProcessos != null && seqsProcessos.Any() && seqServico.HasValue)
            {
                ret = ProcessoService.BuscarProcessosPorServicoSelect(seqServico.Value);
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            else
                throw new Exception("Selecione um ou mais processos e o serviço desejado");
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult Index(SolicitacaoServicoFiltroViewModel filtros)
        {
            //filtros.Servicos = this.ServicoService.BuscarServicosPorInstituicaoNivelEnsinoSelect();
            //filtros.UsuariosResponsaveis = SolicitacaoServicoService.BuscarUsuariosSolicitacoesAtribuidasSelect();
            //filtros.TiposVinculoAluno = TipoVinculoAlunoService.BuscarTipoVinculoAlunoNaInstituicaoSelect(null);
            //filtros.EntidadesResponsaveis = this.ProcessoUnidadeResponsavelService.BuscarUnidadesResponsaveisVinculadasProcessoSelect(usarNomeReduzido: true);

            //if (filtros.SeqServico.HasValue)
            //    filtros.Etapas = this.ServicoService.BuscarEtapasDoServicoSelect(filtros.SeqServico.GetValueOrDefault());

            //if (filtros.SeqProcessoEtapa.HasValue && filtros.SeqServico.HasValue)
            //    filtros.SituacoesEtapa = ProcessoService.BuscarSituacoesEtapasSgfSelect(filtros.SeqProcessoEtapa.GetValueOrDefault(), filtros.SeqServico.GetValueOrDefault());

            filtros.PrimeiroAcessoPagina = true;

            //if (filtros.SeqsProcessos != null && filtros.SeqsProcessos.Any() && filtros.SeqServico.HasValue)
            //    filtros.Processos = ProcessoService.BuscarProcessosPorServicoSelect(filtros.SeqServico.Value);

            //if (filtros.SeqBloqueio.HasValue && filtros.SeqsProcessos != null && filtros.SeqsProcessos.Any())
            //    filtros.Bloqueios = this.SolicitacaoServicoService.BuscarBloqueiosDoProcessoSelect(filtros.SeqsProcessos);

            //if (filtros.SeqGrupoEscalonamento.HasValue && filtros.SeqsProcessos != null && filtros.SeqsProcessos.Any())
            //    filtros.GruposEscalonamento = this.GrupoEscalonamentoService.BuscarGruposEscalonamentoSelect(new GrupoEscalonamentoFiltroData() { SeqsProcessos = filtros.SeqsProcessos });

            //if (filtros.SeqsProcessos != null && filtros.SeqsProcessos.Any())
            //    filtros.BloquearGrupoEscalonamento = this.PreencherBloquearGrupoEscalonamento(filtros.SeqsProcessos);

            //if (filtros.SeqsProcessos != null && filtros.SeqsProcessos.Any())
            //    filtros.BloquearSituacaoDocumentacao = this.PreencherBloquearSituacaoDocumentacao(filtros.SeqsProcessos);

            return View(filtros);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult ListarSolicitacoesServico(SolicitacaoServicoFiltroViewModel filtros)
        {
            // Caso tenha sido clicado no botão limpar do filtro, retorna a mensagem padrão de selecione o filtro
            if (CheckPostClearSubmit())
                return DisplayFilterMessage();

            //Caso seja o primeiro acesso a página, setar o numero de registros exibidos no grid para 20
            if (filtros.PrimeiroAcessoPagina && filtros.PageSettings != null)
            {
                filtros.PageSettings.PageSize = 20;
                filtros.PrimeiroAcessoPagina = false;
            }

            var dto = this.ViewCentralSolicitacaoServicoService.BuscarSolicitacoesServicoLista(filtros.Transform<SolicitacaoServicoFiltroData>());

            var viewModelLista = dto.Transform<SMCPagerData<SolicitacaoServicoListarViewModel>>();
            var model = new SMCPagerModel<SolicitacaoServicoListarViewModel>(viewModelLista, filtros.PageSettings, filtros);
            return PartialView("_ListarSolicitacaoServico", model);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public JsonResult BuscarSituacoesEtapasSgfSelect(long? seqProcessoEtapa, long? seqServico)
        {
            var result = ProcessoService.BuscarSituacoesEtapasSgfSelect(seqProcessoEtapa, seqServico);

            return Json(result);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public JsonResult BuscarProcessosPorServicoSelect(SMCEncryptedLong seqServico)
        {
            var result = this.ProcessoService.BuscarProcessosPorServicoSelect(seqServico);

            return Json(result);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult BuscarEtapasDoServicoSelect(SMCEncryptedLong seqServico)
        {
            var result = new List<SMCDatasourceItem>();

            if (seqServico > 0)
                result = this.ServicoService.BuscarEtapasDoServicoSelect(seqServico);

            return Json(result);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult BuscarGruposEscalonamentoDoProcessoSelect(List<long> seqsProcessos)
        {
            var result = new List<SMCDatasourceItem>();

            if (seqsProcessos.Count > 0)
                result = this.GrupoEscalonamentoService.BuscarGruposEscalonamentoSelect(new GrupoEscalonamentoFiltroData() { SeqsProcessos = seqsProcessos });

            return Json(result);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult BuscarSituacoesDocumentacaoDoProcessoSelect(List<long> seqsProcessos)
        {
            var result = this.SolicitacaoServicoService.BuscarSituacoesDocumentacaoDoProcessoSelect(seqsProcessos);

            return Json(result);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult BuscarBloqueiosDoProcessoSelect(List<long> seqsProcessos)
        {
            var result = this.SolicitacaoServicoService.BuscarBloqueiosDoProcessoSelect(seqsProcessos);

            return Json(result);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult BuscarSituacoesMatriculaPorTipoAtuacaoSelect(TipoAtuacao tipoAtuacao)
        {
            var result = this.SolicitacaoServicoService.BuscarSituacoesMatriculaPorTipoAtuacaoSelect(tipoAtuacao);

            return Json(result);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult PreencherMensagemListaSolicitacaoServico(TipoFiltroCentralSolicitacao tipoFiltroCentralSolicitacao)
        {
            var mensagem = string.Empty;

            switch (tipoFiltroCentralSolicitacao)
            {
                case TipoFiltroCentralSolicitacao.EtapaSituacaoAtualSolicitacao:

                    mensagem = UIResource.MSG_FiltrarPor_EtapaSituacaoAtualSolicitacao;

                    break;

                case TipoFiltroCentralSolicitacao.SituacaoEtapaSelecionada:

                    mensagem = UIResource.MSG_FiltrarPor_SituacaoEtapaSelecionada;

                    break;
            }

            return Content(mensagem);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public bool PreencherBloquearGrupoEscalonamento(List<long> seqsProcessos)
        {
            var existemGruposEscalonamento = false;

            if (seqsProcessos.Count > 0)
                existemGruposEscalonamento = this.GrupoEscalonamentoService.ExistemGruposEscalonamentoPorProcesso(seqsProcessos);

            return !existemGruposEscalonamento;
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public bool PreencherBloquearSituacaoDocumentacao(List<long> seqsProcessos)
        {
            var bloquearSituacaoDocumentacao = false;

            if (seqsProcessos.Count > 0)
                bloquearSituacaoDocumentacao = this.ProcessoService.ValidarBloqueioSituacaoDocumentacao(seqsProcessos);

            return bloquearSituacaoDocumentacao;
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult PreencherTipoFiltroCentralSolicitacaoSelect(long? seqProcessoEtapa, long? seqSituacaoEtapa)
        {
            var lista = new List<SMCSelectListItem>();

            foreach (var item in SMCEnumHelper.GenerateKeyValuePair<TipoFiltroCentralSolicitacao>())
            {
                lista.Add(new SMCSelectListItem()
                {
                    Text = item.Value,
                    Value = ((int)item.Key).ToString(),
                    Selected = seqProcessoEtapa.HasValue && item.Key == TipoFiltroCentralSolicitacao.EtapaSituacaoAtualSolicitacao
                });
            }

            return Json(lista);
        }

        [SMCAuthorize(UC_SRC_004_01_09.REABRIR_SOLICITACAO)]
        public ActionResult ReabrirSolicitacao(SMCEncryptedLong seqSolicitacaoServico)
        {
            try
            {
                var tokenEtapaAtualSolicitacao = SolicitacaoServicoService.BuscarTokenEtapaAtualSolicitacao(seqSolicitacaoServico);
                if (tokenEtapaAtualSolicitacao == TOKEN_ETAPA_SOLICITACAO.ASSINATURA_DOCUMENTO_DIGITAL)
                    throw new Exception(UIResource.MSG_SolicitacaoServicoReabeturaNaoPermitida);

                var modelo = new ReaberturaSolicitacaoViewModel() { SeqSolicitacaoServico = seqSolicitacaoServico, };

                return PartialView("_ReaberturaSolicitacao", modelo);
            }
            catch (Exception ex)
            {
                ex = ex.SMCLastInnerException();
                return ThrowOpenModalException(ex.Message);
            }
        }

        [SMCAuthorize(UC_SRC_004_01_02.CONSULTAR_SOLICITACAO)]
        public ActionResult GerarBotoesSolicitacao(SMCEncryptedLong seqSolicitacaoServico)
        {
            var modelo = new BotoesSolicitacaoViewModel();
            try
            {
                modelo = this.SolicitacaoServicoService.GerarBotoesSolicitacao(seqSolicitacaoServico).Transform<BotoesSolicitacaoViewModel>();
            }
            catch (Exception ex)
            {
                modelo.MensagemErro = ex.Message;
                return ThrowRedirect(ex, nameof(HomeController.Index), "Home", new RouteValueDictionary { { "Area", "" } });
            }

            return PartialView("_BotoesSolicitacao", modelo);
        }

        [SMCAllowAnonymous]
        public ActionResult AtualizarDadosSolicitacao(SMCEncryptedLong seqSolicitacaoServico)
        {
            string msg = string.Format(UIResource.MSG_AtualizacaoDadosSolicitacao_Sucesso, "Solicitação de serviço");

            SetSuccessMessage(msg, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction(nameof(Solicitacao), "SolicitacaoServico", new { @area = "SRC", @seqSolicitacaoServico = seqSolicitacaoServico });
        }

        [SMCAuthorize(UC_SRC_004_01_08.CANCELAR_SOLICITACAO)]
        public ActionResult CancelarSolicitacao(SMCEncryptedLong seqSolicitacaoServico)
        {
            var modelo = new CancelamentoSolicitacaoViewModel()
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                ExigeMotivosCancelamento = this.SolicitacaoServicoService.ValidarMotivoCancelamentoSolicitacao(seqSolicitacaoServico),
                ExigeTipoCancelamento = this.SolicitacaoServicoService.ValidarTipoCancelamentoSolicitacao(seqSolicitacaoServico),
                MotivosCancelamento = this.SolicitacaoServicoService.BuscarMotivosCancelamentoSolicitacaoSelect(seqSolicitacaoServico),
                TiposCancelamento = new List<SMCDatasourceItem>(),
                ClassificacoesInvalidadeDocumento = new List<SMCSelectListItem>()
            };

            var tokenEtapaAtual = this.SolicitacaoServicoService.BuscarTokenEtapaAtualSolicitacao(seqSolicitacaoServico);

            if (tokenEtapaAtual == TOKEN_ETAPA_SOLICITACAO.ENTREGA_DOCUMENTO_DIGITAL)
            {
                modelo.TiposCancelamento.Add(TipoInvalidade.Permanente.SMCToSelectItem());
                modelo.TiposCancelamento.Add(TipoInvalidade.Temporario.SMCToSelectItem());
            }
            else if (tokenEtapaAtual == TOKEN_ETAPA_SOLICITACAO.ASSINATURA_DOCUMENTO_DIGITAL)
            {
                modelo.TiposCancelamento.Add(TipoInvalidade.Permanente.SMCToSelectItem());
            }

            return PartialView("_CancelamentoSolicitacao", modelo);
        }

        [SMCAuthorize(UC_SRC_004_01_08.CANCELAR_SOLICITACAO)]
        public JsonResult HabilitaCampoClassificacaoInvalidadeDocumento(TipoInvalidade? tipoCancelamento)
        {
            var obrigatoriedade = false;
            if (tipoCancelamento != null)
                obrigatoriedade = true;
            return Json(obrigatoriedade);
        }

        [SMCAuthorize(UC_SRC_004_01_08.CANCELAR_SOLICITACAO)]
        public ActionResult BuscarDadosSelectClassificacaoInvalidadeDocumento(TipoInvalidade tipoCancelamento)
        {
            return Json(ClassificacaoInvalidadeDocumentoService.BuscarDadosSelectClassificacaoInvalidadeDocumento(tipoCancelamento));
        }

        [SMCAuthorize(UC_SRC_004_01_08.CANCELAR_SOLICITACAO)]
        public ActionResult CabecalhoCancelamentoSolicitacao(SMCEncryptedLong seqSolicitacaoServico)
        {
            var modelo = this.SolicitacaoServicoService.BuscarDadosCabecalhoCancelamentoSolicitacao(seqSolicitacaoServico);

            return PartialView("_CabecalhoCancelamentoSolicitacao", modelo.Transform<CabecalhoCancelamentoSolicitacaoViewModel>());
        }

        [SMCAuthorize(UC_SRC_004_01_09.REABRIR_SOLICITACAO)]
        public ActionResult CabecalhoReaberturaSolicitacao(SMCEncryptedLong seqSolicitacaoServico)
        {
            var modelo = this.SolicitacaoServicoService.BuscarDadosCabecalhoReaberturaSolicitacao(seqSolicitacaoServico);

            return PartialView("_CabecalhoReaberturaSolicitacao", modelo.Transform<CabecalhoReaberturaSolicitacaoViewModel>());
        }

        [SMCAuthorize(UC_SRC_004_01_08.CANCELAR_SOLICITACAO)]
        public ActionResult SalvarCancelamentoSolicitacao(CancelamentoSolicitacaoViewModel modelo)
        {
            var tokenEtapaAtualSolicitacao = SolicitacaoServicoService.BuscarTokenEtapaAtualSolicitacao(modelo.SeqSolicitacaoServico);

            if (tokenEtapaAtualSolicitacao == TOKEN_ETAPA_SOLICITACAO.ASSINATURA_DOCUMENTO_DIGITAL)
                Assert(modelo, string.Format(UIResource.MSG_Confirmacao_Fluxo_Assinatura));

            this.SolicitacaoServicoService.SalvarCancelamentoSolicitacao(modelo.Transform<CancelamentoSolicitacaoData>());

            string msg = string.Format(UIResource.MSG_CancelamentoSolicitacao_Sucesso, "Solicitação de serviço");

            SetSuccessMessage(msg, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction(nameof(Solicitacao), "SolicitacaoServico", new { @area = "SRC", @seqSolicitacaoServico = modelo.SeqSolicitacaoServico });
        }

        [SMCAuthorize(UC_SRC_004_01_09.REABRIR_SOLICITACAO)]
        public ActionResult SalvarReaberturaSolicitacao(ReaberturaSolicitacaoViewModel modelo)
        {
            var dispensaItemPlano = this.SolicitacaoDispensaService.VerificarReabrirDispensaItemPlano(modelo.SeqSolicitacaoServico);

            if (dispensaItemPlano)
            {
                Assert(modelo, UIResource.MSG_SolicitacaoReabrirDispensaItemPlano);
            }

            this.SolicitacaoServicoService.ReabrirSolicitacao(modelo.SeqSolicitacaoServico, modelo.Observacao);

            string msg = string.Format(UIResource.MSG_ReaberturaSolicitacao_Sucesso, "Solicitação de serviço");

            SetSuccessMessage(msg, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction(nameof(Solicitacao), "SolicitacaoServico", new { @area = "SRC", @seqSolicitacaoServico = modelo.SeqSolicitacaoServico });
        }

        [SMCAuthorize(UC_SRC_004_01_02.CONSULTAR_SOLICITACAO)]
        public ActionResult Solicitacao(SMCEncryptedLong seqSolicitacaoServico, string backUrl = "")
        {
            var modelo = this.SolicitacaoServicoService.PrepararModeloSolicitacaoServico(seqSolicitacaoServico);
            var retorno = modelo.Transform<DadosSolicitacaoViewModel>();
            retorno.BackUrl = backUrl;

            if (!Request.IsAjaxRequest())
            {
                retorno.ExibirBotoesSolicitacao = true;
                return View(retorno);
            }
            else
            {
                retorno.ExibirBotoesSolicitacao = false;
                return PartialView(retorno);
            }
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult BuscarDadosIdentificacaoSolicitante(SMCEncryptedLong seqSolicitacaoServico)
        {
            var result = this.SolicitacaoServicoService.BuscarDadosIdentificacaoSolicitante(seqSolicitacaoServico);

            return PartialView("_DadosIdentificacaoSolicitante", result.Transform<DadosSolicitacaoViewModel>());
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult BuscarHistoricosSolicitacao(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqPessoaAtuacao, string backUrl)
        {
            var result = this.SolicitacaoServicoService.BuscarHistoricosSolicitacao(seqSolicitacaoServico, seqPessoaAtuacao);
            var retorno = result.Transform<DadosSolicitacaoViewModel>();
            retorno.HistoricosSolicitacao.SelectMany(a => a.Etapas).ToList().ForEach(a => a.BackUrl = backUrl);

            return PartialView("_HistoricoSolicitacao", retorno);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult BuscarNotificacoesSolicitacao(NotificacaoSolicitacaoFiltroViewModel filtro)
        {
            var result = this.SolicitacaoServicoService.BuscarNotificacoesSolicitacao(filtro.Transform<NotificacaoSolicitacaoFiltroData>());

            var model = result.Transform<DadosSolicitacaoViewModel>();

            model.NotificacoesSolicitacao = new SMCPagerModel<NotificacaoSolicitacaoViewModel>(result.NotificacoesSolicitacao.TransformList<NotificacaoSolicitacaoViewModel>(), new SMCPageSetting() { Total = result.NotificacoesSolicitacao.Count });

            return PartialView("_NotificacaoSolicitacao", model);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult BuscarDocumentosSolicitacao(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqPessoaAtuacao)
        {
            var result = this.SolicitacaoServicoService.BuscarDocumentosSolicitacao(seqSolicitacaoServico, seqPessoaAtuacao);

            foreach (var documentoConclusao in result.DocumentosConclusaoSolicitacao)
            {
                if (documentoConclusao.SeqDocumentoGAD.HasValue && documentoConclusao.SeqDocumentoGAD != 0)
                {
                    var usuarioInclusao = UsuarioLogado.RetornaUsuarioLogado();
                    documentoConclusao.UrlDocumentoGAD = $"{ConfigurationManager.AppSettings["UrlDocumentoGAD"]}?identificador={new SMCEncryptedLong(documentoConclusao.SeqDocumentoGAD.Value)}&token={documentoConclusao.TokenTipoDocumentoAcademico}&usuario={SMCDESCrypto.EncryptForURL(usuarioInclusao)}";
                }
            }

            return PartialView("_DocumentacaoSolicitacao", result.Transform<DadosSolicitacaoViewModel>());
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult BuscarTaxasSolicitacao(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqServico, string backUrl)
        {
            var result = new DadosSolicitacaoViewModel
            {
                TaxasSolicitacao = this.SolicitacaoServicoBoletoTituloService.BuscarTaxasTitulosPorSolicitacao(seqSolicitacaoServico).TransformList<TaxaSolicitacaoViewModel>()
            };
            result.TaxasSolicitacao.ForEach(a => a.SeqServico = seqServico);
            result.TaxasSolicitacao.ForEach(a => a.SeqSolicitacaoServico = seqSolicitacaoServico);
            result.TaxasSolicitacao.ForEach(a => a.BackUrl = backUrl);

            result.TitulosSolicitacao = this.SolicitacaoServicoBoletoTituloService.BuscarTitulosPorSolicitacao(seqSolicitacaoServico).TransformList<TituloSolicitacaoViewModel>();

            return PartialView("_TaxasSolicitacao", result);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult EmissaoBoletoGerar(SMCEncryptedLong seqTitulo, SMCEncryptedLong seqTaxa, SMCEncryptedLong seqServico, SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, string backUrl)
        {
            try
            {
                long seqNovoTitulo = this.SolicitacaoServicoService.ProcedimentosReemissaoTitulo(seqTitulo, seqTaxa, seqServico, seqSolicitacaoServico);

                //3. A emissão do título válido da solicitação de serviço deverá ser de acordo com: RN_SRC_118 - Solicitação - Emissão título financeiro
                var urlApi = $"{ConfigurationManager.AppSettings[WEB_API_REST.BASE_URL_KEY]}{WEB_API_REST.EMITIR_BOLETO_ALUNO}";
                var cancelationTimer = int.Parse(ConfigurationManager.AppSettings[WEB_API_REST.CANCELLATION_TIME_KEY]);
                var token = SMCDESCrypto.Encrypt(ConfigurationManager.AppSettings[WEB_API_REST.TOKEN_BOLETO_KEY]);
                var filtro = new { SeqTitulo = seqNovoTitulo, SeqServico = seqServico.Value, Token = token, Crei = true };
                var boleto = SMCRest.PostJson(urlApi, filtro, cancellationTimer: cancelationTimer);

                return File(Convert.FromBase64String(boleto), "application/pdf");
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);

                if (!string.IsNullOrEmpty(backUrl))
                {
                    return SMCRedirectToUrl(backUrl);
                }
                else
                {
                    return SMCRedirectToAction(nameof(Solicitacao), routeValues: new { seqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoServico) });
                }
            }
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult VisualizarDadosSolicitante(SMCEncryptedLong seqPessoaAtuacao)
        {
            var result = this.SolicitacaoServicoService.BuscarDadosSolicitante(seqPessoaAtuacao);

            return PartialView("_DadosSolicitante", result.Transform<DadosSolicitanteViewModel>());
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult CabecalhoInformacaoProcesso(SMCEncryptedLong seqProcesso, SMCEncryptedLong seqGrupoEscalonamento)
        {
            var modelo = this.ProcessoService.BuscarCabecalhoDadosProcesso(seqProcesso, seqGrupoEscalonamento);

            return PartialView("_CabecalhoInformacaoProcesso", modelo.Transform<CabecalhoInformacaoProcessoViewModel>());
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult VisualizarDadosProcesso(SMCEncryptedLong seqProcesso, SMCEncryptedLong seqGrupoEscalonamento)
        {
            var model = new InformacaoProcessoListarViewModel() { SeqProcesso = seqProcesso, SeqGrupoEscalonamento = seqGrupoEscalonamento };

            var result = this.ProcessoService.BuscarDadosProcesso(seqProcesso, seqGrupoEscalonamento);

            model.ExibirPrazo = result.ExibirPrazo;
            model.ExibirData = result.ExibirData;
            model.InformacoesProcesso = new SMCPagerModel<InformacaoProcessoItemViewModel>(result.InformacoesProcesso.TransformList<InformacaoProcessoItemViewModel>(), new SMCPageSetting() { Total = result.InformacoesProcesso.Total });

            return PartialView("_DadosProcesso", model);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult ConsultarHistoricosNavegacao(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqSolicitacaoServicoEtapa)
        {
            var result = this.SolicitacaoServicoService.BuscarHistoricosNavegacao(seqSolicitacaoServico, seqSolicitacaoServicoEtapa);

            return PartialView("_HistoricoNavegacao", result.Transform<HistoricoNavegacaoViewModel>());
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult ConsultarBloqueiosEtapa(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqSolicitacaoServicoEtapa, string backUrl)
        {
            var result = this.SolicitacaoServicoService.BuscarBloqueiosEtapaSolicitacao(seqSolicitacaoServico, seqSolicitacaoServicoEtapa);
            var retorno = result.Transform<BloqueioEtapaSolicitacaoViewModel>();
            retorno.BackUrl = backUrl;

            return PartialView("_BloqueioEtapa", retorno);
        }

        [SMCAuthorize(UC_SRC_004_01_05.CONSULTAR_DADOS_NOTIFICACAO)]
        public ActionResult ConsultarDetalheNotificacao(SMCEncryptedLong seqNotificacaoEmail, SMCEncryptedLong seqSolicitacaoServico = null, SMCEncryptedLong seqTipoNotificacao = null)
        {
            var result = this.SolicitacaoServicoService.BuscarDetalheNotificacaoSolicitacao(seqNotificacaoEmail, seqTipoNotificacao, seqSolicitacaoServico);
            var retorno = result.Transform<DetalheNotificacaoSolicitacaoViewModel>();

            retorno.SeqSolicitacaoServico = seqSolicitacaoServico;
            retorno.SeqNotificacaoEmailDestinatario = seqNotificacaoEmail;

            return PartialView("_DetalheNotificacaoSolicitacao", retorno);
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult EnviarNotificacaoSolicitacao(SMCEncryptedLong seqSolicitacaoServico, string tokenTipoNotificacao = null)
        {
            try
            {
                this.SolicitacaoServicoService.EnviarNotificacaoSolicitacao(seqSolicitacaoServico, tokenTipoNotificacao);
                SetSuccessMessage("Notificação encaminhada", target: SMCMessagePlaceholders.Centro);

                return SMCRedirectToAction(nameof(Solicitacao), routeValues: new { seqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoServico) });
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction(nameof(Solicitacao), routeValues: new { seqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoServico) });
            }
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult ReenviarNotificacaoSolicitacao(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqNotificacaoEmailDestinatario, PermiteReenvio permiteReenvio)
        {
            try
            {
                this.SolicitacaoServicoService.ReenviarNotificacaoSolicitacao(seqSolicitacaoServico, seqNotificacaoEmailDestinatario, permiteReenvio);
                SetSuccessMessage("Notificação reenviada!", target: SMCMessagePlaceholders.Centro);

                return SMCRedirectToAction(nameof(Solicitacao), routeValues: new { seqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoServico) });
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction(nameof(Solicitacao), routeValues: new { seqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoServico) });
            }
        }

        [SMCAuthorize(UC_SRC_004_01_01.PESQUISAR_SOLICITACAO)]
        public ActionResult RetornarSolicitacaoParaEtapaAnterior(SMCEncryptedLong seqSolicitacaoServico)
        {
            this.SolicitacaoServicoService.RetornarSolicitacaoParaEtapaAnterior(seqSolicitacaoServico);

            string msg = string.Format(UIResource.MSG_RetornoParaEtapaAnterior_Sucesso, "Solicitação de serviço");

            SetSuccessMessage(msg, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction(nameof(Solicitacao), "SolicitacaoServico", new { @area = "SRC", @seqSolicitacaoServico = seqSolicitacaoServico });
        }
    }
}