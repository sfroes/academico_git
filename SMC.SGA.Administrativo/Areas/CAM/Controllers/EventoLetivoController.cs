using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Data;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CAM.Models;
using SMC.SGA.Administrativo.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CAM.Controllers
{
    public class EventoLetivoController : SMCDynamicControllerBase
    {
        #region Serviços

        private IEventoLetivoService EventoLetivoService
        {
            get { return this.Create<IEventoLetivoService>(); }
        }

        private IInstituicaoNivelModalidadeService InstituicaoNivelModalidadeService
        {
            get { return this.Create<IInstituicaoNivelModalidadeService>(); }
        }

        private IInstituicaoTipoEventoService InstituicaoTipoEventoService
        {
            get { return this.Create<IInstituicaoTipoEventoService>(); }
        }

        private ITurnoService TurnoService
        {
            get { return this.Create<ITurnoService>(); }
        }

        #endregion Serviços

        [SMCAuthorize(UC_CAM_002_03_01.PESQUISAR_EVENTO_LETIVO)]
        public JsonResult BuscarTiposEventosAGDSelect(TipoEventoFiltroViewModel filtros)
        {
            List<SMCDatasourceItem> tiposEvento = this.InstituicaoTipoEventoService.BuscarTiposEventosAGDSelect(filtros.Transform<TipoEventoFiltroData>());
            return Json(tiposEvento);
        }

        [SMCAuthorize(UC_CAM_002_03_01.PESQUISAR_EVENTO_LETIVO)]
        public JsonResult BuscarModalidadesPorNivelEnsinoSelect(long seqNivelEnsino)
        {
            List<SMCDatasourceItem> modalidades = this.InstituicaoNivelModalidadeService.BuscarModalidadesPorNivelEnsinoSelect(seqNivelEnsino);
            return Json(modalidades);
        }

        [SMCAuthorize(UC_CAM_002_03_01.PESQUISAR_EVENTO_LETIVO)]
        public JsonResult BuscarTurnosPorCursoOferta(long seqCursoOferta)
        {
            List<SMCDatasourceItem> turnos = this.TurnoService.BuscarTurnosPorCursoOfertaSelect(seqCursoOferta);
            return Json(turnos);
        }

        [SMCAuthorize(UC_CAM_002_03_02.MANTER_EVENTO_LETIVO)]
        public ActionResult CarregarDados(EventoLetivoDynamicModel model)
        {
            model.ExibirNivelEnsino = true;
            model.ExibirLocalidade = true;
            model.ExibirEntidadeResponsavel = true;

            return SMCViewWizard(model, null);
        }
    }
}