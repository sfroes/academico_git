using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.CNC.Exceptions.SolicitacaoDocumentoConclusao;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Service.Areas.ORT.Services;
using SMC.Academico.Service.Areas.SRC.Services;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CNC.Data.SolicitacaoDocumentoConclusao;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data.Efetivacao;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Data.SolicitacaoReabertura;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.UI.Mvc;
using SMC.Academico.UI.Mvc.Areas.SRC.Controllers;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.DadosMestres.ServiceContract.Areas.GED.Interfaces;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using SMC.SGA.Administrativo.Areas.SRC.Views.RealizarAtendimento.App_LocalResources;
using SMC.SGA.Administrativo.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class RealizarAtendimentoController : SolicitacaoServicoFluxoBaseController
    {
        #region [ Services ]

        private TrabalhoAcademicoService TrabalhoAcademicoService => Create<TrabalhoAcademicoService>();

        private IRequisitoService RequisitoService => Create<IRequisitoService>();

        private IDocumentoConclusaoService DocumentoConclusaoService => Create<IDocumentoConclusaoService>();

        private ISolicitacaoDocumentoConclusaoService SolicitacaoDocumentoConclusaoService => Create<ISolicitacaoDocumentoConclusaoService>();

        private IInstituicaoExternaService InstituicaoExternaService => Create<IInstituicaoExternaService>();

        private IInstituicaoNivelTipoOrientacaoParticipacaoService InstituicaoNivelTipoOrientacaoParticipacaoService => Create<IInstituicaoNivelTipoOrientacaoParticipacaoService>();

        private IColaboradorService ColaboradorService => Create<IColaboradorService>();

        private IInstituicaoNivelTipoOrientacaoService InstituicaoNivelTipoOrientacaoService => this.Create<IInstituicaoNivelTipoOrientacaoService>();

        private IPessoaAtuacaoService PessoaAtuacaoService => Create<IPessoaAtuacaoService>();

        private IInstituicaoNivelTipoTermoIntercambioService InstituicaoNivelTipoTermoIntercambioService => Create<IInstituicaoNivelTipoTermoIntercambioService>();

        private ITermoIntercambioService TermoIntercambioService => Create<ITermoIntercambioService>();

        private IGrupoEscalonamentoService GrupoEscalonamentoService => Create<IGrupoEscalonamentoService>();

        private IPessoaAtuacaoBloqueioService PessoaAtuacaoBloqueioService => Create<IPessoaAtuacaoBloqueioService>();

        private IRegistroDocumentoService RegistroDocumentoService => Create<IRegistroDocumentoService>();

        private IAlunoService AlunoService => Create<IAlunoService>();
        private ITipoDocumentoService ITipoDocumentoService => Create<ITipoDocumentoService>();

        private IInstituicaoNivelTipoMensagemService InstituicaoNivelTipoMensagemService => Create<IInstituicaoNivelTipoMensagemService>();
        private IGrupoDocumentoRequeridoService GrupoDocumentoRequeridoService => Create<IGrupoDocumentoRequeridoService>();

        #endregion [ Services ]

        public override ActionResult RetornarPagina(string token, SolicitacaoServicoPaginaFiltroViewModel model)
        {
            switch (token)
            {
                case TOKEN_SOLICITACAO_SERVICO.ATENDIMENTO_PADRAO_ATENDIMENTO_DADOS_SOLICITACAO_ANEXO:
                    return SolicitacaoDadosAnexos(model);

                case TOKEN_SOLICITACAO_SERVICO.ATENDIMENTO_PADRAO_ATENDIMENTO_PARECER:
                    return AtendimentoParecer(model);

                case TOKEN_SOLICITACAO_SERVICO.ATENDIMENTO_PADRAO_ATENDIMENTO_REABERTURA:
                    return AtendimentoReabertura(model);

                case TOKEN_SOLICITACAO_SERVICO.ATENDIMENTO_PADRAO_ATENDIMENTO_INTERCAMBIO:
                    return AtendimentoIntercambio(model);

                case TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_DISPENSA_AGRUPAMENTO_ITENS:
                    return AtendimentoDispensaAgrupamento(model);

                case TOKEN_SOLICITACAO_SERVICO.ANALISE_EMISSAO_DOCUMENTO_CONCLUSAO:
                    return AtendimentoEmissaoDocumentoConclusao(model);

                case TOKEN_SOLICITACAO_SERVICO.PARECER_ENTREGA_DOCUMENTACAO:
                    return RegistrarEntregaDocumentacaoAtendimento(model);
                default:
                    return base.RetornarPagina(token, model);
            }
        }

        protected override void AposConfigurarPagina<TModelSolicitacao>(TModelSolicitacao pagina, SolicitacaoServicoPaginaFiltroViewModel filtro, string actionSalvar, long seqSolicitacaoHistoricoNavegacao)
        {
            /*
             Se selecionada a opção SIM a solicitação deverá ser atribuída para o usuário logado e, acionada a
             [tela para a realização do atendimento], senão a mensagem de confirmação é fechada e retorna para a tela de pesquisa.
            */

            // Chama a rotina para atribuir o usuário atual como responsável pelo atendimento da solicitação
            this.SolicitacaoServicoService.AtualizarUsuarioResponsavelAtendimento(filtro.SeqSolicitacaoServico);
        }

        /// <summary>
        /// Renderiza o cabeçalho do atendimento padrão
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ActionResult CabecalhoAtendimentoPadrao(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            var model = SolicitacaoServicoService.BuscarDadosCabecalhoAtendimentoPadrao(filtro.SeqSolicitacaoServico).Transform<RealizarAtendimentoPadraoCabecalhoViewModel>();
            return PartialView(model);
        }

        #region Instruções Iniciais

        private ActionResult SolicitacaoDadosAnexos(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_SRC_004_03_01.REALIZAR_ATENDIMENTO_INSTRUCAO_INICIAL))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = this.SolicitacaoServicoService.BuscarDadosFinaisSolicitacaoPadrao(filtro.SeqSolicitacaoServico).Transform<SolicitacaoDadosAnexosViewModel>();

            // Recupera os documentos
            var documentosServico = RegistroDocumentoService.BuscarDocumentosRegistro(filtro.SeqSolicitacaoServico);
            model.Documentos = documentosServico.SelectMany(d => d.Documentos.Where(x => x.DataEntrega.HasValue).Select(x => new DocumentosAtendimentoItemViewModel
            {
                DescricaoTipoDocumento = d.DescricaoTipoDocumento,
                ArquivoAnexado = x.ArquivoAnexado,
                Observacao = x.Observacao,
                ObservacaoSecretaria = x.ObservacaoSecretaria,
                Seq = x.Seq,
            })).ToList();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarSolicitacaoDadosAnexos), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("SolicitacaoDadosAnexos", model);
        }

        [HttpPost]
        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_03_01.REALIZAR_ATENDIMENTO_INSTRUCAO_INICIAL)]
        public ActionResult SalvarSolicitacaoDadosAnexos(SolicitacaoDadosAnexosViewModel model, string tokenRet)
        {
            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", "RealizarAtendimento", new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        #endregion Instruções Iniciais

        #region Realizar Atendimento

        private ActionResult AtendimentoParecer(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_SRC_004_09_01.REALIZAR_ATENDIMENTO_PARECER))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new AtendimentoParecerViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarAtendimentoParecer), historico.Seq);

            // Completa o modelo
            model.TokenServico = filtro.TokenServico;

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("AtendimentoParecer", model);
        }

        [HttpPost]
        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_09_01.REALIZAR_ATENDIMENTO_PARECER)]
        public ActionResult SalvarAtendimentoParecer(AtendimentoParecerViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                // Busca os dados da solicitação de serviço
                var dadosSolicitacao = SolicitacaoServicoService.BuscarDadosParecer(model.SeqSolicitacaoServico).Transform<AtendimentoParecerAssertViewModel>();

                /********************** VALIDAÇÕES DE ASSERT **********************/
                // Mensagem de confirmação de deferimento / indeferimento (RN_SRC_027)
                Assert("ModalDeferido", model, "_AssertDeferido", dadosSolicitacao, () => model.Situacao.GetValueOrDefault());
                Assert("ModalIndeferido", model, "_AssertIndeferido", dadosSolicitacao, () => !model.Situacao.GetValueOrDefault());

                // Se está deferindo e o serviço valida situação futura do aluno, confirma o deferimento (RN_SRC_068)
                if (model.Situacao.GetValueOrDefault())
                {
                    if (dadosSolicitacao.TipoAtuacao == TipoAtuacao.Aluno &&
                        dadosSolicitacao.ValidarSituacaoFutura)
                    {
                        ValidarSituacoesFuturas(model, dadosSolicitacao.DataSolicitacao);
                    }

                    // Se for algum serviço que requer validação de parcela em aberto, confirma a existência de pendencia financeira
                    if (model.TokenServico == TOKEN_SERVICO.TRANCAMENTO_MATRICULA || model.TokenServico == TOKEN_SERVICO.CANCELAMENTO_MATRICULA)
                    {
                        bool pendenciaFinanceira = SolicitacaoServicoService.ValidarNadaConstaFinanceiro(model.SeqSolicitacaoServico);
                        if (model.TokenServico == TOKEN_SERVICO.TRANCAMENTO_MATRICULA)
                            Assert(model, "MSG_Assert_ParcelasEmAberto_Trancamento", () => pendenciaFinanceira);
                        else if (model.TokenServico == TOKEN_SERVICO.CANCELAMENTO_MATRICULA && pendenciaFinanceira)
                            throw new SolicitacaoServicoComPendenciaFinanceira();
                    }

                    else if (model.TokenServico == TOKEN_SERVICO.DISPENSA_INDIVIDUAL)
                    {
                        // Se é dispensa...
                        /* TASK 43383
                         * 1. Verificar se em "Itens a serem dispensados" foram selecionados componentes+assunto que estão 
                         * em algum plano de estudo (ind_atual = 1) do aluno sem histórioco escolar para esse componente, 
                         * assunto (se houver) e ciclo letivo. Se sim, exibir a mensagem de confirmação:
                         * "Os seguintes componentes estão no plano de estudo do aluno: [Lista de componentes+assunto de componente 
                         * com o ciclo letivo separados por vírgula]. O deferimento da dispensa fará uma alteração de plano 
                         * removendo este item. Deseja prosseguir com o deferimento?"
                         * Se usuário clicar em não, abortar a operação.
                         * Se usuário clicar em sim, prosseguir.
                         */
                        var componentePlanoSemHistorico = SolicitacaoDispensaService.VerificarTurmasPlanoEstudoSemHistorico(model.SeqSolicitacaoServico);
                        if (componentePlanoSemHistorico != null && componentePlanoSemHistorico.Count > 0)
                        {
                            var listaComponente = string.Empty;
                            var componentePlanoSemHistoricoDistinct = componentePlanoSemHistorico.Select(i => new
                            {
                                i.DescricaoCicloLetivo,
                                i.DescricaoComponenteCurricular,
                                i.DescricaoComponenteCurricularAssunto
                            }).Distinct().ToList();
                            foreach (var item in componentePlanoSemHistoricoDistinct)
                            {
                                if (!string.IsNullOrEmpty(item.DescricaoComponenteCurricularAssunto))
                                    listaComponente += $"<br/> {item.DescricaoCicloLetivo} - {item.DescricaoComponenteCurricular} - {item.DescricaoComponenteCurricularAssunto}";
                                else
                                    listaComponente += $"<br/> {item.DescricaoCicloLetivo} - {item.DescricaoComponenteCurricular}";
                            }
                            var msgPlanoAssert = string.Format(UIResource.MSG_Assert_ComponenteNaoAprovado_Confirmacao, listaComponente);
                            Assert(model, msgPlanoAssert);
                        }
                    }

                    // Se é Solicitação de depósito de dissertação/tese
                    else if (model.TokenServico == TOKEN_SERVICO.DEPOSITO_DISSERTACAO_TESE)
                    {
                        var dadosTipoTrabalhoAluno = TrabalhoAcademicoService.RecuperarDadosTipoTrabalhoAcademico(model.SeqPessoaAtuacao);
                        if (dadosTipoTrabalhoAluno.SeqDivisaoComponente.HasValue)
                        {
                            var validacao = RequisitoService.ValidarPreRequisitos(model.SeqPessoaAtuacao, new List<long> { dadosTipoTrabalhoAluno.SeqDivisaoComponente.Value }, null, null, null);
                            Assert(model, string.Format(SRC.Views.RealizarAtendimento.App_LocalResources.UIResource.MSG_Assert_Solicitacao_Documento_Dissertacao_Requisitos_Pendentes, dadosSolicitacao.NomeSolicitante, string.Join("<br />", validacao.MensagensErro)), () => !validacao.Valido);
                        }
                    }
                }
                /********************** FIM VALIDAÇÕES DE ASSERT **********************/

                // Realiza o atendimento da solicitação
                SolicitacaoServicoService.RealizarAtendimento(model.SeqSolicitacaoServico, model.Situacao, model.Parecer);

                // Retorna mensagem de sucesso
                string parecer = model.Situacao.GetValueOrDefault() ? "Deferimento" : "Indeferimento";
                string msg = string.Format(UIResource.MSG_RealizarAtendimento_Sucesso, parecer, dadosSolicitacao.Protocolo);
                SetSuccessMessage(msg, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);

            if (string.IsNullOrWhiteSpace(tokenRet))
                return SMCRedirectToAction("Index", "SolicitacaoServico");
            else
                return SMCRedirectToAction("EntrarEtapa", routeValues: new
                {
                    SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico),
                    SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa),
                    tokenRet = tokenRet
                });
        }

        /// <summary>
        /// Realiza assert para confirmar exclusão de situações futuras do aluno.
        /// </summary>
        /// <param name="model">Modelo</param>
        /// <param name="dataReferencia">Data de referencia para consulta de situações futuras</param>
        private void ValidarSituacoesFuturas(AtendimentoParecerViewModel model, DateTime dataReferencia)
        {
            // Busca as situações futuras do aluno
            var situacoesFuturasAluno = this.SolicitacaoServicoService.BuscarSituacoesFuturasAluno(model.SeqPessoaAtuacao, dataReferencia);
            if (situacoesFuturasAluno.Any())
            {
                // Armazena os itens futuros
                var retorno = string.Empty;
                foreach (var item in situacoesFuturasAluno)
                    retorno += string.Format(UIResource.MSG_Confirmacao_Situacao_Futura_Aluno_Item, item.DescricaoSituacaoFutura, item.DataInicio.ToShortDateString(), item.ProtocoloSolicitacaoOrigem, item.DescricaoProcesso);

                var msgAssert = string.Format(UIResource.MSG_Confirmacao_Situacao_Futura_Aluno, retorno);
                Assert(model, msgAssert, () =>
                {
                    return true;
                });
            }
        }

        private ActionResult AtendimentoReabertura(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_SRC_004_15_01.REALIZAR_ATENDIMENTO_PARECER_ATENDIMENTO_REABERTURA))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = this.SolicitacaoServicoService.BuscarDadosAtendimentoReabertura(filtro.SeqSolicitacaoServico).Transform<AtendimentoReaberturaViewModel>();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarAtendimentoReabertura), historico.Seq);

            // Completa o modelo
            model.TokenServico = filtro.TokenServico;

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("AtendimentoReabertura", model);
        }

        [HttpPost]
        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_15_01.REALIZAR_ATENDIMENTO_PARECER_ATENDIMENTO_REABERTURA)]
        public ActionResult SalvarAtendimentoReabertura(AtendimentoReaberturaViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                if (model.SeqGrupoEscalonamentoMatricula != null && model.SeqGrupoEscalonamentoMatricula.Seq > 0)
                {
                    if (!GrupoEscalonamentoService.ValidarDataFimEscalonamentoPorGrupoEscalonamento(model.SeqGrupoEscalonamentoMatricula.Seq))
                        throw new GrupoEscalonamentoExpiradoException();
                }

                SolicitacaoServicoService.SalvarDadosAtendimentoReabertura(model.Transform<AtendimentoReaberturaData>());
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        private ActionResult AtendimentoIntercambio(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_SRC_004_13_01.ATENDIMENTO_INTERCAMBIO))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = this.SolicitacaoServicoService.BuscarDadosIniciaisAtendimentoIntercambio(filtro.SeqSolicitacaoServico).Transform<AtendimentoIntercambioViewModel>();
            //var model = new AtendimentoIntercambioViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarAtendimentoIntercambio), historico.Seq);

            // Completa o modelo
            model.TokenServico = filtro.TokenServico;

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("AtendimentoIntercambio", model);
        }

        [SMCAuthorize(UC_SRC_004_13_01.ATENDIMENTO_INTERCAMBIO)]
        public ActionResult BuscarDadosTermoIntercambio(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqTipoTermoIntercambio)
        {
            if (seqTipoTermoIntercambio == 0)
                return Content("");

            var model = this.SolicitacaoServicoService.BuscarDadosTermoAtendimentoIntercambio(seqSolicitacaoServico, seqTipoTermoIntercambio).Transform<AtendimentoIntercambioViewModel>();
            return PartialView("_AtendimentoIntercambioDados", model);
        }

        [SMCAuthorize(UC_SRC_004_13_01.ATENDIMENTO_INTERCAMBIO)]
        public ActionResult BuscarDadosOrientacaoIntercambio(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqTipoOrientacao, SMCEncryptedLong seqTipoTermoIntercambio, SMCEncryptedLong seqTermoIntercambio)
        {
            if (seqTipoOrientacao == 0)
                return Content("");

            var model = this.SolicitacaoServicoService.BuscarDadosOrientacaoTermoAtendimentoIntercambio(seqSolicitacaoServico, seqTipoOrientacao, seqTipoTermoIntercambio, seqTermoIntercambio).Transform<AtendimentoIntercambioViewModel>();
            return PartialView("_AtendimentoIntercambioDadosOrientacao", model);
        }

        [SMCAuthorize(UC_SRC_004_13_01.ATENDIMENTO_INTERCAMBIO)]
        public JsonResult PreencherDatasTermo(long? seqTermoIntercambio, long seqPessoaAtuacao, DateTime? dataInicio, DateTime? dataFim)
        {
            if (!seqTermoIntercambio.HasValue)
            {
                return Json(new
                {
                    ExigePeriodo = false,
                    DataInicio = string.Empty,
                    DataFim = string.Empty,
                });
            }

            // Recupera os dados do Termo de Intercâmbio selecionado
            var dadosTermo = TermoIntercambioService.BuscarDadosTermoIntercambio(seqTermoIntercambio.Value);

            // Recupera os dados de InstituicaoNivelTipoVinculoAluno
            var dadosInstituicaoNivelAluno = InstituicaoNivelTipoVinculoAlunoService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao);

            // Recupera os dados de InstituicaoNivelTipoTermoIntercambio
            var dadosTipoVinculo = InstituicaoNivelTipoTermoIntercambioService.BuscarInstituicoesNivelTipoVinculoAluno(new InstituicaoNivelTipoTermoIntercambioFiltroData
            {
                SeqInstituicaoNivelTipoVinculoAluno = dadosInstituicaoNivelAluno.Seq,
                SeqTipoTermoIntercambio = dadosTermo.SeqTipoTermoIntercambio
            });

            // Caso não tenha nenhuma configuração para os parâmetros informados, exibe o erro
            if (dadosTipoVinculo == null)
                throw new SolicitacaoIntercambioConfiguracaoNaoEncontradaException();

            return Json(new
            {
                ExigePeriodo = dadosTipoVinculo.ExigePeriodoIntercambioTermo,
                DataInicio = dadosTipoVinculo.ExigePeriodoIntercambioTermo.GetValueOrDefault() ? dadosTermo.DataInicioTipoTermo : (dadosTermo.DataInicioTipoTermo ?? dataInicio),
                DataFim = dadosTipoVinculo.ExigePeriodoIntercambioTermo.GetValueOrDefault() ? dadosTermo.DataFimTipoTermo : (dadosTermo.DataFimTipoTermo ?? dataFim),
            });
        }

        [SMCAuthorize(UC_SRC_004_13_01.ATENDIMENTO_INTERCAMBIO)]
        public JsonResult PreencherDadosTermo(long? seqTermoIntercambio, long seqPessoaAtuacao)
        {
            if (!seqTermoIntercambio.HasValue)
            {
                return Json(new
                {
                    DescricaoTipoTermo = string.Empty,
                    DescricaoInstituicaoExterna = string.Empty,
                    SeqTipoOrientacao = new List<SMCDatasourceItem>(),
                    OrientacaoAluno = CadastroOrientacao.Nenhum,
                    ExisteTipoOrientacaoParametrizado = false
                });
            }

            // Recupera os dados do Termo de Intercâmbio selecionado
            var dadosTermo = TermoIntercambioService.BuscarDadosTermoIntercambio(seqTermoIntercambio.Value);

            // Recupera os dados de InstituicaoNivelTipoVinculoAluno
            var dadosInstituicaoNivelAluno = InstituicaoNivelTipoVinculoAlunoService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao);

            // Recupera os dados de InstituicaoNivelTipoTermoIntercambio
            var dadosTipoVinculo = InstituicaoNivelTipoTermoIntercambioService.BuscarInstituicoesNivelTipoVinculoAluno(new InstituicaoNivelTipoTermoIntercambioFiltroData
            {
                SeqInstituicaoNivelTipoVinculoAluno = dadosInstituicaoNivelAluno.Seq,
                SeqTipoTermoIntercambio = dadosTermo.SeqTipoTermoIntercambio
            });

            // Caso não tenha nenhuma configuração para os parâmetros informados, exibe o erro
            if (dadosTipoVinculo == null)
                throw new SolicitacaoIntercambioConfiguracaoNaoEncontradaException();

            // Recupera o DataSource para Tipos de Orientação
            var tiposOrientacao = InstituicaoNivelTipoOrientacaoService.BuscarTiposOrientacaoSelect(new InstituicaoNivelTipoOrientacaoFiltroData
            {
                SeqInstituicaoNivelTipoTermoIntercambio = dadosTipoVinculo.Seq,
                CadastroOrientacoesAluno = new CadastroOrientacao[] { CadastroOrientacao.Exige, CadastroOrientacao.Permite }
            });

            CadastroOrientacao cadastroOrientacao = CadastroOrientacao.Nenhum;
            if (tiposOrientacao?.FirstOrDefault()?.DataAttributes.Any(d => d.Key == "orientacao-aluno") ?? false)
                cadastroOrientacao = SMCEnumHelper.GetEnum<CadastroOrientacao>(tiposOrientacao.FirstOrDefault().DataAttributes.FirstOrDefault(d => d.Key == "orientacao-aluno").Value);

            return Json(new
            {
                DescricaoTipoTermo = dadosTermo.DescricaoTipoTermo,
                DescricaoInstituicaoExterna = dadosTermo.DescricaoInstituicaoExterna,
                SeqTipoOrientacao = tiposOrientacao,
                OrientacaoAluno = cadastroOrientacao,
                ExisteTipoOrientacaoParametrizado = tiposOrientacao.SMCAny()
            });
        }

        [SMCAuthorize(UC_SRC_004_13_01.ATENDIMENTO_INTERCAMBIO)]
        public JsonResult BuscarDadosTipoOrientacao(long seqTipoOrientacao, long seqPessoaAtuacao, long seqTermoIntercambio)
        {
            // Recupera os dados do Termo de Intercâmbio selecionado
            var dadosTermo = TermoIntercambioService.BuscarDadosTermoIntercambio(seqTermoIntercambio);

            // Recupera os dados de InstituicaoNivelTipoVinculoAluno
            var dadosInstituicaoNivelAluno = InstituicaoNivelTipoVinculoAlunoService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao);

            // Recupera os dados de InstituicaoNivelTipoTermoIntercambio
            var dadosTipoVinculo = InstituicaoNivelTipoTermoIntercambioService.BuscarInstituicoesNivelTipoVinculoAluno(new Academico.ServiceContract.Areas.ALN.Data.InstituicaoNivelTipoTermoIntercambioFiltroData
            {
                SeqInstituicaoNivelTipoVinculoAluno = dadosInstituicaoNivelAluno.Seq,
                SeqTipoTermoIntercambio = dadosTermo.SeqTipoTermoIntercambio
            });

            // Carrega os colaboradores
            var colaboradores = ColaboradorService.BuscarColaboradoresOrientacaoSelect(new ColaboradorFiltroData
            {
                TipoAtividade = TipoAtividadeColaborador.Orientacao,
                VinculoAtivo = true,
                SeqsAlunos = new long[] { seqPessoaAtuacao },
                SeqNivelEnsino = dadosTipoVinculo.SeqNivelEnsino
            });

            // Busca os tipos de participação configurados
            var tiposParticipacaoOrientacao = InstituicaoNivelTipoOrientacaoParticipacaoService.BuscarInstituicaoNivelTipoOrientacaoParticipacaoSelect(new InstituicaoNivelTipoOrientacaoParticipacaoFiltroData()
            {
                SeqNivelEnsino = dadosTipoVinculo.SeqNivelEnsino,
                SeqTipoVinculo = dadosInstituicaoNivelAluno.SeqTipoVinculoAluno,
                SeqTipoOrientacao = seqTipoOrientacao,
                SeqTermoIntercambio = seqTermoIntercambio,
            }).TransformList<SMCSelectListItem>();

            return Json(new
            {
                SeqColaborador = colaboradores,
                TipoParticipacaoOrientacao = tiposParticipacaoOrientacao
            });
        }

        [SMCAuthorize(UC_SRC_004_13_01.ATENDIMENTO_INTERCAMBIO)]
        public JsonResult BuscarDadosColaborador(long seqColaborador, long seqTermoIntercambio, long seqTipoOrientacao, long seqPessoaAtuacao, TipoParticipacaoOrientacao tipoParticipacaoOrientacao)
        {
            // Recupera os dados do Termo de Intercâmbio selecionado
            var dadosTermo = TermoIntercambioService.BuscarDadosTermoIntercambio(seqTermoIntercambio);

            // Recupera os dados de InstituicaoNivelTipoVinculoAluno
            var dadosInstituicaoNivelAluno = InstituicaoNivelTipoVinculoAlunoService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(seqPessoaAtuacao);

            // Recupera os dados de InstituicaoNivelTipoTermoIntercambio
            var dadosTipoVinculo = InstituicaoNivelTipoTermoIntercambioService.BuscarInstituicoesNivelTipoVinculoAluno(new InstituicaoNivelTipoTermoIntercambioFiltroData
            {
                SeqInstituicaoNivelTipoVinculoAluno = dadosInstituicaoNivelAluno.Seq,
                SeqTipoTermoIntercambio = dadosTermo.SeqTipoTermoIntercambio
            });

            var filtros = new InstituicaoExternaFiltroData()
            {
                SeqColaborador = seqColaborador,
                Ativo = true,
                SeqTermoIntercambio = seqTermoIntercambio,
                SeqInstituicaoEnsino = this.GetInstituicaoEnsinoLogada().Seq,
                TipoParticipacaoOrientacao = tipoParticipacaoOrientacao,
                SeqTipoOrientacao = seqTipoOrientacao,
                SeqNivelEnsino = dadosTipoVinculo.SeqNivelEnsino,
                SeqTipoVinculoAluno = dadosInstituicaoNivelAluno.SeqTipoVinculoAluno
            };
            return Json(InstituicaoExternaService.BuscarInstituicaoExternaPorColaboradorSelect(filtros).TransformList<SMCSelectListItem>());
        }

        [HttpPost]
        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_13_01.ATENDIMENTO_INTERCAMBIO)]
        public ActionResult SalvarAtendimentoIntercambio(AtendimentoIntercambioViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                SolicitacaoServicoService.SalvarDadosAtendimentoIntercambio(model.Transform<AtendimentoIntercambioData>());
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        private ActionResult AtendimentoDispensaAgrupamento(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_SRC_004_17_01.PESQUISAR_AGRUPAMENTO_ITENS_DISPENSA))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = SolicitacaoDispensaService.BuscarDadosAgrupamentoAtendimentoDispensa(filtro.SeqSolicitacaoServico).Transform<AtendimentoDispensaAgrupamentoViewModel>();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarAtendimentoDispensaAgrupamento), historico.Seq);

            // Aplica o sequencial da configuração nos grupos
            model.Grupos?.ForEach(g =>
            {
                g.SeqConfiguracaoEtapa = model.SeqConfiguracaoEtapa;
            });

            // Completa o modelo
            model.TokenServico = filtro.TokenServico;

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("AtendimentoDispensaAgrupamento", model);
        }

        [SMCAuthorize(UC_SRC_004_17_02.MANTER_AGRUPAMENTO_ITENS_DISPENSA)]
        public ActionResult AtendimentoDispensaAgrupamentoRemoverGrupo(SMCEncryptedLong seqGrupo, SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa)
        {
            SolicitacaoDispensaService.RemoverGrupoAgrupamentoAtendimentoDispensa(seqGrupo);
            return SMCRedirectToAction("EntrarEtapa", "RealizarAtendimento", new { seqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) });
        }

        [SMCAuthorize(UC_SRC_004_17_02.MANTER_AGRUPAMENTO_ITENS_DISPENSA)]
        public ActionResult AtendimentoDispensaAgrupamentoIncluirGrupo(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa)
        {
            var model = new SolicitacaoDispensaGrupoViewModel();
            model.SeqSolicitacaoDispensa = seqSolicitacaoServico;
            model.SeqConfiguracaoEtapa = seqConfiguracaoEtapa;
            model.ModoExibicaoHistoricoEscolar = ModoExibicaoHistoricoEscolar.AproveitamentoCreditos;

            var itens = SolicitacaoDispensaService.BuscarItensAgrupamentoAtendimentoDispensa(seqSolicitacaoServico, null);
            model.ItensDestinos = itens.ItensDestinos;
            model.ItensOrigensExternas = itens.ItensOrigensExternas;
            model.ItensOrigensInternas = itens.ItensOrigensInternas;

            return PartialView("AtendimentoDispensaAgrupamentoIncluirGrupo", model);
        }

        [SMCAuthorize(UC_SRC_004_17_02.MANTER_AGRUPAMENTO_ITENS_DISPENSA)]
        public ActionResult AtendimentoDispensaAgrupamentoEditarGrupo(SMCEncryptedLong seqGrupo, SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa)
        {
            var model = SolicitacaoDispensaService.BuscarDadosGrupoAgrupamentoAtendimentoDispensa(seqGrupo).Transform<SolicitacaoDispensaGrupoViewModel>();
            model.SeqConfiguracaoEtapa = seqConfiguracaoEtapa;

            var itens = SolicitacaoDispensaService.BuscarItensAgrupamentoAtendimentoDispensa(seqSolicitacaoServico, seqGrupo);
            model.ItensDestinos = itens.ItensDestinos;
            model.ItensOrigensExternas = itens.ItensOrigensExternas;
            model.ItensOrigensInternas = itens.ItensOrigensInternas;

            return PartialView("AtendimentoDispensaAgrupamentoEditarGrupo", model);
        }

        [SMCAuthorize(UC_SRC_004_17_02.MANTER_AGRUPAMENTO_ITENS_DISPENSA)]
        public ActionResult SalvarAtendimentoDispensaAgrupamentoGrupo(SolicitacaoDispensaGrupoViewModel model)
        {
            SolicitacaoDispensaService.SalvarAtendimentoDispensaAgrupamentoGrupo(model.Transform<SolicitacaoDispensaGrupoData>());
            return SMCRedirectToAction("EntrarEtapa", "RealizarAtendimento", new { seqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoDispensa), seqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa) });
        }

        [HttpPost]
        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_17_02.MANTER_AGRUPAMENTO_ITENS_DISPENSA)]
        public ActionResult SalvarAtendimentoDispensaAgrupamento(AtendimentoDispensaAgrupamentoViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                SolicitacaoDispensaService.SalvarAtendimentoDispensaAgrupamento(model.SeqSolicitacaoServico);
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", "RealizarAtendimento", new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        private ActionResult AtendimentoEmissaoDocumentoConclusao(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_SRC_004_18_01.REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ANALISE_EMISSAO))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = SolicitacaoDocumentoConclusaoService.BuscarDadosSolicitacaoDocumentoConclusao(filtro.SeqSolicitacaoServico).Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoViewModel>();
            model.SeqSolicitacaoServicoAuxiliar = filtro.SeqSolicitacaoServico;
            model.SeqConfiguracaoEtapaAuxiliar = filtro.SeqConfiguracaoEtapa;
            model.TiposDocumento = SolicitacaoDocumentoConclusaoService.BuscarTiposDocumentoRequeridoPorEtapaSelect(filtro.SeqSolicitacaoServico);
            model.TiposHistoricoEscolar = SolicitacaoDocumentoConclusaoService.BuscarTiposHistoricoEscolarSelect();
            model.DescricaoCursoDocumentoAuxiliar = model.DescricaoCursoDocumento;

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarAtendimentoEmissaoDocumentoConclusao), historico.Seq);

            // Completa o modelo
            model.TokenServico = filtro.TokenServico;

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("AtendimentoEmissaoDocumentoConclusao", model);
        }

        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_18_01.REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ANALISE_EMISSAO)]
        public ActionResult SincronizarDadosAluno(SMCEncryptedLong codigoAlunoMigracao, SMCEncryptedLong seqPessoaAtuacao, SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, string tokenRet)
        {
            try
            {
                if (!(codigoAlunoMigracao > 0))
                    throw new SolicitacaoDocumentoConclusaoCodigoMigracaoNaoEncontradoException();

                var seqPessoaAtuacaoRetorno = AlunoService.SincronizarDadosAluno(codigoAlunoMigracao.Value, seqPessoaAtuacao);

                if (seqPessoaAtuacaoRetorno != seqPessoaAtuacao)
                    throw new SolicitacaoDocumentoConclusaoCodigoMigracaoDuplicadoException();

                SetSuccessMessage(UIResource.MensagemSucessoSincronismoDadosAluno, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message);
            }

            return SMCRedirectToAction("EntrarEtapa", routeValues: new
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                SeqConfiguracaoEtapa = seqConfiguracaoEtapa,
                //tokenRet = tokenRet
            });
        }

        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_18_01.REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ANALISE_EMISSAO)]
        public ActionResult BuscarBotoesModaisCampoTipoIdentidadeSelecionado(TipoIdentidade? tipoIdentidade, SMCEncryptedLong seqSolicitacaoServicoAuxiliar, bool exibirComandoDocumentacaoAcademica, bool exibirComandoRVDD, SMCEncryptedLong seqCursoOfertaLocalidade, SMCEncryptedLong seqGrauAcademicoSelecionado, DateTime? dataConclusao)
        {
            var modeloCamposModais = new SolicitacaoAnaliseEmissaoDocumentoConclusaoCamposModaisViewModel()
            {
                SeqSolicitacaoServico = seqSolicitacaoServicoAuxiliar,
                ExibirComandoDocumentacaoAcademica = exibirComandoDocumentacaoAcademica,
                ExibirComandoRVDD = exibirComandoRVDD,
                TipoIdentidade = tipoIdentidade,
                SeqCursoOfertaLocalidade = seqCursoOfertaLocalidade,
                SeqGrauAcademico = seqGrauAcademicoSelecionado,
                DataConclusao = dataConclusao
            };

            return PartialView("_BotoesModaisSecaoFormacao", modeloCamposModais);
        }

        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_18_01.REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ANALISE_EMISSAO)]
        public ActionResult ConsultarDocumentacaoAcademica(SMCEncryptedLong seqSolicitacaoServico)
        {
            var model = SolicitacaoDocumentoConclusaoService.BuscarDadosConsultaHistorico(seqSolicitacaoServico).Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoViewModel>();

            return PartialView("_DocumentacaoAcademica", model);
        }

        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_18_01.REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ANALISE_EMISSAO)]
        public ActionResult ListarHistoricoEscolar(SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoViewModel filtro)
        {
            var model = SolicitacaoDocumentoConclusaoService.BuscarHistoricoEscolar(filtro.Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoData>());
            var retorno = new SMCPagerModel<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoListarViewModel>(model);

            return PartialView("_ListaHistoricoEscolar", retorno);
        }

        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_18_01.REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ANALISE_EMISSAO)]
        public ActionResult ListarHistoricoEscolarAtividadeComplementar(SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoViewModel filtro)
        {
            var model = SolicitacaoDocumentoConclusaoService.BuscarHistoricoEscolarAtividadeComplementar(filtro.Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoData>());
            var retorno = new SMCPagerModel<SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarAtividadeComplementarListarViewModel>(model);

            return PartialView("_ListarHistoricoEscolarAtividadeComplementar", retorno);
        }

        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_18_01.REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ANALISE_EMISSAO)]
        public ActionResult ConsultarInformacoesRVDD(SMCEncryptedLong seqSolicitacaoServico, TipoIdentidade? tipoIdentidade)
        {
            var model = SolicitacaoDocumentoConclusaoService.ConsultarInformacoesRVDD(seqSolicitacaoServico, tipoIdentidade);

            return PartialView("_InformacoesRVDD", model);
        }

        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_18_01.REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ANALISE_EMISSAO)]
        public ActionResult ConsultarAtosNormativosCurso(SMCEncryptedLong seqCursoOfertaLocalidade, SMCEncryptedLong seqGrauAcademico, string dataConclusao)
        {
            DateTime? dataConclusaoFormatada = !string.IsNullOrEmpty(dataConclusao) ? Convert.ToDateTime(dataConclusao) : (DateTime?)null;

            var model = SolicitacaoDocumentoConclusaoService.BuscarAtosNormativosCurso(seqCursoOfertaLocalidade, seqGrauAcademico, dataConclusaoFormatada).Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoAtosCursoViewModel>();

            return PartialView("_AtosNormativosCurso", model);
        }

        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_18_01.REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ANALISE_EMISSAO)]
        public ActionResult ListarMensagens(SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemFiltroViewModel filtro)
        {
            var result = this.SolicitacaoDocumentoConclusaoService.BuscarMensagens(filtro.Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemFiltroData>());

            if (filtro.DocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
                result = result.Where(w => w.DocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL).ToList();
            else
                result = result.Where(w => w.DocumentoAcademico != TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL).ToList();

            var model = new SMCPagerModel<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarViewModel>(result.TransformList<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarViewModel>());

            return PartialView("_MensagensEmissaoDocumento", model);
        }

        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_18_01.REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ANALISE_EMISSAO)]
        public ActionResult BuscarMensagensCampoReutilizarSelecionado(bool? reutilizarDados, SMCEncryptedLong seqSolicitacaoServicoAuxiliar, bool existeDocumentoConclusao, string tokenTipoDocumentoAcademico, long? seqTipoDocumentoSolicitado, int? numeroVia, SMCEncryptedLong seqPessoaAtuacaoAuxiliar, SMCEncryptedLong seqInstituicaoEnsino, SMCEncryptedLong seqInstituicaoNivel, string nomePais, string descricaoViaAnterior, string descricaoViaAtual, int? codigoUnidadeSeo, string descricaoGrauAcademico)
        {
            var modeloFiltro = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemFiltroViewModel()
            {
                SeqSolicitacaoServico = seqSolicitacaoServicoAuxiliar,
                ExisteDocumentoConclusao = existeDocumentoConclusao,
                TokenTipoDocumentoAcademico = tokenTipoDocumentoAcademico,
                SeqTipoDocumentoSolicitado = seqTipoDocumentoSolicitado,
                NumeroVia = numeroVia,
                SeqPessoaAtuacao = seqPessoaAtuacaoAuxiliar,
                SeqInstituicaoEnsino = seqInstituicaoEnsino,
                SeqInstituicaoNivel = seqInstituicaoNivel,
                ReutilizarDados = reutilizarDados,
                NomePais = nomePais,
                DescricaoViaAnterior = descricaoViaAnterior,
                DescricaoViaAtual = descricaoViaAtual,
                CodigoUnidadeSeo = codigoUnidadeSeo,
                DescricaoGrauAcademico = descricaoGrauAcademico
            };

            return RenderAction("ListarMensagens", modeloFiltro);
        }

        [SMCAllowAnonymous]
        public ActionResult PreencherDescricaoMensagem(long seqTipoMensagem, long seqInstituicaoEnsino, long seqInstituicaoNivel)
        {
            var result = this.InstituicaoNivelTipoMensagemService.BuscarInstituicaoNivelTipoMensagemSemRefinar(new InstituicaoNivelTipoMensagemFiltroData()
            {
                SeqTipoMensagem = seqTipoMensagem,
                SeqInstituicaoEnsino = seqInstituicaoEnsino,
                SeqInstituicaoNivel = seqInstituicaoNivel
            });

            return Json(result != null ? result.MensagemPadrao : string.Empty);
        }

        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_18_01.REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ANALISE_EMISSAO)]
        public ActionResult IncluirMensagem(long seqSolicitacaoServico, bool existeDocumentoConclusao, string tokenTipoDocumentoAcademico, long? seqTipoDocumentoSolicitado, int? numeroVia, long seqPessoaAtuacao, long seqInstituicaoEnsino, long seqInstituicaoNivel, bool? reutilizarDados, string nomePais, string descricaoViaAnterior, string descricaoViaAtual, int? codigoUnidadeSeo, string descricaoGrauAcademico, string documentoAcademico)
        {
            var primeiraVia = numeroVia.HasValue && numeroVia == 1;
            var modelo = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemViewModel()
            {
                TiposMensagem = this.SolicitacaoDocumentoConclusaoService.BuscarTiposMensagemPorInstituicaoNivel(tokenTipoDocumentoAcademico, seqTipoDocumentoSolicitado, seqInstituicaoEnsino, seqInstituicaoNivel, primeiraVia, documentoAcademico),
                DataInicioVigencia = DateTime.Now,
                SeqSolicitacaoServico = seqSolicitacaoServico,
                ExisteDocumentoConclusao = existeDocumentoConclusao,
                TokenTipoDocumentoAcademico = tokenTipoDocumentoAcademico,
                SeqTipoDocumentoSolicitado = seqTipoDocumentoSolicitado,
                NumeroVia = numeroVia,
                SeqPessoaAtuacao = seqPessoaAtuacao,
                SeqInstituicaoEnsino = seqInstituicaoEnsino,
                SeqInstituicaoNivel = seqInstituicaoNivel,
                ReutilizarDados = reutilizarDados,
                NomePais = nomePais,
                DescricaoViaAnterior = descricaoViaAnterior,
                DescricaoViaAtual = descricaoViaAtual,
                CodigoUnidadeSeo = codigoUnidadeSeo,
                DescricaoGrauAcademico = descricaoGrauAcademico,
                DocumentoAcademico = documentoAcademico
            };

            return PartialView("_DadosMensagem", modelo);
        }

        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_18_01.REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ANALISE_EMISSAO)]
        public ActionResult EditarMensagem(long seq, string tokenTipoDocumentoAcademico, long? seqTipoDocumentoSolicitado, long seqPessoaAtuacao, long seqInstituicaoEnsino, long seqInstituicaoNivel, long seqSolicitacaoServico, int? numeroVia, string documentoAcademico)
        {
            var modelo = this.SolicitacaoDocumentoConclusaoService.BuscarMensagem(seq, seqPessoaAtuacao, seqSolicitacaoServico).Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemViewModel>();

            var primeiraVia = numeroVia.HasValue && numeroVia == 1;
            modelo.TiposMensagem = this.SolicitacaoDocumentoConclusaoService.BuscarTiposMensagemPorInstituicaoNivel(tokenTipoDocumentoAcademico, seqTipoDocumentoSolicitado, seqInstituicaoEnsino, seqInstituicaoNivel, primeiraVia, documentoAcademico);

            return PartialView("_DadosMensagem", modelo);
        }

        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_18_01.REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ANALISE_EMISSAO)]
        public ActionResult SalvarMensagem(SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemViewModel modelo)
        {
            this.SolicitacaoDocumentoConclusaoService.SalvarMensagem(modelo.Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemData>());

            var modeloFiltro = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemFiltroViewModel()
            {
                SeqSolicitacaoServico = modelo.SeqSolicitacaoServico,
                ExisteDocumentoConclusao = modelo.ExisteDocumentoConclusao,
                TokenTipoDocumentoAcademico = modelo.TokenTipoDocumentoAcademico,
                SeqTipoDocumentoSolicitado = modelo.SeqTipoDocumentoSolicitado,
                NumeroVia = modelo.NumeroVia,
                SeqPessoaAtuacao = modelo.SeqPessoaAtuacao,
                SeqInstituicaoEnsino = modelo.SeqInstituicaoEnsino,
                SeqInstituicaoNivel = modelo.SeqInstituicaoNivel,
                ReutilizarDados = modelo.ReutilizarDados,
                NomePais = modelo.NomePais,
                DescricaoViaAnterior = modelo.DescricaoViaAnterior,
                DescricaoViaAtual = modelo.DescricaoViaAtual,
                CodigoUnidadeSeo = modelo.CodigoUnidadeSeo,
                DescricaoGrauAcademico = modelo.DescricaoGrauAcademico,
                DocumentoAcademico = modelo.DocumentoAcademico
            };

            return RenderAction("ListarMensagens", modeloFiltro);
        }

        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_18_01.REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ANALISE_EMISSAO)]
        public ActionResult ExcluirMensagem(long seq, long seqSolicitacaoServico, bool existeDocumentoConclusao, string tokenTipoDocumentoAcademico, long? seqTipoDocumentoSolicitado, int? numeroVia, long seqPessoaAtuacao, long seqInstituicaoEnsino, long seqInstituicaoNivel, bool? reutilizarDados, string nomePais, string descricaoViaAnterior, string descricaoViaAtual, int? codigoUnidadeSeo, string descricaoGrauAcademico, string documentoAcademico)
        {
            this.SolicitacaoDocumentoConclusaoService.ExcluirMensagem(seq, seqPessoaAtuacao, seqSolicitacaoServico);

            var modeloFiltro = new SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemFiltroViewModel()
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                ExisteDocumentoConclusao = existeDocumentoConclusao,
                TokenTipoDocumentoAcademico = tokenTipoDocumentoAcademico,
                SeqTipoDocumentoSolicitado = seqTipoDocumentoSolicitado,
                NumeroVia = numeroVia,
                SeqPessoaAtuacao = seqPessoaAtuacao,
                SeqInstituicaoEnsino = seqInstituicaoEnsino,
                SeqInstituicaoNivel = seqInstituicaoNivel,
                ReutilizarDados = reutilizarDados,
                NomePais = nomePais,
                DescricaoViaAnterior = descricaoViaAnterior,
                DescricaoViaAtual = descricaoViaAtual,
                CodigoUnidadeSeo = codigoUnidadeSeo,
                DescricaoGrauAcademico = descricaoGrauAcademico,
                DocumentoAcademico = documentoAcademico
            };

            return RenderAction("ListarMensagens", modeloFiltro);
        }

        [HttpPost]
        [SMCAllowAnonymous]
        [SMCAuthorize(UC_SRC_004_18_01.REALIZAR_ATENDIMENTO_DOCTO_CONCLUSAO_ANALISE_EMISSAO)]
        public ActionResult SalvarAtendimentoEmissaoDocumentoConclusao(SolicitacaoAnaliseEmissaoDocumentoConclusaoViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                var keySessionMensagemEmissao = string.Format(KEY_SESSION_MENSAGEM.KEY_SESSION_MENSAGEM_EMISSAO, (long)model.SeqSolicitacaoServico);
                var listaMensagensSession = System.Web.HttpContext.Current.Session[keySessionMensagemEmissao] as List<SolicitacaoAnaliseEmissaoDocumentoConclusaoMensagemListarVO>;

                var semMensagem = listaMensagensSession != null && listaMensagensSession.Any(w => string.IsNullOrWhiteSpace(w.Descricao));

                if (semMensagem)
                    throw new EmissaoDocumentoConclusaoMensagemVaziaException();

                if (model.EmissaoPermitida)
                {
                    /*  
                       Se não houver documento de conclusão associado a solicitação de serviço, exibir a seguinte mensagem de confirmação: 
                       "Confirma a emissão do documento de conclusão? Ao confirmar será gerado o cadastro do documento de conclusão".
                       · Ao confirmar executar os seguintes passos, conforme RN_CNC_040 - Documento Conclusão - Emissão
                       · Senão, retornar para a página de Emissão.

                       Senão, exibir a seguinte mensagem de confirmação: 
                       "Confirma a atualização do documento de conclusão? Ao confirmar o cadastro do documento será atualizado.".
                       · Ao confirmar executar os seguintes passos, conforme RN_CNC_041 - Documento Conclusão - Atualização
                       · Senão, retornar para a página de Emissão.
                    */

                    Assert(model, "MSG_Assert_DocumentoConclusao_Novo", () => !model.ExisteDocumentoConclusao);
                    Assert(model, "MSG_Assert_DocumentoConclusao_Atualizar", () => model.ExisteDocumentoConclusao);
                }
                else
                {
                    Assert(model, string.Format(UIResource.MSG_Assert_DocumentoConclusao_Cancelar, model.MotivoEmissaoNaoPermitida, model.ObservacoesEmissaoNaoPermitida), () => !model.EmissaoPermitida);
                }

                //if (model.NumeroVia > 1 && (model.TokenTipoDocumentoAcademicoAnterior == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_FINAL || model.TokenTipoDocumentoAcademicoAnterior == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_2VIA))
                //    Assert(model, string.Format(UIResource.MSG_Assert_Confirma_Emissao_Historico, model.DescricaoTipoDocumentoSolicitado, model.DescricaoTipoDocumentoAcademicoAnterior), () => true);

                if ((model.TokenTipoDocumentoAcademicoAnterior == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_FINAL || model.TokenTipoDocumentoAcademicoAnterior == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_2VIA))
                {
                    // Verifica a CH do histórico e dispara Assert se necessário (TSK 55788)
                    var dadosCH = SolicitacaoDocumentoConclusaoService.RetornarDadosCargaHorariaHistorico(model.CodigoAlunoMigracao.Value, model.NumeroVia);
                    if (dadosCH.CargaHorariaCursoIntegralizada > 0 && dadosCH.CargaHorariaCurso > 0 && dadosCH.CargaHorariaCursoIntegralizada < dadosCH.CargaHorariaCurso)
                        Assert(model, UIResource.MSG_Assert_Confirma_CHIntegralizadaMenorCHCurriculo, () => true);
                }

                if (model.NumeroVia == 1 && (model.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_FINAL || model.TokenTipoDocumentoAcademico == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_2VIA))
                {
                    var texto = DocumentoConclusaoService.BuscarDadosPessoaisDocumentoConclusao(model.SeqPessoaAtuacao, model.DescricaoCursoDocumentoAuxiliar, model.SeqGrauAcademicoSelecionado, model.SeqTitulacao, model.SeqPessoaDadosPessoais, model.TipoIdentidade);
                    if (!string.IsNullOrEmpty(texto))
                        Assert(model, string.Format(UIResource.MSG_Assert_DadosPessoais, texto), () => true);
                }

                SolicitacaoDocumentoConclusaoService.SalvarAtendimentoEmissaoDocumentoConclusao(model.Transform<SolicitacaoAnaliseEmissaoDocumentoConclusaoData>());

                SetSuccessMessage(UIResource.MSG_RealizarAtendimento_Sucesso_ConfirmacaoDocumento, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);

            if (string.IsNullOrWhiteSpace(tokenRet))
                return SMCRedirectToAction("Index", "SolicitacaoServico");
            else
                return SMCRedirectToAction("EntrarEtapa", routeValues: new
                {
                    SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico),
                    SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa),
                    tokenRet = tokenRet
                });
        }

        [SMCAllowAnonymous]
        public JsonResult BuscarDadosOrgaoDeRegistroDocumentoConclusao(TipoRegistroDocumento tipoRegistroDocumento, bool registrado)
        {
            var ret = new List<SMCDatasourceItem>();
            // TODO: Carol - Verificar como fica essa questão com o órgão de registro sendo um cadastro básico e não um dominio
            /*
            ret.Add(new SMCDatasourceItem
            {
                Descricao = SMCEnumHelper.GetDescription(OrgaoDeRegistro.PUCMG),
                Seq = (long)OrgaoDeRegistro.PUCMG,
                Selected = true
            });

            ret.Add(new SMCDatasourceItem
            {
                Descricao = SMCEnumHelper.GetDescription(OrgaoDeRegistro.UFJF),
                Seq = (long)OrgaoDeRegistro.UFJF,
            });

            ret.Add(new SMCDatasourceItem
            {
                Descricao = SMCEnumHelper.GetDescription(OrgaoDeRegistro.UFMG),
                Seq = (long)OrgaoDeRegistro.UFMG,
            });
            */
            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        private ActionResult RegistrarEntregaDocumentacaoAtendimento(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_SRC_004_29_01.REALIZAR_ATENDIMENTO_ENTREGA_DOCUMENTOS))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new RegistroDocumentosAtendimentoViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarRegistrarEntregaDocumentacaoAtendimento), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });
            model.SeqProcesso = filtro.SeqProcesso;

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            var etapasConfiguracao = BuscarEtapas(model.SeqSolicitacaoServico);

            var registroDocumentoAtendimento = RegistroDocumentoService.BuscarDocumentosRegistro(filtro.SeqSolicitacaoServico, atendimento: true);

            if (registroDocumentoAtendimento != null)
            {
                model.SeqSolicitacaoServico = filtro.SeqSolicitacaoServico;
                model.SeqPessoaAtuacao = filtro.SeqPessoaAtuacao;
                model.Documentos = registroDocumentoAtendimento.TransformList<RegistroDocumentosAtendimentoDocumentoViewModel>();
                model.backUrl = Request.UrlReferrer.ToString();
                TempData["TokenServico"] = filtro.TokenServico;

                model.Documentos.ForEach(d =>
                {
                    d.Documentos.ForEach(dc =>
                    {
                        if (dc.ArquivoAnexado != null)
                        {
                            if (System.Web.HttpContext.Current.Request.Url.Host == "localhost")
                                dc.ArquivoAnexadoDownload = $"<a href='/Dev/{SMCContext.ApplicationId}/Home/DownloadFileGuid?GuidFile={dc.ArquivoAnexado.GuidFile.ToString()}' target='_blank'><i></i>Anexo</a>";

                            else
                                dc.ArquivoAnexadoDownload = $"<a href='/{SMCContext.ApplicationId}/Home/DownloadFileGuid?GuidFile={dc.ArquivoAnexado.GuidFile.ToString()}' target='_blank'><i></i>Anexo</a>";
                        }
                        dc.DataMinimaEntrega = DateTime.Now.AddDays(1);
                        dc.DataMaximaEntrega = DateTime.MaxValue;
                        dc.Token = filtro.TokenServico;
                    });
                });

            }

            return View("RegistrarEntregaDocumentosAtendimento", model);
        }

        [HttpPost]
        [SMCAuthorize(UC_SRC_004_29_01.REALIZAR_ATENDIMENTO_ENTREGA_DOCUMENTOS)]
        public ActionResult SalvarRegistrarEntregaDocumentacaoAtendimento(RegistroDocumentosAtendimentoViewModel model, string tokenRet)
        {
            var tokenServico = TempData.Peek("TokenServico") as string;
            if (tokenServico == TOKEN_SERVICO.ENTREGA_DOCUMENTACAO)
                if (model.Documentos.Any(x => x.Documentos.Any(d => d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega || d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoValidacao)))
                    throw new RegistroDocumentoAtendimentoValidacaoException();

            model.DocumentosAlterados = new List<string>();

            //Valida se tem algum documento entregue anteriormente que esteja deferido ou aguardando validação do setor responsável
            //preenchendo uma lista de documentos alterados com a descrição do tipo documento. Será usado em uma mensagem de validação
            //ao confirmar a finalização da solicitação. (RN_SRC_134 - Finalização solicitação entrega documento - item 1)
            model.Documentos.ForEach(d => d.Documentos.ForEach(dc =>
            {
                if (dc.EntregueAnteriormente && (dc.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido || dc.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel))
                {
                    var tipoDocumento = ITipoDocumentoService.BuscarDescricaoTipoDocumento(dc.SeqTipoDocumento);

                    model.NecessitaConfirmacaoEntregaDocumentos = true;

                    if (tipoDocumento != null)
                        model.DocumentosAlterados.Add(tipoDocumento);
                }

            }));

            if (model.NecessitaConfirmacaoEntregaDocumentos)
            {
                if (tokenServico == TOKEN_SERVICO.ENTREGA_DOCUMENTACAO)
                    Assert(model, string.Format(UIResource.Confirmacao_Entrega_Documentacao_Atendimento, string.Join("<br />", model.DocumentosAlterados)));

                if (tokenServico == TOKEN_SERVICO.ATUALIZACAO_DOCUMENTACAO_EMISSAO_DIPLOMA)
                    Assert(model, UIResource.Confirmacao_Atualizacao_Documento_Emissao_Diploma);
            }

            if (tokenServico == TOKEN_SERVICO.ATUALIZACAO_DOCUMENTACAO_EMISSAO_DIPLOMA)
            {
                if (model.Documentos.Any(x => x.Documentos.Any(d => d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoValidacao)))
                    throw new RegistroDocumentoAtendimentoValidacaoDiplomaException();
            }

            RegistroDocumentoService.SalvarRegistrarEntregaDocumentacaoAtendimento(model.Transform<RegistroDocumentoAtendimentoData>(), model.SeqSolicitacaoServico);
            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);

            return SMCRedirectToAction("Index", "SolicitacaoServico", new { area = "SRC" });
        }

        [SMCAuthorize(UC_SRC_004_29_01.REALIZAR_ATENDIMENTO_ENTREGA_DOCUMENTOS)]
        public ActionResult PreencherDataEntrega(SituacaoEntregaDocumento situacaoEntregaDocumento, DateTime? dataEntrega, SMCUploadFile arquivoAnexado)
        {
            if (situacaoEntregaDocumento != SituacaoEntregaDocumento.AguardandoEntrega && situacaoEntregaDocumento != SituacaoEntregaDocumento.Pendente)
            {
                if (dataEntrega == null || !dataEntrega.HasValue)
                    dataEntrega = DateTime.Now.Date;
            }
            else if (situacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega || situacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente)
            {
                    dataEntrega = null;
            }

            return Content(dataEntrega.HasValue ? dataEntrega.Value.ToString("MM/dd/yyyy") : string.Empty);
        }

        [SMCAuthorize(UC_SRC_004_29_01.REALIZAR_ATENDIMENTO_ENTREGA_DOCUMENTOS)]
        public ActionResult PreencherVersaoDocumento(SituacaoEntregaDocumento situacaoEntregaDocumento, VersaoDocumento? versaoDocumento, VersaoDocumento versaoExigida)
        {
            var listaVersaoDocumento = new List<SMCSelectListItem>();

            foreach (var item in SMCEnumHelper.GenerateKeyValuePair<VersaoDocumento>())
            {
                listaVersaoDocumento.Add(new SMCSelectListItem() { Text = item.Value, Value = ((int)item.Key).ToString() });
            }

            if (situacaoEntregaDocumento != SituacaoEntregaDocumento.AguardandoEntrega && situacaoEntregaDocumento != SituacaoEntregaDocumento.Pendente)
            {
                if ((versaoDocumento == null || versaoDocumento == VersaoDocumento.Nenhum) && versaoExigida != VersaoDocumento.Nenhum)
                {
                    var item = listaVersaoDocumento.FirstOrDefault(l => l.Value == ((int)versaoExigida).ToString());
                    if (item != null)
                        item.Selected = true;
                }
                else
                {
                    var item = listaVersaoDocumento.FirstOrDefault(l => l.Value == ((int)(versaoDocumento ?? VersaoDocumento.Nenhum)).ToString());
                    if (item != null)
                        item.Selected = true;
                }
            }

            //Quando o campo "Validação" for preenchido com o valor "Aguardando entrega", e o campo "Versão" deve ficar sem preenchimento e desabilitado.
            if (situacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega)
                listaVersaoDocumento = null;

            return Json(listaVersaoDocumento);
        }

        [SMCAuthorize(UC_SRC_004_29_01.REALIZAR_ATENDIMENTO_ENTREGA_DOCUMENTOS)]
        public ActionResult PreencherFormaEntregaDocumento(SituacaoEntregaDocumento situacaoEntregaDocumento, FormaEntregaDocumento? formaEntregaDocumento, SMCUploadFile arquivoAnexado)
        {
            var listaFormaEntregaDocumento = new List<SMCSelectListItem>();

            foreach (var item in SMCEnumHelper.GenerateKeyValuePair<FormaEntregaDocumento>())
            {
                listaFormaEntregaDocumento.Add(new SMCSelectListItem() { Text = item.Value, Value = ((int)item.Key).ToString() });
            }

            if (situacaoEntregaDocumento != SituacaoEntregaDocumento.AguardandoEntrega && situacaoEntregaDocumento != SituacaoEntregaDocumento.Pendente)
            {
                if (formaEntregaDocumento != null)
                    listaFormaEntregaDocumento.FirstOrDefault(l => l.Value == ((int)formaEntregaDocumento).ToString()).Selected = true;
            }
            else if (situacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega || situacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente)
            {
                if (arquivoAnexado != null)
                {
                    if (formaEntregaDocumento != null)
                        listaFormaEntregaDocumento = null;
                }
            }

            return Json(listaFormaEntregaDocumento);
        }

      
        #endregion Realizar Atendimento
    }
}