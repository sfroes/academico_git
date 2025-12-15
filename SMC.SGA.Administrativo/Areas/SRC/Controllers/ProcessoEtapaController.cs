using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using System;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Areas.SRC.Views.ProcessoEtapa.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class ProcessoEtapaController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IProcessoEtapaService ProcessoEtapaService
        {
            get { return Create<IProcessoEtapaService>(); }
        }

        #endregion [ Services ]

        public ActionResult CabecalhoProcessoEtapa(SMCEncryptedLong seq)
        {
            var modelCabecalho = ExecuteService<ProcessoEtapaCabecalhoData, ProcessoEtapaCabecalhoViewModel>(ProcessoEtapaService.BuscarCabecalhoProcessoEtapa, seq);

            return PartialView("_Cabecalho", modelCabecalho);
        }

        [SMCAuthorize(UC_SRC_002_01_02.MANTER_PROCESSO)]
        public ActionResult AcessarEtapa(SMCEncryptedLong seqProcessoEtapa)
        {
            return RedirectToAction("Index", "ProcessamentoPlanoEstudo", new { Area = "MAT", SeqProcessoEtapa = seqProcessoEtapa });
        }

        [SMCAuthorize(UC_SRC_002_01_01.ENCERRAR_ETAPA)]
        public ActionResult EncerrarEtapa(SMCEncryptedLong seqEtapa)
        {
            try
            {
                // Busca os dados para encerrar a etapa
                var dados = ProcessoEtapaService.BuscarDadosEncerrarProcessoEtapa(seqEtapa);

                if (dados.TipoPrazoEtapa == TipoPrazoEtapa.PeriodoVigencia)
                    Assert(null, UIResource.MSG_ConfirmacaoEncerrarEtapa_PeriodoVigencia, () => true);
                else if (dados.TipoPrazoEtapa == TipoPrazoEtapa.Escalonamento)
                {
                    var exibeAssertEscalonamentoBloqueio = ProcessoEtapaService.ValidarAssertEscalonamentoBloqueiosEncerrarEtapa(seqEtapa);

                    if (exibeAssertEscalonamentoBloqueio)
                        Assert(null, UIResource.MSG_ConfirmacaoEncerrarEtapa_EscalonamentoBloqueio, () => true);

                    string dadosGrupos = string.Empty;
                    if (dados.Escalonamentos != null)
                    {
                        foreach (var item in dados.Escalonamentos)
                        {
                            dadosGrupos += $"<br />{item.DataInicio.ToString("dd/MM/yyyy")} a {item.DataFim?.ToString("dd/MM/yyyy")}:";
                            dadosGrupos += "<ul>";
                            foreach (var grupo in item.DescricaoGruposEscalonamento)
                                dadosGrupos += $"<li>{grupo}</li>";
                            dadosGrupos += "</ul>";
                        }
                    }
                    Assert(null, string.Format(UIResource.MSG_ConfirmacaoEncerrarEtapa_Escalonamento, dadosGrupos), () => true);
                }

                ProcessoEtapaService.EncerrarEtapa(seqEtapa.Value);
                SetSuccessMessage("Etapa encerrada com sucesso!", null, SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, null, SMCMessagePlaceholders.Centro);
            }
            return SMCRedirectToAction("Index", "Processo");
        }

        [SMCAuthorize(UC_SRC_002_01_03.MANTER_ETAPA_PROCESSO)]
        public ActionResult LiberarEtapa(long seqProcessoEtapa)
        {
            var retorno = ProcessoEtapaService.LiberarProcessoEtapa(seqProcessoEtapa);

            return SMCRedirectToAction("Editar", "ProcessoEtapa", routeValues: new { seq = new SMCEncryptedLong(seqProcessoEtapa) });
        }

        [SMCAuthorize(UC_SRC_002_01_03.MANTER_ETAPA_PROCESSO)]
        public ActionResult ColocarManutencaoEtapa(long seqProcessoEtapa)
        {
            var retorno = ProcessoEtapaService.ColocarProcessoEtapaManutencao(seqProcessoEtapa);

            return SMCRedirectToAction("Editar", "ProcessoEtapa", routeValues: new { seq = new SMCEncryptedLong(seqProcessoEtapa) });
        }

        [SMCAuthorize(UC_SRC_002_01_03.MANTER_ETAPA_PROCESSO)]
        public JsonResult PreencherCampoNumeroDiasPrazoEtapa(TipoPrazoEtapa? tipoPrazoEtapa)
        {
            return Json(string.Empty);
        }

        [SMCAuthorize(UC_SRC_002_01_03.MANTER_ETAPA_PROCESSO)]
        public JsonResult PreencherCampoDataInicio(TipoPrazoEtapa? tipoPrazoEtapa)
        {
            return Json(string.Empty);
        }

        [SMCAuthorize(UC_SRC_002_01_03.MANTER_ETAPA_PROCESSO)]
        public JsonResult PreencherCampoDataFim(TipoPrazoEtapa? tipoPrazoEtapa)
        {
            return Json(string.Empty);
        }
    }
}