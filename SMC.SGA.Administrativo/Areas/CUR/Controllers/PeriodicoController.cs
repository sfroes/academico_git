using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using SMC.SGA.Administrativo.Areas.CUR.Views.Periodico.App_LocalResources;
using System;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class PeriodicoController : SMCDynamicControllerBase
    {
        #region Servico

        private IPeriodicoService PeriodicoService
        {
            get { return this.Create<IPeriodicoService>(); }
        }

        #endregion Servico

        [SMCAuthorize(UC_CUR_002_06_01.PESQUISAR_PERIODICO_CAPES)]
        public ActionResult Periodico(PeriodicoFiltroDynamicModel filtro)
        {
            return PartialView("_Periodico", filtro);
        }

        [SMCAuthorize(UC_CUR_002_06_01.PESQUISAR_PERIODICO_CAPES)]
        public JsonResult BuscarAreaAvaliacaoSelect(long seqClassificacaoPeriodico)
        {
            var retorno = PeriodicoService.BuscarAreaAvaliacaoSelect(seqClassificacaoPeriodico);
            return Json(retorno);
        }

        [SMCAuthorize(UC_CUR_002_06_01.PESQUISAR_PERIODICO_CAPES)]
        public JsonResult BuscarQualisCapesSelect(long seqClassificacaoPeriodico)
        {
            var retorno = PeriodicoService.BuscarQualiCapesSelect(seqClassificacaoPeriodico);
            return Json(retorno);
        }

        [SMCAuthorize(UC_CUR_002_06_01.PESQUISAR_PERIODICO_CAPES)]
        public JsonResult BuscarDescricaoClassificacaoCapes(int? anoInicio, int? anoFim)
        {
            var retorno = "Classificação de periódicos";

            if (anoInicio.HasValue)
                retorno += $" {anoInicio.Value.ToString()}";

            if (anoFim.HasValue)
            {
                if (anoInicio.HasValue)
                {
                    if (retorno.Contains(anoInicio.Value.ToString()))
                        retorno += $" - {anoFim.Value.ToString()}";
                    else
                        retorno += $"{anoFim.Value.ToString()}";
                }
                else
                    retorno += $" {anoFim.Value.ToString()}";
            }

            return Json(retorno);
        }

        [SMCAuthorize(UC_CUR_002_06_02.IMPORTAR_ARQUIVO_CLASSIFICACAO)]
        public ActionResult ImportarPeriodico(SMCEncryptedLong seqClassificacaoPeriodico)
        {
            var model = new PeriodicoImportacaoViewModel() { SeqClassificacaoPeriodico = seqClassificacaoPeriodico };

            return PartialView("_ImportarPeriodico", model);
        }

        [SMCAuthorize(UC_CUR_002_06_02.IMPORTAR_ARQUIVO_CLASSIFICACAO)]
        public ActionResult SalvarImportacaoPeriodico(PeriodicoImportacaoViewModel model)
        {
            try
            {
                this.PeriodicoService.SalvarPeriodo(model.Transform<PeriodicoData>());

                SetSuccessMessage(UIResource.MSG_ImportacaoPeriodico_Sucesso, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, UIResource.MSG_Titulo_Erro, SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction("Index");
        }

        [SMCAuthorize(UC_CUR_002_06_03.MANTER_PERIODICO_CAPES)]
        public ActionResult CabecalhoPeriodico(PeriodicoDynamicModel model)
        {
            return PartialView("_Header", model);
        }
    }
}