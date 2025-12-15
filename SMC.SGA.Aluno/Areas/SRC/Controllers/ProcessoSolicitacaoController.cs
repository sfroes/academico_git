using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Service.Areas.CNC.Services;
using SMC.Academico.Service.Areas.ORG.Services;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.UI.Mvc.Areas.SRC.Controllers;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Rest;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Exceptions;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Aluno.Areas.SRC.Filters;
using SMC.SGA.Aluno.Areas.SRC.Models;
using SMC.SGA.Aluno.Extensions;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMC.SGA.Aluno.Areas.SRC.Controllers
{
    [SGAVerificarUsuarioSolicitacaoServico]
    public class ProcessoSolicitacaoController : SolicitacaoServicoFluxoBaseController
    {
        #region [ Services ]

        private InstituicaoEnsinoService InstituicaoEnsinoService => this.Create<InstituicaoEnsinoService>();

        private DocumentoConclusaoService DocumentoConclusaoService => this.Create<DocumentoConclusaoService>();

        private SolicitacaoDocumentoConclusaoEntregaDigitalService SolicitacaoDocumentoConclusaoEntregaDigitalService => this.Create<SolicitacaoDocumentoConclusaoEntregaDigitalService>();

        #endregion [ Services ]

        #region [ Apis ]

        public SMCApiClient APIDiplomaGAD => SMCApiClient.Create("DiplomaGAD");

        public SMCApiClient APIHistoricoGAD => SMCApiClient.Create("HistoricoGAD");

        #endregion APIS

        public override ActionResult EntrarEtapa(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, string tokenRet)
        {
            try
            {
                return base.EntrarEtapa(seqSolicitacaoServico, seqConfiguracaoEtapa, tokenRet);
            }
            catch (Exception e)
            {
                return ThrowRedirect(e, "Index", "SolicitacaoServico");
            }
        }

        public override ActionResult RetornarPagina(string token, SolicitacaoServicoPaginaFiltroViewModel model)
        {
            switch (token)
            {
                case TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_PADRAO_SOLICITACAO_INSTRUCOES_INICIAIS:
                    return InstrucoesIniciaisSolicitacaoPadrao(model);

                case TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_PADRAO_SOLICITACAO_CONFIRMACAO:
                    return ConfirmacaoSolicitacaoPadrao(model);

                case TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_PADRAO_SOLICITACAO_INSTRUCOES_FINAIS:
                    return InstrucoesFinaisSolicitacaoPadrao(model);

                case TOKEN_SOLICITACAO_SERVICO.INSTRUCOES_FINAIS_SOLICITACAO_MATRICULA:
                    return InstrucoesFinaisTurma(model);

                case TOKEN_SOLICITACAO_SERVICO.ENTREGA_DOCUMENTO_DIGITAL:
                    return EntregaDocumentoDigital(model);

                case TOKEN_SOLICITACAO_SERVICO.DOWNLOAD_DOCUMENTO_DIGITAL:
                    return DownloadDocumentoDigital(model);

                default:
                    return base.RetornarPagina(token, model);
            }
        }

        [HttpPost]
        public ActionResult Index(ProcessoSolicitacaoViewModel model)
        {
            try
            {
                var alunoLogado = this.GetAlunoLogado();
                if (alunoLogado == null || alunoLogado.Seq == 0)
                    throw new AlunoLogadoSemVinculoException();

                // Cria a solicitação de serviço
                var dados = SolicitacaoServicoService.CriarSolicitacaoServico(new CriarSolicitacaoData { SeqProcesso = model.SeqProcesso, SeqPessoaAtuacao = alunoLogado.Seq, Origem = OrigemSolicitacaoServico.PortalAluno, SalvarMensagemLinhaTempo = true });

                return SMCRedirectToAction("EntrarEtapa", new RouteValueDictionary { { "seqSolicitacaoServico", new SMCEncryptedLong(dados.SeqSolicitacaoServico) }, { "seqConfiguracaoEtapa", new SMCEncryptedLong(dados.SeqConfiguracaoEtapa) } });
            }
            catch (SMCRequestInterruptException e) // tratamento para funcionar o Assert
            {
                throw e;
            }
            catch (Exception e)
            {
                return ThrowRedirect(e, "Index", "SolicitacaoServico");
            }
        }

        #region Conteúdo

        private ActionResult InstrucoesIniciaisSolicitacaoPadrao(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_SRC_004_04_01.ABERTURA_SOLICITACAO_INSTRUCAO_INICIAL))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            base.VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = SolicitacaoServicoService.BuscarDadosSolicitacaoPadrao(filtro.SeqSolicitacaoServico).Transform<InstrucoesIniciaisSolicitacaoPadraoPaginaViewModel>();
            if (model.ExigeJustificativa)
            {
                model.JustificativasSolicitacao = JustificativaSolicitacaoServicoService.BuscarJustificativasSolicitacaoServicoSelect(new JustificativaSolicitacaoServicoFiltroData { Ativo = true, SeqServico = filtro.SeqServico });
            }
            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarInstrucoesIniciaisSolicitacaoPadrao), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("InstrucoesIniciaisSolicitacaoPadrao", model);
        }

        [HttpPost]
        [SMCAuthorize(UC_SRC_004_04_01.ABERTURA_SOLICITACAO_INSTRUCAO_INICIAL)]
        public ActionResult SalvarInstrucoesIniciaisSolicitacaoPadrao(InstrucoesIniciaisSolicitacaoPadraoPaginaViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                if (model.ExigeJustificativa)
                {
                    // Salva os dados das justificativas da solicitação
                    if (!model.SeqJustificativa.HasValue || string.IsNullOrEmpty(model.ObservacoesJustificativa))
                        throw new SMCApplicationException("Esta solicitação requer que seja informada uma justificativa.");

                    SolicitacaoServicoService.SalvarJustificativaSolicitacao(model.Transform<DadosSolicitacaoPadraoData>());
                }
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", "ProcessoSolicitacao", new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        public ActionResult ConfirmacaoSolicitacaoPadrao(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_SRC_004_07_01.ABERTURA_SOLICITACAO_CONFIRMACAO))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Segundo solicitação, atualizar descrição ao entrar na página de confirmação e exibir os dados
            var model = SolicitacaoServicoService.BuscarDadosConfirmacaoSolicitacaoPadrao(filtro.SeqSolicitacaoServico).Transform<ConfirmacaoSolicitacaoPadraoPaginaViewModel>();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarConfirmacaoSolicitacaoPadrao), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("ConfirmacaoSolicitacaoPadrao", model);
        }

        [HttpPost]
        [SMCAuthorize(UC_SRC_004_07_01.ABERTURA_SOLICITACAO_CONFIRMACAO)]
        public ActionResult SalvarConfirmacaoSolicitacaoPadrao(ConfirmacaoSolicitacaoPadraoPaginaViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                // Executa a regra de negócio de Salvar confirmação
                SolicitacaoServicoService.SalvarConfirmacaoSolicitacaoPadrao(model.SeqSolicitacaoServico, model.SeqPessoaAtuacao, model.SeqConfiguracaoEtapa);
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", "ProcessoSolicitacao", new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        public ActionResult InstrucoesFinaisSolicitacaoPadrao(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_SRC_004_08_01.ABERTURA_SOLICITACAO_INSTRUCAO_FINAL))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro, true);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new InstrucoesFinaisSolicitacaoPadraoPaginaViewModel();

            // Recupera os dados da solicitação
            var dadosFinaisSolicitacao = SolicitacaoServicoService.BuscarDadosFinaisSolicitacaoPadrao(filtro.SeqSolicitacaoServico, filtro.SeqConfiguracaoEtapa);

            // Recupera os dados do formulário caso existam
            model.ExigeJustificativa = dadosFinaisSolicitacao.ExigeJustificativa;
            model.SeqJustificativa = dadosFinaisSolicitacao.SeqJustificativa;
            model.ObservacoesJustificativa = dadosFinaisSolicitacao.ObservacoesJustificativa;
            model.DadoFormulario = dadosFinaisSolicitacao.DadoFormulario.TransformList<FormularioPadraoDadoFormularioViewModel>();
            model.ExigeFormulario = model.DadoFormulario != null && model.DadoFormulario.Any();
            model.NomesFormularios = dadosFinaisSolicitacao.NomesFormularios;
            model.DescricaoOriginal = dadosFinaisSolicitacao.DescricaoOriginal;
            model.DescricaoAtualizada = dadosFinaisSolicitacao.DescricaoAtualizada;
            model.SituacaoAtualSolicitacao = dadosFinaisSolicitacao.SituacaoAtualSolicitacao;
            model.ObservacaoSituacaoAtual = dadosFinaisSolicitacao.ObservacaoSituacaoAtual;

            if (model.SeqJustificativa.HasValue)
                model.JustificativasSolicitacao = JustificativaSolicitacaoServicoService.BuscarJustificativasSolicitacaoServicoSelect(new JustificativaSolicitacaoServicoFiltroData { Ativo = true, SeqServico = filtro.SeqServico });

            // Recupera os documentos
            var documentosServico = RegistroDocumentoService.BuscarDocumentosRegistro(filtro.SeqSolicitacaoServico, filtro.SeqConfiguracaoEtapa);
            model.Documentos = documentosServico.SelectMany(d => d.Documentos.Where(a => a.SeqArquivoAnexado.HasValue).Select(x => new DocumentosSolicitacaoViewModel
            {
                DescricaoTipoDocumento = d.DescricaoTipoDocumento,
                ArquivoAnexado = x.ArquivoAnexado,
                Observacao = x.Observacao,
                ObservacaoSecretaria = x.ObservacaoSecretaria,
                Seq = x.Seq,
            })).ToList();

            model.Taxas = this.SolicitacaoServicoBoletoTituloService.BuscarTaxasTitulosPorSolicitacao(filtro.SeqSolicitacaoServico).TransformList<TaxasSolicitacaoViewModel>();
            model.Taxas.ForEach(a => a.SeqServico = filtro.SeqServico);
            model.Taxas.ForEach(a => a.SeqSolicitacaoServico = filtro.SeqSolicitacaoServico);
            model.Taxas.ForEach(a => a.SeqConfiguracaoEtapa = filtro.SeqConfiguracaoEtapa);

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarInstrucoesFinaisSolicitacaoPadrao), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("InstrucoesFinaisSolicitacaoPadrao", model);
        }

        [SMCAuthorize(UC_SRC_004_08_01.ABERTURA_SOLICITACAO_INSTRUCAO_FINAL)]
        public ActionResult EmissaoBoletoGerar(SMCEncryptedLong seqTitulo, SMCEncryptedLong seqTaxa, SMCEncryptedLong seqServico, SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa)
        {
            try
            {
                var alunoLogado = this.GetAlunoLogado();
                if (alunoLogado == null || alunoLogado.Seq == 0)
                    throw new AlunoLogadoSemVinculoException();

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
                return SMCRedirectToAction("EntrarEtapa", "ProcessoSolicitacao", new { SeqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) });
            }
        }

        [HttpPost]
        [SMCAuthorize(UC_SRC_004_08_01.ABERTURA_SOLICITACAO_INSTRUCAO_FINAL)]
        public ActionResult SalvarInstrucoesFinaisSolicitacaoPadrao(InstrucoesFinaisSolicitacaoPadraoPaginaViewModel model, string tokenRet)
        {
            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            if (!string.IsNullOrWhiteSpace(tokenRet))
                return SMCRedirectToAction("EntrarEtapa", "ProcessoSolicitacao", new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });

            return SMCRedirectToAction("Index", "SolicitacaoServico", new { area = "SRC" });
        }

        public ActionResult EntregaDocumentoDigital(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_SRC_004_25_01.SOLICITACAO_DOCTO_DIGITAL_ENTREGA))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro, true);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = DocumentoConclusaoService.BuscarDadosEntregaDocumentoDigital(filtro.SeqSolicitacaoServico, true).Transform<EntregaDocumentoDigitalPaginaViewModel>();

            model.SeqSolicitacaoServicoAuxiliar = filtro.SeqSolicitacaoServico;
            model.TiposDocumento = DocumentoConclusaoService.BuscarTiposDocumentoEntregaDigital();

            if (model.DocumentosConclusao != null && model.DocumentosConclusao.Any())
            {
                foreach (var documentoConclusao in model.DocumentosConclusao)
                {
                    if (documentoConclusao.DiplomaDigital && !string.IsNullOrEmpty(documentoConclusao.CodigoValidacaoDiploma))
                        documentoConclusao.UrlConsultaPublicaDiploma = $"{ConfigurationManager.AppSettings["UrlConsultaPublica"]}?CodigoVerificacao={documentoConclusao.CodigoValidacaoDiploma}&NomeCompletoDiplomado={documentoConclusao.NomeDiplomado}&ExibirApenasConsulta=true";
                }
            }

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarEntregaDocumentoDigital), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("EntregaDocumentoDigital", model);
        }

        [HttpPost]
        [SMCAuthorize(UC_SRC_004_25_01.SOLICITACAO_DOCTO_DIGITAL_ENTREGA)]
        public ActionResult SalvarEntregaDocumentoDigital(EntregaDocumentoDigitalPaginaViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                if (!model.ConfirmacaoAluno.GetValueOrDefault())
                {
                    Assert(model, string.Format(Views.ProcessoSolicitacao.App_LocalResources.UIResource.MSG_Dados_Pessoais));

                    var tokenEtapaAtualSolicitacao = SolicitacaoServicoService.BuscarTokenEtapaAtualSolicitacao(model.SeqSolicitacaoServicoAuxiliar);

                    if (tokenEtapaAtualSolicitacao == TOKEN_ETAPA_SOLICITACAO.ASSINATURA_DOCUMENTO_DIGITAL)
                        Assert(model, string.Format(Views.ProcessoSolicitacao.App_LocalResources.UIResource.MSG_Confirmacao_Fluxo_Assinatura));
                }

                DocumentoConclusaoService.SalvarEntregaDocumentoDigital(model.Transform<EntregaDocumentoDigitalPaginaData>());
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", "ProcessoSolicitacao", new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        public ActionResult DownloadDocumentoDigital(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_SRC_004_26_01.SOLICITACAO_DOCTO_DIGITAL_DOWNLOAD))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro, true);

            // Salva o Histórico de situações desta página
            // Trecho comentado pois nesse ponto ou a solicitação estará cancelada quando indeferimento, e não pode inserir histórico 
            // finalizado com sucesso, ou já terá esse histórico quando deferimento
            //VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = DocumentoConclusaoService.BuscarDadosDownloadDocumentoDigital(filtro.SeqSolicitacaoServico).Transform<DownloadDocumentoDigitalPaginaViewModel>();
            model.SeqSolicitacaoServicoAuxiliar = filtro.SeqSolicitacaoServico;

            if (model.DocumentosConclusao != null && model.DocumentosConclusao.Any())
            {
                foreach (var documentoConclusao in model.DocumentosConclusao)
                {
                    documentoConclusao.SeqSolicitacaoServico = filtro.SeqSolicitacaoServico;
                    documentoConclusao.SeqConfiguracaoEtapa = filtro.SeqConfiguracaoEtapa;

                    if (documentoConclusao.DiplomaDigital && !string.IsNullOrEmpty(documentoConclusao.CodigoValidacaoDiploma))
                        documentoConclusao.UrlConsultaPublicaDiploma = $"{ConfigurationManager.AppSettings["UrlConsultaPublica"]}?CodigoVerificacao={documentoConclusao.CodigoValidacaoDiploma}&NomeCompletoDiplomado={documentoConclusao.NomeDiplomado}&ExibirApenasConsulta=true";
                }
            }

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarDownloadDocumentoDigital), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("DownloadDocumentoDigital", model);
        }

        [SMCAuthorize(UC_SRC_004_26_01.SOLICITACAO_DOCTO_DIGITAL_DOWNLOAD)]
        public ActionResult DownloadXMLDiplomaAssinado(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, string codigoVerificacao)
        {
            try
            {
                //Poderia já setar a url que retorna da api no botão, mas precisou da action para salvar o log

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var retornoBuscarLinkXmlDiploma = APIDiplomaGAD.Execute<BuscarLinkXmlDiplomaResponseViewModel>($"BuscarLinkXmlDiploma/{codigoVerificacao}", method: Method.GET);

                if (!string.IsNullOrEmpty(retornoBuscarLinkXmlDiploma.ErrorMessage))
                {
                    throw new Exception(retornoBuscarLinkXmlDiploma.ErrorMessage);
                }

                byte[] retData = null;
                string retName = null;
                string retType = null;

                using (WebClient client = new WebClient())
                {
                    retData = client.DownloadData(retornoBuscarLinkXmlDiploma.XmlDiploma);

                    string header = client.ResponseHeaders["Content-Disposition"] ?? string.Empty;
                    string filename = "filename=";
                    int index = header.LastIndexOf(filename, StringComparison.OrdinalIgnoreCase);
                    if (index > -1)
                    {
                        var auxRetName = header.Substring(index + filename.Length);
                        var elementosAuxRetName = auxRetName.Split(';').ToList();

                        if (elementosAuxRetName.Any() && elementosAuxRetName.FirstOrDefault() != null)
                        {
                            retName = elementosAuxRetName.FirstOrDefault();
                        }
                        else
                        {
                            retName = auxRetName;
                        }
                    }
                }

                retType = MimeMapping.GetMimeMapping(retName);

                SolicitacaoDocumentoConclusaoEntregaDigitalService.SalvarLogDownloadDocumentoDigital(seqSolicitacaoServico, TipoArquivoDigital.XmlDiplomaDigital);

                return File(retData, retType, retName);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("EntrarEtapa", "ProcessoSolicitacao", new { SeqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) });
            }
        }

        [SMCAuthorize(UC_SRC_004_26_01.SOLICITACAO_DOCTO_DIGITAL_DOWNLOAD)]
        public ActionResult DownloadRepresentacaoVisualDiploma(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, string codigoVerificacao)
        {
            try
            {
                //Poderia já setar a url que retorna da api no botão, mas precisou da action para salvar o log

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var retornoBuscarLinkRepresentacaoVisualDiploma = APIDiplomaGAD.Execute<BuscarLinkRepresentacaoVisualDiplomaResponseViewModel>($"BuscarLinkRepresentacaoVisualDiploma/{codigoVerificacao}", method: Method.GET);

                if (!string.IsNullOrEmpty(retornoBuscarLinkRepresentacaoVisualDiploma.ErrorMessage))
                {
                    throw new Exception(retornoBuscarLinkRepresentacaoVisualDiploma.ErrorMessage);
                }

                byte[] retData = null;
                string retName = null;
                string retType = null;

                using (WebClient client = new WebClient())
                {
                    retData = client.DownloadData(retornoBuscarLinkRepresentacaoVisualDiploma.RepresentacaoVisual);

                    string header = client.ResponseHeaders["Content-Disposition"] ?? string.Empty;
                    string filename = "filename=";
                    int index = header.LastIndexOf(filename, StringComparison.OrdinalIgnoreCase);
                    if (index > -1)
                    {
                        var auxRetName = header.Substring(index + filename.Length);
                        var elementosAuxRetName = auxRetName.Split(';').ToList();

                        if (elementosAuxRetName.Any() && elementosAuxRetName.FirstOrDefault() != null)
                        {
                            retName = elementosAuxRetName.FirstOrDefault();
                        }
                        else
                        {
                            retName = auxRetName;
                        }
                    }
                }

                retType = MimeMapping.GetMimeMapping(retName);

                SolicitacaoDocumentoConclusaoEntregaDigitalService.SalvarLogDownloadDocumentoDigital(seqSolicitacaoServico, TipoArquivoDigital.RepresentacaoVisualDiplomaDigital);

                if (retName.EndsWith(".pdf") || retType == "application/pdf")
                {
                    Response.AddHeader("Content-Disposition", $"inline; filename=\"${retName}\"");
                    return File(retData, "application/pdf");
                }

                return File(retData, retType, retName);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("EntrarEtapa", "ProcessoSolicitacao", new { SeqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) });
            }
        }

        [SMCAuthorize(UC_SRC_004_26_01.SOLICITACAO_DOCTO_DIGITAL_DOWNLOAD)]
        public ActionResult DownloadXMLHistoricoAssinado(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, string codigoVerificacao)
        {
            try
            {
                //Poderia já setar a url que retorna da api no botão, mas precisou da action para salvar o log

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var retornoBuscarLinkXmlHistorico = APIHistoricoGAD.Execute<LinkXmlHistoricoEscolarViewModel>($"BuscarLinkXmlHistoricoEscolar/{codigoVerificacao}", method: Method.GET);

                if (!string.IsNullOrEmpty(retornoBuscarLinkXmlHistorico.ErrorMessage))
                {
                    throw new Exception(retornoBuscarLinkXmlHistorico.ErrorMessage);
                }

                byte[] retData = null;
                string retName = null;
                string retType = null;

                using (WebClient client = new WebClient())
                {
                    retData = client.DownloadData(retornoBuscarLinkXmlHistorico.XmlHistoricoEscolar);

                    string header = client.ResponseHeaders["Content-Disposition"] ?? string.Empty;
                    string filename = "filename=";
                    int index = header.LastIndexOf(filename, StringComparison.OrdinalIgnoreCase);
                    if (index > -1)
                    {
                        var auxRetName = header.Substring(index + filename.Length);
                        var elementosAuxRetName = auxRetName.Split(';').ToList();

                        if (elementosAuxRetName.Any() && elementosAuxRetName.FirstOrDefault() != null)
                        {
                            retName = elementosAuxRetName.FirstOrDefault();
                        }
                        else
                        {
                            retName = auxRetName;
                        }
                    }
                }

                retType = MimeMapping.GetMimeMapping(retName);

                SolicitacaoDocumentoConclusaoEntregaDigitalService.SalvarLogDownloadDocumentoDigital(seqSolicitacaoServico, TipoArquivoDigital.XMLHistoricoEscolar);

                return File(retData, retType, retName);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("EntrarEtapa", "ProcessoSolicitacao", new { SeqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) });
            }
        }

        [SMCAuthorize(UC_SRC_004_26_01.SOLICITACAO_DOCTO_DIGITAL_DOWNLOAD)]
        public ActionResult DownloadRepresentacaoVisualHistorico(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, string codigoVerificacao)
        {
            try
            {
                //Poderia já setar a url que retorna da api no botão, mas precisou da action para salvar o log

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var retornoBuscarLinkRepresentacaoVisualHistorico = APIHistoricoGAD.Execute<LinkRepresentacaoVisualHistoricoEscolarResponseViewModel>($"BuscarLinkRepresentacaoVisualHistoricoEscolar/{codigoVerificacao}", method: Method.GET);

                if (!string.IsNullOrEmpty(retornoBuscarLinkRepresentacaoVisualHistorico.ErrorMessage))
                {
                    throw new Exception(retornoBuscarLinkRepresentacaoVisualHistorico.ErrorMessage);
                }

                byte[] retData = null;
                string retName = null;
                string retType = null;

                using (WebClient client = new WebClient())
                {
                    retData = client.DownloadData(retornoBuscarLinkRepresentacaoVisualHistorico.RepresentacaoVisual);

                    string header = client.ResponseHeaders["Content-Disposition"] ?? string.Empty;
                    string filename = "filename=";
                    int index = header.LastIndexOf(filename, StringComparison.OrdinalIgnoreCase);
                    if (index > -1)
                    {
                        var auxRetName = header.Substring(index + filename.Length);
                        var elementosAuxRetName = auxRetName.Split(';').ToList();

                        if (elementosAuxRetName.Any() && elementosAuxRetName.FirstOrDefault() != null)
                        {
                            retName = elementosAuxRetName.FirstOrDefault();
                        }
                        else
                        {
                            retName = auxRetName;
                        }
                    }
                }

                retType = MimeMapping.GetMimeMapping(retName);

                SolicitacaoDocumentoConclusaoEntregaDigitalService.SalvarLogDownloadDocumentoDigital(seqSolicitacaoServico, TipoArquivoDigital.RepresentacaoVisualHistoricoEscolar);

                if (retName.EndsWith(".pdf") || retType == "application/pdf")
                {
                    Response.AddHeader("Content-Disposition", $"inline; filename=\"${retName}\"");
                    return File(retData, "application/pdf");
                }

                return File(retData, retType, retName);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("EntrarEtapa", "ProcessoSolicitacao", new { SeqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) });
            }
        }

        [SMCAuthorize(UC_SRC_004_26_01.SOLICITACAO_DOCTO_DIGITAL_DOWNLOAD)]
        public ActionResult DownloadRelatorioManifesto(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, string codigoVerificacao, bool diplomaDigital)
        {
            try
            {
                var retornoBuscarManifestoAssinatura = APIDiplomaGAD.Execute<BuscarManifestoAssinaturaDiplomaResponseViewModel>($"BuscarManifestoAssinatura/{codigoVerificacao}", method: Method.GET);

                if (!string.IsNullOrEmpty(retornoBuscarManifestoAssinatura.ErrorMessage))
                {
                    throw new Exception(retornoBuscarManifestoAssinatura.ErrorMessage);
                }

                if (diplomaDigital)
                    SolicitacaoDocumentoConclusaoEntregaDigitalService.SalvarLogDownloadDocumentoDigital(seqSolicitacaoServico, TipoArquivoDigital.RelatorioManifestoAssinaturasDiplomaDigital);
                else
                    SolicitacaoDocumentoConclusaoEntregaDigitalService.SalvarLogDownloadDocumentoDigital(seqSolicitacaoServico, TipoArquivoDigital.RelatorioManifestoAssinaturasHistoricoEscolar);

                if (retornoBuscarManifestoAssinatura.Nome.EndsWith(".pdf") || retornoBuscarManifestoAssinatura.Tipo == "application/pdf")
                {
                    Response.AddHeader("Content-Disposition", $"inline; filename=\"${retornoBuscarManifestoAssinatura.Nome}\"");
                    return File(retornoBuscarManifestoAssinatura.Conteudo, "application/pdf");
                }

                return File(retornoBuscarManifestoAssinatura.Conteudo, retornoBuscarManifestoAssinatura.Tipo, retornoBuscarManifestoAssinatura.Nome);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("EntrarEtapa", "ProcessoSolicitacao", new { SeqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) });
            }
        }

        [HttpPost]
        [SMCAuthorize(UC_SRC_004_26_01.SOLICITACAO_DOCTO_DIGITAL_DOWNLOAD)]
        public ActionResult SalvarDownloadDocumentoDigital(DownloadDocumentoDigitalPaginaViewModel model, string tokenRet)
        {
            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);

            if (!string.IsNullOrWhiteSpace(tokenRet))
                return SMCRedirectToAction("EntrarEtapa", "ProcessoSolicitacao", new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });

            return SMCRedirectToAction("Index", "SolicitacaoServico", new { area = "SRC" });
        }

        #region [ Intruções Finais de Turma ]

        //[SMCAuthorize(UC_MAT_003_18_01.PAGINA_SELECAO_TURMAS_SGAADMINISTRATIVO)]
        private ActionResult InstrucoesFinaisTurma(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            //if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_18_01.PAGINA_SELECAO_TURMAS_SGAADMINISTRATIVO))
            //    throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro, true);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new SelecaoTurmaPaginaViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarInstrucoesFinaisTurma), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });
            model.SeqProcesso = filtro.SeqProcesso;
            model.SeqSolicitacaoServico = filtro.SeqSolicitacaoServico;
            model.SeqProcessoEtapa = (SMCEncryptedLong)SolicitacaoMatriculaService.BuscarProcessoEtapaPorSolicitacaoMatricula(filtro.SeqSolicitacaoServico, filtro.SeqConfiguracaoEtapa);
            model.SeqConfiguracaoEtapa = filtro.SeqConfiguracaoEtapa;

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("InstrucoesFinaisTurma", model);
        }

        [HttpPost]
        //[SMCAuthorize(UC_MAT_003_18_01.PAGINA_SELECAO_TURMAS_SGAADMINISTRATIVO)]
        public ActionResult SalvarInstrucoesFinaisTurma(SelecaoTurmaPaginaViewModel model, string tokenRet)
        {
            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            if (!string.IsNullOrWhiteSpace(tokenRet))
                return SMCRedirectToAction("EntrarEtapa", "ProcessoSolicitacao", new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });

            return SMCRedirectToAction("Index", "SolicitacaoServico", new { area = "SRC" });
        }

        #endregion [ Intruções Finais de Turma ]

        #endregion Conteúdo
    }
}