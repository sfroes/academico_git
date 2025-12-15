using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Data.Aluno;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Data;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Binders;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.ALN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Areas.ALN.Views.Aluno.App_LocalResources;
using System.Web.Routing;
using SMC.Academico.Common.Constants;
using SMC.Academico.Common.Areas.SRC.Constants;

namespace SMC.SGA.Administrativo.Areas.ALN.Controllers
{
    public class AlunoController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IAlunoService AlunoService { get => Create<IAlunoService>(); }

        private IAlunoFormacaoService AlunoFormacaoService => Create<IAlunoFormacaoService>();

        private IEventoLetivoService EventoLetivoService => Create<IEventoLetivoService>();

        private ITipoVinculoAlunoService TipoVinculoAlunoService => Create<ITipoVinculoAlunoService>();

        private IFormaIngressoService FormaIngressoService => Create<IFormaIngressoService>();

        private ISituacaoMatriculaService SituacaoMatriculaService => Create<ISituacaoMatriculaService>();

        private IPessoaAtuacaoService PessoaAtuacaoService => Create<IPessoaAtuacaoService>();

        private ICicloLetivoService CicloLetivoService => Create<ICicloLetivoService>();

        #endregion [ Services ]

        [SMCAuthorize(UC_ALN_001_01_01.PESQUISAR_ALUNO)]
        public FileContentResult ExportarRelatorio()
        {
            //Utilizar a controller dynamic do filtro
            string key = string.Format(SMCViewModelBinder.FilterKeyIdentifier,
                                 "Aluno",
                                 "AlunoFiltroDynamicModel",
                                 SMCContext.User.SMCGetSequencialUsuario());
            //BUSCANDO O FILTRO NO FRAMEWORK
            var filtro = Session[key] as AlunoFiltroDynamicModel;

            var filtroData = filtro?.Transform<AlunoFiltroData>() ?? new AlunoFiltroData();
            var lista = AlunoService.BuscarAlunos(filtroData)
                .TransformList<AlunoListarDynamicModel>();

            //var instituicao = InstituicaoEnsinoService.BuscarInstituicoesEnsinoLogado();
            //var parameters = new List<ReportParameter>();
            //parameters.Add(new ReportParameter("MensagemRodape", UIResource.Layout_Rodape_Mensagem_SMC));
            //parameters.Add(new ReportParameter("ImagemCabecalho", instituicao.ArquivoLogotipo == null ? string.Empty : Convert.ToBase64String(instituicao.ArquivoLogotipo.FileData)));
            //parameters.Add(new ReportParameter("NomeInstituicao", instituicao.Nome));

            return RenderPdfView("ExportacaoAluno", model: lista);

            //return SMCRenderReport(SMCGetConfigurationInstance(lista, "DSAluno", "AlunoRelatorio.rdlc", new SMCReportViewerHelper(SMCExportTypes.PDF), SMCOrientationReport.Landscape, parameters, false));
        }

        [SMCAuthorize(UC_ALN_001_01_01.PESQUISAR_ALUNO, UC_SRC_004_01_02.CONSULTAR_SOLICITACAO)]
        public ActionResult VisualizarDadosAcademicosAluno(SMCEncryptedLong seq)
        {
            var model = AlunoService.BuscarAlunoVisualizacaoDados(seq).Transform<AlunoListarDynamicModel>();
            return PartialView("ExportacaoAlunoItem", model);
        }

        [SMCAuthorize(UC_ALN_001_02_02.VISUALIZAR_DADOS_MATRICULA)]
        public ActionResult ConsultaDadosAluno(SMCEncryptedLong seq)
        {
            var model = AlunoService.BuscarDadosMatriculaAluno(seq).Transform<ConsultaDadosAlunoViewModel>();

            return View(model);
        }

