using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Controllers
{
    public class MotivoBloqueioController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IMotivoBloqueioService MotivoBloqueioService
        {
            get { return this.Create<IMotivoBloqueioService>(); }
        }

        #endregion [ Services ]

        [SMCAllowAnonymous]
        public JsonResult BuscarMotivosBloqueioSelect(List<long> seqTipoBloqueio)
        {
            var filtros = new MotivoBloqueioFiltroData() { SeqTipoBloqueio = seqTipoBloqueio };
            var motivosBloqueio = this.MotivoBloqueioService.BuscarMotivosBloqueioSelect(filtros);
            return Json(motivosBloqueio);
        }

        [SMCAllowAnonymous]
        public JsonResult BuscarMotivosBloqueioInstituicaoSelect(List<long> seqTipoBloqueio)
        {
            var filtros = new MotivoBloqueioFiltroData() { SeqTipoBloqueio = seqTipoBloqueio };
            var motivosBloqueio = this.MotivoBloqueioService.BuscarMotivosBloqueioInstituicaoSelect(filtros);
            return Json(motivosBloqueio);
        }

        [SMCAllowAnonymous]
        public JsonResult BuscarMotivosBloqueioInstituicaoEditSelect(List<long> seqTipoBloqueio)
        {
            var filtros = new MotivoBloqueioFiltroData() { SeqTipoBloqueio = seqTipoBloqueio, RemoverIntegracao = true };
            var motivosBloqueio = this.MotivoBloqueioService.BuscarMotivosBloqueioInstituicaoSelect(filtros);
            return Json(motivosBloqueio);
        }

        [SMCAllowAnonymous]
        public JsonResult BuscarMotivosBloqueioFormatoManualAmbosSelect(List<long> seqTipoBloqueio)
        {
            var filtros = new MotivoBloqueioFiltroData() { SeqTipoBloqueio = seqTipoBloqueio };
            var motivosBloqueio = this.MotivoBloqueioService.BuscarMotivosBloqueioFormatoManualAmbosSelect(filtros);
            return Json(motivosBloqueio);
        }

        [SMCAllowAnonymous]
        public JsonResult ValidarObrigatoriedadeComprovante(FormaBloqueio formaDesbloqueio)
        {
            List<SMCSelectListItem> lista = new List<SMCSelectListItem>();
            lista.Add(new SMCSelectListItem() { Value = true.ToString(), Text = "Sim" });
            if (formaDesbloqueio == FormaBloqueio.Integracao)
            {
                lista.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não", Selected = true });
            }
            else
            {
                lista.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não" });
            }
           return Json(lista);
        }
    }
}