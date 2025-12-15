using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.Common.Enums;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.AgendadorTarefa.ServiceContract.Areas.ATS.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class PreparacaoRematriculaController : SMCControllerBase
    {
        #region Services

        private IProcessoService ProcessoService => Create<IProcessoService>();

        private IAgendamentoService AgendamentoService => Create<IAgendamentoService>();

        #endregion Services

        // GET: SRC/PreparacaoRematricula
        [SMCAuthorize(UC_SRC_002_01_01.PREPARAR_RENOVACAO_MATRICULA)]
        public ActionResult Index(SMCEncryptedLong seqProcesso)
        {
            try
            {
                var model = ProcessoService.BuscarProcessos(new ProcessoFiltroData() { Seq = seqProcesso }).First()
                                                .Transform<PreparacaoRematriculaViewModel>();

                var (seqAgendamento, situacaoAgendamento) = ProcessoService.BuscarAgendamentoSAT(seqProcesso);
                model.SeqAgendamento = seqAgendamento;

                // Verifica se a chamada não possui um agendamento ou se o agendamento atual não está mais em execução.
                if (!model.SeqAgendamento.HasValue ||
                   (situacaoAgendamento.HasValue && situacaoAgendamento.Value != SituacaoAgendamento.Executando))
                {
                    CriarAgendamento(seqProcesso, model);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return RedirectToAction("Index", "Processo");
            }
        }

        private void CriarAgendamento(SMCEncryptedLong seqProcesso, PreparacaoRematriculaViewModel model)
        {
            // Cria um novo agendamento
            var parametros = AgendamentoService.BuscarParametrosServico(TOKEN_AGENDAMENTO.PREPARACAO_RENOVACAO_MATRICULA);
            foreach (var p in parametros)
            {
                switch (p.Descricao)
                {
                    case "SeqProcesso":
                        p.ValorParametro = seqProcesso.Value.ToString();
                        break;
                    case "IdUsuario":
                        p.ValorParametro = SMCContext.User.Identity.Name;
                        break;
                }
            }

            var agendamento = AgendamentoService.CriarAgendamentoPorTokenServico(TOKEN_AGENDAMENTO.PREPARACAO_RENOVACAO_MATRICULA, model.Descricao, parametros);

            ProcessoService.CriarAgendamentoPreparacaoRematricula(seqProcesso, agendamento.SeqAgendamento);

            model.SeqAgendamento = agendamento.SeqAgendamento;
            model.SeqUltimoHistoricoAgendamento = agendamento.SeqUltimaExecucao;
        }
    }
}