        [SMCAuthorize(UC_ALN_001_01_02.MANTER_ALUNO)]
        public ActionResult BuscarAlunoCabecalho(long seq)
        {
            //BI_ALN_001 - Aluno - Cabeçalho
            var modelHeader = ExecuteService<AlunoCabecalhoData, AlunoCabecalhoViewModel>(AlunoService.BuscarAlunoCabecalho, seq);

            return PartialView("_Cabecalho", modelHeader);
        }

        [SMCAuthorize(UC_ALN_001_04_01.ASSOCIAR_FORMACAO_ESPECIFICA_ALUNO)]
        public ActionResult AssociacaoFormacoesEspecificasAluno(SMCEncryptedLong seqAluno)
        {
            var model = AlunoFormacaoService.BuscarAssociacaoFormacaoEspecifica(seqAluno).Transform<AssociacaoFormacoesEspecificasAlunoViewModel>();
            return View(model);
        }

        [SMCAuthorize(UC_ALN_001_04_01.ASSOCIAR_FORMACAO_ESPECIFICA_ALUNO)]
        public ActionResult SalvarAssociacaoFormacoesEspecificasAluno(AssociacaoFormacoesEspecificasAlunoViewModel model)
        {
            try
            {
                AlunoFormacaoService.SalvarAssociacaoFormacoesEspecificasAluno(model.Transform<AssociacaoFormacaoEspecificaAlunoData>());
                SetSuccessMessage(Views.Aluno.App_LocalResources.UIResource.MSG_AssociacaoFormacaoEspecifica_Sucesso,
                                  Views.Ingressante.App_LocalResources.UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, Views.Ingressante.App_LocalResources.UIResource.MSG_Titulo_Erro, SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction(nameof(AlunoController.AssociacaoFormacoesEspecificasAluno), new RouteValueDictionary(new { seqAluno = new SMCEncryptedLong(model.SeqAluno), voltar = true }));
        }

        [SMCAuthorize(UC_ALN_001_01_01.PESQUISAR_ALUNO)]
        public ActionResult BuscarTipoVinculoAluno(SMCEncryptedLong seqNivelEnsino)
        {
            return Json(TipoVinculoAlunoService.BuscarTipoVinculoAlunoNaInstituicaoSelect(seqNivelEnsino));
        }

        [SMCAuthorize(UC_ALN_001_01_01.PESQUISAR_ALUNO)]
        public ActionResult BuscarFormasIngresso(SMCEncryptedLong seqNivelEnsino, SMCEncryptedLong seqTipoVinculoAluno)
        {
            return Json(FormaIngressoService.BuscarFormasIngressoInstituicaoNivelVinculoSelect(new FormaIngressoFiltroData()
            {
                SeqNivelEnsino = seqNivelEnsino,
                SeqTipoVinculoAluno = seqTipoVinculoAluno
            }));
        }

        [SMCAuthorize(UC_ALN_001_01_01.PESQUISAR_ALUNO)]
        public ActionResult BuscarSituacoesMatricula(SMCEncryptedLong seqNivelEnsino, SMCEncryptedLong seqTipoVinculoAluno)
        {
            return Json(SituacaoMatriculaService.BuscarSituacoesMatriculasDaInstiuicaoSelect(new SituacaoMatriculaFiltroData()
            {
                SeqNivelEnsino = seqNivelEnsino,
                SeqTipoVinculoAluno = seqTipoVinculoAluno
            }));
        }

        [SMCAuthorize(UC_ALN_001_09_01.CANCELAR_MATRICULA)]
        public ActionResult BuscarSituacaoMatricula(SMCEncryptedLong seqSituacaoMatricula)
        {
            return Json(SituacaoMatriculaService.BuscarSituacaoMatricula(seqSituacaoMatricula).Token);
        }


        [SMCAuthorize(UC_ALN_001_01_03.IMPORTAR_ALUNO)]
        public ActionResult ModalSincronizarDadosAluno()
        {
            return PartialView("_ModalSincronizarDadosAluno");
        }

        [SMCAuthorize(UC_ALN_001_01_03.IMPORTAR_ALUNO)]
        public ActionResult BuscarESincronizarDadosAluno(AlunoFiltroDynamicModel filtro)
        {
            try
            {
                var seqPessoaAtuacao = AlunoService.SincronizarDadosALunoSomenteComCodigoMigracao(filtro.CodigoAlunoMigracao.Value);
                var aluno = AlunoService.BuscarAluno(seqPessoaAtuacao);

                var dadosAluno = !string.IsNullOrEmpty(aluno.NomeSocial) ?
                    $"{aluno.NomeSocial} (Nome civil: {aluno.Nome}) ({aluno.CodigoAlunoMigracao}) - RA {aluno.NumeroRegistroAcademico}" :
                    $"{aluno.Nome} ({aluno.CodigoAlunoMigracao}) - RA {aluno.NumeroRegistroAcademico}";

                SetSuccessMessage(string.Format(UIResource.MSG_Sucesso_Importacao_Dados_Aluno, dadosAluno));
            }
            catch (Exception e)
            {
                throw e;
            }
            return Json(string.Empty);
        }


        #region Cancelar Matricula
        [SMCAuthorize(UC_ALN_001_09_01.CANCELAR_MATRICULA)]
        public ActionResult CancelarMatricula(SMCEncryptedLong seqAluno)
        {
            var eventoLetivo = EventoLetivoService.BuscarEventoLetivoAtual(seqAluno);

            var model = new CancelarMatriculaViewModel()
            {
                SeqAluno = seqAluno,
                CiclosLetivos = CicloLetivoService.BuscarCiclosLetivosAlunoComSituacaoSelect(seqAluno, 1),
                SituacoesMatricula = SituacaoMatriculaService.BuscarSituacoesMatriculasDaInstiuicaoSelect(new SituacaoMatriculaFiltroData()
                {
                    Tokens = new List<string>
                    {
                        TOKENS_SITUACAO_MATRICULA.CANCELADO_DESCUMPRIMENTO_NORMAS_CURSO,
                        TOKENS_SITUACAO_MATRICULA.CANCELADO_MOTIVO_FALECIMENTO
                    }
                }),
                DataInicioCicloLetivo = eventoLetivo.DataInicio,
                DataFimCicloLetivo = eventoLetivo.DataFim.Date > DateTime.Now.Date ? DateTime.Now.Date : eventoLetivo.DataFim
            };

            model.SeqCicloLetivo = model.CiclosLetivos.FirstOrDefault(c => c.Selected).Seq;

            return PartialView("_CancelarMatricula", model);
        }

        [SMCAuthorize(UC_ALN_001_09_01.CANCELAR_MATRICULA)]
        [HttpPost]
        public ActionResult CancelarMatricula(CancelarMatriculaViewModel model)
        {
            // Busca os dados do aluno
            var aluno = AlunoService.BuscarAluno(model.SeqAluno);

            // Verifica qual será a data de inicio da situação de cancelamento
            DateTime dataReferencia = DateTime.Now;
            if (model.DataInicio != DateTime.Today) // Lançamento retroativo
                dataReferencia = model.DataInicio.AddHours(23).AddMinutes(59);

            /********************** VALIDAÇÕES DE ASSERT **********************/
            // Confirma o cancelamento do aluno 
            // Mensagem: "Confirma o cancelamento de matrícula do aluno(a) <Nome do aluno>?"
            var nomeAluno = aluno.Nome;
            if (!string.IsNullOrEmpty(aluno.NomeSocial))
                nomeAluno = $"{aluno.NomeSocial} ({aluno.Nome})";
            Assert(model, "_AssertCancelarMatricula", new AssertCancelarMatriculaViewModel { Nome = nomeAluno }, () => true);

            // Verifica se aluno possui altuma pendencia financeira
            if (aluno.CodigoAlunoMigracao.HasValue)
            {
                Assert(model, SRC.Views.RealizarAtendimento.App_LocalResources.UIResource.MSG_Assert_ParcelasEmAberto_Cancelamento, () =>
                            AlunoService.ValidarNadaConstaFinanceiro(aluno.CodigoAlunoMigracao.Value, dataReferencia));
            }
            /********************** FIM VALIDAÇÕES DE ASSERT **********************/

            // Busca informações da pessoa_atuação
            var pessoaAtuacao = PessoaAtuacaoService.RecuperaDadosOrigem(model.SeqAluno);

            // Busca a situação de matrícula de destino
            var situacaoMatricula = SituacaoMatriculaService.BuscarSituacaoMatricula(model.SeqSituacaoMatricula);

            // Verifica o tipo de cancelamento realizado
            // Tipo de cancelamento SGP = tipo de cancelamento de acordo com a situação de matricula selecionada.:
            // Se token da situação for CANCELADO_DESCUMPRIMENTO_NORMAS_CURSO - "0"
            // Se token da situação for CANCELADO_MOTIVO_FALECIMENTO, - "3"
            short tipoCancelamento = (short)(situacaoMatricula.Token == TOKENS_SITUACAO_MATRICULA.CANCELADO_DESCUMPRIMENTO_NORMAS_CURSO ? 0 : 3);

            // Monta as informações para o cancelamento
            var data = new CancelarMatriculaAlunoData()
            {
                TokenSituacaoCancelamento = situacaoMatricula.Token,
                ObservacaoSituacaoMatricula = model.Observacao,
                DataReferencia = dataReferencia,
                Jubilado = situacaoMatricula.Token == TOKENS_SITUACAO_MATRICULA.CANCELADO_DESCUMPRIMENTO_NORMAS_CURSO ? true : false,
                CancelarBeneficio = true,
                ObservacaoCancelamentoSolicitacao = situacaoMatricula.Token == TOKENS_SITUACAO_MATRICULA.CANCELADO_DESCUMPRIMENTO_NORMAS_CURSO ? "Solicitação cancelada devido ao cancelamento da matrícula do(a) aluno(a) por descumprimento de normas acadêmicas." : "Solicitação cancelada devido ao cancelamento da matrícula do(a) aluno(a) por motivo de falecimento.",
                TipoCancelamentoSGP = tipoCancelamento,

                SeqPessoaAtuacao = model.SeqAluno,
                SeqAlunoHistorico = pessoaAtuacao.SeqAlunoHistoricoAtual,
                CodigoAlunoSGP = aluno.CodigoAlunoMigracao,
                SeqCicloLetivo = model.SeqCicloLetivo,
                SeqArquivoAnexado = model.SeqArquivoAnexado,
                ArquivoAnexado = model.ArquivoAnexado.Transform<ArquivoAnexadoData>()
            };
            AlunoService.CancelarMatricula(data);

            SetSuccessMessage(UIResource.MSG_SucessoCancelarMatricula, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction("Index");
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarDataInicioCicloLetivo(SMCEncryptedLong seqCicloLetivo, SMCEncryptedLong seqAluno)
        {
            return Content(EventoLetivoService.BuscarDatasEventoLetivo(seqCicloLetivo, seqAluno, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO).DataInicio.ToString());
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarDataFimCicloLetivo(SMCEncryptedLong seqCicloLetivo, SMCEncryptedLong seqAluno)
        {
            var dataFimCicloLetivo = EventoLetivoService.BuscarDatasEventoLetivo(seqCicloLetivo, seqAluno, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO).DataFim;

            return dataFimCicloLetivo.Date > DateTime.Now.Date ? Content(DateTime.Now.Date.ToString()) : Content(dataFimCicloLetivo.ToString());
        }

        [SMCAllowAnonymous]
        public ActionResult LimparDataInicio(SMCEncryptedLong seqCicloLetivo)
        {
            return Content(string.Empty);
        }

        #endregion
    }
}