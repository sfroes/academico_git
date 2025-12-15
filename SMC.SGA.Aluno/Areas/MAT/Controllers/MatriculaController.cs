using log4net.Util;
using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.Common.Areas.FIN.Constants;
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
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.ServiceContract.Areas.GRD.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Academico.UI.Mvc;
using SMC.Academico.UI.Mvc.Areas.MAT.Models;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Academico.UI.Mvc.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.UI.Mvc.Model;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Rest;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Filters;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.UI.Mvc.Util;
using SMC.SGA.Aluno.Areas.MAT.Models;
using SMC.SGA.Aluno.Areas.MAT.Models.Matricula;
using SMC.SGA.Aluno.Areas.SRC.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMC.SGA.Aluno.Areas.MAT.Controllers
{
    [SGAVerificarUsuarioSolicitacaoServico]
    [SMCNoCache]
    public class MatriculaController : SMCControllerBase
    {
        #region Serviços

        private IAlunoService AlunoService => Create<IAlunoService>();

        private IArquivoAnexadoService ArquivoAnexadoService => Create<IArquivoAnexadoService>();

        private IConfiguracaoComponenteService ConfiguracaoComponenteService => Create<IConfiguracaoComponenteService>();

        private IConfiguracaoEtapaService ConfiguracaoEtapaService => Create<IConfiguracaoEtapaService>();

        private IConfiguracaoEtapaPaginaService ConfiguracaoEtapaPaginaService => Create<IConfiguracaoEtapaPaginaService>();

        private IContratoService ContratoService => Create<IContratoService>();

        private IEventoAulaService EventoAulaService => Create<IEventoAulaService>();

        private IHistoricoEscolarService HistoricoEscolarService => Create<IHistoricoEscolarService>();

        private IInstituicaoNivelTipoVinculoAlunoService InstituicaoNivelTipoVinculoAlunoService => Create<IInstituicaoNivelTipoVinculoAlunoService>();

        private IPessoaAtuacaoBloqueioService PessoaAtuacaoBloqueioService => Create<IPessoaAtuacaoBloqueioService>();

        private IProcessoEtapaService ProcessoEtapaService => Create<IProcessoEtapaService>();

        private IRequisitoService RequisitoService => Create<IRequisitoService>();

        private ISGFHelperService SGFHelperService => Create<ISGFHelperService>();

        private ISolicitacaoHistoricoNavegacaoService SolicitacaoHistoricoNavegacaoService => Create<ISolicitacaoHistoricoNavegacaoService>();

        private ISolicitacaoHistoricoSituacaoService SolicitacaoHistoricoSituacaoService => Create<ISolicitacaoHistoricoSituacaoService>();

        private ISolicitacaoMatriculaItemService SolicitacaoMatriculaItemService => Create<ISolicitacaoMatriculaItemService>();

        private ISolicitacaoMatriculaService SolicitacaoMatriculaService => Create<ISolicitacaoMatriculaService>();

        private ISolicitacaoServicoEtapaService SolicitacaoServicoEtapaService => Create<ISolicitacaoServicoEtapaService>();

        private ISolicitacaoServicoService SolicitacaoServicoService => Create<ISolicitacaoServicoService>();

        private ITurmaService TurmaService => Create<ITurmaService>();

        private IRegistroDocumentoService RegistroDocumentoService => Create<IRegistroDocumentoService>();

        private IDocumentoRequeridoService DocumentoRequeridoService => Create<IDocumentoRequeridoService>();

        private IEscalonamentoService EscalonamentoService => Create<IEscalonamentoService>();

        private IGrupoDocumentoRequeridoService GrupoDocumentoRequeridoService => Create<IGrupoDocumentoRequeridoService>();

        #endregion Serviços

        #region Fluxo Página

        [ChildActionOnly]
        public ActionResult MenuPaginas(MatriculaPaginaViewModelBase model)
        {
            // Busca os dados para apresentação do menu de matrículas
            var cabecalho = SolicitacaoMatriculaService.BuscarCabecalhoMenu(model.SeqSolicitacaoMatricula);

            model.DescricaoProcesso = cabecalho.DescricaoProcesso;
            model.DescricaoNivelEnsino = cabecalho.DescricaoNivelEnsino;
            model.DescricaoVinculo = cabecalho.DescricaoVinculo;
            model.DescricaoUnidadeResponsavel = cabecalho.DescricaoUnidadeResponsavel;

            model.DescricaoCurso = cabecalho.DescricaoCurso;
            model.ExibeDescricaoCurso = cabecalho.ExibeDescricaoCurso;
            model.ExibeEntidadeResponsavel = cabecalho.ExibeEntidadeResponsavel;
            model.ExibeNivelEnsino = cabecalho.ExibeNivelEnsino;
            model.ExibeVinculo = cabecalho.ExibeVinculo;

            return PartialView("_MenuProcesso", model);
        }

        private List<EtapaListaViewModel> BuscarEtapas(long seqSolicitacaoServico, bool ignoreSession = false)
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
        /// <param name="solicitacaoMatricula">Solicitação de matrícula atual</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração da etapa atual</param>
        /// <param name="tokenRet">Token de uma página específica para retorno</param>
        /// <returns>Filtro com os dados da página</returns>
        private MatriculaPaginaFiltroViewModel RecuperarPaginaAtual(SolicitacaoMatriculaData solicitacaoMatricula, long seqConfiguracaoEtapa, string tokenRet)
        {
            // Recupera a etapa atual, de acordo com a solicitação de matrícula
            var etapaAtual = solicitacaoMatricula.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == seqConfiguracaoEtapa);

            // Recupera as configurações da etapa atual
            var configuracaoEtapaAtual = ConfiguracaoEtapaService.BuscarConfiguracaoEtapa(seqConfiguracaoEtapa, IncludesConfiguracaoEtapa.ConfiguracoesPagina | IncludesConfiguracaoEtapa.ConfiguracoesPagina_Arquivos | IncludesConfiguracaoEtapa.ConfiguracoesPagina_TextosSecao | IncludesConfiguracaoEtapa.ProcessoEtapa);
            configuracaoEtapaAtual.ConfiguracoesPagina = configuracaoEtapaAtual.ConfiguracoesPagina.OrderBy(p => p.Ordem).ToList();

            // Cria o retorno
            MatriculaPaginaFiltroViewModel ret = null;
            long SeqPaginaEtapaSgf = 0;

            if (!etapaAtual.HistoricosNavegacao.Any())
            {
                // Caso não tenha histórico, aponta o usuário para a primeira página da etapa

                var primeiraPagina = configuracaoEtapaAtual.ConfiguracoesPagina.FirstOrDefault();
                ret = new MatriculaPaginaFiltroViewModel
                {
                    RedirecionarHome = false,
                    SeqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa),
                    SeqConfiguracaoEtapaPagina = new SMCEncryptedLong(primeiraPagina.Seq),
                    SeqIngressante = new SMCEncryptedLong(solicitacaoMatricula.SeqPessoaAtuacao),
                    SeqSolicitacaoMatricula = new SMCEncryptedLong(solicitacaoMatricula.Seq),
                    SeqProcesso = new SMCEncryptedLong(solicitacaoMatricula.SeqProcesso),
                    SeqEntidadeResponsavel = new SMCEncryptedLong(solicitacaoMatricula.SeqEntidadeResponsavel),
                    SeqSolicitacaoServicoEtapa = etapaAtual.Seq,
                    ConfiguracaoEtapaPagina = primeiraPagina.Transform<ConfiguracaoEtapaPaginaViewModel>(),
                    ConfiguracaoEtapa = configuracaoEtapaAtual.Transform<ConfiguracaoEtapaViewModel>(),
                    DescricaoEtapa = configuracaoEtapaAtual.ProcessoEtapa.DescricaoEtapa
                };
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

                    ret = new MatriculaPaginaFiltroViewModel
                    {
                        RedirecionarHome = false,
                        SeqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa),
                        SeqConfiguracaoEtapaPagina = new SMCEncryptedLong(paginaToken.Seq),
                        SeqIngressante = new SMCEncryptedLong(solicitacaoMatricula.SeqPessoaAtuacao),
                        SeqSolicitacaoMatricula = new SMCEncryptedLong(solicitacaoMatricula.Seq),
                        SeqProcesso = new SMCEncryptedLong(solicitacaoMatricula.SeqProcesso),
                        SeqEntidadeResponsavel = new SMCEncryptedLong(solicitacaoMatricula.SeqEntidadeResponsavel),
                        SeqSolicitacaoServicoEtapa = etapaAtual.Seq,
                        ConfiguracaoEtapaPagina = paginaToken.Transform<ConfiguracaoEtapaPaginaViewModel>(),
                        ConfiguracaoEtapa = configuracaoEtapaAtual.Transform<ConfiguracaoEtapaViewModel>(),
                        DescricaoEtapa = configuracaoEtapaAtual.ProcessoEtapa.DescricaoEtapa
                    };
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
                            ret = new MatriculaPaginaFiltroViewModel
                            {
                                RedirecionarHome = false,
                                SeqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa),
                                SeqConfiguracaoEtapaPagina = new SMCEncryptedLong(proximaPagina.Seq),
                                SeqIngressante = new SMCEncryptedLong(solicitacaoMatricula.SeqPessoaAtuacao),
                                SeqSolicitacaoMatricula = new SMCEncryptedLong(solicitacaoMatricula.Seq),
                                SeqProcesso = new SMCEncryptedLong(solicitacaoMatricula.SeqProcesso),
                                SeqEntidadeResponsavel = new SMCEncryptedLong(solicitacaoMatricula.SeqEntidadeResponsavel),
                                SeqSolicitacaoServicoEtapa = etapaAtual.Seq,
                                ConfiguracaoEtapaPagina = proximaPagina.Transform<ConfiguracaoEtapaPaginaViewModel>(),
                                ConfiguracaoEtapa = configuracaoEtapaAtual.Transform<ConfiguracaoEtapaViewModel>(),
                                DescricaoEtapa = configuracaoEtapaAtual.ProcessoEtapa.DescricaoEtapa
                            };
                            SeqPaginaEtapaSgf = proximaPagina.SeqPaginaEtapaSgf;
                        }
                        else
                        {
                            // Não tem próxima página.
                            // Retorna a última página da etapa
                            ret = new MatriculaPaginaFiltroViewModel
                            {
                                RedirecionarHome = false,
                                SeqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa),
                                SeqConfiguracaoEtapaPagina = new SMCEncryptedLong(configuracaoPaginaHistorico.Seq),
                                SeqIngressante = new SMCEncryptedLong(solicitacaoMatricula.SeqPessoaAtuacao),
                                SeqSolicitacaoMatricula = new SMCEncryptedLong(solicitacaoMatricula.Seq),
                                SeqProcesso = new SMCEncryptedLong(solicitacaoMatricula.SeqProcesso),
                                SeqEntidadeResponsavel = new SMCEncryptedLong(solicitacaoMatricula.SeqEntidadeResponsavel),
                                SeqSolicitacaoServicoEtapa = etapaAtual.Seq,
                                ConfiguracaoEtapaPagina = configuracaoPaginaHistorico.Transform<ConfiguracaoEtapaPaginaViewModel>(),
                                ConfiguracaoEtapa = configuracaoEtapaAtual.Transform<ConfiguracaoEtapaViewModel>(),
                                DescricaoEtapa = configuracaoEtapaAtual.ProcessoEtapa.DescricaoEtapa
                            };
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
                        ret = new MatriculaPaginaFiltroViewModel
                        {
                            RedirecionarHome = false,
                            SeqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa),
                            SeqConfiguracaoEtapaPagina = new SMCEncryptedLong(ultimoHistorico.SeqConfiguracaoEtapaPagina),
                            SeqIngressante = new SMCEncryptedLong(solicitacaoMatricula.SeqPessoaAtuacao),
                            SeqSolicitacaoMatricula = new SMCEncryptedLong(solicitacaoMatricula.Seq),
                            SeqProcesso = new SMCEncryptedLong(solicitacaoMatricula.SeqProcesso),
                            SeqEntidadeResponsavel = new SMCEncryptedLong(solicitacaoMatricula.SeqEntidadeResponsavel),
                            SeqSolicitacaoServicoEtapa = etapaAtual.Seq,
                            ConfiguracaoEtapaPagina = configuracaoPaginaHistorico.Transform<ConfiguracaoEtapaPaginaViewModel>(),
                            ConfiguracaoEtapa = configuracaoEtapaAtual.Transform<ConfiguracaoEtapaViewModel>(),
                            DescricaoEtapa = configuracaoEtapaAtual.ProcessoEtapa.DescricaoEtapa
                        };
                        SeqPaginaEtapaSgf = configuracaoPaginaHistorico.SeqPaginaEtapaSgf;
                    }
                }
            }

            // Caso tenha sequencial de página do SGF, busca a configuração da página no SGF para recuperar o sequencial do histórico de situação da mesma
            if (SeqPaginaEtapaSgf != 0)
            {
            }
            return ret;
        }

        private ActionResult EntrarEtapa(long seqSolicitacaoMatricula, SMCEncryptedLong seqConfiguracaoEtapa, string tokenRet)
        {
            return EntrarEtapa(new SMCEncryptedLong(seqSolicitacaoMatricula), seqConfiguracaoEtapa, tokenRet);
        }

        [SMCAuthorize(
            UC_MAT_003_01_01.PAGINA_INSTRUCOES_INICIAIS,
            UC_MAT_003_04_01.PAGINA_VISUALIZACAO_BLOQUEIOS_SGAALUNO,
            UC_MAT_003_08_01.PAGINA_SELECAO_TURMAS_SGAALUNO,
            UC_MAT_003_12_01.PAGINA_CONDICOES_PAGAMENTO,
            UC_MAT_003_13_01.PAGINA_CONTRATO_PRESTACAO_SERVICOS_EDUCACIONAIS,
            UC_MAT_003_21_01.PAGINA_INSTRUCOES_FINAIS_SGAALUNO,
            UC_MAT_003_31_01.PAGINA_SELECAO_ATIVIDADES_ACADEMICAS,
            UC_MAT_003_34_01.PAGINA_ADESAO_RENOVACAO,
            UC_MAT_003_06_01.PAGINA_ENTREGA_DOCUMENTOS,
            UC_MAT_003_41_01.PAGINA_ADESAO_CONTRATO_LATO_SENSU
            )]
        public ActionResult EntrarEtapa(SMCEncryptedLong seqSolicitacaoMatricula, SMCEncryptedLong seqConfiguracaoEtapa, string tokenRet, string backUrl = null)
        {

            if (seqSolicitacaoMatricula <= 0 || seqConfiguracaoEtapa <= 0)
                return RedirectToAction("Index", "Home", new { area = "" });

            // Busca a solicitação de matrícula.
            var solicitacaoMatricula = SolicitacaoMatriculaService.BuscarSolicitacaoMatricula(seqSolicitacaoMatricula);

            // Busca as etapas configuradas para este ingressante
            var etapasIngressante = BuscarEtapas(seqSolicitacaoMatricula, true);
            var etapaIngressanteAtual = etapasIngressante.FirstOrDefault(e => e.SeqConfiguracaoEtapa == seqConfiguracaoEtapa);
            var etapaIngressanteAnterior = etapasIngressante.OrderBy(x => x.OrdemEtapaSGF).FirstOrDefault(e => e.OrdemEtapaSGF == (etapaIngressanteAtual.OrdemEtapaSGF - 1));

            // Com o template de processo em mãos, e a etapa atual com seus históricos, conseguimos decidir qual página exibir
            var paginaAtual = RecuperarPaginaAtual(solicitacaoMatricula, seqConfiguracaoEtapa, tokenRet);
            paginaAtual.EtapaFinalizada = etapaIngressanteAtual.SituacaoEtapaIngressante.HasFlag(SituacaoEtapaSolicitacaoMatricula.Finalizada);

            //Seta o back url da pagina para retornar a página que chamou o fluxo 
            paginaAtual.BackUrl = backUrl;

            // Redireciona para a primeira página
            if (paginaAtual.RedirecionarHome)
            {

                return RedirectToAction("Index", "Solicitacao");
            }
            else
            {
                // Redireciona corretamente para página da etapa atual
                switch (paginaAtual.ConfiguracaoEtapaPagina.TokenPagina)
                {
                    case MatriculaTokens.INSTRUCOES_INICIAIS_MATRICULA:
                        return InstrucoesIniciais(paginaAtual);

                    case MatriculaTokens.SELECAO_TURMA_MATRICULA:
                        return SelecaoTurma(paginaAtual);

                    case MatriculaTokens.SELECAO_ATIVIDADE_ACADEMICA_MATRICULA:
                        return SelecaoAtividadeAcademica(paginaAtual);

                    case MatriculaTokens.SELECAO_CONDICAO_PGTO_MATRICULA:
                        return SelecaoCondicaoPagamento(paginaAtual);

                    case MatriculaTokens.ADESAO_CONTRATO_MATRICULA:
                        return AdesaoContrato(paginaAtual);

                    case MatriculaTokens.ADESAO_CONTRATO_RENOVACAO:
                        return AdesaoContratoRenovacao(paginaAtual);

                    case MatriculaTokens.ADESAO_CONTRATO_LATO_SENSU:
                        return AdesaoContratoRenovacaoResidenciaMedica(paginaAtual);

                    case MatriculaTokens.INSTRUCOES_FINAIS_MATRICULA:
                        return InstrucoesFinais(paginaAtual);

                    case MatriculaTokens.INSTRUCOES_FINAIS_SOLICITACAO_MATRICULA:
                        return InstrucoesFinaisSolicitacaoMatricula(paginaAtual);

                    case MatriculaTokens.SOLICITACAO_UPLOAD_DOCUMENTO:
                        return SolicitacaoUpload(paginaAtual);

                    case MatriculaTokens.COMPLEMENTACAO_DADOS_CADASTRAIS:
                        return ComplementacaoDadosCadastrais(paginaAtual);

                    case MatriculaTokens.MONTAGEM_PLANO_ESTUDOS:
                        return MontagemPlanoEstudos(paginaAtual);

                    case MatriculaTokens.SOLICITACAO_DISPENSA_DISCIPLINA:
                        return SelecaoDisciplinasParaDispensa(paginaAtual);

                    //case MatriculaTokens.SOLICITACAO_FORMULARIO:
                    //    return SolicitacaoFormulario(paginaAtual);

                    default:
                        return SolicitacaoFormulario(paginaAtual);
                        //throw new SMCApplicationException($"Token '{paginaAtual.ConfiguracaoEtapaPagina.TokenPagina}' não existe no fluxo de matrícula.");
                }
            }
        }

        #endregion Fluxo Página

        #region Download de Arquivos

        public FileResult DownloadDocumentoGuid(Guid guidFile)
        {
            var arquivo = ArquivoAnexadoService.BuscarArquivoAnexadoPorGuid(guidFile);
            Response.AddHeader("Content-Disposition", "inline; filename= " + arquivo.Name);
            return File(arquivo.FileData, arquivo.Type);
        }

        public FileResult DownloadDocumento(SMCEncryptedLong id)
        {
            var arquivo = ArquivoAnexadoService.BuscarArquivoAnexado(id);
            Response.AddHeader("Content-Disposition", "inline; filename= " + arquivo.Name);
            return File(arquivo.FileData, arquivo.Type);
        }

        #endregion Download de Arquivos

        #region Configuração das páginas

        private void VerificarPreCondicoesEntradaPagina(MatriculaPaginaFiltroViewModel filtro, bool ignoreFinalizedValidation = false)
        {
            // Verifica se está selecionada a solicitação de matrícula corretamente
            if (Session[SolicitacaoMatriculaConstants.KEY_SESSION_PESSOA_ATUACAO_SELECIONADA] as SolicitacaoMatriculaSelecionadaViewModel == null)
                ThrowRedirect(new SMCApplicationException("Você deve selecionar qual processo deseja prosseguir para matrícula."), "Index", "Home", new RouteValueDictionary { { "area", "MAT" } });

            if (filtro.EtapaFinalizada && (filtro.ConfiguracaoEtapaPagina.TokenPagina != MatriculaTokens.INSTRUCOES_FINAIS_MATRICULA && filtro.ConfiguracaoEtapaPagina.TokenPagina != MatriculaTokens.INSTRUCOES_FINAIS_SOLICITACAO_MATRICULA))
                ThrowRedirect(new SMCApplicationException($"Não é possível acessar esta página pois esta etapa de sua inscrição encontra-se finalizada."), "Index", "Home", new RouteValueDictionary { { "area", "MAT" } });

            // Verifica se existe alguma outra situação que impede o acesso à etapa
            SolicitacaoServicoEtapaService.VerificarAcessoEtapa(filtro.SeqSolicitacaoMatricula, filtro.SeqConfiguracaoEtapa, ignoreFinalizedValidation);
        }

        private void VerificarPreCondicoesEntradaPagina(MatriculaPaginaViewModelBase modelBase)
        {
            // Verifica se está selecionada a solicitação de matrícula corretamente
            if (Session[SolicitacaoMatriculaConstants.KEY_SESSION_PESSOA_ATUACAO_SELECIONADA] as SolicitacaoMatriculaSelecionadaViewModel == null)
                ThrowRedirect(new SMCApplicationException("Você deve selecionar qual processo deseja prosseguir para matrícula."), "Index", "Home", new RouteValueDictionary { { "area", "MAT" } });

            // Verifica se existe alguma outra situação que impede o acesso à etapa
            SolicitacaoServicoEtapaService.VerificarAcessoEtapa(modelBase.SeqSolicitacaoMatricula, modelBase.SeqConfiguracaoEtapa);
        }

        private void VerificarHistoricoSituacao(MatriculaPaginaFiltroViewModel filtro)
        {
            // Busca a solicitação de matrícula.
            var solicitacaoMatricula = SolicitacaoMatriculaService.BuscarSolicitacaoMatricula(filtro.SeqSolicitacaoMatricula);

            // Busca as etapas configuradas para este ingressante
            var etapasIngressante = BuscarEtapas(filtro.SeqSolicitacaoMatricula);
            var etapaIngressanteAtual = etapasIngressante.FirstOrDefault(e => e.SeqConfiguracaoEtapa == filtro.SeqConfiguracaoEtapa);

            // Após recuperar a página que deve ser exibida, antes de retornar o usuário para a mesma, vamos
            var paginaSGFAtual = etapaIngressanteAtual.Paginas.FirstOrDefault(p => p.Seq == filtro.ConfiguracaoEtapaPagina.SeqPaginaEtapaSgf);
            if (paginaSGFAtual != null && paginaSGFAtual.SeqSituacaoEtapaInicial.HasValue)
            {
                var historicos = solicitacaoMatricula.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == filtro.SeqConfiguracaoEtapa)?.HistoricosSituacao;
                //if (historicos == null || historicos.LastOrDefault(h => !h.DataExclusao.HasValue && h.SeqSituacaoEtapaSgf == paginaSGFAtual.SeqSituacaoEtapaInicial) == null)
                if (historicos == null || historicos.LastOrDefault(h => !h.DataExclusao.HasValue)?.SeqSituacaoEtapaSgf != paginaSGFAtual.SeqSituacaoEtapaInicial)
                {
                    // Salva o histórico novo
                    SolicitacaoHistoricoSituacaoService.AtualizarHistoricoSituacao(filtro.SeqSolicitacaoServicoEtapa, paginaSGFAtual.SeqSituacaoEtapaInicial.Value);
                }
            }
        }

        private void ConfigurarPagina<TModelMatricula>(TModelMatricula pagina, MatriculaPaginaFiltroViewModel filtro, Func<TModelMatricula, string, ActionResult> metodoSalvarEtapa, long seqSolicitacaoHistoricoNavegacao)
            where TModelMatricula : MatriculaPaginaViewModelBase
        {
            if (string.IsNullOrWhiteSpace(pagina.Token))
                throw new SMCApplicationException("A página deve ter um token configurado.");

            pagina.SeqConfiguracaoEtapa = filtro.SeqConfiguracaoEtapa;
            pagina.SeqConfiguracaoEtapaPagina = filtro.SeqConfiguracaoEtapaPagina;
            pagina.Titulo = filtro.ConfiguracaoEtapaPagina.TituloPagina;
            //pagina.ExibeMenu = filtro.ConfiguracaoEtapaPagina.ExibeMenu;
            pagina.Ordem = filtro.ConfiguracaoEtapaPagina.Ordem;
            pagina.SeqSolicitacaoMatricula = filtro.SeqSolicitacaoMatricula;
            pagina.ActionSalvarEtapa = metodoSalvarEtapa.Method.Name;
            pagina.SeqSolicitacaoHistoricoNavegacao = (SMCEncryptedLong)seqSolicitacaoHistoricoNavegacao;
            pagina.EtapaFinalizada = filtro.EtapaFinalizada;
            pagina.DescricaoEtapa = filtro.DescricaoEtapa;
            pagina.SeqSolicitacaoServicoEtapa = filtro.SeqSolicitacaoServicoEtapa;
            pagina.SeqPessoaAtuacao = filtro.SeqIngressante;

            // Verifica se existe algum bloqueio
            pagina.Bloqueios = PessoaAtuacaoBloqueioService.BuscarPessoaAtuacaoBloqueios(filtro.SeqIngressante, filtro.SeqConfiguracaoEtapa, false).TransformList<PessoaAtuacaoBloqueioViewModel>();

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
                Arquivos = x.Select(a => new TemplateArquivoSecaoViewModel
                {
                    Descricao = a.MensagemArquivo,
                    NomeLink = a.LinkArquivo,
                    SeqArquivo = a.SeqArquivoAnexado,
                    //GuidFile = a.uid
                }).ToList()
            });

            if (secoesTexto != null)
                pagina.Secoes.AddRange(secoesTexto);

            if (secoesArquivo != null)
                pagina.Secoes.AddRange(secoesArquivo);

            // Recupera as configurações de página do SGF para saber qual é a situação final da etapa
            var etapas = BuscarEtapas(filtro.SeqSolicitacaoMatricula);
            pagina.SeqSituacaoFinalSucesso = etapas?.FirstOrDefault(e => e.SeqConfiguracaoEtapa == filtro.SeqConfiguracaoEtapa)?.Situacoes.FirstOrDefault(s => s.SituacaoFinalEtapa && s.ClassificacaoSituacaoFinal == Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.FinalizadoComSucesso).Seq;
            pagina.SeqSituacaoFinalSemSucesso = etapas?.FirstOrDefault(e => e.SeqConfiguracaoEtapa == filtro.SeqConfiguracaoEtapa)?.Situacoes.FirstOrDefault(s => s.SituacaoFinalEtapa && s.ClassificacaoSituacaoFinal == Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.FinalizadoSemSucesso).Seq;
            pagina.SeqSituacaoFinalCancelada = etapas?.FirstOrDefault(e => e.SeqConfiguracaoEtapa == filtro.SeqConfiguracaoEtapa)?.Situacoes.FirstOrDefault(s => s.SituacaoFinalEtapa && s.ClassificacaoSituacaoFinal == Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.Cancelado).Seq;

            pagina.FluxoPaginas = filtro.ConfiguracaoEtapa.ConfiguracoesPagina.Select(p => new TemplateFluxoPaginaViewModel
            {
                Ordem = p.Ordem,
                SeqConfiguracaoEtapaPagina = p.Seq,
                Titulo = p.TituloPagina,
                Token = p.TokenPagina,
                //SeqFormularioSGF = ,
                //SeqVisaoSGF = ,
            }).ToList<ITemplateFluxoPagina>();
        }

        #endregion Configuração das páginas

        // Esta página não faz mais parte do fluxo
        /*#region Página ExibicaoBloqueio

        private ActionResult ExibicaoBloqueio(MatriculaPaginaFiltroViewModel filtro)
        {
            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new PaginaExibicaoBloqueioViewModel();

            // Busca os bloqueios do usuário
            // COMENTADO PARA NAO TRAVAR A TELA SE PORVENTURA ALGUEM CLICAR EM BLOQUEIOS
            model.Bloqueios = PessoaAtuacaoBloqueioService.BuscarPessoaAtuacaoBloqueiosMatricula(filtro.SeqIngressante, filtro.SeqConfiguracaoEtapa).TransformList<PessoaAtuacaoBloqueioViewModel>();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, SalvarExibicaoBloqueioMatricula, historico.Seq);

            /*  RN_MAT_014
                Ao acessar a página de visualização de bloqueios, verificar os bloqueios que foram configurados para a etapa em questão que impedem a solicitação do serviço e que impedem a execução do serviço.
                Dentre essa lista de bloqueios, se a pessoa correspondente à pessoa-atuação não possuir algum bloqueio:
                - Exibir a próxima página após a página atual, do fluxo de páginas da etapa que foi configurada para a configuração do processo da pessoa-atuação em questão.
                - Gravar o registro de saída no histórico de navegação da etapa. * /

            // Caso não tenha nenhum bloqueio, passa para próxima página
            if (model.Bloqueios == null || model.Bloqueios.Count == 0)
                return SalvarExibicaoBloqueioMatricula(model, null);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("ExibicaoBloqueio", model);
        }

        [HttpPost]
        public ActionResult SalvarExibicaoBloqueioMatricula(PaginaExibicaoBloqueioViewModel model, string tokenRet)
        {
            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", "Matricula", new { SeqSolicitacaoMatricula = new SMCEncryptedLong(model.SeqSolicitacaoMatricula), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        #endregion Página ExibicaoBloqueio*/

        #region Página Instruções Iniciais

        private ActionResult InstrucoesIniciais(MatriculaPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_01_01.PAGINA_INSTRUCOES_INICIAIS))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new PaginaInstrucoesIniciaisViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, SalvarInstrucoesIniciaisMatricula, historico.Seq);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;
            return View("InstrucoesIniciais", model);
        }

        [SMCAuthorize(UC_MAT_003_01_01.PAGINA_INSTRUCOES_INICIAIS)]
        [HttpPost]
        public ActionResult SalvarInstrucoesIniciaisMatricula(PaginaInstrucoesIniciaisViewModel model, string tokenRet)
        {
            // Verifica se pode efetuar essa ação
            VerificarPreCondicoesEntradaPagina(model);

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", "Matricula", new { SeqSolicitacaoMatricula = new SMCEncryptedLong(model.SeqSolicitacaoMatricula), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        #endregion Página Instruções Iniciais

        #region Seleção de Turma

        private ActionResult SelecaoTurma(MatriculaPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_08_01.PAGINA_SELECAO_TURMAS_SGAALUNO))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new PaginaSelecaoTurmaMatriculaViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, SalvarSelecaoTurmaMatricula, historico.Seq);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            var parametrosValidacao = InstituicaoNivelTipoVinculoAlunoService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(filtro.SeqIngressante);

            model.SeqIngressante = filtro.SeqIngressante;
            model.SeqProcesso = filtro.SeqProcesso;
            model.SeqProcessoEtapa = filtro.ConfiguracaoEtapa.SeqProcessoEtapa;
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoMatricula = new SMCEncryptedLong(filtro.SeqSolicitacaoMatricula), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });
            model.ExigirCurso = parametrosValidacao.ExigeCurso;
            model.ExigirMatrizCurricularOferta = parametrosValidacao.ExigeOfertaMatrizCurricular;
            model.EtapaFinalizada = filtro.EtapaFinalizada;
            model.ExibirIntegralizacao = HistoricoEscolarService.VisualizarConsultaConcederFormacaoIntegralizacao(filtro.SeqIngressante);

            TurmaPreencherMatriculaItem(filtro.SeqSolicitacaoMatricula, model);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("SelecaoTurma", model);
        }

        [SMCAuthorize(UC_MAT_003_08_01.PAGINA_SELECAO_TURMAS_SGAALUNO)]
        [HttpPost]
        public ActionResult SalvarSelecaoTurmaMatricula(PaginaSelecaoTurmaMatriculaViewModel model, string tokenRet)
        {
            // Verifica se pode efetuar essa ação
            VerificarPreCondicoesEntradaPagina(model);
            var tokenPagina = ConfiguracaoEtapaPaginaService.BuscarConfiguracaoEtapaPaginaPorSeqProcessoEtapa(model.SeqProcessoEtapa);
            // Validar pre/co requisitos
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                var validarTodosItens = SolicitacaoMatriculaItemService.ValidaSolicitacaoMatriculaItemPreCoRequisito(model.SeqSolicitacaoMatricula, model.SeqPessoaAtuacao, TipoGestaoDivisaoComponente.Turma);

                if (!validarTodosItens.valido)
                    throw new TurmaPreCoRequisitoInvalidoException($"</br> {string.Join("</br>", validarTodosItens.mensagemErro)}");

                ////TODO Task_24734 - Código comentado para o momento e assim que ocorrer acerto de dados será utlizado novamente
                //if (geraOrientacao.Count > 0)
                //    throw new TurmaGerarOrientacaoInvalidoException($"</br> {string.Join(",</br>", geraOrientacao)}");

                // Bug 32655: Solicitaram tirar a validação de turmas repetidas ao salvar.
                //var validarTurmaIgual = SolicitacaoMatriculaItemService.ValidaSolicitacaoMatriculaItemTurmaDuplicada(model.SeqSolicitacaoMatricula);
                //if (!validarTurmaIgual.valido)
                //    throw new TurmaIgualSelecionadaInvalidoException($"</br> {string.Join("</br>", validarTurmaIgual.mensagemErro)}");
                
                var turmasDuplicadas = SolicitacaoMatriculaItemService.ValidarTurmasDuplicadas(model.SeqSolicitacaoMatricula, model.SeqPessoaAtuacao, model.SeqProcessoEtapa, null);
                if(!string.IsNullOrEmpty(turmasDuplicadas))
                    throw new TurmaIgualSelecionadaInvalidoException(turmasDuplicadas);

                // Task 24482: Bug 24483: Segundo analista não deve bloquear caso não tenha turma selecionada.
                // Task 28479: Bug 28476: Quando for disciplina isolada deve validar se informou pelo menos uma turma
                var registrosValidacao = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(model.SeqSolicitacaoMatricula, model.SeqPessoaAtuacao, null, model.SeqProcessoEtapa, false).ToList();
                if (!model.ExigirCurso.GetValueOrDefault() && (registrosValidacao == null || registrosValidacao.Count == 0))
                {
                    // Task 28932: Bug 28930: Independente do motivo da situação do item, considerar a regra.
                    //if (SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemCanceladoPorSolicitante(model.SeqSolicitacaoMatricula))
                    //    throw new TurmaSemCursoCanceladoInvalidoException();
                    //else
                    throw new TurmaSemCursoSelecionadoInvalidoException();
                }

                //Valida se ja foi aprovado ou dispensado na turma 
                var turmasAprovadasDispensadas = SolicitacaoMatriculaItemService.VerificarTurmasAprovadasDispensadasSelecaoTurma(model.SeqSolicitacaoMatricula);
                if (!string.IsNullOrEmpty(turmasAprovadasDispensadas))
                {
                    throw new TurmaJaAprovadaDispensadaException(turmasAprovadasDispensadas);
                }


                if(!tokenPagina.Any(x => x.TokenPagina.Equals("SELECAO_ATIVIDADE_ACADEMICA_MATRICULA") || x.TokenPagina.Equals("CONFIRMACAO_SELECAO_ATIVIDADE_ACADEMICA")))
                {
                    if(!registrosValidacao.Any(x => (x.SituacaoInicial.HasValue && x.SituacaoInicial.Value) 
                    && (x.SituacaoFinal.HasValue && x.SituacaoFinal.Value) && x.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso && (x.PertencePlanoEstudo.HasValue && !x.PertencePlanoEstudo.Value)))
                    {
                        throw new TurmaSelecaoObrigatoriaException();
                    }
                }
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

                    var turmas = this.SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(model.SeqSolicitacaoMatricula, 0, null, model.SeqProcessoEtapa, false);
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

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", "Matricula", new { SeqSolicitacaoMatricula = new SMCEncryptedLong(model.SeqSolicitacaoMatricula), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        private void TurmaPreencherMatriculaItem(long seqSolicitacaoMatricula, PaginaSelecaoTurmaMatriculaViewModel model)
        {
            // Cria o modelo para a página em questão
            model.ExibirCancelados = SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaTurmasCancelada(seqSolicitacaoMatricula) || SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemCancelado(seqSolicitacaoMatricula);
            model.TurmasMatriculaItem = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(seqSolicitacaoMatricula, model.SeqIngressante, null, model.SeqProcessoEtapa, true).TransformList<TurmaMatriculaItemViewModel>();
            model.TurmasMatriculaItem.SMCForEach(f =>
            {
                f.ExigirCurso = model.ExigirCurso;
                f.ExigirMatrizCurricularOferta = model.ExigirMatrizCurricularOferta;
                f.SeqProcessoEtapa = model.SeqProcessoEtapa;
            });

            if (model.ExibirCancelados.HasValue &&
                model.ExibirCancelados.Value &&
                model.TurmasMatriculaItem.Count == 0 &&
                model.ExigirCurso == false)
            {
                model.TodosCancelados = !SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemCanceladoPorSolicitante(seqSolicitacaoMatricula);
            }

            //Session[MatriculaConstants.KEY_SESSION_TURMAS_SELECIONADAS] = model.TurmasMatriculaItem.SelectMany(x => x.TurmaMatriculaDivisoes.Select(d => d.Seq.GetValueOrDefault())).ToList();
        }

        [HttpGet]
        public ActionResult TurmaAlterarSelecaoAluno(long seqTurma, string seqSolicitacaoMatricula, long seqProcessoEtapa, long seqIngressante, string seqConfiguracaoEtapa)
        {
            var turmaEditar = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(new SMCEncryptedLong(seqSolicitacaoMatricula).Value, seqIngressante, seqTurma, seqProcessoEtapa, false).TransformList<TurmaMatriculaItemViewModel>().First();

            if (!(turmaEditar != null && turmaEditar.TurmaMatriculaDivisoes != null))
                throw new SelecionarTurmaInvalidoException();

            if (turmaEditar.TurmaMatriculaDivisoes.Count(c => c.PermitirGrupo && c.DivisoesTurmas.Count() > 1) == 0)
                ThrowOpenModalException(new EditarTurmaSemGrupoInvalidoException().Message);

            if (turmaEditar.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso && turmaEditar.PertencePlanoEstudo == false)
                ThrowOpenModalException(new EditarTurmaGrupoChancelaIndeferidaException().Message);

            Session["SeqIngressanteAlterarTurma"] = seqIngressante;

            turmaEditar.TurmaMatriculaDivisoes.ForEach(d =>
            {
                d.SeqDivisaoTurmaDisplay = d.SeqDivisaoTurma;
            });

            turmaEditar.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoMatricula = new SMCEncryptedLong(seqSolicitacaoMatricula), seqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) });
            turmaEditar.SeqProcessoEtapa = seqProcessoEtapa;
            turmaEditar.SeqIngressante = seqIngressante;
            turmaEditar.SeqSolicitacaoMatricula = new SMCEncryptedLong(seqSolicitacaoMatricula).Value;
            return PartialView("_SelecaoTurmaOfertaEditar", turmaEditar);
        }

        [HttpPost]
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

                var turmas = this.SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasItens(model.SeqSolicitacaoMatricula, model.SeqIngressante, null, model.SeqProcessoEtapa, false);
                var turmasMatricula = turmas.Where(w => w.CodigoFormatado != model.CodigoFormatado).SelectMany(s => s.TurmaMatriculaDivisoes).ToList();

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
        public ActionResult TurmaExcluirSelecaoAluno(long seqTurma, string seqSolicitacaoMatricula, long seqProcessoEtapa, string seqConfiguracaoEtapa)
        {
            // Simula um modelo para enviar pra validação de pré condições de entrada na página para validar se pode ou não fazer modificações
            VerificarPreCondicoesEntradaPagina(new MatriculaPaginaViewModelBase { SeqSolicitacaoMatricula = new SMCEncryptedLong(seqSolicitacaoMatricula).Value, SeqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa).Value });

            SolicitacaoMatriculaItemService.RemoverSolicitacaoMatriculaItemPorTurma(new SMCEncryptedLong(seqSolicitacaoMatricula).Value, seqTurma, seqProcessoEtapa);

            return SMCRedirectToUrl(Url.Action("EntrarEtapa", new { seqSolicitacaoMatricula = new SMCEncryptedLong(seqSolicitacaoMatricula), seqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) }));
        }

        #endregion Seleção de Turma

        #region Seleção Atividade Acadêmica

        private ActionResult SelecaoAtividadeAcademica(MatriculaPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_31_01.PAGINA_SELECAO_ATIVIDADES_ACADEMICAS))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new PaginaSelecaoAtividadeAcademicaMatriculaViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, SalvarSelecaoAtividadeAcademicaMatricula, historico.Seq);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            model.SeqIngressante = filtro.SeqIngressante;
            model.SeqProcesso = filtro.SeqProcesso;
            model.SeqProcessoEtapa = filtro.ConfiguracaoEtapa.SeqProcessoEtapa;
            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoMatricula = new SMCEncryptedLong(filtro.SeqSolicitacaoMatricula), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });
            model.EtapaFinalizada = filtro.EtapaFinalizada;
            model.ExibirIntegralizacao = HistoricoEscolarService.VisualizarConsultaConcederFormacaoIntegralizacao(filtro.SeqIngressante);

            AtividadePreencherMatriculaItem(filtro.SeqSolicitacaoMatricula, model);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("SelecaoAtividadeAcademica", model);
        }

        [SMCAuthorize(UC_MAT_003_31_01.PAGINA_SELECAO_ATIVIDADES_ACADEMICAS)]
        [HttpPost]
        public ActionResult SalvarSelecaoAtividadeAcademicaMatricula(PaginaSelecaoAtividadeAcademicaMatriculaViewModel model, string tokenRet)
        {
            // Verifica se pode efetuar essa ação
            VerificarPreCondicoesEntradaPagina(model);

            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                // Busca as atividades já selecionadas para a validação
                var validarTodosItens = SolicitacaoMatriculaItemService.ValidaSolicitacaoMatriculaItemPreCoRequisito(model.SeqSolicitacaoMatricula, model.SeqIngressante);

                if (!validarTodosItens.valido)
                    throw new ConfiguracaoComponenteCoRequisitoInvalidoException($"</br> {string.Join("</br>", validarTodosItens.mensagemErro)}");
            }

            if (string.IsNullOrWhiteSpace(tokenRet) &&
                !(model.AtividadesAcademicaMatriculaItem != null && model.AtividadesAcademicaMatriculaItem.Count > 0) &&
                !(SolicitacaoMatriculaItemService.VerificarTurmasAtividadesCadastradas(model.SeqSolicitacaoMatricula)))
                throw new SelecionarAtividadeInvalidoException();

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", "Matricula", new { SeqSolicitacaoMatricula = new SMCEncryptedLong(model.SeqSolicitacaoMatricula), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        private void AtividadePreencherMatriculaItem(long seqSolicitacaoMatricula, PaginaSelecaoAtividadeAcademicaMatriculaViewModel model)
        {
            // Cria o modelo para a página em questão
            model.ExibirCancelados = SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaTurmasCancelada(seqSolicitacaoMatricula) || SolicitacaoMatriculaItemService.VerificarSolicitacaoMatriculaItemCancelado(seqSolicitacaoMatricula);
            model.AtividadesAcademicaMatriculaItem = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaAtividadesItens(seqSolicitacaoMatricula, model.SeqIngressante, model.SeqProcessoEtapa, ClassificacaoSituacaoFinal.FinalizadoComSucesso).TransformList<AtividadeAcademicaMatriculaItemViewModel>();

            model.AtividadesAcademicaMatriculaItem.SMCForEach(f => f.SeqProcessoEtapa = model.SeqProcessoEtapa);

            //Session[MatriculaConstants.KEY_SESSION_ATIVIDADES_SELECIONADAS] = model.AtividadesAcademicaMatriculaItem.Select(x => x.Seq).ToList();
        }

        [HttpGet]
        public ActionResult AtividadeExcluirSelecaoAluno(long seqSolicitacaoMatriculaItem, long seqProcessoEtapa, string seqSolicitacaoMatricula, string seqConfiguracaoEtapa)
        {
            // Simula um modelo para enviar pra validação de pré condições de entrada na página para validar se pode ou não fazer modificações
            VerificarPreCondicoesEntradaPagina(new MatriculaPaginaViewModelBase { SeqSolicitacaoMatricula = new SMCEncryptedLong(seqSolicitacaoMatricula).Value, SeqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa).Value });

            SolicitacaoMatriculaItemService.AlterarSolicitacaoMatriculaItemParaCancelado(seqSolicitacaoMatriculaItem, seqProcessoEtapa, Academico.Common.Areas.MAT.Enums.MotivoSituacaoMatricula.PeloSolicitante);

            return SMCRedirectToUrl(Url.Action("EntrarEtapa", new { seqSolicitacaoMatricula = new SMCEncryptedLong(seqSolicitacaoMatricula), seqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) }));
        }

        #endregion Seleção Atividade Acadêmica

        #region Seleção de Condição de Pagamento da Matrícula

        private ActionResult SelecaoCondicaoPagamento(MatriculaPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_12_01.PAGINA_CONDICOES_PAGAMENTO))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão

            var model = SolicitacaoMatriculaService.BuscarCondicaoPagamentoSolicitacaoMatricula(filtro.SeqSolicitacaoMatricula, false).Transform<PaginaSelecaoCondicaoPagamentoMatriculaViewModel>();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, SalvarSelecaoCondicaoPagamentoMatricula, historico.Seq);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("SelecaoCondicaoPagamento", model);
        }

        [SMCAuthorize(UC_MAT_003_12_01.PAGINA_CONDICOES_PAGAMENTO)]
        [HttpPost]
        public ActionResult SalvarSelecaoCondicaoPagamentoMatricula(PaginaSelecaoCondicaoPagamentoMatriculaViewModel model, string tokenRet)
        {
            // Verifica se pode efetuar essa ação
            VerificarPreCondicoesEntradaPagina(model);

            // Não deixa prosseguir se o valor não foi selecionado
            if (model.SeqCondicaoPagamento.GetValueOrDefault() == 0 && string.IsNullOrWhiteSpace(tokenRet))
                throw new CondicaoPagamentoRequeridoException();

            // Salva a condição de pagamento
            SolicitacaoMatriculaService.SalvarCondicaoPagamento(model.SeqSolicitacaoMatricula, model.SeqCondicaoPagamento);

            // Salva o histórico
            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);

            // Retorna próxima página da etapa
            return SMCRedirectToAction("EntrarEtapa", "Matricula", new { SeqSolicitacaoMatricula = new SMCEncryptedLong(model.SeqSolicitacaoMatricula), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        #endregion Seleção de Condição de Pagamento da Matrícula

        #region Adesão de Contrato

        private ActionResult AdesaoContrato(MatriculaPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_13_01.PAGINA_CONTRATO_PRESTACAO_SERVICOS_EDUCACIONAIS))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Busca o contrato
            var model = ContratoService.BuscarAdesaoContrato(filtro.SeqSolicitacaoMatricula).Transform<PaginaAdesaoContratoMatriculaViewModel>();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, SalvarAdesaoContratoMatricula, historico.Seq);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("AdesaoContrato", model);
        }

        [SMCAuthorize(UC_MAT_003_13_01.PAGINA_CONTRATO_PRESTACAO_SERVICOS_EDUCACIONAIS,
                      UC_MAT_003_34_01.PAGINA_ADESAO_RENOVACAO)]
        public FileResult AdesaoContratoVisualizar(SMCEncryptedLong seqArquivoContrato)
        {
            var arquivo = ArquivoAnexadoService.BuscarArquivoAnexado(seqArquivoContrato);
            // Coloca para exibir o arquivo inline ao invés de download
            Response.AppendHeader("Content-Disposition", "inline; filename=" + arquivo.Name);
            return File(arquivo.FileData, arquivo.Type);
        }

        public ViewResult AdesaoContratoTermo(SMCEncryptedLong seqSolicitacaoMatricula)
        {
            // Busca o contrato
            var model = ContratoService.BuscarAdesaoContrato(seqSolicitacaoMatricula).Transform<PaginaAdesaoContratoMatriculaViewModel>();

            return null;
        }

        [SMCAuthorize(UC_MAT_003_13_01.PAGINA_CONTRATO_PRESTACAO_SERVICOS_EDUCACIONAIS)]
        public PartialViewResult AdesaoContratoAderir(SMCEncryptedLong seqSolicitacaoMatricula)
        {
            // Aderir ao contrato
            //var ret = ContratoService.AderirContrato(seqSolicitacaoMatricula).Transform<PaginaAdesaoContratoMatriculaViewModel>();

            return null;// PartialView("_AdesaoContratoBotoes", ret);
        }

        [HttpPost]
        public ActionResult SalvarAdesaoContratoMatricula(PaginaAdesaoContratoMatriculaViewModel model, string tokenRet)
        {
            // Verifica se pode efetuar essa ação
            VerificarPreCondicoesEntradaPagina(model);

            // Verifica se existe algum bloqueio que impede a finalização da etapa
            model.Bloqueios = PessoaAtuacaoBloqueioService.BuscarPessoaAtuacaoBloqueios(model.SeqPessoaAtuacao, model.SeqConfiguracaoEtapa, true).TransformList<PessoaAtuacaoBloqueioViewModel>();

            // Caso tenha algum bloqueio de fim de etapa, retorna a view de bloqueio em modal
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                // Caso tenha essa variável, exibe o retorno em uma modal
                ViewBag.ExibeEmModal = true;
                return PartialView("ExibicaoBloqueio", model);
            }

            PaginaAdesaoContratoMatriculaViewModel aderido = null;

            // Caso não esteja voltando, significa que o usuário clicou em concluir e está aderindo aos termos
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                if (!model.AceitoTermo)
                    throw new TermoAdesaoRequeridoException();

                aderido = ContratoService.AderirContrato(new AdesaoContratoDadosData
                {
                    SeqSolicitacaoMatricula = model.SeqSolicitacaoMatricula,
                    SeqSolicitacaoServicoEtapa = model.SeqSolicitacaoServicoEtapa,
                    SeqSituacaoFinalEtapaSucesso = model.SeqSituacaoFinalSucesso.GetValueOrDefault(),
                    SeqConfiguracaoEtapa = model.SeqConfiguracaoEtapa,
                    SeqPessoaAtuacao = model.SeqPessoaAtuacao
                }).Transform<PaginaAdesaoContratoMatriculaViewModel>();

                // Validar se já aceitei o termo. Caso contrário, não deixa prosseguir.
                if (!aderido.CodigoAdesao.HasValue || !aderido.DataAdesao.HasValue)
                    throw new TermoAdesaoRequeridoException();

                // Chamada ao método para gerar o termo de adesão
                ContratoService.GerarTermoAdesaoContrato(model.SeqSolicitacaoMatricula);
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", "Matricula", new { SeqSolicitacaoMatricula = new SMCEncryptedLong(model.SeqSolicitacaoMatricula), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        public FileResult VisualizarTermoAdesao(SMCEncryptedLong SeqSolicitacaoMatricula)
        {
            var arquivo = ContratoService.GerarTermoAdesaoContrato(SeqSolicitacaoMatricula, true);

            Response.AppendHeader("Content-Disposition", "inline; filename=" + arquivo.Name);
            return File(arquivo.FileData, arquivo.Type);
        }

        public FileResult VisualizarTermoAdesaoResidenciaMedica(SMCEncryptedLong SeqSolicitacaoMatricula)
        {
            var arquivo = ContratoService.GerarTermoAdesaoContratoResidenciaMedica(SeqSolicitacaoMatricula, true);

            Response.AppendHeader("Content-Disposition", "inline; filename=" + arquivo.Name);
            return File(arquivo.FileData, arquivo.Type);
        }

        #endregion Adesão de Contrato

        #region Adesão de Contrato de Renovação

        private ActionResult AdesaoContratoRenovacao(MatriculaPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_34_01.PAGINA_ADESAO_RENOVACAO))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Busca o contrato
            var model = ContratoService.BuscarAdesaoContrato(filtro.SeqSolicitacaoMatricula).Transform<PaginaAdesaoContratoRenovacaoMatriculaViewModel>();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, SalvarAdesaoContratoRenovacao, historico.Seq);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("AdesaoContratoRenovacao", model);
        }

        [HttpPost]
        public ActionResult SalvarAdesaoContratoRenovacao(PaginaAdesaoContratoRenovacaoMatriculaViewModel model, string tokenRet)
        {
            /*  2.1. Caso seja selecionado "Sim":
                2.1.1. consistir a regra RN_MAT_066 - Procedimentos ao finalizar etapa;
                2.1.2. Realizar a adesão ao contrato de acordo com a RN_MAT_022 - Adesão contrato;
                2.1.3. Criar o ingressante no financeiro de acordo com a FAZ ALGO NO FINANCEIRO???
                2.1.4. Atribuir bloqueios para o ingressante de acordo com a RN_MAT_013 - Inclusão bloqueio financeiro parcela matrícula em aberto;
                2.1.5. prosseguir e acionar a próxima página de acordo com a RN_MAT_004 - Fluxo de Páginas..*/

            // Verifica se pode efetuar essa ação
            VerificarPreCondicoesEntradaPagina(model);

            // Verifica se existe algum bloqueio que impede a finalização da etapa
            model.Bloqueios = PessoaAtuacaoBloqueioService.BuscarPessoaAtuacaoBloqueios(model.SeqPessoaAtuacao, model.SeqConfiguracaoEtapa, true).TransformList<PessoaAtuacaoBloqueioViewModel>();

            // Caso tenha algum bloqueio de fim de etapa, retorna a view de bloqueio em modal
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                // Caso tenha essa variável, exibe o retorno em uma modal
                ViewBag.ExibeEmModal = true;
                return PartialView("ExibicaoBloqueio", model);
            }

            PaginaAdesaoContratoRenovacaoMatriculaViewModel aderido = null;

            // Caso não esteja voltando, significa que o usuário clicou em concluir e está aderindo aos termos
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                if (!model.AceitoTermo)
                    throw new TermoAdesaoRequeridoException();

                aderido = ContratoService.AderirContratoRenovacao(new AdesaoContratoDadosData
                {
                    SeqSolicitacaoMatricula = model.SeqSolicitacaoMatricula,
                    SeqSolicitacaoServicoEtapa = model.SeqSolicitacaoServicoEtapa,
                    SeqSituacaoFinalEtapaSucesso = model.SeqSituacaoFinalSucesso.GetValueOrDefault(),
                    SeqConfiguracaoEtapa = model.SeqConfiguracaoEtapa,
                    SeqPessoaAtuacao = model.SeqPessoaAtuacao
                }).Transform<PaginaAdesaoContratoRenovacaoMatriculaViewModel>();

                // Validar se já aceitei o termo. Caso contrário, não deixa prosseguir.
                if (!aderido.CodigoAdesao.HasValue || !aderido.DataAdesao.HasValue)
                    throw new TermoAdesaoRequeridoException();

                // Chamada ao método para gerar o termo de adesão
                ContratoService.GerarTermoAdesaoContrato(model.SeqSolicitacaoMatricula);
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", "Matricula", new { SeqSolicitacaoMatricula = new SMCEncryptedLong(model.SeqSolicitacaoMatricula), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        #endregion Adesão de Contrato Renovação

        #region Adesão de Contrato de Residência Médica
        private ActionResult AdesaoContratoRenovacaoResidenciaMedica(MatriculaPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_41_01.PAGINA_ADESAO_CONTRATO_LATO_SENSU))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Busca o contrato
            var model = ContratoService.BuscarAdesaoContratoResidenciaMedica(filtro.SeqSolicitacaoMatricula).Transform<PaginaAdesaoContratoResidenciaMedicaViewModel>();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, SalvarAdesaoContratoResidenciaMedica, historico.Seq);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("AdesaoContratoResidenciaMedica", model);
        }

        [HttpPost]
        public ActionResult SalvarAdesaoContratoResidenciaMedica(PaginaAdesaoContratoResidenciaMedicaViewModel model, string tokenRet)
        {
          
            // Verifica se pode efetuar essa ação
            VerificarPreCondicoesEntradaPagina(model);

            // Verifica se existe algum bloqueio que impede a finalização da etapa
            model.Bloqueios = PessoaAtuacaoBloqueioService.BuscarPessoaAtuacaoBloqueios(model.SeqPessoaAtuacao, model.SeqConfiguracaoEtapa, true).TransformList<PessoaAtuacaoBloqueioViewModel>();

            // Caso tenha algum bloqueio de fim de etapa, retorna a view de bloqueio em modal
            if (model.Bloqueios != null && model.Bloqueios.Any())
            {
                // Caso tenha essa variável, exibe o retorno em uma modal
                ViewBag.ExibeEmModal = true;
                return PartialView("ExibicaoBloqueio", model);
            }

            PaginaAdesaoContratoResidenciaMedicaViewModel aderido = null;

            // Caso não esteja voltando, significa que o usuário clicou em concluir e está aderindo aos termos
            if (string.IsNullOrWhiteSpace(tokenRet))
            {
                if (!model.AceitoTermo)
                    throw new TermoAdesaoRequeridoException();

                aderido = ContratoService.AderirContratoResidenciaMedica(new AdesaoContratoDadosData
                {
                    SeqSolicitacaoMatricula = model.SeqSolicitacaoMatricula,
                    SeqSolicitacaoServicoEtapa = model.SeqSolicitacaoServicoEtapa,
                    SeqSituacaoFinalEtapaSucesso = model.SeqSituacaoFinalSucesso.GetValueOrDefault(),
                    SeqConfiguracaoEtapa = model.SeqConfiguracaoEtapa,
                    SeqPessoaAtuacao = model.SeqPessoaAtuacao
                }).Transform<PaginaAdesaoContratoResidenciaMedicaViewModel>();

                // Validar se já aceitei o termo. Caso contrário, não deixa prosseguir.
                if (!aderido.CodigoAdesao.HasValue || !aderido.DataAdesao.HasValue)
                    throw new TermoAdesaoRequeridoException();

                // Chamada ao método para gerar o termo de adesão
                ContratoService.GerarTermoAdesaoContratoResidenciaMedica(model.SeqSolicitacaoMatricula);
            }

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", "Matricula", new { SeqSolicitacaoMatricula = new SMCEncryptedLong(model.SeqSolicitacaoMatricula), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }
        #endregion Adesão de Contrato de Residência Médica

        #region Emissão de Boleto
        // Esta página não faz mais parte do fluxo

        /*
        private ActionResult EmissaoBoleto(MatriculaPaginaFiltroViewModel filtro)
        {
            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = SolicitacaoMatriculaService.BuscarParcelasPagamentoSolicitacaoMatricula(filtro.SeqSolicitacaoMatricula).Transform<PaginaEmissaoBoletoMatriculaViewModel>();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, SalvarEmissaoBoletoMatricula, historico.Seq);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("EmissaoBoleto", model);
        }

        [HttpPost]
        public ActionResult SalvarEmissaoBoletoMatricula(PaginaEmissaoBoletoMatriculaViewModel model, string tokenRet)
        {
            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", "Matricula", new { SeqSolicitacaoMatricula = new SMCEncryptedLong(model.SeqSolicitacaoMatricula), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }*/

        public ActionResult EmissaoBoletoGerar(SMCEncryptedLong seqTitulo, SMCEncryptedLong seqServico, SMCEncryptedLong seqSolicitacaoMatricula, string parcela)
        {
            try
            {
                if (!Convert.ToBoolean(ConfigurationManager.AppSettings["ApiBoleto"]))
                {
                    var dados = SolicitacaoMatriculaService.GerarBoletoMatricula(seqTitulo, seqServico, seqSolicitacaoMatricula);
                    if (!string.IsNullOrWhiteSpace(parcela))
                        dados.DescricaoParcela = SMCDESCrypto.DecryptForURL(parcela);
                    //ViewBag.Evento = SolicitacaoMatriculaService.BuscarDescricaoEventoFinanceiroInscricao(id.Value);
                    //return View(dados);
                    return RenderPdfView(dados, new GridOptions { PageMargins = new MarginInfo { Left = 10, Right = 10, Top = 10, Bottom = 10 } });
                }

                var filtro = new { SeqTitulo = (long)seqTitulo, SeqServico = (long)seqServico, token = SMCDESCrypto.Encrypt(ConfigurationManager.AppSettings[WEB_API_REST.TOKEN_BOLETO_KEY]) };
                var url = $"{ConfigurationManager.AppSettings[WEB_API_REST.BASE_URL_KEY]}{WEB_API_REST.EMITIR_BOLETO_ALUNO}";
                var boleto = SMCRest.PostJson(url, filtro, cancellationTimer: int.Parse(ConfigurationManager.AppSettings[WEB_API_REST.CANCELLATION_TIME_KEY]));
                return File(Convert.FromBase64String(boleto), "application/pdf");
            }
            catch (Exception e)
            {
                return ThrowRedirect(e, "Index", "Solicitacao");
            }
        }

        #endregion Emissão de Boleto

        #region Instruções Finais

        private ActionResult InstrucoesFinais(MatriculaPaginaFiltroViewModel filtro)
        {
            if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_21_01.PAGINA_INSTRUCOES_FINAIS_SGAALUNO))
                throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro, true);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new PaginaInstrucoesFinaisMatriculaViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, SalvarInstrucoesFinaisMatricula, historico.Seq);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            // Preencher dados do boleto do model
            var boleto = SolicitacaoMatriculaService.BuscarParcelasPagamentoSolicitacaoMatricula(filtro.SeqSolicitacaoMatricula).Transform<PaginaInstrucoesFinaisMatriculaViewModel>();
            model.BoletoZerado = boleto.Parcelas.Any(h => h.ValorPagar <= 0);
            model.BoletoComValor = boleto.Parcelas.Any(h => h.ValorPagar > 0);
            model.ValorTotalMatricula = boleto.ValorTotalMatricula;
            model.Parcelas = boleto.Parcelas;
            model.BeneficioIncluiDesbloqueioTemporario = boleto.BeneficioIncluiDesbloqueioTemporario;
            model.BeneficioDeferidoCobranca = boleto.BeneficioDeferidoCobranca;

            //Preencher back url da página
            model.BackUrl = filtro.BackUrl;

            // Recupera os dados do aluno
            var codigoMigracaoAluno = AlunoService.BuscarCodigoMigracaoAluno(filtro.SeqIngressante);

            // Atualiza o sequencial da solicitação de matrícula
            model.Parcelas.ForEach(s => { s.SeqSolicitacaoMatricula = filtro.SeqSolicitacaoMatricula; s.SeqIngressante = filtro.SeqIngressante; s.BeneficioDeferidoCobranca = boleto.BeneficioDeferidoCobranca; s.BeneficioIncluiDesbloqueioTemporario = boleto.BeneficioIncluiDesbloqueioTemporario; s.CodigoMigracaoAluno = codigoMigracaoAluno; });

            // Preencher dados da adesão do contrato
            var adesao = ContratoService.BuscarAdesaoContrato(filtro.SeqSolicitacaoMatricula);
            model.SeqArquivoContrato = adesao.SeqArquivoContrato;
            model.DataAdesao = adesao.DataAdesao;
            model.CodigoAdesao = adesao.CodigoAdesao;
            model.TermoAdesao = adesao.TermoAdesao;

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("InstrucoesFinais", model);
        }

        [HttpPost]
        [SMCAuthorize(UC_MAT_003_21_01.PAGINA_INSTRUCOES_FINAIS_SGAALUNO)]
        public ActionResult SalvarInstrucoesFinaisMatricula(PaginaInstrucoesFinaisMatriculaViewModel model, string tokenRet)
        {
            // Verifica se pode efetuar essa ação
            VerificarPreCondicoesEntradaPagina(model);

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            if (!string.IsNullOrWhiteSpace(tokenRet))
                return SMCRedirectToAction("EntrarEtapa", "Matricula", new { SeqSolicitacaoMatricula = new SMCEncryptedLong(model.SeqSolicitacaoMatricula), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });

            return RedirectToAction("Index", "Solicitacao", new { area = "MAT" });
        }



        private ActionResult InstrucoesFinaisSolicitacaoMatricula(MatriculaPaginaFiltroViewModel filtro)
        {
            //if (!SMCAuthorizationHelper.Authorize(UC_MAT_003_21_01.PAGINA_INSTRUCOES_FINAIS_SGAALUNO))
            //    throw new UnauthorizedAccessException();

            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro, true);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new PaginaInstrucoesFinaisMatriculaViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, SalvarInstrucoesFinaisSolicitacaoMatricula, historico.Seq);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            //Preencher back url da página
            model.BackUrl = filtro.BackUrl;

            // Recupera os dados do aluno
            var codigoMigracaoAluno = AlunoService.BuscarCodigoMigracaoAluno(filtro.SeqIngressante);

            // Preencher dados da adesão do contrato
            var adesao = ContratoService.BuscarAdesaoContratoResidenciaMedica(filtro.SeqSolicitacaoMatricula);
            model.SeqArquivoContrato = adesao.SeqArquivoContrato;
            model.DataAdesao = adesao.DataAdesao;
            model.CodigoAdesao = adesao.CodigoAdesao;
            model.TermoAdesao = adesao.TermoAdesao;

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("InstrucoesFinaisResidencia", model);
        }

        [HttpPost]
        [SMCAuthorize(UC_MAT_003_21_01.PAGINA_INSTRUCOES_FINAIS_SGAALUNO)]
        public ActionResult SalvarInstrucoesFinaisSolicitacaoMatricula(PaginaInstrucoesFinaisMatriculaViewModel model, string tokenRet)
        {
            // Verifica se pode efetuar essa ação
            VerificarPreCondicoesEntradaPagina(model);

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            if (!string.IsNullOrWhiteSpace(tokenRet))
                return SMCRedirectToAction("EntrarEtapa", "Matricula", new { SeqSolicitacaoMatricula = new SMCEncryptedLong(model.SeqSolicitacaoMatricula), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });

            return RedirectToAction("Index", "Solicitacao", new { area = "MAT" });
        }

        #endregion Instruções Finais

        #region Solicitação formulário

        private ActionResult SolicitacaoFormulario(MatriculaPaginaFiltroViewModel filtro)
        {
            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            var seqFormulario = ConfiguracaoEtapaPaginaService.BuscarConfiguracaoEtapaPagina(filtro.SeqConfiguracaoEtapaPagina)?.SeqFormulario;

            if (seqFormulario == null)
            {
                throw new SMCApplicationException($"Token '{filtro.ConfiguracaoEtapaPagina.TokenPagina}' não existe no fluxo de matrícula.");
            }

            // Cria o modelo para a página em questão
            var model = new SolicitacaoFormularioViewModel()
            {
                DadoFormulario = new FormularioPadraoDadoFormularioViewModel()
                {
                    SeqFormulario = seqFormulario.Value,
                    SeqSolicitacaoServico = filtro.SeqSolicitacaoMatricula,
                    SeqConfiguracaoEtapaPagina = filtro.SeqConfiguracaoEtapaPagina
                }
            };

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, SalvarSolicitacaoFormulario, historico.Seq);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;
            return View("SolicitacaoFormulario", model);
        }

        //[SMCAuthorize(UC_MAT_003_01_01.PAGINA_INSTRUCOES_INICIAIS)]
        [SMCAllowAnonymous]
        [HttpPost]
        public ActionResult SalvarSolicitacaoFormulario(SolicitacaoFormularioViewModel model, string tokenRet)
        {
            // Verifica se pode efetuar essa ação
            //VerificarPreCondicoesEntradaPagina(model);

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            return SMCRedirectToAction("EntrarEtapa", "Matricula", new { SeqSolicitacaoMatricula = new SMCEncryptedLong(model.SeqSolicitacaoMatricula), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        #endregion Solicitação formulário

        #region Entrega de documentos

        private ActionResult SolicitacaoUpload(MatriculaPaginaFiltroViewModel filtro)
        {
            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new SolicitacaoUploadViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, SalvarSolicitacaoUpload, historico.Seq);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            model.Documentos = RegistroDocumentoService.BuscarDocumentosRegistro(filtro.SeqSolicitacaoMatricula, filtro.SeqConfiguracaoEtapa, exibirDocumentoNaoPermiteUpload: true)
                                                       .TransformList<SolicitacaoMatriculaDocumentoViewModel>();

            model.DescricaoTermoEntregaDocumentacao = filtro.ConfiguracaoEtapa.DescricaoTermoEntregaDocumentacao;

            //valida se por ventura o termo de aceite foi marcado ou recebera false
            //Desta forma buscamos ao banco se existe algum documento marcado para entega posterior marcado
            //caso contrario já recebe false
            if (model.Documentos.Any(a => a.Documentos.Any(aa => aa.EntregaPosterior.GetValueOrDefault())))
            {
                model.AceiteTermoEntregaDocumentacao = SolicitacaoServicoService.BuscarTermoEntregaDocumentacaoFoiAceito(filtro.SeqSolicitacaoMatricula);
            }
            else
            {
                model.AceiteTermoEntregaDocumentacao = false;
            }

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;
            return View("SolicitacaoUpload", model);
        }

        //Quando clica em cada botão de anexar doc
        public ActionResult AnexarDocumentos(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, SMCEncryptedLong seqPessoaAtuacao, SMCEncryptedLong grupoDocumento, bool grupoObrigatorio = false, bool exibirDocumentoPermiteUpload = true)
        {
            var aplicacaoAluno = SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ALUNO;
            var etapas = BuscarEtapas(seqSolicitacaoServico, true);
            var etapaAtual = etapas.FirstOrDefault(f => f.SeqConfiguracaoEtapa == seqConfiguracaoEtapa);
            var primeiraEtapa = etapas.OrderBy(o => o.OrdemEtapaSGF).First();
            var ehPrimeiraEtapa = primeiraEtapa.SeqConfiguracaoEtapa == seqConfiguracaoEtapa;

            var dataFimEscalonamento = etapaAtual != null && etapaAtual.SeqEscalonamento.HasValue && etapaAtual.SeqEscalonamento != 0 ? EscalonamentoService.BuscarEscalonamento(etapaAtual.SeqEscalonamento.Value).DataFim : null;

            var documentos = (seqConfiguracaoEtapa == null || seqConfiguracaoEtapa.Value == 0) ?
                                this.RegistroDocumentoService.BuscarDocumentosRegistro(seqSolicitacaoServico, null, exibirDocumentoPermiteUpload: exibirDocumentoPermiteUpload) :
                                this.RegistroDocumentoService.BuscarDocumentosRegistro(seqSolicitacaoServico, seqConfiguracaoEtapa ?? null, exibirDocumentoPermiteUpload: exibirDocumentoPermiteUpload)
                             .Where(c => c.PermiteUploadArquivo == exibirDocumentoPermiteUpload)
                             .ToList();

            var registroDocumento = new SolicitacaoRegistroDocumentoViewModel()
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                SeqConfiguracaoEtapa = seqConfiguracaoEtapa,
                SeqPessoaAtuacao = seqPessoaAtuacao,
                ExibeLegenda = aplicacaoAluno && !ehPrimeiraEtapa,
                GrupoDocumento = grupoDocumento,
                GrupoObrigatorio = grupoObrigatorio,
                Documentos = documentos.TransformList<SolicitacaoDocumentoViewModel>(),
                BackUrl = Request.UrlReferrer.ToString()
            };

            registroDocumento.Documentos.SMCForEach(sf => sf.Documentos.SMCForEach(f =>
            {
                f.AplicacaoAluno = aplicacaoAluno;
                f.DataLimiteEntrega = sf.DataLimiteEntrega;
                f.Observacao = aplicacaoAluno ? f.Observacao : f.ObservacaoSecretaria;
                f.ExibirEntregaPosterior = sf.PermiteEntregaPosterior && aplicacaoAluno && ehPrimeiraEtapa;
                f.ExibirDataPrazoEntrega = sf.PermiteEntregaPosterior && aplicacaoAluno && !ehPrimeiraEtapa;
                f.TextoEntregaPosterior = sf.DataLimiteEntrega != null ? string.Format(Views.Matricula.App_LocalResources.UIResource.Mensagem_EntregareiAte, sf.DataLimiteEntrega.SMCDataAbreviada()) : Views.Matricula.App_LocalResources.UIResource.Mensagem_EntregareiPosteriormente;

                if (f.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega)
                    f.DataPrazoEntrega = dataFimEscalonamento;

                if (f.ArquivoAnexado != null && f.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Indeferido)
                {
                    if (System.Web.HttpContext.Current.Request.Url.Host == "localhost")
                        f.AnexoAnterior = $"<a href='http://{System.Web.HttpContext.Current.Request.Url.Host}/Dev/{SMCContext.ApplicationId}/Home/DownloadFileGuid?GuidFile={f.ArquivoAnexado.GuidFile.ToString()}' target='_blank'><i></i>Anexo anterior</a>";

                    else
                        f.AnexoAnterior = $"<a href='http://{System.Web.HttpContext.Current.Request.Url.Host}/{SMCContext.ApplicationId}/Home/DownloadFileGuid?GuidFile={f.ArquivoAnexado.GuidFile.ToString()}' target='_blank'><i></i>Anexo anterior</a>";
                }
                f.EhPrimeiraEtapa = ehPrimeiraEtapa;
                f.ArquivoAnexado = aplicacaoAluno && !ehPrimeiraEtapa ? null : f.ArquivoAnexado;
            }));

            RevisarDocumentos(grupoDocumento, grupoObrigatorio, registroDocumento);

            var view = GetExternalView(AcademicoExternalViews._REGISTRAR_DOCUMENTOS);
            return PartialView(view, registroDocumento);
        }

        /// <summary>
        /// [ UC_SRC_004_28_01 ] - Upload de arquivo alterada
        /// </summary>
        private static void RevisarDocumentos(SMCEncryptedLong grupoDocumento, bool grupoObrigatorio, SolicitacaoRegistroDocumentoViewModel registroDocumento)
        {
            // Grupo de documentos
            if (grupoDocumento != null && grupoDocumento.Value != 0)
            {
                /**
                    Listar, separadamente, cada um dos grupos de documentos de acordo com a configuração da etapa atual da configuração do processo 
                    da solicitação da pessoa-atuação em questão, que não possuem o mínimo de documentos obrigatórios com a situação "Deferido" e "Aguardando análise do setor responsável". 
                    Para cada grupo de documento, exibir a descrição e o número mínimo de documentos obrigatórios como "cabeçalho" do grupo e, logo abaixo, exibir um registro para cada item do grupo. 
                    Ordena-los alfabeticamente.
                        2.2.1. Se no grupo a ser exibido, existir algum documento com a situação  "Deferido" ou "Aguardando análise do setor responsável", 
                        os campos referentes ao documento devem ficar desabilitados
                **/

                var grupoDocumentos = registroDocumento.Documentos.Where(f => f.Grupos.Any(g => g.Seq == grupoDocumento.Value)).ToList();
                registroDocumento.Documentos = grupoDocumentos;
            }
            // Documentos obrigatórios
            else if (grupoObrigatorio)
            {
                /** 
                 1.2. Listar todos os tipos de documentos obrigatórios de acordo com configuração da etapa atual da configuração do processo 
                 da solicitação da pessoa-atuação em questão, que estão com uma das situações: Pendente, Aguardando validação e Aguardando entrega.
                 Ordena - los alfabeticamente.
                **/
                var documentosObrigatorios = registroDocumento.Documentos.Where(f => !f.Grupos.Any() && f.Obrigatorio)
                                                                         .Where(doc => doc.Documentos.Any(d => d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente
                                                                                    || d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoValidacao
                                                                                    || d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega))
                                                                         .ToList();

                registroDocumento.Documentos = documentosObrigatorios;
            }
            // Documentos não obrigatórios
            else
            {
                /**
                 3.2. Listar todos os tipos de documentos não obrigatórios de acordo com configuração da etapa atual da configuração do processo 
                 da solicitação da pessoa-atuação em questão, que estão com uma das situações: Pendente, Aguardando validação e Aguardando entrega. 
                 Ordena-los alfabeticamente.
                **/
                var documentos = registroDocumento.Documentos
                                                  .Where(f => !f.Grupos.Any() && !f.Obrigatorio)
                                                  .Where(c => c.Documentos.SMCAny(d => d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente
                                                                                    || d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoValidacao
                                                                                    || d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega))
                                                  .ToList();

                registroDocumento.Documentos = documentos;
            }
        }


        [HttpPost]
        [SMCAuthorize(UC_MAT_003_06_01.PAGINA_ENTREGA_DOCUMENTOS)]
        public ActionResult SalvarSolicitacaoUpload(SolicitacaoUploadViewModel model, string tokenRet)
        {
            // Verifica se pode efetuar essa ação
            VerificarPreCondicoesEntradaPagina(model);

            ValidarModelo(model, tokenRet);

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);
            SolicitacaoServicoService.AtualizarTermoEntregaDocumentacao(model.SeqSolicitacaoMatricula, model.AceiteTermoEntregaDocumentacao);
            return SMCRedirectToAction("EntrarEtapa", "Matricula", new { SeqSolicitacaoMatricula = new SMCEncryptedLong(model.SeqSolicitacaoMatricula), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        private void ValidarModelo(SolicitacaoUploadViewModel model, string tokenRet)
        {
            model.Documentos = RegistroDocumentoService.BuscarDocumentosRegistro(model.SeqSolicitacaoMatricula, model.SeqConfiguracaoEtapa).TransformList<SolicitacaoMatriculaDocumentoViewModel>();

            var grupoObrigatorio = model.Documentos.Where(d => d.Grupos != null && d.Grupos.Any()).ToList();
            var grupos = new Dictionary<Models.Matricula.GrupoDocumentoViewModel, List<SolicitacaoMatriculaDocumentoViewModel>>();
            foreach (var documento in grupoObrigatorio)
            {
                foreach (var grupo in documento.Grupos)
                {
                    if (grupos.ContainsKey(grupo))
                        grupos[grupo].Add(documento);
                    else
                        grupos.Add(grupo, new List<SolicitacaoMatriculaDocumentoViewModel> { documento });
                }
            }

            #region [ validações antigas - as novas são feitas em ValidaArquivosAvancarProximaEtapa]
            //var obrigatorio = model.Documentos?.Where(d => d.Obrigatorio && (d.Grupos == null || !d.Grupos.Any())).ToList();
            //if (obrigatorio.SelectMany(s => s.Documentos).Any(a => a.ArquivoAnexado == null && (a.EntregaPosterior.HasValue && !a.EntregaPosterior.Value)))
            //    throw new SMCApplicationException(Views.Matricula.App_LocalResources.UIResource.Mensagem_Entregar_Documentos_Ou_Aceite_Entrega_Posteriormente);

            //foreach (var doc in grupos)
            //{
            //    var quantidadeMinima = doc.Value.SelectMany(s => s.Documentos).Count(c => c.ArquivoAnexado != null) + doc.Value.SelectMany(s => s.Documentos).Count(c => c.ArquivoAnexado == null && (c.EntregaPosterior.HasValue && c.EntregaPosterior.Value));
            //    if (quantidadeMinima < doc.Key.NumeroMinimoDocumentosRequerido)
            //    {
            //        if (doc.Value.SelectMany(s => s.Documentos).Any(a => a.ArquivoAnexado == null && (a.EntregaPosterior.HasValue && !a.EntregaPosterior.Value)))
            //            throw new SMCApplicationException(Views.Matricula.App_LocalResources.UIResource.Mensagem_Entregar_Documentos_Ou_Aceite_Entrega_Posteriormente);
            //    }
            //}
            #endregion

            ValidaArquivosAvancarProximaEtapa(model.SeqSolicitacaoMatricula, model.SeqConfiguracaoEtapa, tokenRet);

            //Valida somente se for a próxima página caso seja uma página de retorno não valida
            if (model.Documentos.Any(a => a.Documentos.Any(aa => aa.EntregaPosterior.GetValueOrDefault())) && !model.AceiteTermoEntregaDocumentacao && tokenRet == null)
                throw new SMCApplicationException(Views.Matricula.App_LocalResources.UIResource.Mensagem_Aceite_Termo);
        }

        [SMCAuthorize(UC_MAT_003_06_01.PAGINA_ENTREGA_DOCUMENTOS)]
        public ActionResult SalvarRegistroDocumentos(SolicitacaoRegistroDocumentoViewModel model)
        {
            var etapas = BuscarEtapas(model.SeqSolicitacaoServico, true);
            var etapaEmAndamento = etapas.Where(s => s.SituacaoEtapaIngressante == SituacaoEtapaSolicitacaoMatricula.EmAndamento).FirstOrDefault();
            var seqConfiguracaoEtapaEmAndamento = etapaEmAndamento?.SeqConfiguracaoEtapa;
            var primeiraEtapa = etapas.OrderBy(o => o.OrdemEtapaSGF).First();
            var ehPrimeiraEtapa = primeiraEtapa.SeqConfiguracaoEtapa == seqConfiguracaoEtapaEmAndamento;

            model.Documentos.SMCForEach(sf => sf.Documentos.SMCForEach(f =>
              {
                  if (f.EntregaPosterior.HasValue && f.EntregaPosterior.Value && ehPrimeiraEtapa)
                  {
                      f.Observacao = null;
                      f.ArquivoAnexado = null;
                  }
                  else if (f.ArquivoAnexado != null && !ehPrimeiraEtapa)
                  {
                      f.EntregaPosterior = null;
                  }
              }));

            ValidarDocumentosMesmoTipoPermitemVarios(model.Documentos);

            ValidarEntregaPosteriorEmArquivos(model.Documentos);

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
                    f.ObservacaoSecretaria = f.Observacao;
                }
            }));
            RegistroDocumentoService.AnexarDocumentosSolicitacao(registros);
            return SMCRedirectToUrl(model.BackUrl);
        }

        private void ValidarEntregaPosteriorEmArquivos(List<SolicitacaoDocumentoViewModel> grupoDeDocumentos)
        {
            foreach (var documentos in grupoDeDocumentos)
            {
                if (documentos.PermiteVarios)
                {
                    var numDocumentosEntregaPosterior = documentos.Documentos.Count(c => c.EntregaPosterior.HasValue && c.EntregaPosterior.Value == true && c.Seq != 0);

                    if (numDocumentosEntregaPosterior > 0 && numDocumentosEntregaPosterior < documentos.Documentos.Count())
                    {
                        throw new SMCApplicationException($"Caso um dos arquivos do documento <b>{documentos.DescricaoTipoDocumento}</b> esteja marcado para entrega posterior, todos os seus outros arquivos também devem ser marcados, ou novos arquivos devem ser excluídos.");
                    }
                }
            }
        }

        private void ValidaArquivosAvancarProximaEtapa(long seqSolicitacaoServico, long seqConfiguracaoEtapa, string tokenRet)
        {
            var etapas = BuscarEtapas(seqSolicitacaoServico, true);
            var etapaEmAndamento = etapas.Where(s => s.SituacaoEtapaIngressante == SituacaoEtapaSolicitacaoMatricula.EmAndamento).FirstOrDefault();
            var seqConfiguracaoEtapaEmAndamento = etapaEmAndamento?.SeqConfiguracaoEtapa;
            var primeiraEtapa = etapas.OrderBy(o => o.OrdemEtapaSGF).First();
            var ehPrimeiraEtapa = primeiraEtapa.SeqConfiguracaoEtapa == seqConfiguracaoEtapaEmAndamento;

            //Validação somente para proxima pagina não necessaria para pagina de retorno
            if (((SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ALUNO && ehPrimeiraEtapa) || SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ADMINISTRATIVO) && tokenRet == null)
            {
                List<string> listaErros = new List<string>();
                var todosDocumentos = RegistroDocumentoService.BuscarDocumentosRegistro(seqSolicitacaoServico, seqConfiguracaoEtapa).TransformList<SolicitacaoMatriculaDocumentoViewModel>();
                var documentosObrigatoriosSemGrupo = todosDocumentos.Where(c => c.Obrigatorio == true && c.Grupos.Count() == 0).ToList();

                foreach (var item in documentosObrigatoriosSemGrupo)
                {
                    foreach (var documento in item.Documentos)
                    {
                        var doc = documentosObrigatoriosSemGrupo.Where(w => w.SeqDocumentoRequerido == item.SeqDocumentoRequerido).FirstOrDefault();

                        string erro = ValidarArquivoObrigatorio(documento, doc.Obrigatorio, doc.PermiteUploadArquivo, doc.PermiteEntregaPosterior, doc.DescricaoTipoDocumento);

                        if (!string.IsNullOrEmpty(erro))
                            listaErros.Add(erro);
                    }
                }

                var docGrupoDocumentacao = todosDocumentos.Where(c => c.Grupos != null && c.Grupos.Any()).ToList();

                var grupos = docGrupoDocumentacao.SelectMany(c => c.Grupos).Distinct().ToList();

                foreach (var grupo in grupos)
                {
                    var grupoDocumentoRequerido = GrupoDocumentoRequeridoService.BuscarGrupoDocumentoRequerido(grupo.Seq);
                    var documentosDoGrupo = docGrupoDocumentacao.Where(c => c.Grupos.Contains(grupo)).ToList();
                    var grupoPermiteEntregaPosterior = documentosDoGrupo.Any(c => c.PermiteEntregaPosterior == true);

                    string erro = ValidarArquivoGrupoDocumentos(documentosDoGrupo, grupoPermiteEntregaPosterior, grupoDocumentoRequerido.MinimoObrigatorio, grupoDocumentoRequerido.Descricao);

                    if (!string.IsNullOrEmpty(erro))
                        listaErros.Add(erro);
                }

                if (listaErros.Count > 0)
                {
                    string erro = "";

                    foreach (var erroDoc in listaErros)
                    {
                        erro += erroDoc + "<br/>";
                    }

                    throw new SMCApplicationException(erro);
                }
            }
        }

        private void ValidarUpload(SolicitacaoRegistroDocumentoViewModel model)
        {
            var etapas = BuscarEtapas(model.SeqSolicitacaoServico, true);
            var etapaEmAndamento = etapas.Where(s => s.SituacaoEtapaIngressante == SituacaoEtapaSolicitacaoMatricula.EmAndamento).FirstOrDefault();
            var seqConfiguracaoEtapaEmAndamento = etapaEmAndamento?.SeqConfiguracaoEtapa;
            var primeiraEtapa = etapas.OrderBy(o => o.OrdemEtapaSGF).First();
            var ehPrimeiraEtapa = primeiraEtapa.SeqConfiguracaoEtapa == seqConfiguracaoEtapaEmAndamento;

            if ((SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ALUNO && ehPrimeiraEtapa) || SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ADMINISTRATIVO)
            {
                List<string> listaErros = new List<string>();
                var documentos = RegistroDocumentoService.BuscarDocumentosRegistro(model.SeqSolicitacaoServico, model.SeqConfiguracaoEtapa).TransformList<SolicitacaoMatriculaDocumentoViewModel>();
                RemoverDocumentosExecendentesQuandoSeTemEntregaPosteror(model);

                if (!model.GrupoDocumento.HasValue)
                {
                    foreach (var item in model.Documentos)
                    {
                        foreach (var documento in item.Documentos)
                        {
                            var doc = documentos.Where(w => w.SeqDocumentoRequerido == item.SeqDocumentoRequerido).FirstOrDefault();

                            string erro = ValidarArquivoObrigatorio(documento, doc.Obrigatorio, doc.PermiteUploadArquivo, doc.PermiteEntregaPosterior, doc.DescricaoTipoDocumento);

                            if (!string.IsNullOrEmpty(erro))
                                listaErros.Add(erro);
                        }
                    }
                }
                else if (model.GrupoDocumento != null && model.GrupoDocumento.Value != 0)
                {
                    var grupoDocumentoRequerido = GrupoDocumentoRequeridoService.BuscarGrupoDocumentoRequerido(model.GrupoDocumento.Value);
                    var permiteEntregaPosterior = documentos.Where(c => c.Grupos.Any(d => d.Seq == grupoDocumentoRequerido.Seq)).Any(c => c.PermiteEntregaPosterior == true);
                    var grupoPermiteEntregaPosterior = model.Documentos.Any(c => c.PermiteEntregaPosterior == true);
                    var documentosDoGrupo = model.Documentos.TransformList<SolicitacaoMatriculaDocumentoViewModel>();

                    string erro = ValidarArquivoGrupoDocumentos(documentosDoGrupo, grupoPermiteEntregaPosterior, grupoDocumentoRequerido.MinimoObrigatorio, grupoDocumentoRequerido.Descricao);

                    if (!string.IsNullOrEmpty(erro))
                        listaErros.Add(erro);
                }

                if (listaErros.Count > 0)
                {
                    string erro = "";

                    foreach (var erroDoc in listaErros)
                    {
                        erro += erroDoc + "<br/>";
                    }

                    throw new SMCApplicationException(erro);
                }
            }
        }

        /// <summary>
        /// Validação da regra RN_MAT_135 - Consistência do upload da documentação > Documentos obrigatórios
        /// </summary>
        private string ValidarArquivoObrigatorio(SolicitacaoDocumentoDocumentoViewModel documento, bool obrigatorio, bool permiteUploadArquivo, bool permiteEntregaPosterior, string descricaoDocumento)
        {
            bool obrigatorioEPermiteUpload = obrigatorio && permiteUploadArquivo;
            bool entregaPosteriorDesmarcado = !documento.EntregaPosterior.HasValue || (documento.EntregaPosterior.HasValue && !documento.EntregaPosterior.Value);

            if (obrigatorioEPermiteUpload && permiteEntregaPosterior && !documento.EntregueAnteriormente &&
               (documento.ArquivoAnexado == null && entregaPosteriorDesmarcado))
            {
                return $"O arquivo para o documento <b>{descricaoDocumento}</b> deve ser anexado ou a entrega posterior deve ser selecionada.";
            }
            else if (obrigatorioEPermiteUpload && !documento.EntregueAnteriormente && !permiteEntregaPosterior && documento.ArquivoAnexado == null)
            {
                return $"O arquivo para o documento <b>{descricaoDocumento}</b> deve ser anexado.";
            }

            return string.Empty;
        }

        /// <summary>
        /// Validação da regra RN_MAT_135 - Consistência do upload da documentação > Grupos de documentos obrigatórios
        /// </summary>
        private string ValidarArquivoGrupoDocumentos(List<SolicitacaoMatriculaDocumentoViewModel> documentosDoGrupo, bool entregaPosterior, int minObrigatorio, string descricaoGrupo)
        {
            var numeroDocsAnexados = documentosDoGrupo.Count(c => c.Documentos.Any(d => d.ArquivoAnexado != null));
            var numeroDocsSemAnexo = documentosDoGrupo.Count(c => c.Documentos.Any(d => d.ArquivoAnexado == null && (d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido || d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel)));
            var numEntregaPosterior = documentosDoGrupo.Count(d => d.Documentos.Any(c => c.EntregaPosterior.HasValue && c.EntregaPosterior == true));

            if (entregaPosterior)
            {
                if ((numEntregaPosterior + numeroDocsAnexados + numeroDocsSemAnexo) < minObrigatorio)
                    return $"O arquivo para os documentos do grupo <b>{descricaoGrupo}</b> deve ser anexado ou a entrega posterior deve ser selecionada, de acordo com o mínimo de itens obrigatórios.";
            }
            else
            {
                if ((numeroDocsAnexados + numeroDocsSemAnexo) < minObrigatorio)
                    return $"O arquivo para os documentos do grupo <b>{descricaoGrupo}</b> deve ser anexado, de acordo com o mínimo de itens obrigatórios.";
            }

            return string.Empty;
        }

        private void RemoverDocumentosExecendentesQuandoSeTemEntregaPosteror(SolicitacaoRegistroDocumentoViewModel documentos)
        {
            //Iremos validar se todos os documentos estão marcados com entrega posteiror caso estejam irá deixar o primerio e excluir os outros
            foreach (var documentoEntregue in documentos.Documentos)
            {
                var quantidadeDocumentosComEntregaPosterior = documentoEntregue.Documentos.Where(w => w.EntregaPosterior.GetValueOrDefault()).Count();
                if (quantidadeDocumentosComEntregaPosterior == documentoEntregue.Documentos.Count())
                {
                    var primeiroDocumentoEntreguePosteriormente = documentoEntregue.Documentos.FirstOrDefault();
                    documentoEntregue.Documentos = new SMCMasterDetailList<SolicitacaoDocumentoDocumentoViewModel>();
                    documentoEntregue.Documentos.Add(primeiroDocumentoEntreguePosteriormente);
                }
            }
        }

        private void ValidarDocumentosMesmoTipoPermitemVarios(List<SolicitacaoDocumentoViewModel> documentos)
        {
            foreach (var item in documentos)
                if (item.Documentos.Count() > 10)
                {
                    var retorno = DocumentoRequeridoService.BuscarDescricaoDocumentoRequeridoPermiteVarios(item.SeqDocumentoRequerido);
                    throw new NumeroDeDocumentosExcedidoException(retorno.DescricaoTipoDocumento);
                }
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

        //[SMCAllowAnonymous]
        //public virtual ActionResult DownloadDocumento(Guid guidFile)
        //{
        //    var arquivo = ArquivoAnexadoService.BuscarArquivoAnexadoPorGuid(guidFile);
        //    return File(arquivo.FileData, arquivo.Type, arquivo.Name);
        //}

        #endregion Entrega de documentos

        #region Complementação de dados cadastrais

        private ActionResult ComplementacaoDadosCadastrais(MatriculaPaginaFiltroViewModel filtro)
        {
            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new ComplementacaoDadosCadastraisViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, SalvarComplementacaoDadosCadastrais, historico.Seq);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;
            return View("ComplementacaoDadosCadastrais", model);
        }

        [HttpPost]
        [SMCAuthorize(UC_MAT_003_06_01.PAGINA_ENTREGA_DOCUMENTOS)]
        public ActionResult SalvarComplementacaoDadosCadastrais(ComplementacaoDadosCadastraisViewModel model, string tokenRet)
        {
            //TODO: Alterar o token quando for criado o caso de uso para essa página 

            // Verifica se pode efetuar essa ação
            VerificarPreCondicoesEntradaPagina(model);

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);

            return SMCRedirectToAction("EntrarEtapa", "Matricula", new { SeqSolicitacaoMatricula = new SMCEncryptedLong(model.SeqSolicitacaoMatricula), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        #endregion Complementação de dados cadastrais

        #region Montagem de plano de estudo

        private ActionResult MontagemPlanoEstudos(MatriculaPaginaFiltroViewModel filtro)
        {
            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new MontagemPlanoEstudosViewModel();
            model.SeqSolicitacaoMatricula = filtro.SeqSolicitacaoMatricula.Value;

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, SalvaMontagemPlanoEstudo, historico.Seq);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            model.backUrl = Url.Action("EntrarEtapa", new { seqSolicitacaoMatricula = new SMCEncryptedLong(filtro.SeqSolicitacaoMatricula), seqConfiguracaoEtapa = new SMCEncryptedLong(filtro.SeqConfiguracaoEtapa) });

            BuscarTurmasGraduacaoSelecionadas(model, filtro.SeqSolicitacaoMatricula.Value, filtro.SeqConfiguracaoEtapa);

            return View("MontagemPlanoEstudo", model);
        }

        [HttpPost]
        [SMCAuthorize(UC_MAT_003_35_01.PAGINA_SELECAO_TURMAS_GRADUACAO)]
        public ActionResult SalvaMontagemPlanoEstudo(MontagemPlanoEstudosViewModel model, string tokenRet)
        {
            //TODO: Alterar o token quando for criado o caso de uso para essa página 

            // Verifica se pode efetuar essa ação
            VerificarPreCondicoesEntradaPagina(model);

            SolicitacaoMatriculaService.PlanoEstudosConsistirProsseguirEtapa(model.SeqSolicitacaoMatricula);

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);

            return SMCRedirectToAction("EntrarEtapa", "Matricula", new { SeqSolicitacaoMatricula = new SMCEncryptedLong(model.SeqSolicitacaoMatricula), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        [HttpGet]
        public ActionResult SelecaoPlanoEstudoExcluirTurma(long seqSolicitacaoMatriculaItem, long seqSolicitacaoMatricula, long seqConfiguracaoEtapa)
        {
            // Simula um modelo para enviar pra validação de pré condições de entrada na página para validar se pode ou não fazer modificações
            VerificarPreCondicoesEntradaPagina(new MatriculaPaginaViewModelBase { SeqSolicitacaoMatricula = new SMCEncryptedLong(seqSolicitacaoMatricula).Value, SeqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa).Value });

            SolicitacaoMatriculaService.SelecaoPlanoEstudoExcluirTurma(new SMCEncryptedLong(seqSolicitacaoMatriculaItem).Value, new SMCEncryptedLong(seqSolicitacaoMatricula).Value);

            return SMCRedirectToUrl(Url.Action("EntrarEtapa", new { seqSolicitacaoMatricula = new SMCEncryptedLong(seqSolicitacaoMatricula), seqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) }));
        }

        /// <summary>
        /// Busca as turmas salvas do Aluno no PLano de Estudo
        /// </summary>
        private void BuscarTurmasGraduacaoSelecionadas(MontagemPlanoEstudosViewModel model, long seqSolicitacaoMatricula, long SeqConfiguracaoEtapa)
        {
            model.SeqSolicitacaoMatricula = seqSolicitacaoMatricula;
            model.DivisaoTurmas = new List<DivisaoTurmaSelecionadaItemViewModel>();

            var turmasSelecionadas = SolicitacaoMatriculaService.BuscarTurmasGraduacaoSelecionadas(seqSolicitacaoMatricula);

            var periodos = turmasSelecionadas.Select(c => c.NumeroPeriodo).Distinct().ToList();

            foreach (var periodo in periodos)
            {
                var turmasNoPeriodo = turmasSelecionadas.Where(c => c.NumeroPeriodo == periodo).OrderBy(c => c.NomeDisciplina).ToList();

                var divisaoTurmaOfertadaItemViewModel = new DivisaoTurmaSelecionadaItemViewModel()
                {
                    NomePeriodoFormatado = $"{periodo}º Período",
                    TurmaOfertadaItens = new List<TurmaSelecionadaItemViewModel>()
                };

                foreach (var turma in turmasNoPeriodo)
                {
                    var novoItem = new TurmaSelecionadaItemViewModel()
                    {
                        NomeDisciplina = turma.NomeDisciplina,
                        PermiteExclusao = turma.PermiteExclusao,
                        SeqTurma = turma.SeqTurma,
                        SeqSolicitacaoMatriculaItem = turma.SeqSolicitacaoMatriculaItem,
                        SeqSolicitacaoMatricula = turma.SeqSolicitacaomatricula,
                        SeqConfiguracaoEtapa = SeqConfiguracaoEtapa,
                        ListaCargasHorarias = new List<string>()
                    };

                    if (turma.CodGrupoProposicoes.HasValue && turma.CodGrupoProposicoes.Value > 0)
                    {
                        novoItem.DescricaoConteudoTurma = !string.IsNullOrEmpty(turma.DescricaoConteudo) ? $"{turma.NomeDisciplina} - {turma.DescricaoConteudo} - {turma.FormacaoCurriculo}: {turma.DescricaoTituloFormacaoCurricular}"
                                                                                                         : $"{turma.NomeDisciplina} - {turma.FormacaoCurriculo}: {turma.DescricaoTituloFormacaoCurricular}";
                    }
                    else
                    {
                        novoItem.DescricaoConteudoTurma = !string.IsNullOrEmpty(turma.DescricaoConteudo) ? $"{turma.NomeDisciplina} - {turma.DescricaoConteudo}" : turma.NomeDisciplina;
                    }

                    if (turma.CargaHoraria?.Count > 0)
                    {
                        foreach (var cargaHoraria in turma.CargaHoraria)
                        {
                            novoItem.ListaCargasHorarias.Add(cargaHoraria.CargaHorariaFormatada);
                        }
                    }

                    novoItem.PertenceAoCurriculo = turma.PertenceAoCurriculo ? TurmaOfertaMatricula.ComponentePertence : TurmaOfertaMatricula.ComponenteNaoPertence;

                    divisaoTurmaOfertadaItemViewModel.TurmaOfertadaItens.Add(novoItem);
                };

                model.DivisaoTurmas.Add(divisaoTurmaOfertadaItemViewModel);
            }
        }

        #endregion

        #region Seleção de Disciplinas para Dispensa

        private ActionResult SelecaoDisciplinasParaDispensa(MatriculaPaginaFiltroViewModel filtro)
        {
            // Verifica pré condições para entrada na página
            VerificarPreCondicoesEntradaPagina(filtro);

            // Salva o Histórico de situações desta página
            VerificarHistoricoSituacao(filtro);

            // Recupera o Histórico de navegação desta página
            var historico = SolicitacaoHistoricoNavegacaoService.BuscarSolicitacaoHistoricoNavegacao(filtro.SeqSolicitacaoServicoEtapa, filtro.SeqConfiguracaoEtapaPagina, true);

            // Cria o modelo para a página em questão
            var model = new SelecaoDisciplinasParaDispensaViewModel();

            // Aplica todas as configurações necessárias para uma página de uma etapa do template de processo
            ConfigurarPagina(model, filtro, SalvarSelecaoDisciplinasParaDispensa, historico.Seq);

            // Caso tenha algum bloqueio, retorna a view de bloqueio
            if (model.Bloqueios != null && model.Bloqueios.Any())
                return View("ExibicaoBloqueio", model);

            ViewBag.Title = filtro.ConfiguracaoEtapaPagina.TituloPagina;

            return View("SelecaoDisciplinasParaDispensa", model);
        }

        [HttpPost]
        [SMCAuthorize(UC_MAT_003_37_01.PAGINA_SELECAO_DISCIPLINAS_DISPENSA_GRADUACAO)]
        public ActionResult SalvarSelecaoDisciplinasParaDispensa(SelecaoDisciplinasParaDispensaViewModel model, string tokenRet)
        {
            // Verifica se pode efetuar essa ação
            VerificarPreCondicoesEntradaPagina(model);

            SolicitacaoHistoricoNavegacaoService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(model.SeqSolicitacaoHistoricoNavegacao);

            return SMCRedirectToAction("EntrarEtapa", "Matricula", new { SeqSolicitacaoMatricula = new SMCEncryptedLong(model.SeqSolicitacaoMatricula), SeqConfiguracaoEtapa = new SMCEncryptedLong(model.SeqConfiguracaoEtapa), tokenRet = tokenRet });
        }

        #endregion
    }
}