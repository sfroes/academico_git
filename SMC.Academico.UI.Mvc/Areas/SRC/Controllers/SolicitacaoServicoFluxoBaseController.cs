using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.MAT.Exceptions;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Common.Areas.TUR.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Service.Areas.SRC.Services;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.ServiceContract.Areas.GRD.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Data.Efetivacao;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Academico.UI.Mvc.Areas.MAT.Models;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Academico.UI.Mvc.Areas.SRC.Models.SolicitacaoServico;
using SMC.Academico.UI.Mvc.Areas.SRC.Views.SolicitacaoServicoFluxoBase.App_LocalResources;
using SMC.Academico.UI.Mvc.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.UI.Mvc;
using SMC.Formularios.UI.Mvc.Model;
using SMC.Formularios.UI.Mvc.Models;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Filters;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.UI.Mvc.Util;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Controllers
{
    [SMCNoCache]
    public abstract class SolicitacaoServicoFluxoBaseController : SGFController
    {
        #region [ Services ]

        private IEscalaApuracaoService EscalaApuracaoService => Create<IEscalaApuracaoService>();

        private IMatrizCurricularService MatrizCurricularService => Create<IMatrizCurricularService>();

        private IGrupoCurricularComponenteService GrupoCurricularComponenteService => Create<IGrupoCurricularComponenteService>();

        protected IAlunoService AlunoService => Create<IAlunoService>();

        protected IAlunoHistoricoService AlunoHistoricoService => Create<IAlunoHistoricoService>();

        protected IArquivoAnexadoService ArquivoAnexadoService => Create<IArquivoAnexadoService>();

        protected IContratoService ContratoService => Create<IContratoService>();

        protected IComponenteCurricularService ComponenteCurricularService => Create<IComponenteCurricularService>();

        protected IConfiguracaoComponenteService ConfiguracaoComponenteService => Create<IConfiguracaoComponenteService>();

        protected IConfiguracaoEtapaService ConfiguracaoEtapaService => Create<IConfiguracaoEtapaService>();
        protected IConfiguracaoEtapaPaginaService ConfiguracaoEtapaPaginaService => Create<IConfiguracaoEtapaPaginaService>();

        protected ICurriculoCursoOfertaService CurriculoCursoOfertaService => Create<ICurriculoCursoOfertaService>();

        protected IDivisaoComponenteService DivisaoComponenteService => Create<IDivisaoComponenteService>();

        protected IEntidadeService EntidadeService => Create<IEntidadeService>();

        protected IEventoAulaService EventoAulaService => Create<IEventoAulaService>();

        protected IGrupoCurricularService GrupoCurricularService => Create<IGrupoCurricularService>();

        protected IHistoricoEscolarService HistoricoEscolarService => Create<IHistoricoEscolarService>();

        protected IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        protected IInstituicaoNivelTipoVinculoAlunoService InstituicaoNivelTipoVinculoAlunoService => Create<IInstituicaoNivelTipoVinculoAlunoService>();

        protected IJustificativaSolicitacaoServicoService JustificativaSolicitacaoServicoService => Create<IJustificativaSolicitacaoServicoService>();

        protected IPessoaAtuacaoBloqueioService PessoaAtuacaoBloqueioService => Create<IPessoaAtuacaoBloqueioService>();

        protected IPlanoEstudoItemService PlanoEstudoItemService => Create<IPlanoEstudoItemService>();

        protected IProcessoEtapaService ProcessoEtapaService => Create<IProcessoEtapaService>();

        protected IRegistroDocumentoService RegistroDocumentoService => Create<IRegistroDocumentoService>();

        protected IRequisitoService RequisitoService => Create<IRequisitoService>();

        protected IServicoService ServicoService => Create<IServicoService>();

        protected ISGFHelperService SGFHelperService => Create<ISGFHelperService>();

        protected ISolicitacaoDispensaService SolicitacaoDispensaService => Create<ISolicitacaoDispensaService>();

        protected ISolicitacaoHistoricoNavegacaoService SolicitacaoHistoricoNavegacaoService => Create<ISolicitacaoHistoricoNavegacaoService>();

        protected ISolicitacaoHistoricoSituacaoService SolicitacaoHistoricoSituacaoService => Create<ISolicitacaoHistoricoSituacaoService>();

        protected ISolicitacaoMatriculaService SolicitacaoMatriculaService => Create<ISolicitacaoMatriculaService>();

        protected ISolicitacaoMatriculaItemService SolicitacaoMatriculaItemService => Create<ISolicitacaoMatriculaItemService>();

        protected ISolicitacaoServicoEtapaService SolicitacaoServicoEtapaService => Create<ISolicitacaoServicoEtapaService>();

        protected ISolicitacaoServicoService SolicitacaoServicoService => Create<ISolicitacaoServicoService>();

        protected ITipoDivisaoComponenteService TipoDivisaoComponenteService => Create<ITipoDivisaoComponenteService>();

        protected ITurmaService TurmaService => Create<ITurmaService>();

        protected ISolicitacaoServicoBoletoTituloService SolicitacaoServicoBoletoTituloService => Create<ISolicitacaoServicoBoletoTituloService>();

        private IDocumentoRequeridoService DocumentoRequeridoService => Create<IDocumentoRequeridoService>();

        private IEscalonamentoService EscalonamentoService => Create<IEscalonamentoService>();
        private IDivisaoMatrizCurricularComponenteService DivisaoMatrizCurricularComponenteService => Create<IDivisaoMatrizCurricularComponenteService>();
        private ISolicitacaoTrabalhoAcademicoService SolicitacaoTrabalhoAcademicoService => Create<ISolicitacaoTrabalhoAcademicoService>();

        #endregion [ Services ]

        #region Fluxo Página

        [ChildActionOnly]
        public ActionResult MenuFluxo(SolicitacaoServicoPaginaViewModelBase model)
        {
            return PartialView("_MenuFluxo", model);
        }

        protected List<EtapaListaViewModel> BuscarEtapas(long seqSolicitacaoServico, bool ignoreSession = false)
        {
            // Verifica se ja busquei. Se sim, armazeno em seção para não ficar buscando novamente
            List<EtapaListaViewModel> etapasSecao = Session[SGFConstants.KEY_SESSION_SGF_ETAPAS] as List<EtapaListaViewModel>;
            if (etapasSecao == null || ignoreSession)
                etapasSecao = SGFHelperService.BuscarEtapas(seqSolicitacaoServico).TransformList<EtapaListaViewModel>();

            Session[SGFConstants.KEY_SESSION_SGF_ETAPAS] = etapasSecao;
            return etapasSecao;
        }

        /// <summary>
        /// Recupera o filtro para retornar o usuário à página atual do fluxo ou à uma página anterior informada via token
        /// </summary>
        /// <param name="solicitacaoServico">Solicitação de serviço atual</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração da etapa atual</param>
        /// <param name="tokenRet">Token de uma página específica para retorno</param>
        /// <returns>Filtro com os dados da página</returns>
        protected SolicitacaoServicoPaginaFiltroViewModel RecuperarPaginaAtual(SolicitacaoServicoData solicitacaoServico, long seqConfiguracaoEtapa, string tokenRet)
        {
            // Recupera a etapa atual, de acordo com a solicitação de matrícula
            var etapaAtual = solicitacaoServico.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == seqConfiguracaoEtapa);

            // Recupera as configurações da etapa atual
            var configuracaoEtapaAtual = ConfiguracaoEtapaService.BuscarConfiguracaoEtapa(seqConfiguracaoEtapa, IncludesConfiguracaoEtapa.ConfiguracoesPagina | IncludesConfiguracaoEtapa.ConfiguracoesPagina_Arquivos | IncludesConfiguracaoEtapa.ConfiguracoesPagina_TextosSecao | IncludesConfiguracaoEtapa.ProcessoEtapa);
            configuracaoEtapaAtual.ConfiguracoesPagina = configuracaoEtapaAtual.ConfiguracoesPagina.OrderBy(p => p.Ordem).ToList();

            // Cria o retorno
            SolicitacaoServicoPaginaFiltroViewModel ret = null;
            long SeqPaginaEtapaSgf = 0;

            if (!etapaAtual.HistoricosNavegacao.Any())
            {
                // Caso não tenha histórico, aponta o usuário para a primeira página da etapa

                var primeiraPagina = configuracaoEtapaAtual.ConfiguracoesPagina.FirstOrDefault();
                ret = new SolicitacaoServicoPaginaFiltroViewModel();
                ret.RedirecionarHome = false;
                ret.SeqConfiguracaoEtapaPagina = new SMCEncryptedLong(primeiraPagina.Seq);
                ret.ConfiguracaoEtapaPagina = primeiraPagina.Transform<ConfiguracaoEtapaPaginaViewModel>();
                SeqPaginaEtapaSgf = primeiraPagina.SeqPaginaEtapaSgf;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(tokenRet))
                {
                    // Caso tenha sido informado algum token, significa que o usuário está voltando para uma etapa anterior.
                    var paginaToken = configuracaoEtapaAtual.ConfiguracoesPagina.FirstOrDefault(p => p.TokenPagina == tokenRet);

                    // Verifica se existe algum histórico de navegação para página informada.
                    var historicoPaginaToken = etapaAtual.HistoricosNavegacao.FirstOrDefault(h => h.SeqConfiguracaoEtapaPagina == paginaToken.Seq);
                    if (historicoPaginaToken == null)
                        throw new SMCApplicationException("Você não pode voltar à esta página pois a mesma ainda não foi acessada.");

                    // Recupera o último registro do histórico de navegação. Caso esteja sem data de saída preenchida, preenche
                    var ultimoHistorico = etapaAtual.HistoricosNavegacao.LastOrDefault();

                    // Valida se a última página salva no histórico é anterior à página que desejo acessar.
                    // Não deixar acessar caso seja posterior
                    var paginaUltimo = configuracaoEtapaAtual.ConfiguracoesPagina.FirstOrDefault(p => p.Seq == ultimoHistorico.SeqConfiguracaoEtapaPagina);
                    if (paginaUltimo != null && paginaUltimo.Ordem < paginaToken.Ordem)
                        throw new SMCApplicationException("Você so pode retornar para páginas anteriores à página atual do fluxo.");

                    // Caso a data de saída do último histórico seja null, atualiza informando que o usuário está saindo dela para entrar em outra página.
                    if ((paginaUltimo.TokenPagina != paginaToken.TokenPagina) && (ultimoHistorico.DataSaida == null || ultimoHistorico.DataSaida == default(DateTime)))
                        SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(ultimoHistorico.Seq);

                    ret = new SolicitacaoServicoPaginaFiltroViewModel();
                    ret.RedirecionarHome = false;
                    ret.SeqConfiguracaoEtapaPagina = new SMCEncryptedLong(paginaToken.Seq);
                    ret.ConfiguracaoEtapaPagina = paginaToken.Transform<ConfiguracaoEtapaPaginaViewModel>();
                    SeqPaginaEtapaSgf = paginaToken.SeqPaginaEtapaSgf;
                }
                else
                {
                    // Armazena o último histórico
                    var ultimoHistorico = etapaAtual.HistoricosNavegacao.LastOrDefault();

                    // Recupera a configuração da página atual do histórico para saber a ordem da mesma
                    var configuracaoPaginaHistorico = configuracaoEtapaAtual.ConfiguracoesPagina.FirstOrDefault(p => p.Seq == ultimoHistorico.SeqConfiguracaoEtapaPagina);

                    if (ultimoHistorico.DataSaida != null && ultimoHistorico.DataSaida != default(DateTime))
                    {
                        // Verifica qual a próxima página configurada para a etapa
                        var proximaPagina = configuracaoEtapaAtual.ConfiguracoesPagina.FirstOrDefault(p => p.Ordem > configuracaoPaginaHistorico.Ordem);
                        if (proximaPagina != null)
                        {
                            // Existe próxima página. retorna
                            ret = new SolicitacaoServicoPaginaFiltroViewModel();
                            ret.RedirecionarHome = false;
                            ret.SeqConfiguracaoEtapaPagina = new SMCEncryptedLong(proximaPagina.Seq);
                            ret.ConfiguracaoEtapaPagina = proximaPagina.Transform<ConfiguracaoEtapaPaginaViewModel>();
                            SeqPaginaEtapaSgf = proximaPagina.SeqPaginaEtapaSgf;
                        }
                        else
                        {
                            // Não tem próxima página.
                            // Retorna a última página da etapa
                            ret = new SolicitacaoServicoPaginaFiltroViewModel();
                            ret.RedirecionarHome = false;
                            ret.SeqConfiguracaoEtapaPagina = new SMCEncryptedLong(configuracaoPaginaHistorico.Seq);
                            ret.ConfiguracaoEtapaPagina = configuracaoPaginaHistorico.Transform<ConfiguracaoEtapaPaginaViewModel>();
                            SeqPaginaEtapaSgf = configuracaoPaginaHistorico.SeqPaginaEtapaSgf;

                            // Redireciona para home
                            //ret.RedirecionarHome = true;
                            //ret.ConfiguracaoEtapa = configuracaoEtapaAtual.Transform<ConfiguracaoEtapaViewModel>();
                            //return ret;
                        }
                    }
                    else
                    {
                        // é a página atual. retorna ela.
                        ret = new SolicitacaoServicoPaginaFiltroViewModel();
                        ret.RedirecionarHome = false;
                        ret.SeqConfiguracaoEtapaPagina = new SMCEncryptedLong(ultimoHistorico.SeqConfiguracaoEtapaPagina);
                        ret.ConfiguracaoEtapaPagina = configuracaoPaginaHistorico.Transform<ConfiguracaoEtapaPaginaViewModel>();
                        SeqPaginaEtapaSgf = configuracaoPaginaHistorico.SeqPaginaEtapaSgf;
                    }
                }
            }

            if (ret != null)
            {
                // Define as propriedades padrões
                ret.SeqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa);
                ret.SeqPessoaAtuacao = new SMCEncryptedLong(solicitacaoServico.SeqPessoaAtuacao);
                ret.SeqSolicitacaoServico = new SMCEncryptedLong(solicitacaoServico.Seq);
                ret.SeqProcesso = new SMCEncryptedLong(solicitacaoServico.SeqProcesso);
                ret.DescricaoProcesso = solicitacaoServico.DescricaoProcesso;
                ret.SeqServico = new SMCEncryptedLong(solicitacaoServico.SeqServico);
                ret.DescricaoServico = solicitacaoServico.DescricaoServico;
                ret.TokenServico = solicitacaoServico.TokenServico;
                ret.Protocolo = solicitacaoServico.NumeroProtocolo;
                ret.SeqEntidadeResponsavel = new SMCEncryptedLong(solicitacaoServico.SeqEntidadeResponsavel);
                ret.SeqEntidadeCompartilhada = new SMCEncryptedLong(solicitacaoServico.SeqEntidadeCompartilhada.GetValueOrDefault());
                ret.SeqSolicitacaoServicoEtapa = new SMCEncryptedLong(etapaAtual.Seq);
                ret.TokenEtapa = configuracaoEtapaAtual.ProcessoEtapa.Token;
                ret.ConfiguracaoEtapa = configuracaoEtapaAtual.Transform<ConfiguracaoEtapaViewModel>();
                ret.DescricaoEtapa = configuracaoEtapaAtual.ProcessoEtapa.DescricaoEtapa;
            }
            return ret;
        }

        protected ActionResult EntrarEtapa(long seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, string tokenRet)
        {
            return EntrarEtapa(new SMCEncryptedLong(seqSolicitacaoServico), seqConfiguracaoEtapa, tokenRet);
        }

        public virtual ActionResult RetornarPagina(string token, SolicitacaoServicoPaginaFiltroViewModel model)
        {
            throw new SMCApplicationException($"Token '{token}' não existe no fluxo de serviço.");
        }

        /// <summary>
        /// Cria o objeto de filtro para os parâmetros atuais da requisição
        /// </summary>
        /// <returns>Filtro com os dados da página atual</returns>
        private SolicitacaoServicoPaginaFiltroViewModel RecuperarPaginaFiltroViewModel(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, string tokenRet)
        {
            if (seqSolicitacaoServico <= 0 || seqConfiguracaoEtapa <= 0)
                throw new SMCApplicationException("Sequencial da solicitação ou da configuração da etapa não informados");

            // Busca a solicitação de matrícula.
            var solicitacaoServico = SolicitacaoServicoService.BuscarSolicitacaoServico(seqSolicitacaoServico);

            // Busca as etapas configuradas para este ingressante
            var etapasIngressante = BuscarEtapas(seqSolicitacaoServico, true);
            var etapaIngressanteAtual = etapasIngressante.FirstOrDefault(e => e.SeqConfiguracaoEtapa == seqConfiguracaoEtapa);
            var etapaIngressanteAnterior = etapasIngressante.OrderBy(x => x.OrdemEtapaSGF).FirstOrDefault(e => e.OrdemEtapaSGF == (etapaIngressanteAtual.OrdemEtapaSGF - 1));

            // Com o template de processo em mãos, e a etapa atual com seus históricos, conseguimos decidir qual página exibir
            var paginaAtual = RecuperarPaginaAtual(solicitacaoServico, seqConfiguracaoEtapa, tokenRet);
            paginaAtual.EtapaFinalizada = etapaIngressanteAtual.SituacaoEtapaIngressante.HasFlag(SituacaoEtapaSolicitacaoMatricula.Finalizada);

            return paginaAtual;
        }

        /// <summary>
        /// Faz a chamada para efetuar uma validação prévia, afim de permitir exibir mensagem de erro ajax.
        /// Método foi criado pois foi necessário realizar validações através da tela de realizar atendimentos do administrativo, sem que
        /// estourasse erro na tela do usuário. Desta maneira, é chamada essa action via ajax e, caso dê erro, exibe na própria tela da
        /// central. Se der certo redireciona para tela do fluxo, onde terá a validação novamente.
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração da etapa</param>
        /// <param name="tokenRet">Token de retorno</param>
        /// <returns>Erro ou redirecionamento para tela de entrar etapa</returns>
        [SMCAllowAnonymous]
        public virtual ActionResult EntrarEtapaValidacao(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, string tokenRet)
        {
            if (seqSolicitacaoServico <= 0 || seqConfiguracaoEtapa <= 0)
                return RedirectToAction("Index", "Home", new { area = "" });

            // Recupera a página filtro atual
            var paginaAtual = RecuperarPaginaFiltroViewModel(seqSolicitacaoServico, seqConfiguracaoEtapa, tokenRet);

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(paginaAtual);

            return SMCRedirectToAction(nameof(EntrarEtapa), null, new { seqSolicitacaoServico = seqSolicitacaoServico, seqConfiguracaoEtapa = seqConfiguracaoEtapa, tokenRet = tokenRet });
        }

        [SMCAllowAnonymous]
        public virtual ActionResult EntrarEtapa(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, string tokenRet)
        {
            if (seqSolicitacaoServico <= 0 || seqConfiguracaoEtapa <= 0)
                return RedirectToAction("Index", "Home", new { area = "" });

            // Recupera a página filtro atual
            var paginaAtual = RecuperarPaginaFiltroViewModel(seqSolicitacaoServico, seqConfiguracaoEtapa, tokenRet);

            // Redireciona para a primeira página
            if (paginaAtual.RedirecionarHome)
                return RedirectToAction("Index", "Ingressante");
            else
            {
                // Redireciona corretamente para página da etapa atual
                switch (paginaAtual.ConfiguracaoEtapaPagina.TokenPagina)
                {
                    case TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_PADRAO_SOLICITACAO_FORMULARIO:
                        return FormularioSolicitacaoPadrao(paginaAtual);

                    case TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_PADRAO_SOLICITACAO_UPLOAD_DOCUMENTO:
                        return UploadSolicitacaoPadrao(paginaAtual);

                    case TOKEN_SOLICITACAO_SERVICO.SELECAO_TURMA_MATRICULA:
                        if (paginaAtual.TokenServico == TOKEN_SOLICITACAO_SERVICO.DISCIPLINA_ELETIVA)
                            return SelecaoTurmaDisciplinaEletiva(paginaAtual);
                        else
                            return SelecaoTurmaPlanoEstudo(paginaAtual);

                    case TOKEN_SOLICITACAO_SERVICO.SELECAO_ATIVIDADE_ACADEMICA_MATRICULA:
                        return SelecaoAtividadeAcademicaPlanoEstudo(paginaAtual);

                    case TOKEN_SOLICITACAO_SERVICO.CONFIRMACAO_SOLICITACAO_MATRICULA:
                        return ConfirmacaoSolicitacaoMatricula(paginaAtual);

                    case TOKEN_SOLICITACAO_SERVICO.CHANCELA_PLANO_ESTUDO:
                        return SelecaoChancelaPlanoEstudo(paginaAtual);

                    case TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_ATIVIDADE_COMPLEMENTAR:
                        return SolicitacaoAtividadeComplementar(paginaAtual);

                    case TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_DISPENSA_ITENS_SEREM_DISPENSADOS:
                        return SolicitacaoDispensaItensDispensados(paginaAtual);

                    case TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_DISPENSA_ITENS_CURSADOS:
                        return SolicitacaoDispensaItensCursados(paginaAtual);

                    case TOKEN_SOLICITACAO_SERVICO.CONFIRMACAO_SELECAO_TURMA:
                        return ConfirmacaoSelecaoTurma(paginaAtual);

                    case TOKEN_SOLICITACAO_SERVICO.CONFIRMACAO_SELECAO_ATIVIDADE_ACADEMICA:
                        return ConfirmacaoSelecaoAtividadeAcademica(paginaAtual);

                    case TOKEN_SOLICITACAO_SERVICO.CONCLUSAO_MATRICULA:
                        return EfetivacaoMatricula(paginaAtual);

                    case TOKEN_SOLICITACAO_SERVICO.REGISTRO_DOCUMENTO_ENTREGUE:
                        return RegistrarEntregaDocumentos(paginaAtual);

                    case TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_COBRANCA_TAXA:
                        return SolicitacaoCobrancaTaxa(paginaAtual);

                    case TOKEN_SOLICITACAO_SERVICO.REGISTRO_ENTREGA_DOCUMENTACAO_LATO_SENSU:
                        return RegistrarEntregaDocumentosResidenciaMedica(paginaAtual);
                    case TOKEN_SOLICITACAO_SERVICO.SELECAO_COMPONENTE_CURRICULAR:
                        return SelecaoComponenteCurricular(paginaAtual);
                    default:
                        return RetornarPagina(paginaAtual.ConfiguracaoEtapaPagina.TokenPagina, paginaAtual);
                }
                throw new SMCApplicationException($"Token '{paginaAtual.ConfiguracaoEtapaPagina.TokenPagina}' não existe no fluxo de serviço.");
            }
        }

        #endregion Fluxo Página

        #region Configuração das páginas

        protected void VerificarPreCondicoesEntradaPagina(SolicitacaoServicoPaginaFiltroViewModel filtro, bool ignoreFinalizedValidation = false)
        {
            // Recupera as configurações do processo etapa para saber se a mesma encontra-se liberada ou não para acesso
            var configuracaoEtapaAtual = ConfiguracaoEtapaService.BuscarConfiguracaoEtapa(filtro.SeqConfiguracaoEtapa, IncludesConfiguracaoEtapa.Nenhum);
            var processoEtapa = ProcessoEtapaService.BuscarProcessoEtapa(configuracaoEtapaAtual.SeqProcessoEtapa);

            if (filtro.EtapaFinalizada && (filtro.ConfiguracaoEtapaPagina.TokenPagina != TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_PADRAO_SOLICITACAO_INSTRUCOES_FINAIS &&
                                           filtro.ConfiguracaoEtapaPagina.TokenPagina != TOKEN_SOLICITACAO_SERVICO.INSTRUCOES_FINAIS_SOLICITACAO_MATRICULA &&
                                           filtro.ConfiguracaoEtapaPagina.TokenPagina != TOKEN_SOLICITACAO_SERVICO.CONCLUSAO_MATRICULA &&
                                           filtro.ConfiguracaoEtapaPagina.TokenPagina != TOKEN_SOLICITACAO_SERVICO.DOWNLOAD_DOCUMENTO_DIGITAL))
                ThrowRedirect(new SMCApplicationException($"Não é possível acessar esta página pois esta etapa encontra-se finalizada."), "Index", "SolicitacaoServico", new RouteValueDictionary { { "area", "SRC" } });

            // Verifica se existe alguma outra situação que impede o acesso à etapa
            SolicitacaoServicoEtapaService.VerificarAcessoEtapa(filtro.SeqSolicitacaoServico, filtro.SeqConfiguracaoEtapa, ignoreFinalizedValidation);
        }

        protected void VerificarHistoricoSituacao(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            // Busca a solicitação de matrícula.
            var solicitacaoServico = SolicitacaoServicoService.BuscarSolicitacaoServico(filtro.SeqSolicitacaoServico);

            // Busca as etapas configuradas para este ingressante
            var etapasIngressante = BuscarEtapas(filtro.SeqSolicitacaoServico);
            var etapaIngressanteAtual = etapasIngressante.FirstOrDefault(e => e.SeqConfiguracaoEtapa == filtro.SeqConfiguracaoEtapa);

            // Após recuperar a página que deve ser exibida, antes de retornar o usuário para a mesma, vamos
            var paginaSGFAtual = etapaIngressanteAtual.Paginas.FirstOrDefault(p => p.Seq == filtro.ConfiguracaoEtapaPagina.SeqPaginaEtapaSgf);
            if (paginaSGFAtual != null && paginaSGFAtual.SeqSituacaoEtapaInicial.HasValue)
            {
                var historicos = solicitacaoServico.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == filtro.SeqConfiguracaoEtapa)?.HistoricosSituacao;
                if (historicos == null || historicos.LastOrDefault(h => !h.DataExclusao.HasValue)?.SeqSituacaoEtapaSgf != paginaSGFAtual.SeqSituacaoEtapaInicial)
                {
                    // Salva o histórico novo
                    SolicitacaoHistoricoSituacaoService.AtualizarHistoricoSituacao(filtro.SeqSolicitacaoServicoEtapa, paginaSGFAtual.SeqSituacaoEtapaInicial.Value);
                }
            }
        }

        /// <summary>
        /// Chamado após as configurações de página
        /// </summary>
        /// <typeparam name="TModelSolicitacao"></typeparam>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <param name="actionSalvar"></param>
        /// <param name="seqSolicitacaoHistoricoNavegacao"></param>
        protected virtual void AposConfigurarPagina<TModelSolicitacao>(TModelSolicitacao pagina, SolicitacaoServicoPaginaFiltroViewModel filtro, string actionSalvar, long seqSolicitacaoHistoricoNavegacao)
            where TModelSolicitacao : SolicitacaoServicoPaginaViewModelBase
        {
        }

        protected void ConfigurarPagina<TModelSolicitacao>(TModelSolicitacao pagina, SolicitacaoServicoPaginaFiltroViewModel filtro, string actionSalvar, long seqSolicitacaoHistoricoNavegacao)
            where TModelSolicitacao : SolicitacaoServicoPaginaViewModelBase
        {
            if (string.IsNullOrWhiteSpace(pagina.Token))
                throw new SMCApplicationException("A página deve ter um token configurado.");

            pagina.SeqConfiguracaoEtapa = filtro.SeqConfiguracaoEtapa;
            pagina.SeqConfiguracaoEtapaPagina = filtro.SeqConfiguracaoEtapaPagina;
            pagina.Titulo = filtro.ConfiguracaoEtapaPagina.TituloPagina;
            //pagina.ExibeMenu = filtro.ConfiguracaoEtapaPagina.ExibeMenu;
            pagina.Ordem = filtro.ConfiguracaoEtapaPagina.Ordem;
            pagina.SeqSolicitacaoServico = filtro.SeqSolicitacaoServico;
            pagina.ActionSalvarEtapa = actionSalvar;
            pagina.SeqSolicitacaoHistoricoNavegacao = (SMCEncryptedLong)seqSolicitacaoHistoricoNavegacao;
            pagina.EtapaFinalizada = filtro.EtapaFinalizada;
            pagina.DescricaoEtapa = filtro.DescricaoEtapa;
            pagina.SeqSolicitacaoServicoEtapa = (SMCEncryptedLong)filtro.SeqSolicitacaoServicoEtapa;
            pagina.SeqPessoaAtuacao = filtro.SeqPessoaAtuacao;
            pagina.SeqProcesso = filtro.SeqProcesso;
            pagina.SeqServico = filtro.SeqServico;
            pagina.DescricaoServico = filtro.DescricaoServico;
            pagina.Protocolo = filtro.Protocolo;
            pagina.DescricaoProcesso = filtro.DescricaoProcesso;

            // Verifica se existe algum bloqueio
            pagina.Bloqueios = PessoaAtuacaoBloqueioService.BuscarPessoaAtuacaoBloqueios(filtro.SeqPessoaAtuacao, filtro.SeqConfiguracaoEtapa, false).TransformList<PessoaAtuacaoBloqueioViewModel>();

            // Configura as seções do template
            pagina.Secoes = new List<ITemplateSecaoPagina>();

            var secoesTexto = filtro?.ConfiguracaoEtapaPagina?.TextosSecao?.Select(x => new TemplateSecaoPaginaTextoViewModel
            {
                Texto = x.Texto,
                Token = x.TokenSecao
            });

            var secoesArquivo = filtro?.ConfiguracaoEtapaPagina?.Arquivos?.GroupBy(s => s.TokenSecao).Select(x => new TemplateSecaoPaginaArquivoViewModel
            {
                Token = x.Key,
                Arquivos = x.OrderBy(a => a.Ordem).Select(a => new TemplateArquivoSecaoViewModel
                {
                    Descricao = a.MensagemArquivo,
                    NomeLink = a.LinkArquivo,
                    SeqArquivo = a.SeqArquivoAnexado
                }).ToList()
            });

            if (secoesTexto != null)
                pagina.Secoes.AddRange(secoesTexto);

            if (secoesArquivo != null)
                pagina.Secoes.AddRange(secoesArquivo);

            // Recupera as configurações de página do SGF para saber qual é a situação final da etapa
            var etapas = BuscarEtapas(filtro.SeqPessoaAtuacao);
            pagina.SeqSituacaoFinalSucesso = (SMCEncryptedLong)etapas?.FirstOrDefault(e => e.SeqConfiguracaoEtapa == filtro.SeqConfiguracaoEtapa)?.Situacoes?.FirstOrDefault(s => s.SituacaoFinalEtapa && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)?.Seq;
            pagina.SeqSituacaoFinalSemSucesso = (SMCEncryptedLong)etapas?.FirstOrDefault(e => e.SeqConfiguracaoEtapa == filtro.SeqConfiguracaoEtapa)?.Situacoes?.FirstOrDefault(s => s.SituacaoFinalEtapa && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso)?.Seq;
            pagina.SeqSituacaoFinalCancelada = (SMCEncryptedLong)etapas?.FirstOrDefault(e => e.SeqConfiguracaoEtapa == filtro.SeqConfiguracaoEtapa)?.Situacoes?.FirstOrDefault(s => s.SituacaoFinalEtapa && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado)?.Seq;

            pagina.FluxoPaginas = filtro.ConfiguracaoEtapa.ConfiguracoesPagina.Select(p => new TemplateFluxoPaginaViewModel
            {
                Ordem = p.Ordem,
                SeqConfiguracaoEtapaPagina = p.Seq,
                Titulo = p.TituloPagina,
                Token = p.TokenPagina,
                //SeqFormularioSGF = ,
                //SeqVisaoSGF = ,
            }).ToList<ITemplateFluxoPagina>();

            AposConfigurarPagina(pagina, filtro, actionSalvar, seqSolicitacaoHistoricoNavegacao);
        }

        #endregion Configuração das páginas

        #region Conteúdo

        [SMCAllowAnonymous]
        public virtual ActionResult DownloadDocumentoGuid(Guid guidFile)
        {
            var arquivo = ArquivoAnexadoService.BuscarArquivoAnexadoPorGuid(guidFile);
            return File(arquivo.FileData, arquivo.Type, arquivo.Name);
        }

        public FileResult DownloadDocumento(SMCEncryptedLong id)
        {
            var arquivo = ArquivoAnexadoService.BuscarArquivoAnexado(id);
            Response.AddHeader("Content-Disposition", "inline; filename= " + arquivo.Name);
            return File(arquivo.FileData, arquivo.Type);
        }

        /// <summary>
        /// Efetua o download de um documento que foi enviado via tela de upload de documentos padrão
        /// </summary>
        [SMCAllowAnonymous]
        public virtual ActionResult DownloadDocumentoEnviado(string guidFile, string name, string type, string propertyName, SMCEncryptedLong seqEntity)
        {
            if (Guid.TryParse(guidFile, out Guid guidParsed))
            {
                var data = SMCUploadHelper.GetFileData(new SMCUploadFile { GuidFile = guidFile });
                if (data != null)
                {
                    return File(data, type, name);
                }
                else if (guidParsed != Guid.Empty)
                {
                    return DownloadDocumentoGuid(guidParsed);
                }
            }

            if (!Int64.TryParse(guidFile, out long seq))
            {
                seq = new SMCEncryptedLong(guidFile);
            }
            else if (seqEntity != null)
            {
                seq = seqEntity.Value;
            }

            if (seq != 0)
            {
                var arquivo = ArquivoAnexadoService.BuscarArquivoAnexado(seq);
                return File(arquivo.FileData, arquivo.Type, arquivo.Name);
            }

            throw new SMCUnauthorizedAccessException();
        }

        #region Solicitação Padrão

        protected ActionResult FormularioSolicitacaoPadrao(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_SRC_004_05_01.SOLICITACAO_FORMULARIO))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new FormularioSolicitacaoPadraoPaginaViewModel();
            model.DadoFormulario = SolicitacaoServicoService.BuscarDadosFormularioSolicitacaoPadrao(filtro.SeqSolicitacaoServico, filtro.SeqConfiguracaoEtapaPagina).Transform<FormularioPadraoDadoFormularioViewModel>();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarFormularioSolicitacaoPadrao), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            var view = GetExternalView(AcademicoExternalViews.FORMULARIO_SOLICITACAO_PADRAO);
            return View(view, model);
        }

        [HttpPost]
        [SMCAuthorize(UC_SRC_004_05_01.SOLICITACAO_FORMULARIO)]
        public ActionResult SalvarFormularioSolicitacaoPadrao(DadoFormularioViewModel dados, FormularioSolicitacaoPadraoPaginaViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                // Chama o método de Salvar os dados que foram preenchidos para a solicitação
                var dadosData = dados.Transform<DadosFormularioSolicitacaoPadraoData>();
                dadosData.SeqConfiguracaoEtapaPagina = model.SeqConfiguracaoEtapaPagina;
                dadosData.SeqSolicitacaoServico = model.SeqSolicitacaoServico;
                SolicitacaoServicoService.SalvarDadosFormularioSolicitacaoPadrao(dadosData);
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        public ActionResult UploadSolicitacaoPadrao(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_SRC_004_06_01.ABERTURA_SOLICITACAO_UPLOAD_ARQUIVOS))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new UploadSolicitacaoPadraoPaginaViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarUploadSolicitacaoPadrao), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            model.Documentos = RegistroDocumentoService.BuscarDocumentosRegistro(filtro.SeqSolicitacaoServico, filtro.SeqConfiguracaoEtapa, true).TransformList<SolicitacaoDocumentoViewModel>();

            //Anteriormente, se o aluno enviasse um documento, a situação dele ficava como pendente de entrega. Após solicitação da Jéssica, se houver arquivo é para marcar
            //como entregue.
            foreach (var item in model.Documentos)
            {
                if (item.Documentos.Any(x => x.SeqArquivoAnexado.HasValue))
                {
                    item.SituacaoEntregaDocumentoListaAluno = Common.Areas.SRC.Enums.SituacaoEntregaDocumentoLista.RegistroDocumentoOK;
                    item.SituacaoEntregaDocumentoLista = Common.Areas.SRC.Enums.SituacaoEntregaDocumentoLista.RegistroDocumentoOK;
                }
                else
                {
                    item.SituacaoEntregaDocumentoListaAluno = Common.Areas.SRC.Enums.SituacaoEntregaDocumentoLista.AguardandoRegistroDocumento;
                    item.SituacaoEntregaDocumentoLista = Common.Areas.SRC.Enums.SituacaoEntregaDocumentoLista.AguardandoRegistroDocumento;

                }
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            var view = GetExternalView(AcademicoExternalViews.UPLOAD_SOLICITACAO_PADRAO);
            return View(view, model);
        }

        [HttpPost]
        [SMCAuthorize(UC_SRC_004_06_01.ABERTURA_SOLICITACAO_UPLOAD_ARQUIVOS)]
        public ActionResult SalvarUploadSolicitacaoPadrao(UploadSolicitacaoPadraoPaginaViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                // Executa a regra de negócio de Salvar da página de Upload
                SolicitacaoServicoService.SalvarDocumentosSolicitacao(model.SeqSolicitacaoServico, model.SeqConfiguracaoEtapa);
            }

            // buscando documentos da solicitação
            var documentos = RegistroDocumentoService.BuscarDocumentosRegistro(model.SeqSolicitacaoServico, model.SeqConfiguracaoEtapa, true).TransformList<SolicitacaoDocumentoViewModel>();
            var servico = ServicoService.BuscarServico(model.SeqServico);

            if (servico != null && servico.Token == TOKEN_SERVICO.ENTREGA_DOCUMENTACAO && string.IsNullOrEmpty(tokenRet) && (documentos.Any() && documentos.Count() > 0))
            {
                if (!documentos.Any(x => x.Documentos.Any(y => y.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoValidacao)))
                    throw new DocumentoAguardandoValidacaoException();
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        [HttpGet]
        [SMCAuthorize(UC_SRC_004_08_01.ABERTURA_SOLICITACAO_INSTRUCAO_FINAL,
                      UC_SRC_004_03_01.REALIZAR_ATENDIMENTO_INSTRUCAO_INICIAL)]
        public ActionResult ImprimirDadosSolicitacaoPadrao(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, SMCEncryptedLong seqServico)
        {
            try
            {
                // Recupera os seqs das configurações etapas da solicitação
                var seqsConfiguracoesEtapas = SolicitacaoServicoService.BuscarSeqsConfiguracoesEtapaSolicitacao(seqSolicitacaoServico);

                // Cria o modelo para a página em questão
                var model = new ImpressaoSolicitacaoPadraoViewModel();

                var solicitacaoServico = SolicitacaoServicoService.BuscarSolicitacaoServico(seqSolicitacaoServico);

                // Recupera o logo e nome da instituição
                var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsinoPorPessoaAtuacao(solicitacaoServico.SeqPessoaAtuacao);

                model.NomeInstituicao = instituicao.Nome;
                model.LogoInstituicao = instituicao.ArquivoLogotipo?.FileData;

                model.DescricaoProcessoImprimir = solicitacaoServico.DescricaoProcesso;
                model.Protocolo = solicitacaoServico.NumeroProtocolo;

                var configuracaoEtapaAtual = ConfiguracaoEtapaService.BuscarConfiguracaoEtapa(seqConfiguracaoEtapa, IncludesConfiguracaoEtapa.ProcessoEtapa);
                model.DescricaoEtapa = configuracaoEtapaAtual.ProcessoEtapa.DescricaoEtapa;

                // Recupera os dados da solicitação
                var dadosFinaisSolicitacao = SolicitacaoServicoService.BuscarDadosFinaisSolicitacaoPadrao(seqSolicitacaoServico, seqsConfiguracoesEtapas.FirstOrDefault());

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
                model.NomeSolicitante = string.IsNullOrEmpty(solicitacaoServico.NomeSocial) ? solicitacaoServico.NomeSolicitante : solicitacaoServico.NomeSocial;
                model.RASolicitante = solicitacaoServico.RASolicitante;

                if (model.SeqJustificativa.HasValue && model.SeqJustificativa.Value > 0)
                {
                    model.JustificativasSolicitacao = JustificativaSolicitacaoServicoService.BuscarJustificativasSolicitacaoServicoSelect(new JustificativaSolicitacaoServicoFiltroData { Ativo = true, SeqServico = seqServico });
                    model.DescricaoJustificativa = model.JustificativasSolicitacao.FirstOrDefault(f => f.Seq == model.SeqJustificativa).Descricao;
                }

                // Recupera os documentos
                var documentosServico = RegistroDocumentoService.BuscarDocumentosRegistro(seqSolicitacaoServico, seqsConfiguracoesEtapas.FirstOrDefault());
                model.Documentos = documentosServico.SelectMany(d => d.Documentos.Where(a => a.SeqArquivoAnexado.HasValue).Select(x => new ImpressaoSolicitacaoPadraoDocumentoViewModel
                {
                    DescricaoTipoDocumento = d.DescricaoTipoDocumento,
                    ArquivoAnexado = x.ArquivoAnexado,
                    Observacao = x.Observacao,
                    ObservacaoSecretaria = x.ObservacaoSecretaria,
                    Seq = x.Seq,
                })).ToList();

                ViewBag.Title = "Comprovante de Solicitação";

                var view = GetExternalView(AcademicoExternalViews.IMPRESSAO_SOLICITACAO_PADRAO);
                return RenderPdfView(view, null, model, new Framework.GridOptions { PageMargins = new Framework.MarginInfo { Left = 10, Right = 13, Top = 13, Bottom = 13 } });
            }
            catch (Exception e)
            {
                return SMCRedirectToAction("EntrarEtapa", null, new { SeqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) });
            }
        }

        #endregion Solicitação Padrão

        #region [ Seleção de Turma ]

        protected ActionResult SelecaoTurmaDisciplinaEletiva(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_25_01.PAGINA_SELECAO_TURMAS))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new SelecaoTurmaPaginaViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarSelecaoTurmaDisciplinaEletiva), historico.Seq);

            //Verifica se foi criado uma solicitação de matrícula para a solicitação de serviço
            SolicitacaoMatriculaService.CriarSolicitacaoMatriculaPorSolicitacaoServico(filtro.SeqSolicitacaoServico);

            //Recupera a entidade vinculo do aluno e remove
            var grupoAluno = AlunoHistoricoService.BuscarEntidadeVinculoAluno(filtro.SeqPessoaAtuacao);

            //Recupera as entidades do tipo GRUPO_PROGRAMA
            var grupoPrograma = EntidadeService.BuscarEntidadesSelect(TOKEN_TIPO_ENTIDADE.GRUPO_PROGRAMA, grupoAluno.Item2.GetValueOrDefault());
            model.GruposPrograma = grupoPrograma.Where(w => w.Seq != grupoAluno.Item1).TransformList<SMCSelectListItem>();

            foreach (var item in model.GruposPrograma)
                item.Value = new SMCEncryptedLong(long.Parse(item.Value)).ToString();

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });
            model.SeqProcesso = filtro.SeqProcesso;
            model.SeqSolicitacaoServico = filtro.SeqSolicitacaoServico;
            model.SeqProcessoEtapa = (SMCEncryptedLong)SolicitacaoMatriculaService.BuscarProcessoEtapaPorSolicitacaoMatricula(filtro.SeqSolicitacaoServico, filtro.SeqConfiguracaoEtapa);
            model.TokenServico = filtro.TokenServico;

            var parametrosValidacao = InstituicaoNivelTipoVinculoAlunoService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(filtro.SeqPessoaAtuacao);
            model.ExigirCurso = parametrosValidacao.ExigeCurso;

            //Exibir o botão de integralização curricular
            model.ExibirIntegralizacao = HistoricoEscolarService.VisualizarConsultaConcederFormacaoIntegralizacao(filtro.SeqPessoaAtuacao);

            //Copia o plano de estudo do aluno
            SolicitacaoMatriculaItemService.ConverterPlanoEstudoItemEmSolicitacaoMatriculaItem(model.SeqSolicitacaoServico, model.SeqProcessoEtapa, filtro.SeqPessoaAtuacao);

            //Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            //Recupera os registros da solicitação de matrícula
            model.TurmasMatriculaItem = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(model.SeqSolicitacaoServico, 0, null, model.SeqProcessoEtapa, true).TransformList<TurmaMatriculaItemViewModel>();

            model.ExibirPertencePlanoEstudo = false;

            model.TurmasMatriculaItem.SMCForEach(f =>
            {
                f.ExigirCurso = model.ExigirCurso;
                f.ExigirMatrizCurricularOferta = model.ExigirMatrizCurricularOferta;
                f.SeqProcessoEtapa = model.SeqProcessoEtapa;
                f.ExibirMatriculaPertence = false;
                f.TurmaMatriculaDivisoes.SMCForEach(ff => ff.ExibirPertencePlanoEstudo = model.ExibirPertencePlanoEstudo);
            });

            model.ExibirCancelados = SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaTurmasCancelada(model.SeqSolicitacaoServico) || SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemCancelado(model.SeqSolicitacaoServico);

            if (model.ExibirCancelados.HasValue &&
                model.ExibirCancelados.Value &&
                model.TurmasMatriculaItem.Count == 0 &&
                model.ExigirMatrizCurricularOferta == false)
            {
                model.TodosCancelados = !SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemCanceladoPorSolicitante(model.SeqSolicitacaoServico);
            }

            //Caso já tenha cadastrado alguma turma seleciona o programa responsável
            if (model.TurmasMatriculaItem.SMCCount() > 0 && filtro.SeqEntidadeCompartilhada.Value > 0)
                model.SeqGrupoPrograma = filtro.SeqEntidadeCompartilhada;

            var view = GetExternalView(AcademicoExternalViews.SOLICITACAO_SELECAO_TURMA);
            return View(view, model);
        }

        protected ActionResult SelecaoTurmaPlanoEstudo(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_25_01.PAGINA_SELECAO_TURMAS))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new SelecaoTurmaPaginaViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarSelecaoTurmaPlanoEstudo), historico.Seq);

            //Verifica se foi criado uma solicitação de matrícula para a solicitação de serviço
            SolicitacaoMatriculaService.CriarSolicitacaoMatriculaPorSolicitacaoServico(filtro.SeqSolicitacaoServico);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });
            model.SeqProcesso = filtro.SeqProcesso;
            model.SeqSolicitacaoServico = filtro.SeqSolicitacaoServico;
            model.SeqProcessoEtapa = (SMCEncryptedLong)SolicitacaoMatriculaService.BuscarProcessoEtapaPorSolicitacaoMatricula(filtro.SeqSolicitacaoServico, filtro.SeqConfiguracaoEtapa);
            model.SeqConfiguracaoEtapa = filtro.SeqConfiguracaoEtapa;

            var parametrosValidacao = InstituicaoNivelTipoVinculoAlunoService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(filtro.SeqPessoaAtuacao);
            model.ExigirCurso = parametrosValidacao.ExigeCurso;

            //Exibir o botão de integralização curricular
            model.ExibirIntegralizacao = HistoricoEscolarService.VisualizarConsultaConcederFormacaoIntegralizacao(filtro.SeqPessoaAtuacao);

            //Copia o plano de estudo do aluno
            SolicitacaoMatriculaItemService.ConverterPlanoEstudoItemEmSolicitacaoMatriculaItem(model.SeqSolicitacaoServico, model.SeqProcessoEtapa, filtro.SeqPessoaAtuacao);

            //Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            //Recupera os registros da solicitação de matrícula
            model.TurmasMatriculaItem = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(model.SeqSolicitacaoServico, filtro.SeqPessoaAtuacao, null, model.SeqProcessoEtapa, true, false, true).TransformList<TurmaMatriculaItemViewModel>();
            model.ExibirPertencePlanoEstudo = true;

            model.TurmasMatriculaItem.SMCForEach(f =>
            {
                f.ExigirCurso = model.ExigirCurso;
                f.ExigirMatrizCurricularOferta = model.ExigirMatrizCurricularOferta;
                f.SeqProcessoEtapa = model.SeqProcessoEtapa;
                f.ExibirMatriculaPertence = true;
                f.TurmaMatriculaDivisoes.SMCForEach(ff =>
                {
                    ff.ExibirLegendaInclusao = (f.SituacaoInicial.GetValueOrDefault() &&
                                                       !f.PertencePlanoEstudo.GetValueOrDefault() &&
                                                       f.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso);

                    ff.ExibirPertencePlanoEstudo = model.ExibirPertencePlanoEstudo;
                });

                if (f.TurmaMatriculaDivisoes.All(a => a.SituacaoPlanoEstudo == MatriculaPertencePlanoEstudo.Exclusao))
                    f.MatriculaPertence = MatriculaPertencePlanoEstudo.Exclusao;
            });

            model.ExibirCancelados = SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaTurmasCancelada(model.SeqSolicitacaoServico) || SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemCancelado(model.SeqSolicitacaoServico);

            if (model.ExibirCancelados.HasValue &&
                model.ExibirCancelados.Value &&
                model.TurmasMatriculaItem.Count == 0 &&
                model.ExigirMatrizCurricularOferta == false)
            {
                model.TodosCancelados = !SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemCanceladoPorSolicitante(model.SeqSolicitacaoServico);
            }

            model.SeqGrupoPrograma = filtro.SeqEntidadeResponsavel;

            var view = GetExternalView(AcademicoExternalViews.SOLICITACAO_SELECAO_TURMA_PLANO);
            return View(view, model);
        }

        [SMCAllowAnonymous]
        public ActionResult SelecaoTurmaDependencyGrupoPrograma(SMCEncryptedLong seqGrupoPrograma, SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqPessoaAtuacao, SMCEncryptedLong seqProcesso, SMCEncryptedLong seqProcessoEtapa, string backUrl, bool exibirIntegralizacao)
        {
            SelecaoTurmaPaginaViewModel turma = new SelecaoTurmaPaginaViewModel();

            turma.SeqGrupoPrograma = seqGrupoPrograma;
            turma.SeqSolicitacaoServico = seqSolicitacaoServico;
            turma.SeqPessoaAtuacao = seqPessoaAtuacao;
            turma.SeqProcesso = seqProcesso;
            turma.SeqProcessoEtapa = seqProcessoEtapa;
            turma.backUrl = backUrl;
            turma.ExibirIntegralizacao = exibirIntegralizacao;

            var view = GetExternalView(AcademicoExternalViews._SELECAO_TURMA_DEPENDENCY_GRUPO_PROGRAMA);
            return PartialView(view, turma);
        }

        [HttpGet]
        [SMCAllowAnonymous]
        public ActionResult TurmaDesfazerExcluirSelecaoAluno(long seqTurma, string seqSolicitacaoMatricula, long seqProcessoEtapa, string seqConfiguracaoEtapa, string descricao)
        {
            // TASK 41786
            // Caso seja uma etapa que possua um dos tokens:
            // CHANCELA_ALTERACAO_PLANO_ESTUDO, CHANCELA_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA, CHANCELA_DISCIPLINA_ELETIVA_ORIGEM,
            // CHANCELA_DISCIPLINA_ELETIVA_DESTINO, CHANCELA_PLANO_ESTUDO, CHANCELA_RENOVACAO_MATRICULA,
            // não consistir essa regra
            string tokenProcesso = ProcessoEtapaService.BuscarTokenProcessoEtapa(seqProcessoEtapa);
            if (!tokenProcesso.Contains("CHANCELA_ALTERACAO_PLANO_ESTUDO")
                && !tokenProcesso.Contains("CHANCELA_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA")
                && !tokenProcesso.Contains("CHANCELA_DISCIPLINA_ELETIVA_ORIGEM")
                && !tokenProcesso.Contains("CHANCELA_DISCIPLINA_ELETIVA_DESTINO")
                && !tokenProcesso.Contains("CHANCELA_PLANO_ESTUDO")
                && !tokenProcesso.Contains("CHANCELA_RENOVACAO_MATRICULA"))
            {
                List<long> seqsDivisoesTurma = new List<long>();

                var turmas = this.SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(new SMCEncryptedLong(seqSolicitacaoMatricula).Value, 0, null, seqProcessoEtapa, false);
                var turmasMatricula = turmas.SelectMany(s => s.TurmaMatriculaDivisoes).Where(w => w.SeqTurma == seqTurma || w.SituacaoPlanoEstudo != MatriculaPertencePlanoEstudo.Exclusao).ToList();

                if (turmasMatricula != null && turmasMatricula.Count > 0)
                {
                    seqsDivisoesTurma.AddRange(turmasMatricula.Where(w => w.SeqDivisaoTurma.HasValue && !w.DivisoesTurmas.Any(a => seqsDivisoesTurma.Contains(a.Seq))).Select(s => s.SeqDivisaoTurma.Value).ToList());
                }

                var turmaHorario = this.EventoAulaService.ValidarColisaoHorariosDivisoes(seqsDivisoesTurma);
                if (turmaHorario != null && turmaHorario.Count > 0)
                {
                    List<string> descricaoTurmaHorario = new List<string>();
                    descricaoTurmaHorario.AddRange(turmas.Where(w => w.TurmaMatriculaDivisoes.Any(a => a.SeqDivisaoTurma.HasValue && turmaHorario.Contains(a.SeqDivisaoTurma.Value))).Select(l => l.TurmaFormatado).Distinct().ToList());
                    throw new TurmaCoincidenciaHorarioInvalidoException($"</br> {string.Join("</br>", descricaoTurmaHorario)}");
                }
            }

            string erroGravar = SolicitacaoMatriculaItemService.DesfazerRemoverSolicitacaoMatriculaItemPorTurma(new SMCEncryptedLong(seqSolicitacaoMatricula).Value, seqTurma, seqProcessoEtapa, descricao);
            if (!string.IsNullOrEmpty(erroGravar))
                throw new SMCApplicationException(erroGravar);

            return SMCRedirectToUrl(Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoMatricula), seqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) }));
        }

        [HttpGet]
        [SMCAllowAnonymous]
        public ActionResult TurmaAlterarSelecaoAluno(long seqTurma, string seqSolicitacaoMatricula, long seqProcessoEtapa, long seqIngressante, string seqConfiguracaoEtapa)
        {
            var turmaEditar = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(new SMCEncryptedLong(seqSolicitacaoMatricula).Value, seqIngressante, seqTurma, seqProcessoEtapa, false, true).TransformList<TurmaMatriculaItemViewModel>().First();

            if (!(turmaEditar != null && turmaEditar.TurmaMatriculaDivisoes != null))
                throw new SelecionarTurmaInvalidoException();

            if (turmaEditar.TurmaMatriculaDivisoes.Count(c => c.PermitirGrupo && c.DivisoesTurmas.Count() > 1) == 0)
                ThrowOpenModalException(new EditarTurmaSemGrupoInvalidoException().Message);

            if (turmaEditar.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso && turmaEditar.PertencePlanoEstudo == false)
                ThrowOpenModalException(new EditarTurmaGrupoChancelaIndeferidaException().Message);

            turmaEditar.TurmaMatriculaDivisoes.ForEach(d =>
            {
                d.SeqDivisaoTurmaDisplay = d.SeqDivisaoTurma;
                d.SituacaoPlanoEstudo = MatriculaPertencePlanoEstudo.Nenhum;
            });

            Session["SeqIngressanteAlterarTurma"] = seqIngressante;

            turmaEditar.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoMatricula), seqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) });
            turmaEditar.SeqProcessoEtapa = seqProcessoEtapa;
            turmaEditar.SeqSolicitacaoMatricula = new SMCEncryptedLong(seqSolicitacaoMatricula).Value;
            var view = GetExternalView(AcademicoExternalViews._SELECAO_TURMA_OFERTA_EDITAR);
            return PartialView(view, turmaEditar);
        }

        [HttpPost]
        [SMCAllowAnonymous]
        public ActionResult TurmaAlterarSelecaoAlunoSalvar(TurmaMatriculaItemViewModel model)
        {
            model.TurmaMatriculaDivisoes.ForEach(f =>
            {
                if (f.SeqDivisaoTurma == null)
                    f.SeqDivisaoTurma = f.SeqDivisaoTurmaDisplay;
            });

            if (model.TurmaMatriculaDivisoes.Any(a => !a.SeqDivisaoTurma.HasValue))
            {
                throw new SMCApplicationException("É obrigatório selecionar um grupo para a divisão.");
            }
            model.SeqIngressante = (long)Session["SeqIngressanteAlterarTurma"];

            // TASK 41786
            // Caso seja uma etapa que possua um dos tokens:
            // CHANCELA_ALTERACAO_PLANO_ESTUDO, CHANCELA_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA, CHANCELA_DISCIPLINA_ELETIVA_ORIGEM,
            // CHANCELA_DISCIPLINA_ELETIVA_DESTINO, CHANCELA_PLANO_ESTUDO, CHANCELA_RENOVACAO_MATRICULA,
            // não consistir essa regra
            string tokenProcesso = ProcessoEtapaService.BuscarTokenProcessoEtapa(model.SeqProcessoEtapa);
            if (!tokenProcesso.Contains("CHANCELA_ALTERACAO_PLANO_ESTUDO")
                && !tokenProcesso.Contains("CHANCELA_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA")
                && !tokenProcesso.Contains("CHANCELA_DISCIPLINA_ELETIVA_ORIGEM")
                && !tokenProcesso.Contains("CHANCELA_DISCIPLINA_ELETIVA_DESTINO")
                && !tokenProcesso.Contains("CHANCELA_PLANO_ESTUDO")
                && !tokenProcesso.Contains("CHANCELA_RENOVACAO_MATRICULA"))
            {
                List<long> seqsDivisoesTurma = new List<long>();

                var turmas = this.SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(model.SeqSolicitacaoMatricula, model.SeqIngressante, null, model.SeqProcessoEtapa, false, true);
                var turmasMatricula = turmas.Where(w => w.CodigoFormatado != model.CodigoFormatado && !w.Situacao.Contains("exclusão")).SelectMany(s => s.TurmaMatriculaDivisoes).ToList();

                seqsDivisoesTurma.AddRange(model.TurmaMatriculaDivisoes.Where(w => w.SeqDivisaoTurma.HasValue).Select(s => s.SeqDivisaoTurma.Value).ToList());

                if (turmasMatricula != null && turmasMatricula.Count > 0)
                {
                    seqsDivisoesTurma.AddRange(turmasMatricula.Where(w => w.SeqDivisaoTurma.HasValue && !w.DivisoesTurmas.Any(a => seqsDivisoesTurma.Contains(a.Seq))).Select(s => s.SeqDivisaoTurma.Value).ToList());
                }

                var turmaHorario = this.EventoAulaService.ValidarColisaoHorariosDivisoes(seqsDivisoesTurma);
                if (turmaHorario != null && turmaHorario.Count > 0)
                {
                    List<string> descricaoTurmaHorario = new List<string>();
                    if (model.TurmaMatriculaDivisoes.Any(w => turmaHorario.Contains(w.SeqDivisaoTurma.Value)))
                    {
                        descricaoTurmaHorario.Add(model.TurmaFormatado);
                    }
                    descricaoTurmaHorario.AddRange(turmas.Where(w => w.TurmaMatriculaDivisoes.Any(a => a.SeqDivisaoTurma.HasValue && turmaHorario.Contains(a.SeqDivisaoTurma.Value))).Select(l => l.TurmaFormatado).Distinct().ToList());


                    throw new TurmaCoincidenciaHorarioInvalidoException($"</br> {string.Join("</br>", descricaoTurmaHorario)}");
                }
            }
            var pertencePlanoEstudo = SolicitacaoMatriculaItemService.VerificaPertenceAoPlano(model.SeqSolicitacaoMatricula, model.TurmaMatriculaDivisoes.First().SeqDivisaoTurma.Value);
            var erro = SolicitacaoMatriculaItemService.ValidarVagaTurmaAtividadeIngressante(model.TurmaMatriculaDivisoes.TransformList<SolicitacaoMatriculaItemData>(), model.SeqIngressante);
            if (!string.IsNullOrEmpty(erro) && !pertencePlanoEstudo)
                throw new TurmaVagasExcedidasException(erro);

            SolicitacaoMatriculaItemService.AlterarSolicitacaoMatriculaTurmasItens(model.TurmaMatriculaDivisoes.TransformList<SolicitacaoMatriculaItemData>(), model.SeqProcessoEtapa);
            return SMCRedirectToUrl(model.backUrl);
        }

        [HttpGet]
        [SMCAllowAnonymous]
        public ActionResult TurmaExcluirSelecaoAluno(long seqTurma, string seqSolicitacaoMatricula, long seqProcessoEtapa, string seqConfiguracaoEtapa)
        {
            SolicitacaoMatriculaItemService.RemoverSolicitacaoMatriculaItemPorTurma(new SMCEncryptedLong(seqSolicitacaoMatricula).Value, seqTurma, seqProcessoEtapa);

            return SMCRedirectToUrl(Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoMatricula), seqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) }));
        }

        [HttpPost]
        public ActionResult SalvarSelecaoTurmaDisciplinaEletiva(SelecaoTurmaPaginaViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                var validarTurmaIgual = SolicitacaoMatriculaItemService.ValidaSolicitacaoMatriculaItemTurmaDuplicada(model.SeqSolicitacaoServico);
                if (!validarTurmaIgual.valido)
                    throw new TurmaIgualSelecionadaInvalidoException($"</br> {string.Join("</br>", validarTurmaIgual.mensagemErro)}");

                // Busca os itens selecionados com seus pré/co requisitos
                var registrosValidacao = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(model.SeqSolicitacaoServico, 0, null, model.SeqProcessoEtapa, false)
                    .TransformList<TurmaMatriculaItemViewModel>();

                //Task 30079 quando o token do serviço é DISCIPLINA_ELETIVA não precisa validar parâmetro de ExigirCurso
                if (model.TokenServico == TOKEN_SOLICITACAO_SERVICO.DISCIPLINA_ELETIVA && registrosValidacao.Count == 0)
                {
                    throw new TurmaSemCursoSelecionadoInvalidoException();
                }

                //Task 26215 na disciplina isolada validar se foi alguma turma selecionada
                if (!model.ExigirCurso.GetValueOrDefault() && registrosValidacao.Count == 0)
                {
                    if (!SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemCanceladoPorSolicitante(model.SeqSolicitacaoServico))
                        throw new TurmaSemCursoCanceladoInvalidoException();
                    else
                        throw new TurmaSemCursoSelecionadoInvalidoException();
                }

                // Task 32880: Somente para aluno, verificar se as turmas foram concluídas/dispensadas de acordo com a regra abaixo:
                var turmasCursadas = SolicitacaoMatriculaItemService.VerificarTurmasAprovadasDispensadasSelecaoTurma(model.SeqSolicitacaoServico);
                if (!string.IsNullOrEmpty(turmasCursadas))
                    throw new TurmaJaAprovadaDispensadaException(turmasCursadas);

                //Validar Coincidencia de Horário
                ValidarCoincidenciaHorarioSelecaoTurma(model.SeqSolicitacaoServico, model.SeqProcessoEtapa);
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        [HttpPost]
        public ActionResult SalvarSelecaoTurmaPlanoEstudo(SelecaoTurmaPaginaViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                var validarTodosItens = SolicitacaoMatriculaItemService.ValidaSolicitacaoMatriculaItemCoRequisito(model.SeqSolicitacaoServico, model.SeqPessoaAtuacao, TipoGestaoDivisaoComponente.Turma);

                if (!validarTodosItens.valido)
                    throw new TurmaPreCoRequisitoInvalidoException($"</br> {string.Join("</br>", validarTodosItens.mensagemErro)}");

                var validarTurmaIgual = SolicitacaoMatriculaItemService.ValidaSolicitacaoMatriculaItemTurmaDuplicada(model.SeqSolicitacaoServico);
                if (!validarTurmaIgual.valido)
                    throw new TurmaIgualSelecionadaInvalidoException($"</br> {string.Join("</br>", validarTurmaIgual.mensagemErro)}");

                var validarTurmasDuplicadas = SolicitacaoMatriculaItemService.ValidarTurmasDuplicadas(model.SeqSolicitacaoServico, model.SeqPessoaAtuacao, model.SeqProcessoEtapa, null);
                if (!string.IsNullOrEmpty(validarTurmasDuplicadas))
                {
                    throw new TurmaIgualSelecionadaInvalidoException(validarTurmasDuplicadas);
                }

                if (!model.ExigirCurso.GetValueOrDefault() && !SolicitacaoMatriculaItemService.VerificarTurmasAtividadesCanceladasPlano(model.SeqSolicitacaoServico))
                {
                    // Task 28932: Bug 28930: Independente do motivo da situação do item, considerar a regra.
                    //if (SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemCanceladoPorSolicitante(model.SeqSolicitacaoMatricula))
                    //    throw new TurmaSemCursoCanceladoInvalidoException();
                    //else
                    throw new SelecionarTurmaAtividadeAlteracaoPlanoEstudoInvalidoException();
                }

                // Task 32880: Somente para aluno, verificar se as turmas foram concluídas/dispensadas de acordo com a regra abaixo:
                var turmasCursadas = SolicitacaoMatriculaItemService.VerificarTurmasAprovadasDispensadasSelecaoTurma(model.SeqSolicitacaoServico);
                if (!string.IsNullOrEmpty(turmasCursadas))
                    throw new TurmaJaAprovadaDispensadaException(turmasCursadas);

                //Validar Coincidencia de Horário
                ValidarCoincidenciaHorarioSelecaoTurma(model.SeqSolicitacaoServico, model.SeqProcessoEtapa);
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        public void ValidarCoincidenciaHorarioSelecaoTurma(long seqSolicitacaoMatricula, long seqProcessoEtapa)
        {
            // TASK 41786
            // Caso seja uma etapa que possua um dos tokens:
            // CHANCELA_ALTERACAO_PLANO_ESTUDO, CHANCELA_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA, CHANCELA_DISCIPLINA_ELETIVA_ORIGEM,
            // CHANCELA_DISCIPLINA_ELETIVA_DESTINO, CHANCELA_PLANO_ESTUDO, CHANCELA_RENOVACAO_MATRICULA,
            // não consistir essa regra
            string tokenProcesso = ProcessoEtapaService.BuscarTokenProcessoEtapa(seqProcessoEtapa);
            if (!tokenProcesso.Contains("CHANCELA_ALTERACAO_PLANO_ESTUDO")
                && !tokenProcesso.Contains("CHANCELA_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA")
                && !tokenProcesso.Contains("CHANCELA_DISCIPLINA_ELETIVA_ORIGEM")
                && !tokenProcesso.Contains("CHANCELA_DISCIPLINA_ELETIVA_DESTINO")
                && !tokenProcesso.Contains("CHANCELA_PLANO_ESTUDO")
                && !tokenProcesso.Contains("CHANCELA_RENOVACAO_MATRICULA"))
            {
                List<long> seqsDivisoesTurma = new List<long>();

                var turmas = this.SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(seqSolicitacaoMatricula, 0, null, seqProcessoEtapa, false);
                var turmasMatricula = turmas.Where(w => !w.Situacao.Contains("exclusão")).SelectMany(s => s.TurmaMatriculaDivisoes).ToList();

                if (turmasMatricula != null && turmasMatricula.Count > 0)
                {
                    seqsDivisoesTurma.AddRange(turmasMatricula.Where(w => w.SeqDivisaoTurma.HasValue && !w.DivisoesTurmas.Any(a => seqsDivisoesTurma.Contains(a.Seq))).Select(s => s.SeqDivisaoTurma.Value).ToList());
                }

                var turmaHorario = this.EventoAulaService.ValidarColisaoHorariosDivisoes(seqsDivisoesTurma);
                if (turmaHorario != null && turmaHorario.Count > 0)
                {
                    List<string> descricaoTurmaHorario = new List<string>();
                    descricaoTurmaHorario.AddRange(turmas.Where(w => w.TurmaMatriculaDivisoes.Any(a => a.SeqDivisaoTurma.HasValue && turmaHorario.Contains(a.SeqDivisaoTurma.Value))).Select(l => l.TurmaFormatado).Distinct().ToList());
                    throw new TurmaCoincidenciaHorarioInvalidoException($"</br> {string.Join("</br>", descricaoTurmaHorario)}");
                }
            }
        }

        #endregion [ Seleção de Turma ]

        #region [ Seleção Atividade Acadêmica ]

        protected ActionResult SelecaoAtividadeAcademicaPlanoEstudo(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            //if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_31_01.PAGINA_SELECAO_ATIVIDADES_ACADEMICAS))
            //    throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new SelecaoAtividadeAcademicaViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarAtividadeAcademicaPlanoEstudo), historico.Seq);

            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            model.SeqProcesso = filtro.SeqProcesso;
            model.SeqSolicitacaoServico = filtro.SeqSolicitacaoServico;
            model.SeqProcessoEtapa = SolicitacaoMatriculaService.BuscarProcessoEtapaPorSolicitacaoMatricula(filtro.SeqSolicitacaoServico, filtro.SeqConfiguracaoEtapa);
            model.EtapaFinalizada = filtro.EtapaFinalizada;
            model.TokenServico = filtro.TokenServico;
            model.SeqConfiguracaoEtapa = filtro.SeqConfiguracaoEtapa;

            //Exibir o botão de integralização curricular
            model.ExibirIntegralizacao = HistoricoEscolarService.VisualizarConsultaConcederFormacaoIntegralizacao(filtro.SeqPessoaAtuacao);

            // Cria o modelo para a página em questão
            model.ExibirCancelados = SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaTurmasCancelada(model.SeqSolicitacaoServico) || SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemCancelado(model.SeqSolicitacaoServico);

            model.AtividadesAcademicaMatriculaItem = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaAtividadesItens(model.SeqSolicitacaoServico, filtro.SeqPessoaAtuacao, model.SeqProcessoEtapa, null).TransformList<AtividadeAcademicaMatriculaItemViewModel>();
            model.ExibirPertencePlanoEstudo = true;

            model.AtividadesAcademicaMatriculaItem.SMCForEach(f =>
            {
                f.SeqProcessoEtapa = model.SeqProcessoEtapa;
                f.ExibirMatriculaPertence = true;
                if (f.SituacaoInicial == true && f.SituacaoFinal == true && f.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                    f.MatriculaPertence = MatriculaPertencePlanoEstudo.Inclusao;
                else if (f.PertencePlanoEstudo == true && f.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado && f.Motivo == MotivoSituacaoMatricula.PeloSolicitante)
                    f.MatriculaPertence = MatriculaPertencePlanoEstudo.Exclusao;
                else if (f.PertencePlanoEstudo == true && f.SituacaoInicial == true && f.SituacaoFinal == true && f.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado)
                    f.MatriculaPertence = MatriculaPertencePlanoEstudo.NaoAlterado;
                else
                    f.MatriculaPertence = MatriculaPertencePlanoEstudo.Nenhum;
            });

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            var view = GetExternalView(AcademicoExternalViews.SOLICITACAO_SELECAO_ATIVIDADE_ACADEMICA);
            return View(view, model);
        }

        [HttpPost]
        public ActionResult SalvarAtividadeAcademicaPlanoEstudo(SelecaoAtividadeAcademicaViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                var validarTodosItens = SolicitacaoMatriculaItemService.ValidaSolicitacaoMatriculaItemCoRequisito(model.SeqSolicitacaoServico, model.SeqPessoaAtuacao);

                if (!validarTodosItens.valido)
                    throw new ConfiguracaoComponenteCoRequisitoInvalidoException($"</br> {string.Join("</br>", validarTodosItens.mensagemErro)}");

                if (!(SolicitacaoMatriculaItemService.VerificarTurmasAtividadesCanceladasPlano(model.SeqSolicitacaoServico)))
                    throw new SelecionarTurmaAtividadeAlteracaoPlanoEstudoInvalidoException();
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        [HttpGet]
        [SMCAllowAnonymous]
        public ActionResult AtividadeDesfazerExcluirSelecaoAluno(long seqSolicitacaoMatriculaItem, long seqProcessoEtapa, string seqSolicitacaoServico, string seqConfiguracaoEtapa)
        {
            SolicitacaoMatriculaItemService.DesfazerRemoverSolicitacaoMatriculaItemPorAtividade(seqSolicitacaoMatriculaItem, seqProcessoEtapa);

            return SMCRedirectToUrl(Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) }));
        }

        [HttpGet]
        [SMCAllowAnonymous]
        public ActionResult AtividadeExcluirSelecaoAluno(long seqSolicitacaoMatriculaItem, long seqProcessoEtapa, string seqSolicitacaoServico, string seqConfiguracaoEtapa)
        {
            SolicitacaoMatriculaItemService.AlterarSolicitacaoMatriculaItemParaCancelado(seqSolicitacaoMatriculaItem, seqProcessoEtapa, MotivoSituacaoMatricula.PeloSolicitante);

            return SMCRedirectToUrl(Url.Action("EntrarEtapa", new { SeqSolicitacaoServico = new SMCEncryptedLong(seqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) }));
        }

        #endregion [ Seleção Atividade Acadêmica ]

        #region [ Confirmação Solicitação Matricula ]

        protected ActionResult ConfirmacaoSolicitacaoMatricula(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_26_01.PAGINA_CONFIRMACAO_PROCESSOS))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new ConfirmacaoTurmaAtividadeViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarConfirmacaoSolicitacaoMatricula), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });
            model.SeqProcesso = filtro.SeqProcesso;
            model.SeqSolicitacaoServico = filtro.SeqSolicitacaoServico;
            model.SeqProcessoEtapa = SolicitacaoMatriculaService.BuscarProcessoEtapaPorSolicitacaoMatricula(filtro.SeqSolicitacaoServico, filtro.SeqConfiguracaoEtapa);
            model.Justificativa = SolicitacaoServicoService.BuscarSolicitacaoServicoJustificativaComplementar(filtro.SeqSolicitacaoServico);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            //Recupera os registros da solicitação de matrícula
            //Verifica o token para utilizar o filtros de acordo com o fluxo principal da solicitação
            if (filtro.TokenServico == TOKEN_SOLICITACAO_SERVICO.DISCIPLINA_ELETIVA ||
                filtro.TokenServico == TOKEN_SOLICITACAO_SERVICO.ALTERACAO_PLANO_ESTUDO_DI)
            {
                model.TurmasMatriculaItem = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(model.SeqSolicitacaoServico, filtro.SeqPessoaAtuacao, null, model.SeqProcessoEtapa, true).TransformList<TurmaMatriculaItemViewModel>();
                model.ExibirAtividade = false;
            }
            else if (filtro.TokenServico == TOKEN_SOLICITACAO_SERVICO.ALTERACAO_PLANO_ESTUDO)
            // Segundo a Jéssica, DI Não seleciona atividade acadêmica.
            // || filtro.TokenServico == TOKEN_SOLICITACAO_SERVICO.ALTERACAO_PLANO_ESTUDO_DI)
            {
                model.TurmasMatriculaItem = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(model.SeqSolicitacaoServico, filtro.SeqPessoaAtuacao, null, model.SeqProcessoEtapa, true).TransformList<TurmaMatriculaItemViewModel>();
                model.AtividadesAcademicaMatriculaItem = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaAtividadesItens(model.SeqSolicitacaoServico, model.SeqPessoaAtuacao, model.SeqProcessoEtapa, null).TransformList<AtividadeAcademicaMatriculaItemViewModel>();
                model.ExibirAtividade = true;
            }

            model.TokenServico = filtro.TokenServico;

            model.TurmasMatriculaItem.SMCForEach(f =>
            {
                f.ExigirCurso = model.ExigirCurso;
                f.ExigirMatrizCurricularOferta = model.ExigirMatrizCurricularOferta;
                f.SeqProcessoEtapa = model.SeqProcessoEtapa;
            });

            if (model.ExibirCancelados.HasValue &&
                model.ExibirCancelados.Value &&
                model.TurmasMatriculaItem.Count == 0 &&
                model.ExigirMatrizCurricularOferta == false)
            {
                model.TodosCancelados = !SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemCanceladoPorSolicitante(model.SeqSolicitacaoServico);
            }

            var view = GetExternalView(AcademicoExternalViews.CONFIRMACAO_TURMA);
            return View(view, model);
        }

        [HttpPost]
        public ActionResult SalvarConfirmacaoSolicitacaoMatricula(SelecaoTurmaPaginaViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                /*if (model.TokenServico == TOKEN_SOLICITACAO_SERVICO.DISCIPLINA_ELETIVA)
                    Assert(model, UIResource.Texto_Confirmar_Disciplina_Eletiva);
                else if (model.TokenServico == TOKEN_SOLICITACAO_SERVICO.ALTERACAO_PLANO_ESTUDO || model.TokenServico == TOKEN_SOLICITACAO_SERVICO.ALTERACAO_PLANO_ESTUDO_DI)
                    Assert(model, UIResource.Texto_Confirmar_Alteracao_Plano);
                */
                if (!string.IsNullOrEmpty(model.Justificativa))
                    SolicitacaoServicoService.AtualizarSolicitacaoServicoJustificativaComplementar(model.SeqSolicitacaoServico, model.Justificativa);

                SolicitacaoServicoService.SalvarConfirmacaoSolicitacaoPadrao(model.SeqSolicitacaoServico, model.SeqPessoaAtuacao, model.SeqConfiguracaoEtapa);
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        #endregion [ Confirmação Solicitação Matricula ]

        #region [ Chancela ]

        public virtual ActionResult SelecaoChancelaPlanoEstudo(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = SolicitacaoMatriculaService.BuscarSolicitacaoMatriculaChancela(filtro.SeqSolicitacaoServico, filtro.TokenEtapa, filtro.Orientador, true).Transform<SelecaoChancelaPlanoViewModel>();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarChancelaPlanoEstudo), historico.Seq);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            SMCEncryptedLong seqHistoricoSolicitacao = model.SeqSolicitacaoHistoricoNavegacao;

            //Exibir o botão de integralização curricular
            model.ExibirIntegralizacao = filtro.TokenEtapa != MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_DESTINO
                                         && HistoricoEscolarService.VisualizarConsultaConcederFormacaoIntegralizacao(filtro.SeqPessoaAtuacao);

            model.ExibirCancelados = SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaTurmasCancelada(model.SeqSolicitacaoServico) || SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemCancelado(model.SeqSolicitacaoServico);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });
            model.TokenEtapa = filtro.TokenEtapa;
            model.Orientador = filtro.Orientador;
            model.TokenServico = filtro.TokenServico;

            var exibirLegenda = model.Atividades.FirstOrDefault(w => w.LegendaPertencePlanoEstudo == true) == null ? false : true;
            foreach (var item in model.Atividades)
            {
                item.LegendaPertencePlanoEstudo = exibirLegenda;
            }

            exibirLegenda = model.Turmas.FirstOrDefault(w => w.LegendaPertencePlanoEstudo == true) == null ? false : true;

            bool divisaoTurmaExibirLegenda = model.Turmas.Any(c => c.TurmaMatriculaDivisoes.Any(d => d.PertencePlanoEstudo == true));

            if (!exibirLegenda && divisaoTurmaExibirLegenda)
                exibirLegenda = divisaoTurmaExibirLegenda;

            foreach (var item in model.Turmas)
            {
                item.LegendaPertencePlanoEstudo = exibirLegenda;
            }

            var view = GetExternalView(AcademicoExternalViews.CHANCELA_PLANO);
            return View(view, model);
        }

        [HttpPost]
        [SMCAuthorize(UC_MAT_003_22_01.CHANCELAR_PLANO_ESTUDO)]
        public ActionResult SalvarChancelaPlanoEstudo(SelecaoChancelaPlanoViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                try
                {
                    var registro = model.Transform<ChancelaData>();

                    long seqItemInicio = model.SeqSituacaoInicioChancela.Value;
                    if ((model.Turmas != null && model.Turmas.SelectMany(t => t.TurmaMatriculaDivisoes).SMCAny(t => t.SeqSituacaoItemMatricula == 0 || t.SeqSituacaoItemMatricula == seqItemInicio)) ||
                        model.Atividades.SMCAny(t => t.SeqSituacaoItemMatricula == 0 || t.SeqSituacaoItemMatricula == seqItemInicio))
                        throw new SMCApplicationException(UIResource.Texto_Chancela_Item_Aguardando);

                    long seqItemDeferido = model.SeqSituacaoFinalSucessoChancela.Value;

                    /// 6. Verificar se existe pelo menos um item deferido, de acordo com a regra:
                    /// Se pelo menos um item da solicitação possuir o valor do campo "Pertence ao plano de estudo" igual a "Sim":
                    /// 6.1.Verificar se pelo menos um item NÃO está com a situação configurada de acordo com a etapa para ser final
                    /// com a classificação "Finalizada com sucesso".Caso ocorra:
                    /// 6.1.1.Se o token da etapa é “CHANCELA_ALTERACAO_PLANO_ESTUDO” ou “CHANCELA_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA”,
                    /// exibir mensagem de confirmação:
                    /// "Não foram realizadas alterações no plano de estudo do(a) aluno(a) <Nome da pessoa-atuação>.
                    /// Confirma a chancela ?”
                    /// Caso seja selecionado "Sim": NÃO criar um novo plano de estudo.
                    /// Caso seja selecionado "Não": Abortar a operação e permanecer na página em questão.
                    /// 6.1.2.E o token da etapa for “CHANCELA_DISCIPLINA_ELETIVA_DESTINO”, NÃO criar um novo plano de estudo.
                    if (model.TokenServico == TOKEN_SOLICITACAO_SERVICO.ALTERACAO_PLANO_ESTUDO
                        || model.TokenServico == TOKEN_SOLICITACAO_SERVICO.ALTERACAO_PLANO_ESTUDO_DI
                        || model.TokenEtapa == MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_DESTINO)
                    {
                        if ((model.Turmas == null || !model.Turmas.SelectMany(t => t.TurmaMatriculaDivisoes).Any(t => t.SeqSituacaoItemMatricula == seqItemDeferido)) &&
                            (model.Atividades == null || !model.Atividades.Any(t => t.SeqSituacaoItemMatricula == seqItemDeferido)))
                        {
                            registro.NaoAtualizarPlano = true;

                            if (model.TokenEtapa != MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_DESTINO)
                                Assert(model, string.Format(UIResource.Texto_Chancela_Alterar_Plano_Sem_Alteracao, model.Nome));
                        }

                        // Task 36544 - Alterar implementação UC_MAT_003_22_01 - Chancela de Plano de Estudo
                        // NV14 - Verificar se a atividade academica já possui histórico. Caso possua, exibir assert informando ao usuário
                        /*1.2. Caso estiver e for uma atividade acadêmica, verificar se existe registro no histórico escolar  do aluno para o componente que está sendo excluído para o ciclo letivo do processo em questão, com situação "Aprovado", 'Reprovado" ou "Aluno sem nota". Caso existir, enviar a seguinte mensagem de confirmação:
                            "Existe lançamento de nota no histórico escolar para o(s) item(ns) abaixo. Tem certeza que deseja removê-lo do plano de estudo do aluno?
                            - <Descrição da configuração de componente 1>
                            - <Descrição da configuração de componente 2>"
                        */
                        if (model.Atividades != null && model.Atividades.Any())
                        {
                            var atividaesAcademicasComHistorico = SolicitacaoMatriculaService.VerificarChancelaExclusaoAtividadesAcademicasComHistoricoEscolar(model.SeqSolicitacaoServico, model.SeqConfiguracaoEtapa, model.Atividades.Select(a => (a.SeqItem, a.SeqSituacaoItemMatricula)).ToList(), false);
                            if (atividaesAcademicasComHistorico != null && atividaesAcademicasComHistorico.Any())
                                Assert(model, string.Format(UIResource.Mensagem_Aluno_Existe_Historico_Escolar_Atividade_Academica, string.Join("<br />", atividaesAcademicasComHistorico)));
                        }

                        // TASK 43223
                        /* 1.1.Todos os itens da solicitação cuja a situação ATUAL seja uma situação configurada de acordo com a etapa para ser a final e com a classificação “não alterado”, verificar:
	                       1.1.1.Se o item está no plano de estudo atual do ciclo letivo do processo.
                           1.1.1.1.Se estiver, incluí - los no plano de estudo;
                           1.1.1.2.Se não estiver, não incluí - los e enviar uma mensagem:
			               “A turma<Descrição da turma*> não pertence mais ao plano de estudos do aluno(a), portanto ele(a) não será matriculado nela.”*/
                        var itensPlanoAtual = PlanoEstudoItemService.BuscarSeqsItensPlanoEstudoAtualAluno(model.SeqPessoaAtuacao, model.SeqCicloLetivo);
                        var turmasNaoAlterado = model.Turmas != null ? model.Turmas.SelectMany(t => t.TurmaMatriculaDivisoes).Where(w => w.PertencePlanoEstudo && w.SituacaoPlanoEstudo == MatriculaPertencePlanoEstudo.NaoAlterado).ToList() : new List<SelecaoChancelaPlanoTurmaDivisoesViewModel>();
                        var atividadesNaoAlterado = model.Atividades != null ? model.Atividades.Where(w => w.SituacaoPlanoEstudo == MatriculaPertencePlanoEstudo.NaoAlterado).ToList() : new List<SelecaoChancelaPlanoAtividadeViewModel>();

                        if ((turmasNaoAlterado != null && turmasNaoAlterado.Count() > 0) ||
                            (atividadesNaoAlterado != null && atividadesNaoAlterado.Count() > 0))
                        {
                            List<string> descricaoItem = new List<string>();
                            var turmasRetirada = turmasNaoAlterado.Where(w => !itensPlanoAtual.Any(a => a.SeqDivisaoTurma == w.SeqDivisaoTurma)).ToList();
                            turmasRetirada.ForEach(f =>
                            {
                                var turmaDescricao = model.Turmas.Where(w => w.TurmaMatriculaDivisoes.Any(a => a.SeqDivisaoTurma == f.SeqDivisaoTurma)).FirstOrDefault().TurmaFormatado;
                                descricaoItem.Add(turmaDescricao);
                            });

                            var atividadeRetirada = atividadesNaoAlterado.Where(w => !itensPlanoAtual.Any(a => a.SeqConfiguracaoComponente == w.SeqItem)).ToList();
                            atividadeRetirada.ForEach(f =>
                            {
                                descricaoItem.Add(f.Descricao);
                            });

                            if (descricaoItem.Count() > 0)
                            {
                                Assert(model, string.Format(UIResource.Mensagem_Assert_Plano_NaoAlterado_Removido, string.Join("<br />", descricaoItem)));
                            }
                        }
                    }

                    // TASK 41792
                    if (model.Turmas != null && model.Turmas.Count > 0)
                    {
                        List<long> seqsDivisoesTurma = new List<long>();

                        var turmas = model.Turmas.SelectMany(t => t.TurmaMatriculaDivisoes).Where(w => w.SeqSituacaoItemMatricula == seqItemDeferido || (w.SeqSituacaoItemMatricula != seqItemDeferido && w.PertencePlanoEstudo)).ToList();
                        if (turmas != null && turmas.Count > 0)
                        {
                            turmas.ForEach(f =>
                            {
                                if (f.SeqDivisaoTurma.HasValue && (f.SeqSituacaoItemMatricula == seqItemDeferido || f.SituacaoPlanoEstudo == MatriculaPertencePlanoEstudo.NaoAlterado))
                                {
                                    if (f.SituacaoPlanoEstudo != MatriculaPertencePlanoEstudo.Exclusao)
                                        seqsDivisoesTurma.Add(f.SeqDivisaoTurma.Value);
                                }
                                else
                                {
                                    if (f.SeqDivisaoTurma.HasValue)
                                    {
                                        if (!turmas.Any(w => w.SeqDivisaoComponente == f.SeqDivisaoComponente && w.SeqSituacaoItemMatricula == seqItemDeferido))
                                        {
                                            var divisaogrupo = turmas.Where(w => w.SeqDivisaoComponente == f.SeqDivisaoComponente && w.SituacaoPlanoEstudo == MatriculaPertencePlanoEstudo.Exclusao).FirstOrDefault();
                                            if (divisaogrupo != null)
                                            {
                                                seqsDivisoesTurma.Add(divisaogrupo.SeqDivisaoTurma.Value);
                                            }
                                        }
                                    }
                                }
                            });

                            List<SolicitacaoMatriculaItemData> planoTurmas = new List<SolicitacaoMatriculaItemData>();
                            if (model.TokenEtapa == MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_ORIGEM || model.TokenEtapa == MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_DESTINO)
                            {
                                planoTurmas = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaItensPlano(model.SeqSolicitacaoServico).Where(w => w.PertencePlanoEstudo == true && w.SeqDivisaoTurma.HasValue && !seqsDivisoesTurma.Contains(w.SeqDivisaoTurma.Value)).ToList();
                                seqsDivisoesTurma.AddRange(planoTurmas.Select(s => s.SeqDivisaoTurma.Value).ToList());
                            }

                            var turmaHorario = this.EventoAulaService.ValidarColisaoHorariosDivisoes(seqsDivisoesTurma);
                            if (turmaHorario != null && turmaHorario.Count > 0)
                            {
                                List<string> descricaoTurmaHorario = new List<string>();
                                descricaoTurmaHorario.AddRange(model.Turmas.Where(w => w.TurmaMatriculaDivisoes.Any(a => a.SeqDivisaoTurma.HasValue && turmaHorario.Contains(a.SeqDivisaoTurma.Value))).Select(l => l.TurmaFormatado).Distinct().ToList());

                                if (model.TokenEtapa == MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_ORIGEM || model.TokenEtapa == MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_DESTINO)
                                {
                                    var seqsTurmaDescricao = planoTurmas.Where(w => w.SeqDivisaoTurma.HasValue && turmaHorario.Contains(w.SeqDivisaoTurma.Value)).Select(s => s.SeqTurma.Value).ToList();

                                    seqsTurmaDescricao.ForEach(d =>
                                    {
                                        descricaoTurmaHorario.Add(this.TurmaService.BuscarDescricaoTurmaConcatenado(d));
                                    });
                                }
                                throw new TurmaCoincidenciaHorarioInvalidoException($"</br> {string.Join("</br>", descricaoTurmaHorario)}");
                            }
                        }
                    }

                    // Se for disciplina eletiva, desabilita o filtro de dados, pois a pessoa pode não ter acesso as informações
                    // do aluno/disciplinas do outro programa.
                    bool desabilitarFiltro = model.TokenEtapa == MatriculaTokens.CHANCELA_DISCIPLINA_ELETIVA_DESTINO;

                    // Chama rotina para salvar a chancela
                    SolicitacaoMatriculaService.SalvarChancelaMatricula(registro, model.TokenEtapa, desabilitarFiltro);
                    SetSuccessMessage("Chancela efetuada com sucesso!", "Sucesso", SMCMessagePlaceholders.Centro);
                }
                catch (Exception ex)
                {
                    SetErrorMessage(ex.Message, "Erro", SMCMessagePlaceholders.Centro);
                    throw ex;
                    //return SMCRedirectToUrl(model.backUrl);
                }
            }

            // Atualiza a data de saida no histórico de navegação de páginas
            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);

            // Retorna para a página que chamou
            if (model.Orientador)
                return SMCRedirectToAction("Index", "ChancelaRoute", new { area = "" });
            else
                return SMCRedirectToAction("Index", "SolicitacaoServico", new { area = "SRC" });
        }

        #endregion [ Chancela ]

        #region [ Efetivação ]

        [SMCAuthorize(UC_MAT_003_18_01.PAGINA_SELECAO_TURMAS_SGAADMINISTRATIVO)]
        private ActionResult ConfirmacaoSelecaoTurma(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_18_01.PAGINA_SELECAO_TURMAS_SGAADMINISTRATIVO))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new SelecaoTurmaPaginaViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarConfirmacaoSelecaoTurma), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });
            model.SeqProcesso = filtro.SeqProcesso;

            //Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            var parametrosValidacao = InstituicaoNivelTipoVinculoAlunoService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(filtro.SeqPessoaAtuacao);
            model.SeqProcessoEtapa = (SMCEncryptedLong)SolicitacaoMatriculaService.BuscarProcessoEtapaPorSolicitacaoMatricula(filtro.SeqSolicitacaoServico, filtro.SeqConfiguracaoEtapa);
            model.ExigirCurso = parametrosValidacao.ExigeCurso;
            model.ExigirMatrizCurricularOferta = parametrosValidacao.ExigeOfertaMatrizCurricular;
            model.EtapaFinalizada = filtro.EtapaFinalizada;

            //Exibir o botão de integralização curricular
            model.ExibirIntegralizacao = HistoricoEscolarService.VisualizarConsultaConcederFormacaoIntegralizacao(filtro.SeqPessoaAtuacao);

            model.ExibirCancelados = SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaTurmasCancelada(filtro.SeqSolicitacaoServico) || SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemCancelado(filtro.SeqSolicitacaoServico);
            model.TurmasMatriculaItem = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(filtro.SeqSolicitacaoServico, filtro.SeqPessoaAtuacao, null, model.SeqProcessoEtapa, true).TransformList<TurmaMatriculaItemViewModel>();
            model.TurmasMatriculaItem.SMCForEach(f =>
            {
                f.ExigirCurso = model.ExigirCurso;
                f.ExigirMatrizCurricularOferta = model.ExigirMatrizCurricularOferta;
                f.SeqProcessoEtapa = model.SeqProcessoEtapa;
            });

            if (model.ExibirCancelados.HasValue &&
                model.ExibirCancelados.Value &&
                model.TurmasMatriculaItem.Count == 0 &&
                model.ExigirMatrizCurricularOferta == false)
            {
                model.TodosCancelados = !SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemCanceladoPorSolicitante(model.SeqSolicitacaoServico);
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            var view = GetExternalView(AcademicoExternalViews.SOLICITACAO_SELECAO_TURMA_EFETIVACAO);
            return View(view, model);
        }

        [HttpPost]
        [SMCAuthorize(UC_MAT_003_18_01.PAGINA_SELECAO_TURMAS_SGAADMINISTRATIVO)]
        public ActionResult SalvarConfirmacaoSelecaoTurma(SelecaoTurmaPaginaViewModel model, string tokenRet)
        {
            var tokenPagina = ConfiguracaoEtapaPaginaService.BuscarConfiguracoesEtapaPaginaPorSeqConfiguracaoEtapa(model.SeqConfiguracaoEtapa);

            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                var validarTodosItens = SolicitacaoMatriculaItemService.ValidaSolicitacaoMatriculaItemPreCoRequisito(model.SeqSolicitacaoServico, model.SeqPessoaAtuacao, TipoGestaoDivisaoComponente.Turma);

                if (!validarTodosItens.valido)
                    throw new TurmaPreCoRequisitoInvalidoException($"</br> {string.Join("</br>", validarTodosItens.mensagemErro)}");

                // Bug 32665: Solicitaram retirar a validação de turma repetida ao salvar.
                //var validarTurmaIgual = SolicitacaoMatriculaItemService.ValidaSolicitacaoMatriculaItemTurmaDuplicada(model.SeqSolicitacaoServico);
                //if (!validarTurmaIgual.valido)
                //	throw new TurmaIgualSelecionadaInvalidoException($"</br> {string.Join("</br>", validarTurmaIgual.mensagemErro)}");


                if (!tokenPagina.Any(x => x.TokenPagina.Equals("SELECAO_ATIVIDADE_ACADEMICA_MATRICULA") || x.TokenPagina.Equals("CONFIRMACAO_SELECAO_ATIVIDADE_ACADEMICA")))
                {
                    var registrosValidacao = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(model.SeqSolicitacaoServico, model.SeqPessoaAtuacao, null, null, false).ToList();
                    if (!registrosValidacao.Any(x => (x.SituacaoInicial.HasValue && x.SituacaoInicial.Value)
                     && (x.SituacaoFinal.HasValue && x.SituacaoFinal.Value) && x.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso && (x.PertencePlanoEstudo.HasValue && !x.PertencePlanoEstudo.Value)))
                    {
                        throw new TurmaSelecaoObrigatoriaException();
                    }
                }

                if (!model.ExigirCurso.GetValueOrDefault() && !SolicitacaoMatriculaItemService.VerificarTurmasAtividadesCadastradas(model.SeqSolicitacaoServico))
                {
                    // Task 28932: Bug 28930: Independente do motivo da situação do item, considerar a regra.
                    //if (SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemCanceladoPorSolicitante(model.SeqSolicitacaoMatricula))
                    //    throw new TurmaSemCursoCanceladoInvalidoException();
                    //else
                    throw new TurmaSemCursoSelecionadoInvalidoException();
                }

                //Valida se ja foi aprovado ou dispensado na turma 
                var turmasAprovadasDispensadas = SolicitacaoMatriculaItemService.VerificarTurmasAprovadasDispensadasSelecaoTurma(model.SeqSolicitacaoServico);
                if (!string.IsNullOrEmpty(turmasAprovadasDispensadas))
                {
                    throw new TurmaJaAprovadaDispensadaException(turmasAprovadasDispensadas);
                }
            }
            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        [SMCAuthorize(UC_MAT_003_32_01.PAGINA_SELECAO_ATIVIDADES_ACADEMICAS_SGAADMINISTRATIVO)]
        private ActionResult ConfirmacaoSelecaoAtividadeAcademica(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_32_01.PAGINA_SELECAO_ATIVIDADES_ACADEMICAS_SGAADMINISTRATIVO))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new ConfirmacaoSelecaoAtividadeAcademicaViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarConfirmacaoSelecaoAtividadeAcademica), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            model.SeqProcesso = filtro.SeqProcesso;
            model.SeqSolicitacaoServico = filtro.SeqSolicitacaoServico;
            model.SeqProcessoEtapa = SolicitacaoMatriculaService.BuscarProcessoEtapaPorSolicitacaoMatricula(filtro.SeqSolicitacaoServico, filtro.SeqConfiguracaoEtapa);
            model.EtapaFinalizada = filtro.EtapaFinalizada;
            model.TokenServico = filtro.TokenServico;

            model.ExibirCancelados = SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaTurmasCancelada(model.SeqSolicitacaoServico) || SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemCancelado(model.SeqSolicitacaoServico);
            model.AtividadesAcademicaMatriculaItem = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaAtividadesItens(model.SeqSolicitacaoServico, model.SeqPessoaAtuacao, model.SeqProcessoEtapa, ClassificacaoSituacaoFinal.FinalizadoComSucesso).TransformList<AtividadeAcademicaMatriculaItemViewModel>();

            model.AtividadesAcademicaMatriculaItem.SMCForEach(f => f.SeqProcessoEtapa = model.SeqProcessoEtapa);

            model.ExibirPertencePlanoEstudo = false;

            //Exibir o botão de integralização curricular
            model.ExibirIntegralizacao = HistoricoEscolarService.VisualizarConsultaConcederFormacaoIntegralizacao(filtro.SeqPessoaAtuacao);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            //model.proxim

            var view = GetExternalView(AcademicoExternalViews.SOLICITACAO_SELECAO_ATIVIDADE_ACADEMICA);
            return View(view, model);
        }

        [HttpPost]
        [SMCAuthorize(UC_MAT_003_32_01.PAGINA_SELECAO_ATIVIDADES_ACADEMICAS_SGAADMINISTRATIVO)]
        public ActionResult SalvarConfirmacaoSelecaoAtividadeAcademica(SelecaoAtividadeAcademicaViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                // Busca as atividades já selecionadas para a validação
                var validarTodosItens = SolicitacaoMatriculaItemService.ValidaSolicitacaoMatriculaItemPreCoRequisito(model.SeqSolicitacaoServico, model.SeqPessoaAtuacao);

                if (!validarTodosItens.valido)
                    throw new ConfiguracaoComponenteCoRequisitoInvalidoException($"</br> {string.Join("</br>", validarTodosItens.mensagemErro)}");

                //Valida se ja foi aprovado ou dispensado na turma 
                var turmasAprovadasDispensadas = SolicitacaoMatriculaItemService.VerificarTurmasAprovadasDispensadasSelecaoTurma(model.SeqSolicitacaoServico);
                if (!string.IsNullOrEmpty(turmasAprovadasDispensadas))
                {
                    throw new TurmaJaAprovadaDispensadaException(turmasAprovadasDispensadas);
                }
            }

            if (string.IsNullOrWhiteSpace(tokenRet) &&
                !(model.AtividadesAcademicaMatriculaItem != null && model.AtividadesAcademicaMatriculaItem.Count > 0) &&
                !(SolicitacaoMatriculaItemService.VerificarTurmasAtividadesCadastradas(model.SeqSolicitacaoServico)))
                throw new SelecionarAtividadeInvalidoException();

            // Caso não esteja voltando, significa que o usuário clicou em concluir e está aderindo aos termos
            // Toda essa sessão foi comentada pois os dois tokens da condiçã oagora também tem página de registro de documentos
            //if (string.IsNullOrWhiteSpace(tokenRet) && model.TokenServico == TOKEN_SERVICO.MATRICULA_REABERTURA) //ANTIGO -> (model.TokenServico == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU || model.TokenServico == TOKEN_SERVICO.MATRICULA_REABERTURA))
            //{
            //    if (model.AtividadesAcademicaMatriculaItem != null && model.AtividadesAcademicaMatriculaItem.Any())
            //    {
            //        var atividaesAcademicasComHistorico = SolicitacaoMatriculaService.VerificarEfetivacaoAtividadesAcademicasComHistoricoEscolar(model.SeqSolicitacaoServico, model.SeqConfiguracaoEtapa, model.AtividadesAcademicaMatriculaItem.Select(a => a.Seq).ToList());
            //        if (atividaesAcademicasComHistorico != null && atividaesAcademicasComHistorico.Any())
            //            throw new AtividadeAcademicaComHistoricoEscolarException(atividaesAcademicasComHistorico);
            //    }

            //    // Caso já exista um aluno para esta pessoa na mesma oferta e faça disciplina isolada, pergunta se deseja matricular apenas incluindo as novas turmas no aluno anterior
            //    //Assert(model, "Mensagem_Aluno_Rematricula_Efetivar");

            //    // Faz a efetivação da matrícula
            //    SolicitacaoMatriculaService.EfetivarRenovacaoMatricula(model.Transform<EfetivacaoMatriculaData>());
            //}

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        private ActionResult EfetivacaoMatricula(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_19_01.PAGINA_INSTRUCOES_FINAIS_SGAADMINISTRATIVO))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro, true);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new SolicitacaoInstrucaoFinalEfetivacaoViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarEfetivacaoMatricula), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoMatricula = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });
            model.SeqProcesso = filtro.SeqProcesso;

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoMatricula = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            model.SeqIngressante = filtro.SeqPessoaAtuacao;
            model.SeqSolicitacaoServico = filtro.SeqSolicitacaoServico;
            model.EtapaFinalizada = filtro.EtapaFinalizada;

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            var view = GetExternalView(AcademicoExternalViews.SOLICITACAO_INSTRUCOES_FINAIS_EFETIVACAO);
            return View(view, model);
        }

        [HttpPost]
        [SMCAuthorize(UC_MAT_003_19_01.PAGINA_INSTRUCOES_FINAIS_SGAADMINISTRATIVO)]
        public ActionResult SalvarEfetivacaoMatricula(SolicitacaoInstrucaoFinalEfetivacaoViewModel model, string tokenRet)
        {
            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            if (!string.IsNullOrWhiteSpace(tokenRet))
                return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });

            return RedirectToAction("Index", "SolicitacaoServico", new { area = "SRC" });
        }

        //[SMCAuthorize(UC_MAT_003_15_01.PAGINA_REGISTRAR_ENTREGA_DOCUMENTOS_MATRICULA)]
        [SMCAllowAnonymous]
        public ActionResult TermoAdesao(SMCEncryptedLong seqSolicitacaoMatricula)
        {
            var tokenSolicitacao = SolicitacaoServicoService.BuscarSolicitacaoServico(seqSolicitacaoMatricula).TokenServico;
            var arquivo = new SMCUploadFile();

            if (tokenSolicitacao == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_LATO_SENSU)
                arquivo = ContratoService.GerarTermoAdesaoContratoResidenciaMedica(seqSolicitacaoMatricula, true);
            else
                // Busca o contrato
                arquivo = ContratoService.GerarTermoAdesaoContrato(seqSolicitacaoMatricula, true);

            Response.AppendHeader("Content-Disposition", "inline; filename=" + arquivo.Name);
            return File(arquivo.FileData, arquivo.Type);
        }

        private ActionResult RegistrarEntregaDocumentos(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_15_01.PAGINA_REGISTRAR_ENTREGA_DOCUMENTOS_MATRICULA))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new RegistrarEntregaDocumentoViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarRegistrarEntregaDocumentos), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });
            model.SeqProcesso = filtro.SeqProcesso;

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            // Recupera os documentos
            //model.Documentos = RegistroDocumentoService.BuscarDocumentosRegistro(filtro.SeqSolicitacaoServico).TransformList<SolicitacaoDocumentoViewModel>();

            // Validação para exibir botão de segunda via
            model.ExibirSegundaVia = SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemRegistroDomumentoVia(filtro.SeqSolicitacaoServico, filtro.SeqPessoaAtuacao);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            var etapasConfiguracao = BuscarEtapas(model.SeqSolicitacaoServico);

            model.Documentos = new List<SolicitacaoDocumentoViewModel>();
            foreach (var etapa in etapasConfiguracao)
            {
                var documentos = RegistroDocumentoService.BuscarDocumentosRegistro(etapa.SeqSolicitacaoServico, etapa.SeqConfiguracaoEtapa, exibirDocumentoNaoPermiteUpload: true).TransformList<SolicitacaoDocumentoViewModel>();
                //Acerta a descricao de cada etapa
                foreach (var item in documentos)
                {
                    item.DescricaoEtapa = etapa.DescricaoEtapa;

                    if (item.Documentos.Any(x => x.SeqArquivoAnexado.HasValue))
                    {
                        item.SituacaoEntregaDocumentoListaAluno = Common.Areas.SRC.Enums.SituacaoEntregaDocumentoLista.RegistroDocumentoOK;
                        item.SituacaoEntregaDocumentoLista = Common.Areas.SRC.Enums.SituacaoEntregaDocumentoLista.RegistroDocumentoOK;
                    }
                    else
                    {
                        item.SituacaoEntregaDocumentoListaAluno = Common.Areas.SRC.Enums.SituacaoEntregaDocumentoLista.AguardandoRegistroDocumento;
                        item.SituacaoEntregaDocumentoLista = Common.Areas.SRC.Enums.SituacaoEntregaDocumentoLista.AguardandoRegistroDocumento;

                    }
                }

                model.Documentos.AddRange(documentos);
            }

            var situacaoSolicitacao = SolicitacaoServicoService.BuscarSolicitacaoServico(model.SeqSolicitacaoServico).SituacaoDocumentacao;

            // removido pois a validação de documento pendennte sem data prevista feita na viewmodel não é só para a renovação
            //model.RenovacaoMatricula = filtro.TokenServico == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU ? true : false;
            model.HabilitarEfetivacaoMatricula = situacaoSolicitacao == SituacaoDocumentacao.Entregue || situacaoSolicitacao == SituacaoDocumentacao.EntregueComPendencia;
            model.TokenServico = filtro.TokenServico;

            var view = GetExternalView(AcademicoExternalViews.REGISTRAR_ENTREGA_DOCUMENTOS);
            return View(view, model);
        }

        [SMCAuthorize(UC_MAT_003_15_01.PAGINA_REGISTRAR_ENTREGA_DOCUMENTOS_MATRICULA)]
        public ActionResult SalvarRegistrarEntregaDocumentos(RegistrarEntregaDocumentoViewModel model, string tokenRet)
        {
            // Valida pré condições de entrada na página
            //VerificarPreCondicoesEntradaPagina(model);
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                // Caso já exista um aluno para esta pessoa na mesma oferta e faça disciplina isolada, pergunta se deseja matricular apenas incluindo as novas turmas no aluno anterior
                /*Assert(model, "Mensagem_Aluno_Existente_Disciplina_Isolada", () =>
				{
					return SolicitacaoMatriculaService.VerificarAlunoDisciplinasIsoladas(model.SeqSolicitacaoServico);
				});*/

                //if (this.GetInstituicaoEnsinoLogada().Seq <= 0)
                //    this.HttpContext.SetFilterGlobal();

                // Faz a efetivação da matrícula
                SolicitacaoMatriculaService.EfetivarMatricula(model.Transform<EfetivacaoMatriculaData>());
            }

            // Atualiza a data de saída da página da etapa
            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);

            return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        #endregion [ Efetivação ]

        #region [ Solicitação de Dispensa - Itens Dispensados ]

        [SMCAuthorize(UC_SRC_004_11_01.SOLICITACAO_ITENS_DISPENSADOS)]
        protected ActionResult SolicitacaoDispensaItensDispensados(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = this.SolicitacaoDispensaService.PrepararModeloSolicitacaoDispensaItensDispensados(filtro.SeqPessoaAtuacao, filtro.SeqSolicitacaoServico).Transform<SolicitacaoDispensaItensDispensadosViewModel>();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarSolicitacaoDispensaItensDispensados), historico.Seq);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });

                return View("ExibicaoBloqueio", model);
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            model.SeqPessoaAtuacao = filtro.SeqPessoaAtuacao;
            model.SeqProcesso = filtro.SeqProcesso;
            model.SeqSolicitacaoServico = filtro.SeqSolicitacaoServico;
            model.SeqProcessoEtapa = new SMCEncryptedLong(SolicitacaoMatriculaService.BuscarProcessoEtapaPorSolicitacaoMatricula(filtro.SeqSolicitacaoServico, filtro.SeqConfiguracaoEtapa));

            //Exibir o botão de integralização curricular
            model.ExibirIntegralizacao = HistoricoEscolarService.VisualizarConsultaConcederFormacaoIntegralizacao(filtro.SeqPessoaAtuacao);

            // Busca o datasource de assuntos para cada item
            foreach (var item in model.ComponentesCurriculares)
            {
                item.Assuntos = ComponenteCurricularService.BuscarAssuntosComponenteCurricularSelect(item.SeqGrupoCurricularComponente?.Seq, item.SeqComponenteCurricular?.Seq, model.SeqCicloLetivo, model.SeqPessoaAtuacao, false);

                var validar = this.ComponenteCurricularService.ValidarComponenteCurricularExigeAssunto(item.SeqGrupoCurricularComponente?.Seq, item.SeqComponenteCurricular?.Seq);

                if (!validar)
                    item.ExibirAssunto = false;
                else
                    item.ExibirAssunto = item.Assuntos?.Any();
            }

            model.PermitirSelecionarGruposComComponentes = true;

            var view = GetExternalView(AcademicoExternalViews.SOLICITACAO_DISPENSA_ITENS_DISPENSADOS);
            return View(view, model);
        }

        [HttpPost]
        [SMCAuthorize(UC_SRC_004_11_01.SOLICITACAO_ITENS_DISPENSADOS)]
        public ActionResult SalvarSolicitacaoDispensaItensDispensados(SolicitacaoDispensaItensDispensadosViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                // Exibe mensagem de confirmação caso tenha algum item que não faz parte da matriz do aluno
                if (model.ComponentesCurricularesForaMatriz != null && model.ComponentesCurricularesForaMatriz.Any())
                {
                    var itensFormatados = "<ul>";
                    foreach (var item in model.ComponentesCurricularesForaMatriz)
                    {
                        itensFormatados += $"<li>{item.Descricao}</li>";
                    }
                    itensFormatados += "</ul>";

                    var msg = string.Format(UIResource.Mensagem_Assert_Itens_Matriz, itensFormatados);
                    Assert(model, msg);
                }

                // Se na seleção de componentes curriculares a dispensar foram selecionados componentes já cursados
                // com aprovação ou já dispensados, abortar a operação e exibir mensagem de erro:
                // "Os seguintes componentes já foram cursados com aprovação ou dispensados: [Lista de componentes separados por vírgula]"
                var solicitacaoData = model.Transform<SolicitacaoDispensaItensDispensadosData>();
                var componentesAssunto = solicitacaoData.ComponentesCurriculares.Where(x => x.SeqComponenteCurricular.HasValue).Select(s => new HistoricoEscolarAprovadoFiltroData
                {
                    SeqComponenteCurricular = s.SeqComponenteCurricular,
                    SeqComponenteCurricularAssunto = s.SeqComponenteCurricularAssunto
                }).ToList();
                if (componentesAssunto.Count > 0)
                {
                    var validacaoHistorico = HistoricoEscolarService.VerificarHistoricoComponentesAprovadosDispensados(model.SeqPessoaAtuacao, componentesAssunto);
                    if (!string.IsNullOrEmpty(validacaoHistorico))
                    {
                        throw new SolicitacaoDispensaComponenteCurricularAprovadoException(validacaoHistorico);
                    }
                }

                SolicitacaoDispensaService.SalvarSolicitacaoDispensaItensDispensados(solicitacaoData);
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        [SMCAuthorize(UC_SRC_004_11_01.SOLICITACAO_ITENS_DISPENSADOS)]
        public ActionResult PreencherComponenteCurricularAssunto(SMCEncryptedLong seqGrupoCurricularComponente, SMCEncryptedLong seqComponenteCurricular, SMCEncryptedLong seqCicloLetivo, SMCEncryptedLong seqPessoaAtuacao)
        {
            var result = this.ComponenteCurricularService.BuscarAssuntosComponenteCurricularSelect(seqGrupoCurricularComponente, seqComponenteCurricular, seqCicloLetivo, seqPessoaAtuacao, true);

            return Json(result);
        }

        [SMCAuthorize(UC_SRC_004_11_01.SOLICITACAO_ITENS_DISPENSADOS)]
        public ActionResult ValidarAssuntoComponenteCurricular(SMCEncryptedLong seqGrupoCurricularComponente, SMCEncryptedLong seqComponenteCurricular, SMCEncryptedLong seqCicloLetivo, SMCEncryptedLong seqPessoaAtuacao)
        {
            var validar = this.ComponenteCurricularService.ValidarComponenteCurricularExigeAssunto(seqGrupoCurricularComponente, seqComponenteCurricular);

            if (!validar)
                return Json(false);

            var result = this.ComponenteCurricularService.BuscarAssuntosComponenteCurricularSelect(seqGrupoCurricularComponente, seqComponenteCurricular, seqCicloLetivo, seqPessoaAtuacao, true);

            return Json(result.Count > 0);
        }

        [SMCAuthorize(UC_SRC_004_11_01.SOLICITACAO_ITENS_DISPENSADOS)]
        public ActionResult PreencherTotalDispensado(SMCEncryptedLong seqGrupoCurricular)
        {
            var result = this.GrupoCurricularService.BuscarTotalDispensadoSolicitacaoDispensa(seqGrupoCurricular);

            return Json(result);
        }

        [SMCAuthorize(UC_SRC_004_11_01.SOLICITACAO_ITENS_DISPENSADOS)]
        public ActionResult ValidarFormatoConfiguracaoGrupoCurricular(SMCEncryptedLong seqGrupoCurricular)
        {
            var result = this.GrupoCurricularService.ValidarFormatoConfiguracaoGrupoCurricular(seqGrupoCurricular);

            return Json(result);
        }

        #endregion [ Solicitação de Dispensa - Itens Dispensados ]

        #region [ Solicitação de Dispensa - Itens Cursados ]

        [SMCAuthorize(UC_SRC_004_12_01.SOLICITACAO_ITENS_CURSADOS_DISPENSA)]
        protected ActionResult SolicitacaoDispensaItensCursados(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            //Verifica se foi criado uma solicitação de dispensa para a solicitação de serviço
            SolicitacaoDispensaService.CriarSolicitacaoDispensaPorSolicitacaoServico(filtro.SeqSolicitacaoServico);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = this.SolicitacaoDispensaService.PrepararModeloSolicitacaoDispensaItensCursados(filtro.SeqPessoaAtuacao, filtro.SeqSolicitacaoServico).Transform<SolicitacaoDispensaItensCursadosViewModel>();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarSolicitacaoDispensaItensCursados), historico.Seq);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });
            model.SeqPessoaAtuacao = filtro.SeqPessoaAtuacao;
            model.SeqProcesso = filtro.SeqProcesso;
            model.SeqSolicitacaoServico = filtro.SeqSolicitacaoServico;

            //Exibir o botão de integralização curricular
            model.ExibirIntegralizacao = HistoricoEscolarService.VisualizarConsultaConcederFormacaoIntegralizacao(filtro.SeqPessoaAtuacao);

            var view = GetExternalView(AcademicoExternalViews.SOLICITACAO_DISPENSA_ITENS_CURSADOS);
            return View(view, model);
        }

        [HttpPost]
        [SMCAuthorize(UC_SRC_004_12_01.SOLICITACAO_ITENS_CURSADOS_DISPENSA)]
        public ActionResult SalvarSolicitacaoDispensaItensCursados(SolicitacaoDispensaItensCursadosViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                //De acordo com a Janice não precisa validar pois pode abrir solicitação sem itens
                //if (!model.ItensCursadosOutrasInstituicoes.Any() && !model.ItensCursadosNestaInstituicao.Any())
                //    throw new SolicitacaoServicoDispensaCursadosNaoSelecionadoException();
                var data = model.Transform<SolicitacaoDispensaItensCursadosData>();
                SolicitacaoDispensaService.SalvarSolicitacaoDispensaItensCursados(data);
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        #endregion [ Solicitação de Dispensa - Itens Cursados ]

        #region [ Solicitação Atividade Complementar ]

        [SMCAuthorize(UC_SRC_004_10_01.SOLICITACAO_ATIVIDADE_COMPLEMENTAR)]
        public virtual ActionResult SolicitacaoAtividadeComplementar(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_SRC_004_10_01.SOLICITACAO_ATIVIDADE_COMPLEMENTAR))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            var model = SolicitacaoServicoService.BuscarSolicitacaoAtividadeComplementar(filtro.SeqSolicitacaoServico)
                                                    .Transform<SolicitacaoAtividadeComplementarPaginaViewModel>() ?? new SolicitacaoAtividadeComplementarPaginaViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarSolicitacaoAtividadeComplementar), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            // Preenche os DataSources
            model.DivisoesComponente = DivisaoComponenteService.BuscarDivisaoComponenteAluno(model.SeqPessoaAtuacao)
                                                                        .TransformList<SMCSelectListItem>();
            if (!model.SeqCicloLetivo.HasValue)
                model.SeqCicloLetivo = AlunoService.BuscarCicloLetivoAtual(model.SeqPessoaAtuacao);

            if (model.SeqDivisaoComponente > 0)
            {
                var dadosDivisao = CarregarDadosDivisaoComponente(model.SeqPessoaAtuacao, model.SeqDivisaoComponente);

                model.ApuracaoNota = dadosDivisao.ApuracaoNota;
                model.ApuracaoFrequencia = dadosDivisao.ApuracaoFrequencia;
                model.ApuracaoEscala = dadosDivisao.ApuracaoEscala;
                model.NotaMaxima = dadosDivisao.NotaMaxima.GetValueOrDefault();
                model.SeqEscalaApuracao = dadosDivisao.SeqEscalaApuracao;
                model.EscalaApuracaoItens = BuscarItensEscalaApuracao(model.SeqDivisaoComponente, (short?)model.Nota, model.SeqPessoaAtuacao);
                model.PercentualFrequenciaAprovado = dadosDivisao.PercentualFrequenciaAprovado;
                model.PercentualNotaAprovado = dadosDivisao.PercentualNotaAprovado;
                model.PermiteAlunoSemNota = dadosDivisao.PermiteAlunoSemNota;

                if (model.SeqEscalaApuracaoItem.HasValue || model.Nota.HasValue || model.Faltas.HasValue)
                    model.SituacaoFinal = BuscarSituacaoFinalAluno(model.SeqDivisaoComponente, model.SeqEscalaApuracaoItem, (short?)model.Nota, (short?)model.Faltas, model.SeqPessoaAtuacao, model.SeqEscalaApuracao, model.PercentualFrequenciaAprovado, model.PercentualNotaAprovado, model.PermiteAlunoSemNota);
                else
                    model.SituacaoFinal = SituacaoHistoricoEscolar.Nenhum;
            }

            model.CiclosLetivos = AlunoService.BuscarCiclosLetivosAlunoHistoricoSelect(model.SeqPessoaAtuacao);
            model.ExibirIntegralizacao = SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ADMINISTRATIVO;
            model.SeqPessoaAtuacao = filtro.SeqPessoaAtuacao;

            var view = GetExternalView(AcademicoExternalViews.SOLICITACAO_ATIVIDADE_COMPLEMENTAR);
            return View(view, model);
        }

        [HttpPost]
        [SMCAllowAnonymous]
        public JsonResult BuscarDadosConfiguracaoDivisaoComponente(long seqDivisaoComponente, SMCEncryptedLong seqPessoaAtuacao)
        {
            var dados = CarregarDadosDivisaoComponente(seqPessoaAtuacao, seqDivisaoComponente);
            return Json(new
            {
                NotaMaxima = dados.NotaMaxima,
                ApuracaoNota = dados.ApuracaoNota,
                ApuracaoEscala = dados.ApuracaoEscala,
                ApuracaoFrequencia = dados.ApuracaoFrequencia,
                SeqEscalaApuracao = dados.SeqEscalaApuracao,
                PercentualFrequenciaAprovado = dados.PercentualFrequenciaAprovado,
                PercentualNotaAprovado = dados.PercentualNotaAprovado,
                PermiteAlunoSemNota = dados.PermiteAlunoSemNota
            });
        }

        private SituacaoHistoricoEscolar BuscarSituacaoFinalAluno(long seqDivisaoComponente, long? seqEscalaApuracaoItem, decimal? nota, short? faltas, long seqPessoaAtuacao, long? seqEscalaApuracao, short? PercentualFrequenciaAprovado, short? PercentualNotaAprovado, bool PermiteAlunoSemNota)
        {
            var seqCicloLetivo = AlunoService.BuscarCicloLetivoAtual(seqPessoaAtuacao);
            var matrizCurricular = MatrizCurricularService.BuscarMatrizCurricularAluno(seqPessoaAtuacao, seqCicloLetivo, true);

            var situacao = HistoricoEscolarService.CalcularSituacaoFinal(new HistoricoEscolarSituacaoFinalData
            {
                SeqDivisaoComponente = seqDivisaoComponente,
                SeqEscalaApuracaoItem = seqEscalaApuracaoItem,
                SeqMatrizCurricular = matrizCurricular.Seq,
                SeqEscalaApuracao = seqEscalaApuracao,
                Nota = (short?)nota,
                Faltas = faltas,
                PercentualMinimoFrequencia = PercentualFrequenciaAprovado,
                PercentualMinimoNota = PercentualNotaAprovado,
                IndicadorPermiteAlunoSemNota = PermiteAlunoSemNota
            });
            return situacao;
        }

        [HttpPost]
        [SMCAllowAnonymous]
        public JsonResult BuscarSituacaoFinal(long seqDivisaoComponente, long? seqEscalaApuracaoItem, decimal? nota, short? faltas, SMCEncryptedLong seqPessoaAtuacao, long? seqEscalaApuracao, short? PercentualFrequenciaAprovado, short? PercentualNotaAprovado, bool PermiteAlunoSemNota)
        {
            SituacaoHistoricoEscolar situacao = SituacaoHistoricoEscolar.Nenhum;
            if (seqEscalaApuracaoItem.HasValue || nota.HasValue || faltas.HasValue)
                situacao = BuscarSituacaoFinalAluno(seqDivisaoComponente, seqEscalaApuracaoItem, nota, faltas, seqPessoaAtuacao, seqEscalaApuracao, PercentualFrequenciaAprovado, PercentualNotaAprovado, PermiteAlunoSemNota);
            return Json(new
            {
                SituacaoFinal = situacao,
                DescricaoSituacaoFinal = SMCEnumHelper.GetDescription(situacao)
            });
        }

        private (short? NotaMaxima, bool ApuracaoNota, bool ApuracaoEscala, bool ApuracaoFrequencia, long? SeqEscalaApuracao, short? PercentualFrequenciaAprovado, short? PercentualNotaAprovado, bool PermiteAlunoSemNota) CarregarDadosDivisaoComponente(long seqPessoaAtuacao, long seqDivisaoComponente)
        {
            var seqCicloLetivo = AlunoService.BuscarCicloLetivoAtual(seqPessoaAtuacao);
            var matrizCurricular = MatrizCurricularService.BuscarMatrizCurricularAluno(seqPessoaAtuacao, seqCicloLetivo, true);

            var dados = DivisaoComponenteService.BuscarDadosConfiguracaoDivisaoComponente(seqDivisaoComponente, matrizCurricular.Seq);

            if (dados == null)
                throw new DivisaoMatrizCurricularComponenteNaoEncontradaException();

            return (dados.NotaMaxima,
                dados.ApuracaoNota,
                dados.ApuracaoEscala,
                dados.ApuracaoFrequencia,
                dados.SeqEscalaApuracao,
                dados.PercentualFrequenciaAprovado,
                dados.PercentualNotaAprovado,
                dados.PermiteAlunoSemNota
            );
        }

        private List<SMCDatasourceItem> BuscarItensEscalaApuracao(long seqDivisaoComponente, decimal? nota, long seqPessoaAtuacao)
        {
            var seqCicloLetivo = AlunoService.BuscarCicloLetivoAtual(seqPessoaAtuacao);
            var matrizCurricular = MatrizCurricularService.BuscarMatrizCurricularAluno(seqPessoaAtuacao, seqCicloLetivo, true);

            var dados = DivisaoComponenteService.BuscarDadosConfiguracaoDivisaoComponente(seqDivisaoComponente, matrizCurricular.Seq);

            if (dados.SeqEscalaApuracao.GetValueOrDefault() > 0)
            {
                // Busca os itens da escala
                var escala = EscalaApuracaoService.BuscarEscalaApuracao(dados.SeqEscalaApuracao.GetValueOrDefault());
                return escala.Itens.Select(i => new SMCDatasourceItem
                {
                    Seq = i.Seq,
                    Descricao = i.Descricao,
                    Selected = (nota.HasValue ? i.PercentualMinimo <= nota && i.PercentualMaximo >= nota : false)
                }).ToList();
            }
            return null;
        }

        [HttpPost]
        [SMCAllowAnonymous]
        public JsonResult BuscarEscalaApuracaoPorNota(long seqDivisaoComponente, decimal? nota, SMCEncryptedLong seqPessoaAtuacao)
        {
            var itens = BuscarItensEscalaApuracao(seqDivisaoComponente, (short?)nota, seqPessoaAtuacao);
            return Json(itens);
        }

        //[HttpPost]
        //[SMCAllowAnonymous]
        //public ActionResult BuscarAtividadesComplementares(SMCEncryptedLong seqTipoDivisaoComponente, SMCEncryptedLong seqPessoaAtuacao)
        //{
        //    var items = DivisaoComponenteService.BuscarDivisaoComponenteAluno(seqTipoDivisaoComponente, seqPessoaAtuacao);
        //    return Json(items);
        //}

        [HttpPost]
        [SMCAuthorize(UC_SRC_004_10_01.SOLICITACAO_ATIVIDADE_COMPLEMENTAR)]
        public ActionResult SalvarSolicitacaoAtividadeComplementar(SolicitacaoAtividadeComplementarPaginaViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                // Salva a solicitação de atividade complementar
                SolicitacaoServicoService.SalvarSolicitacaoAtividadeComplementar(model.Transform<SolicitacaoAtividadeComplementarPaginaData>());
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        #endregion [ Solicitação Atividade Complementar ]

        #region [ Solicitação Cobrança Taxa ]

        private ActionResult SolicitacaoCobrancaTaxa(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_SRC_004_22_01.ABERTURA_SOLICITACAO_COBRANCA_TAXA))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = this.SolicitacaoServicoService.PrepararModeloSolicitacaoCobrancaTaxa(filtro.SeqSolicitacaoServico).Transform<SolicitacaoCobrancaTaxaViewModel>();
            model.TiposEmissaoTaxas = this.ServicoService.BuscarTiposEmissaoCobrancaTaxa();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarSolicitacaoCobrancaTaxa), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            var view = GetExternalView(AcademicoExternalViews.SOLICITACAO_COBRANCA_TAXA);
            return View(view, model);
        }

        [HttpPost]
        [SMCAuthorize(UC_SRC_004_22_01.ABERTURA_SOLICITACAO_COBRANCA_TAXA)]
        public ActionResult SalvarSolicitacaoCobrancaTaxa(SolicitacaoCobrancaTaxaViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                if (model.ExisteTaxaAssociada)
                {
                    if (model.ExisteTaxaSemValor)
                        throw new SolicitacaoCobrancaTaxaSemValorException();

                    if (model.ExisteTaxaComValorIncorreto)
                        throw new SolicitacaoCobrancaTaxaValorIncorretoException();

                    SolicitacaoServicoService.AtualizarSolicitacaoServicoTipoEmissaoTaxa(model.SeqSolicitacaoServico, model.TipoEmissaoTaxa);
                }
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        #endregion

        #endregion Conteúdo

        #region Upload de arquivos

        [SMCAuthorize(UC_SRC_004_06_01.ABERTURA_SOLICITACAO_UPLOAD_ARQUIVOS)]
        public ActionResult RegistrarDocumentos(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, SMCEncryptedLong seqPessoaAtuacao, SMCEncryptedLong grupoDocumento, bool grupoObrigatorio = false)
        {

            var etapas = BuscarEtapas(seqSolicitacaoServico, true);
            var etapaAtual = etapas.FirstOrDefault(f => f.SeqConfiguracaoEtapa == seqConfiguracaoEtapa);
            var primeiraEtapa = etapas.OrderBy(o => o.OrdemEtapaSGF).First();
            var ehPrimeiraEtapa = primeiraEtapa.SeqConfiguracaoEtapa == seqConfiguracaoEtapa;

            var documentos = (seqConfiguracaoEtapa == null || seqConfiguracaoEtapa.Value == 0) ?
                                this.RegistroDocumentoService.BuscarDocumentosRegistro(seqSolicitacaoServico, null, true) :
                                this.RegistroDocumentoService.BuscarDocumentosRegistro(seqSolicitacaoServico, seqConfiguracaoEtapa ?? null, true);

            var registroDocumento = PreencheModeloSolicitacaoRegistroDocumento(seqSolicitacaoServico,
                                                                               seqConfiguracaoEtapa,
                                                                               seqPessoaAtuacao,
                                                                               etapaAtual,
                                                                               ehPrimeiraEtapa,
                                                                               grupoDocumento,
                                                                               grupoObrigatorio,
                                                                               documentos.TransformList<SolicitacaoDocumentoViewModel>());

            if (grupoDocumento != null && grupoDocumento.Value != 0)
            {
                registroDocumento.Documentos = registroDocumento.Documentos.Where(f => f.Grupos.Any(g => g.Seq == grupoDocumento.Value)).ToList();
            }
            else if (grupoObrigatorio)
            {
                registroDocumento.Documentos = registroDocumento.Documentos.Where(f => !f.Grupos.Any() && f.Obrigatorio).ToList();
            }
            else
            {
                registroDocumento.Documentos = registroDocumento.Documentos.Where(f => !f.Grupos.Any() && !f.Obrigatorio).ToList();
            }

            var view = GetExternalView(AcademicoExternalViews._REGISTRAR_DOCUMENTOS);
            return PartialView(view, registroDocumento);
        }

        public SolicitacaoRegistroDocumentoViewModel PreencheModeloSolicitacaoRegistroDocumento(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, SMCEncryptedLong seqPessoaAtuacao, EtapaListaViewModel etapaAtual, bool ehPrimeiraEtapa, SMCEncryptedLong grupoDocumento, bool? grupoObrigatorio, List<SolicitacaoDocumentoViewModel> documentos)
        {
            var aplicacaoAluno = SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ALUNO;
            var dataFimEscalonamento = etapaAtual != null && etapaAtual.SeqEscalonamento.HasValue && etapaAtual.SeqEscalonamento != 0 ? EscalonamentoService.BuscarEscalonamento(etapaAtual.SeqEscalonamento.Value).DataFim : null;

            var registroDocumento = new SolicitacaoRegistroDocumentoViewModel()
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                SeqConfiguracaoEtapa = seqConfiguracaoEtapa,
                SeqPessoaAtuacao = seqPessoaAtuacao,
                ExibeLegenda = aplicacaoAluno && !ehPrimeiraEtapa,
                GrupoDocumento = grupoDocumento,
                GrupoObrigatorio = grupoObrigatorio,
                Documentos = documentos,
                BackUrl = Request.UrlReferrer.ToString()
            };

            var solicitacao = SolicitacaoServicoService.BuscarSolicitacaoServico(seqSolicitacaoServico);
            var tokenEntregaDocumento = solicitacao.TokenTipoServico == TOKEN_TIPO_SERVICO.ENTREGA_DOCUMENTACAO;
            if (tokenEntregaDocumento)
                registroDocumento.TokenServicoEntregaDocumentacao = true;

            registroDocumento.Documentos.SMCForEach(sf => sf.Documentos.SMCForEach(f =>
            {
                f.AplicacaoAluno = aplicacaoAluno;
                f.DataLimiteEntrega = sf.DataLimiteEntrega;
                f.Observacao = aplicacaoAluno ? f.Observacao : f.ObservacaoSecretaria;
                f.ExibirEntregaPosterior = sf.PermiteEntregaPosterior && aplicacaoAluno && ehPrimeiraEtapa;
                f.ExibirDataPrazoEntrega = aplicacaoAluno && !ehPrimeiraEtapa;
                f.TextoEntregaPosterior = sf.DataLimiteEntrega != null ? string.Format(UIResource.Mensagem_EntregareiAte, sf.DataLimiteEntrega.SMCDataAbreviada()) : UIResource.Mensagem_EntregareiPosteriormente;
                f.ExibirLegendaDocumento = aplicacaoAluno && !ehPrimeiraEtapa;

                if (tokenEntregaDocumento)
                {
                    f.TokenServico = TOKEN_SERVICO.ENTREGA_DOCUMENTACAO;
                }
                else
                {
                    f.TokenServico = solicitacao.TokenServico;
                }



                if (f.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega)
                    f.DataPrazoEntrega = dataFimEscalonamento;

                if (f.ArquivoAnexado != null && f.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Indeferido
                || (tokenEntregaDocumento && f.ArquivoAnexado != null && f.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido || f.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel))
                {
                    if (System.Web.HttpContext.Current.Request.Url.Host == "localhost")
                        f.AnexoAnterior = $"<a href='http://{System.Web.HttpContext.Current.Request.Url.Host}/Dev/{SMCContext.ApplicationId}/Home/DownloadFileGuid?GuidFile={f.ArquivoAnexado.GuidFile.ToString()}' target='_blank'><i></i>Anexo anterior</a>";

                    else
                        f.AnexoAnterior = $"<a href='https://{System.Web.HttpContext.Current.Request.Url.Host}/{SMCContext.ApplicationId}/Home/DownloadFileGuid?GuidFile={f.ArquivoAnexado.GuidFile.ToString()}' target='_blank'><i></i>Anexo anterior</a>";

                    f.TemAnexoAnterior = true;
                }

                f.EhPrimeiraEtapa = ehPrimeiraEtapa;

                //f.ArquivoAnexado = aplicacaoAluno && !ehPrimeiraEtapa ? null : f.ArquivoAnexado;

                var validacaoArquivo = tokenEntregaDocumento
                && f.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido
                || f.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel;


                f.ArquivoAnexado = aplicacaoAluno && !ehPrimeiraEtapa || validacaoArquivo ? null : f.ArquivoAnexado;
            }));

            return registroDocumento;
        }

        [SMCAuthorize(UC_SRC_004_06_01.ABERTURA_SOLICITACAO_UPLOAD_ARQUIVOS)]
        public ActionResult SalvarRegistroDocumentos(SolicitacaoRegistroDocumentoViewModel model)
        {
            var etapas = BuscarEtapas(model.SeqSolicitacaoServico, true);
            var etapaAtual = etapas.FirstOrDefault(f => f.SeqConfiguracaoEtapa == model.SeqConfiguracaoEtapa);
            var primeiraEtapa = etapas.OrderBy(o => o.OrdemEtapaSGF).First();
            var ehPrimeiraEtapa = primeiraEtapa.SeqConfiguracaoEtapa == model.SeqConfiguracaoEtapa;

            model.DocumentosEntreguesAnteriormente = new List<string>();

            model.Documentos.SMCForEach(sf => sf.Documentos.SMCForEach(f =>
            {
                if (f.EntregaPosterior.HasValue && f.EntregaPosterior.Value && ehPrimeiraEtapa)
                {
                    f.Observacao = null;
                    f.ArquivoAnexado = null;
                }
                else if (f.ArquivoAnexado != null && !ehPrimeiraEtapa)
                {
                    f.EntregaPosterior = false;
                }

                if (f.EntregueAnteriormente && f.ArquivoAnexado != null)
                {
                    var descricaoTipoDocumento = SolicitacaoServicoService.BuscarDescricaoTipoDocumento(f.SeqTipoDocumento);
                    model.DocumentosEntreguesAnteriormente.Add(descricaoTipoDocumento);
                }
            }));

            if (model.DocumentosEntreguesAnteriormente.Count() > 0)
                Assert(model, string.Format(UIResource.Confirmacao_Entrega_Documentacao, string.Join("<br />", model.DocumentosEntreguesAnteriormente)));

            ValidarDocumentosMesmoTipoPermitemVarios(model.Documentos);
            ValidarUpload(model);

            var registros = model.Transform<RegistroDocumentoData>();
            registros.Documentos.SMCForEach(sf => sf.Documentos.SMCForEach(f =>
            {
                if (SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ALUNO)
                {
                    f.Observacao = f.Observacao;
                }
                else
                {
                    f.Observacao = f.Observacao;
                    f.ObservacaoSecretaria = f.Observacao;
                }
            }));
            RegistroDocumentoService.AnexarDocumentosSolicitacao(registros);
            return SMCRedirectToUrl(model.BackUrl);
        }

        public void ValidarUpload(SolicitacaoRegistroDocumentoViewModel model)
        {
            var etapas = BuscarEtapas(model.SeqSolicitacaoServico, true);
            var etapaEmAndamento = etapas.Where(s => s.SituacaoEtapaIngressante == SituacaoEtapaSolicitacaoMatricula.EmAndamento).FirstOrDefault();
            var seqConfiguracaoEtapaEmAndamento = etapaEmAndamento?.SeqConfiguracaoEtapa;
            var primeiraEtapa = etapas.OrderBy(o => o.OrdemEtapaSGF).FirstOrDefault();
            var ehPrimeiraEtapa = primeiraEtapa.SeqConfiguracaoEtapa == seqConfiguracaoEtapaEmAndamento;

            if ((SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ALUNO && ehPrimeiraEtapa) || SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ADMINISTRATIVO)
                if (model.GrupoObrigatorio.HasValue)
                {
                    var documentos = RegistroDocumentoService.BuscarDocumentosRegistro(model.SeqSolicitacaoServico, model.SeqConfiguracaoEtapa).TransformList<SolicitacaoDocumentoViewModel>();

                    foreach (var item in model.Documentos)
                    {
                        foreach (var documento in item.Documentos)
                        {
                            var doc = documentos.Where(w => w.SeqDocumentoRequerido == item.SeqDocumentoRequerido).FirstOrDefault();
                            if (doc.PermiteEntregaPosterior && ((documento.EntregaPosterior.HasValue && !documento.EntregaPosterior.Value) && documento.ArquivoAnexado == null))
                            {
                                throw new SMCApplicationException($"O arquivo para o documento {doc.DescricaoTipoDocumento} deve ser anexado ou a entrega posterior deve ser selecionada.");
                            }
                            else if ((!documento.EntregaPosterior.HasValue || !documento.EntregaPosterior.Value) && documento.ArquivoAnexado == null || (documento.ArquivoAnexado != null && string.IsNullOrEmpty(documento.ArquivoAnexado.Name)))
                            {
                                throw new SMCApplicationException($"O arquivo para o documento {doc.DescricaoTipoDocumento} deve ser anexado.");
                            }

                            if ((documento.EntregaPosterior.HasValue && documento.EntregaPosterior.Value) && documento.ArquivoAnexado != null && !string.IsNullOrEmpty(documento.ArquivoAnexado.Name))
                            {
                                throw new SMCApplicationException($"Se o documento {doc.DescricaoTipoDocumento} será entregue posteriormente, não se deve anexar nenhum arquivo para ele.Mas, caso queira entrega-lo agora, desmarque a opção de entrega posterior");
                            }
                        }
                    }
                }
                else if (model.GrupoDocumento != null && model.GrupoDocumento.Value != 0)
                {
                    var documentos = RegistroDocumentoService.BuscarDocumentosRegistro(model.SeqSolicitacaoServico, model.SeqConfiguracaoEtapa).TransformList<SolicitacaoDocumentoViewModel>();
                    var numeroMinimoDocumentosRequerido = documentos.Where(d => d.Grupos != null && d.Grupos.Any()).SelectMany(s => s.Grupos).FirstOrDefault(f => f.Seq == model.GrupoDocumento.Value).NumeroMinimoDocumentosRequerido;
                    var quantidadeMinima = model.Documentos.SelectMany(s => s.Documentos).Count(c => c.ArquivoAnexado != null) + model.Documentos.SelectMany(s => s.Documentos).Count(c => c.ArquivoAnexado == null && (c.EntregaPosterior.HasValue && c.EntregaPosterior.Value));

                    if (quantidadeMinima < numeroMinimoDocumentosRequerido)
                        foreach (var item in model.Documentos)
                        {
                            foreach (var documento in item.Documentos)
                            {
                                var doc = documentos.Where(w => w.SeqDocumentoRequerido == item.SeqDocumentoRequerido).FirstOrDefault();
                                if (doc.PermiteEntregaPosterior && ((documento.EntregaPosterior.HasValue && !documento.EntregaPosterior.Value) && documento.ArquivoAnexado == null))
                                {
                                    throw new SMCApplicationException($"O arquivo para o documento {doc.DescricaoTipoDocumento} deve ser anexado ou a entrega posterior deve ser selecionada.");
                                }
                                else if ((!documento.EntregaPosterior.HasValue || !documento.EntregaPosterior.Value) && documento.ArquivoAnexado == null || (documento.ArquivoAnexado != null && string.IsNullOrEmpty(documento.ArquivoAnexado.Name)))
                                {
                                    throw new SMCApplicationException($"O arquivo para o documento {doc.DescricaoTipoDocumento} deve ser anexado.");
                                }

                                if ((documento.EntregaPosterior.HasValue && documento.EntregaPosterior.Value) && documento.ArquivoAnexado != null && !string.IsNullOrEmpty(documento.ArquivoAnexado.Name))
                                {
                                    throw new SMCApplicationException($"Se o documento {doc.DescricaoTipoDocumento} será entregue posteriormente, não se deve anexar nenhum arquivo para ele.Mas, caso queira entrega-lo agora, desmarque a opção de entrega posterior");
                                }
                            }
                        }
                }
        }

        public void ValidarDocumentosMesmoTipoPermitemVarios(List<SolicitacaoDocumentoViewModel> documentos)
        {
            foreach (var item in documentos)
                if (item.Documentos.Count() > 10)
                {
                    var retorno = DocumentoRequeridoService.BuscarDescricaoDocumentoRequeridoPermiteVarios(item.SeqDocumentoRequerido);
                    throw new NumeroDeDocumentosExcedidoException(retorno.DescricaoTipoDocumento);
                }
        }

        #endregion Upload de arquivos

        #region Residência Médica


        private ActionResult RegistrarEntregaDocumentosResidenciaMedica(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_42_01.PAGINA_REGISTRAR_ENTREGA_DOCUMENTOS_MATRICULA_LATO_SENSU))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new RegistrarEntregaDocumentoViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarRegistrarEntregaDocumentosResidenciaMedica), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });
            model.SeqProcesso = filtro.SeqProcesso;

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa), @bloqueio = true });
                return View("ExibicaoBloqueio", model);
            }

            // Recupera os documentos
            //model.Documentos = RegistroDocumentoService.BuscarDocumentosRegistro(filtro.SeqSolicitacaoServico).TransformList<SolicitacaoDocumentoViewModel>();

            // Validação para exibir botão de segunda via
            model.ExibirSegundaVia = SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemRegistroDomumentoVia(filtro.SeqSolicitacaoServico, filtro.SeqPessoaAtuacao);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            var etapasConfiguracao = BuscarEtapas(model.SeqSolicitacaoServico);

            model.Documentos = new List<SolicitacaoDocumentoViewModel>();
            foreach (var etapa in etapasConfiguracao)
            {
                var documentos = RegistroDocumentoService.BuscarDocumentosRegistro(etapa.SeqSolicitacaoServico, etapa.SeqConfiguracaoEtapa, exibirDocumentoNaoPermiteUpload: true).TransformList<SolicitacaoDocumentoViewModel>();
                //Acerta a descricao de cada etapa
                foreach (var item in documentos)
                {
                    item.DescricaoEtapa = etapa.DescricaoEtapa;

                    if (item.Documentos.Any(x => x.SeqArquivoAnexado.HasValue))
                    {
                        item.SituacaoEntregaDocumentoListaAluno = Common.Areas.SRC.Enums.SituacaoEntregaDocumentoLista.RegistroDocumentoOK;
                        item.SituacaoEntregaDocumentoLista = Common.Areas.SRC.Enums.SituacaoEntregaDocumentoLista.RegistroDocumentoOK;
                    }
                    else
                    {
                        item.SituacaoEntregaDocumentoListaAluno = Common.Areas.SRC.Enums.SituacaoEntregaDocumentoLista.AguardandoRegistroDocumento;
                        item.SituacaoEntregaDocumentoLista = Common.Areas.SRC.Enums.SituacaoEntregaDocumentoLista.AguardandoRegistroDocumento;

                    }
                }

                model.Documentos.AddRange(documentos);
            }

            var situacaoSolicitacao = SolicitacaoServicoService.BuscarSolicitacaoServico(model.SeqSolicitacaoServico).SituacaoDocumentacao;

            // removido pois a validação de documento pendennte sem data prevista feita na viewmodel não é só para a renovação
            //model.RenovacaoMatricula = filtro.TokenServico == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU ? true : false;
            model.HabilitarEfetivacaoMatricula = situacaoSolicitacao == SituacaoDocumentacao.Entregue || situacaoSolicitacao == SituacaoDocumentacao.EntregueComPendencia;
            model.TokenServico = filtro.TokenServico;

            var view = GetExternalView(AcademicoExternalViews.REGISTRAR_ENTREGA_DOCUMENTOS);
            return View(view, model);
        }

        [SMCAuthorize(UC_MAT_003_42_01.PAGINA_REGISTRAR_ENTREGA_DOCUMENTOS_MATRICULA_LATO_SENSU)]
        public ActionResult SalvarRegistrarEntregaDocumentosResidenciaMedica(RegistrarEntregaDocumentoViewModel model, string tokenRet)
        {
            // Valida pré condições de entrada na página
            //VerificarPreCondicoesEntradaPagina(model);
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                // Faz a efetivação da matrícula
                SolicitacaoMatriculaService.EfetivarMatricula(model.Transform<EfetivacaoMatriculaData>());
            }

            // Atualiza a data de saída da página da etapa
            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);

            return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        #endregion Residência Médica


        private ActionResult SelecaoComponenteCurricular(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_SRC_004_30_01.SOLICITACAO_DEPOSITO_PROJETO_QUALIFICACAO))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new ComponenteCurricularSolicitacaoViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, nameof(SalvarSelecaoComponenteCurricular), historico.Seq);

            //Necessário para o retorno do bloqueio
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoServico = new SMCEncryptedLong(filtro.SeqSolicitacaoServico), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });
            model.SeqProcesso = filtro.SeqProcesso;

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;
            var etapasConfiguracao = BuscarEtapas(model.SeqSolicitacaoServico);
            model.DivisoesComponente = new SMCMasterDetailList<DivisaoComponenteCurricularSolicitacaoViewModel>();

            model.DivisoesComponentesSelect = DivisaoMatrizCurricularComponenteService.BuscarDivisaoComponenteCurricularProjetoQualificacao(new DivisaoMatrizCurricularComponenteFiltroData { SeqAluno = filtro.SeqPessoaAtuacao });

            var divisaoComponente = SolicitacaoTrabalhoAcademicoService.BuscarSeqDivisaoComponentePorSolicitacao(filtro.SeqSolicitacaoServico);
            if (divisaoComponente > 0)
                model.DivisoesComponente.Add(new DivisaoComponenteCurricularSolicitacaoViewModel { SeqDivisaoComponente = divisaoComponente });
            var view = GetExternalView(AcademicoExternalViews.SELECAO_COMPONENTE_CURRICULAR);
            return View(view, model);
        }

        [SMCAuthorize(UC_SRC_004_30_01.SOLICITACAO_DEPOSITO_PROJETO_QUALIFICACAO)]
        public ActionResult SalvarSelecaoComponenteCurricular(ComponenteCurricularSolicitacaoViewModel model, string tokenRet)
        {
            if (string.IsNullOrWhiteSpace(tokenRet))
                SolicitacaoServicoService.SalvarSelecaoComponenteCurricular(model.SeqSolicitacaoServico, model.DivisoesComponente.First().SeqDivisaoComponente);

            // Atualiza a data de saída da página da etapa
            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);

            return SMCRedirectToAction("EntrarEtapa", routeValues: new { SeqSolicitacaoServico = new SMCEncryptedLong(model.SeqSolicitacaoServico), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        [SMCAllowAnonymous]
        public ActionResult FinalizarSolicitacaoCRA(long seqSolicitacaoServico)
        {
            RegistroDocumentoService.FinalizarSolicitacaoCRA(seqSolicitacaoServico);

            return SMCRedirectToAction("Index", "SolicitacaoServico", new { area = "SRC" });
        }

    }
}