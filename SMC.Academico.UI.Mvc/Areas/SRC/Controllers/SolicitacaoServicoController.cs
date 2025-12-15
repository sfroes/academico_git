using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Rest;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Controllers
{
    public class SolicitacaoServicoController : SMCControllerBase
    {
        #region [ Services ]

        private IGrupoEscalonamentoService GrupoEscalonamentoService { get => Create<IGrupoEscalonamentoService>(); }

        private IProcessoEtapaService ProcessoEtapaService { get => Create<IProcessoEtapaService>(); }

        private IProcessoService ProcessoService { get => Create<IProcessoService>(); }

        private ISolicitacaoServicoService SolicitacaoServicoService { get => Create<ISolicitacaoServicoService>(); }

        private ISolicitacaoServicoBoletoTituloService SolicitacaoServicoBoletoTituloService { get => Create<ISolicitacaoServicoBoletoTituloService>(); }

        #endregion [ Services ]

        [SMCAllowAnonymous]
        public ActionResult BuscarGruposEscalonamentoSelect(long seqProcesso)
        {
            var retorno = GrupoEscalonamentoService.BuscarGruposEscalonamentoSelect(new GrupoEscalonamentoFiltroData() { SeqProcesso = seqProcesso });

            return Json(retorno);
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarProcessoEtapaSelect(long seqProcesso)
        {
            var retorno = ProcessoEtapaService.BuscarProcessoEtapaPorProcessoSelect(seqProcesso);

            return Json(retorno);
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarProcessosPorServicoSelect(long seqServico)
        {
            var retorno = ProcessoService.BuscarProcessosPorServicoSelect(seqServico);

            return Json(retorno);
        }

        [SMCAllowAnonymous]
        public JsonResult BuscarSituacoesEtapasComCategoriaSelect(long? seqProcessoEtapa, long seqProcesso)
        {
            var result = ProcessoService.BuscarSituacoesEtapasComCategoriaSelect(seqProcessoEtapa, new List<long> { seqProcesso });

            return Json(result);
        }

        [SMCAllowAnonymous]
        [HttpGet]
        public ActionResult ConsultarSolicitacaoServicoModal(long? seqSolicitacaoServico, long? seqPessoaAtuacao, string numeroProtocolo, string backUrl)
        {
            var protocolo = string.Empty;
            if (!string.IsNullOrEmpty(numeroProtocolo))
                protocolo = SMCDESCrypto.DecryptForURL(numeroProtocolo);

            var model = SolicitacaoServicoService.BuscarDadosModalSolicitacaoServico(seqSolicitacaoServico, protocolo).Transform<DadosModalSolicitacaoServicoViewModel>();
            model.BackUrl = backUrl;

            var viewPrincipal = GetExternalView(AcademicoExternalViews.DADOS_MODAL_SOLICITACAO_SERVICO);

            if (SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ADMINISTRATIVO)
            {
                return PartialView(viewPrincipal, model);
            }
            else
            {
                return View(viewPrincipal, model);
            }
        }

        [SMCAllowAnonymous]
        [HttpGet]
        public ActionResult BuscarDadosIdentificacaoSolicitante(long? seqSolicitacaoServico, string numeroProtocolo)
        {
            var model = SolicitacaoServicoService.BuscarDadosModalSolicitacaoServico(seqSolicitacaoServico, numeroProtocolo).Transform<DadosModalSolicitacaoServicoViewModel>();

            model.ExibirInformacao = SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ADMINISTRATIVO;

            var viewPrincipal = GetExternalView(AcademicoExternalViews.DADOS_MODAL_SOLICITACAO_SERVICO_IDENTIFICACAO_SOLICITANTE);

            return PartialView(viewPrincipal, model);
        }

        [SMCAllowAnonymous]
        [HttpGet]
        public ActionResult BuscarDocumentosSolicitacao(long seqSolicitacaoServico, long seqPessoaAtuacao)
        {
            var documentosSolicitacao = SolicitacaoServicoService.BuscarDocumentosSolicitacao(seqSolicitacaoServico, seqPessoaAtuacao);

            var model = documentosSolicitacao.Transform<DadosModalSolicitacaoServicoViewModel>();

            var viewPrincipal = GetExternalView(AcademicoExternalViews.DADOS_MODAL_SOLICITACAO_SERVICO_DOCUMENTACAO);

            return PartialView(viewPrincipal, model);
        }

        [SMCAllowAnonymous]
        [HttpGet]
        public ActionResult BuscarTaxasSolicitacao(long seqSolicitacaoServico, long seqServico, string numeroProtocolo, string backUrl)
        {
            var model = new DadosModalSolicitacaoServicoViewModel();

            model.TaxasSolicitacao = this.SolicitacaoServicoBoletoTituloService.BuscarTaxasTitulosPorSolicitacao(seqSolicitacaoServico).TransformList<DadosModalSolicitacaoTaxaViewModel>();
            model.TaxasSolicitacao.ForEach(a => a.SeqServico = seqServico);
            model.TaxasSolicitacao.ForEach(a => a.SeqSolicitacaoServico = seqSolicitacaoServico);
            model.TaxasSolicitacao.ForEach(a => a.NumeroProtocolo = numeroProtocolo);
            model.TaxasSolicitacao.ForEach(a => a.BackUrl = backUrl);

            var viewPrincipal = GetExternalView(AcademicoExternalViews.DADOS_MODAL_SOLICITACAO_SERVICO_TAXAS);

            return PartialView(viewPrincipal, model);
        }

        [SMCAllowAnonymous]
        [HttpGet]
        public ActionResult EmissaoBoletoGerar(SMCEncryptedLong seqTitulo, SMCEncryptedLong seqTaxa, SMCEncryptedLong seqServico, SMCEncryptedLong seqSolicitacaoServico, string numeroProtocolo, string backUrl)
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

                if (SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ADMINISTRATIVO)
                {
                    return SMCRedirectToUrl(backUrl);
                }
                else
                {
                    return SMCRedirectToAction(nameof(ConsultarSolicitacaoServicoModal), routeValues: new { seqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoServico), numeroProtocolo = SMCDESCrypto.EncryptForURL(numeroProtocolo) });
                }
            }
        }

        [SMCAllowAnonymous]
        [HttpGet]
        public ActionResult BuscarHistoricosSolicitacao(long seqSolicitacaoServico, long seqPessoaAtuacao)
        {
            var historicoSolicitacao = this.SolicitacaoServicoService.BuscarHistoricosSolicitacao(seqSolicitacaoServico, seqPessoaAtuacao);

            var model = historicoSolicitacao.Transform<DadosModalSolicitacaoServicoViewModel>();

            var viewPrincipal = GetExternalView(AcademicoExternalViews.DADOS_MODAL_SOLICITACAO_SERVICO_HISTORICO);

            return PartialView(viewPrincipal, model);
        }

        public ActionResult TernoAdesao(SMCEncryptedLong seqSolicitacaoMatricula)
        {
            return View();
        }
    }
}