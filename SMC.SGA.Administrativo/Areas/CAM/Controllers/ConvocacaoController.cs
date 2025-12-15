using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.AgendadorTarefa.ServiceContract.Areas.ATS.Data;
using SMC.AgendadorTarefa.ServiceContract.Areas.ATS.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.CAM.Models;
using SMC.SGA.Administrativo.Areas.CAM.Views.Convocacao.App_LocalResources;
using SMC.SGA.Administrativo.Extensions;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CAM.Controllers
{
    public class ConvocacaoController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IHistoricoAgendamentoService HistoricoAgendamentoService => Create<IHistoricoAgendamentoService>();

        private IChamadaService ChamadaService => Create<IChamadaService>();

        private IConvocacaoService ConvocacaoService
        {
            get { return Create<IConvocacaoService>(); }
        }

        private IServicoService ServicoService
        {
            get { return Create<IServicoService>(); }
        }

        private IAgendamentoService AgendamentoService
        {
            get { return Create<IAgendamentoService>(); }
        }

        private ICampanhaService CampanhaService
        {
            get { return Create<ICampanhaService>(); }
        }

        private IProcessoSeletivoService ProcessoSeletivoService
        {
            get { return Create<IProcessoSeletivoService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_CAM_001_05_02.MANTER_CONVOCACAO)]
        public ActionResult CarregarIngressantes(SMCEncryptedLong seqChamada)
        {
            long? seqUltimoHistoricoAgendamento = null;

            var chamada = ChamadaService.BuscarChamadaParaConvocacao(seqChamada);

            // Verifica se a chamada ainda possui um agendamento.
            if (!chamada.SeqAgendamento.HasValue)
            {
                // Se não existirem inscrições convocadas e ainda não existir um agendamento, não permite entrar na tela.
                if (!ChamadaService.ExisteInscricoesConvocadas(seqChamada))
                {
                    SetErrorMessage(UIResource.MSG_Sem_ConvocadosGPI, UIResource.MSG_Titulo_Erro, SMCMessagePlaceholders.Centro);
                    return Redirect(Request.UrlReferrer.ToString());
                }

                // Cria um novo agendamento
                var agendamento = CriarAgendamento(seqChamada);
                chamada.SeqAgendamento = agendamento.SeqAgendamento;
                seqUltimoHistoricoAgendamento = agendamento.SeqUltimaExecucao;
            }
            else
            {
                // Existindo agendamento, verifica se a chamada não está encerrada
                if (chamada.SituacaoChamada != SituacaoChamada.ChamadaEncerrada)
                {
                    // Se não houver, verifica se existe alguma convocação em execução.
                    bool emExecucao = AgendamentoService.EstaEmExecucao(chamada.SeqAgendamento.Value);
                    if (!emExecucao)
                    {
                        bool existeConvocados = ChamadaService.ExisteInscricoesConvocadas(seqChamada);
                        if (existeConvocados)
                        {
                            // Cria um novo agendamento.
                            var agendamento = CriarAgendamento(seqChamada);
                            chamada.SeqAgendamento = agendamento.SeqAgendamento;
                            seqUltimoHistoricoAgendamento = agendamento.SeqUltimaExecucao;
                        }
                    }
                }
            }

            return View(new CarregarIngressantesViewModel()
            {
                SeqChamada = seqChamada,
                SeqAgendamento = chamada.SeqAgendamento.Value,
                SeqUltimoHistoricoAgendamento = seqUltimoHistoricoAgendamento,
                Campanha = chamada.Campanha,
                ProcessoSeletivo = chamada.ProcessoSeletivo,
                Chamada = $"{chamada.NumeroChamada}ª {SMCEnumHelper.GetDescription(chamada.TipoChamada)}"
            });
        }

        private AgendamentoTokenData CriarAgendamento(long seqChamada)
        {
            var parametros = AgendamentoService.BuscarParametrosServico(TOKEN_AGENDAMENTO.CARGA_INGRESSANTES);
            foreach (var p in parametros)
            {
                switch (p.Descricao)
                {
                    case "SeqChamada":
                        p.ValorParametro = seqChamada.ToString();
                        break;

                    case "SeqEntidadeInstituicao":
                        p.ValorParametro = HttpContext.GetInstituicaoEnsinoLogada().Seq.ToString();
                        break;

                    case "SeqSolicitante":
                        p.ValorParametro = SMCContext.User.SMCGetSequencialUsuario()?.ToString();
                        break;

                    case "NomeSolicitante":
                        p.ValorParametro = SMCContext.User.Identity.Name;
                        break;
                }
            }
            var dadosChamda = ChamadaService.BuscarChamadaParaConvocacao(seqChamada);
            string descricaoCampannha = $"{dadosChamda.NumeroChamada} Chamada - {dadosChamda.Convocacao} - {dadosChamda.DescricaoCicloLetivo} em {DateTime.Now}";
            //throw new Exception("agendamento ok");
            var agendamento = AgendamentoService.CriarAgendamentoPorTokenServico(TOKEN_AGENDAMENTO.CARGA_INGRESSANTES, descricaoCampannha, parametros);

            ChamadaService.AtualizarSeqAgendamento(seqChamada, agendamento.SeqAgendamento);

            return agendamento;
        }

        [SMCAuthorize(UC_CAM_001_05_08.VISUALIZAR_IMPEDIMENTOS)]
        public ActionResult LiberacaoDeMatricula(SMCEncryptedLong seqChamada)
        {
            var seqInstituicao = HttpContext.GetInstituicaoEnsinoLogada().Seq;

            var assertNaoExisteConvocados = ConvocacaoService.VerificarExistenciaConvocados(seqInstituicao, seqChamada);
            Assert(null, "MSG_Confirmacao_Encerrar_Chamada", () => !assertNaoExisteConvocados);

            var modelo = ConvocacaoService.VerificarImpedimentosExecutarMatriculaPorChamada(seqInstituicao, seqChamada).Transform<ConvocacaoImpedimentosDeMatriculaViewModel>();

            if (modelo.SemImpedimento)
            {
                SetSuccessMessage(UIResource.Liberação_Ingressantes_Concluida, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("Index", "Convocacao");
            }

            return PartialView("LiberacaoDeMatricula", modelo);
        }

        [SMCAuthorize(UC_CAM_001_05_08.VISUALIZAR_IMPEDIMENTOS)]
        public ActionResult EncerrarChamada(SMCEncryptedLong seqChamada)
        {
            ChamadaService.EncerrarChamada(seqChamada);

            SetSuccessMessage(UIResource.MSG_Chamada_Encerrada, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);
            return SMCRedirectToAction("Index", "Convocacao");
        }

        [SMCAuthorize(UC_CAM_001_05_01.PESQUISAR_CONVOCACAO)]
        public JsonResult BuscarCampanhasSelect(CampanhaFiltroViewModel filtros)
        {
            List<SMCDatasourceItem> campanhas = CampanhaService.BuscarCampanhasSelect(filtros.Transform<CampanhaFiltroData>());
            return Json(campanhas);
        }

        [SMCAuthorize(UC_CAM_001_05_01.PESQUISAR_CONVOCACAO)]
        public JsonResult BuscarProcessosSeletivosPorCampanhaSelect(long seqCampanha)
        {
            List<SMCDatasourceItem> processosSeletivos = ProcessoSeletivoService.BuscarProcessosSeletivosPorCampanhaSelect(seqCampanha);
            return Json(processosSeletivos);
        }

        [SMCAuthorize(UC_CAM_001_05_01.PESQUISAR_CONVOCACAO)]
        public JsonResult RetornaInformacaoGrupoEscalonamento(SituacaoChamada? situacaoChamada)
        {
            string descricaoSituacaoChamada = "Nenhum";

            if (situacaoChamada.HasValue)
            {
                descricaoSituacaoChamada = situacaoChamada.SMCGetDescription();
            }

            var mensagem = string.Format(UIResource.MSG_Grupo_escalonamento_Nao_Pode_Ser_Alterado, descricaoSituacaoChamada);
                        
            return Json(mensagem);
        }
    }
}