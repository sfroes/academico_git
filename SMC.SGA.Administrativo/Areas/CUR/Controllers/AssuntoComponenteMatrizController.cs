using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using SMC.SGA.Administrativo.Areas.CUR.Views.AssuntoComponenteMatriz.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class AssuntoComponenteMatrizController : SMCControllerBase
    {
        #region [ Services ]

        private IDivisaoMatrizCurricularComponenteService DivisaoMatrizCurricularComponenteService => Create<IDivisaoMatrizCurricularComponenteService>();
        private IDivisaoMatrizCurricularService DivisaoMatrizCurricularService => Create<IDivisaoMatrizCurricularService>();
        private IComponenteCurricularService ComponenteCurricularService => Create<IComponenteCurricularService>();
        private IInstituicaoNivelTipoComponenteCurricularService InstituicaoNivelTipoComponenteCurricularService => Create<IInstituicaoNivelTipoComponenteCurricularService>();
        private IMatrizCurricularService MatrizCurricularService => Create<IMatrizCurricularService>();
        private IConfiguracaoComponenteService ConfiguracaoComponenteService => Create<IConfiguracaoComponenteService>();

        #endregion [ Services ]

        public ActionResult AssuntoComponenteMatrizCabecalho(long seqDivisaoMatrizCurricularComponente)
        {
            var cabecalho = DivisaoMatrizCurricularComponenteService
                .BusacarAssuntoComponenteMatrizCabecalho(seqDivisaoMatrizCurricularComponente)
                .Transform<AssuntoComponenteMatrizCabecalhoViewModel>();
            return PartialView("_Cabecalho", cabecalho);
        }

        [SMCAuthorize(UC_CUR_001_05_08.PESQUISAR_ASSUNTO_COMPONENTE_MATRIZ)]
        public ActionResult Index(long seqDivisaoMatrizCurricularComponente)
        {
            List<AssuntoComponenteMatrizListarViewModel> modelo = DivisaoMatrizCurricularComponenteService.
                                                                  BuscarAssuntosComponenteMatrizPorConfiguracaoComponente(seqDivisaoMatrizCurricularComponente)
                                                                  .TransformList<AssuntoComponenteMatrizListarViewModel>();
            return View(modelo);
        }

        [SMCAuthorize(UC_CUR_001_05_09.MANTER_ASSUNTO_COMPONENTE_MATRIZ)]
        public ActionResult Incluir(long seq, long seqDivisaoMatrizCurricularComponente)
        {
            AssuntoComponenteMatrizViewModel modelo = new AssuntoComponenteMatrizViewModel();
            modelo.SeqGrupoCurricularComponente = seq;
            modelo.SeqDivisaoMatrizCurricularComponente = seqDivisaoMatrizCurricularComponente;

            return PartialView("_Incluir", modelo);
        }

        [SMCAuthorize(UC_CUR_001_05_09.MANTER_ASSUNTO_COMPONENTE_MATRIZ)]
        public ActionResult SalvarAssunto(AssuntoComponenteMatrizViewModel model)
        {
            try
            {
                DivisaoMatrizCurricularComponenteService.SalvarAssunto(model.ComponentesCurricularSubstitutos.Seq.Value, model.SeqDivisaoMatrizCurricularComponente);

                SetSuccessMessage(UIResource.Mensagem_Sucesso_Inclusao, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction("Index", routeValues: new { model.SeqDivisaoMatrizCurricularComponente });
        }

        [SMCAuthorize(UC_CUR_001_05_08.EXCLUSAO_ASSUNTO_COMPONENTE_MATRIZ)]
        public ActionResult Excluir(SMCEncryptedLong seq, SMCEncryptedLong seqDivisaoMatrizCurricularComponente)
        {
            try
            {
                DivisaoMatrizCurricularComponenteService.ExcluirAssunto(seq, seqDivisaoMatrizCurricularComponente);

                SetSuccessMessage(UIResource.Mensagem_Sucesso_Exclusao, target: SMCMessagePlaceholders.Centro);               
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction("Index", routeValues: new { seqDivisaoMatrizCurricularComponente });
        }
    }
}