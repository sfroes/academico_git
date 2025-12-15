using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CAM.Controllers
{
    public class TipoProcessoSeletivoController : SMCDynamicControllerBase
    {
        private ITipoProcessoSeletivoService TipoProcessoSeletivoService => Create<ITipoProcessoSeletivoService>();

        [SMCAllowAnonymous]
        public ActionResult BuscarTipoProcessoPorNiveislEnsino(List<long> seqsNivelEnsino)
        {
            if (seqsNivelEnsino == null || seqsNivelEnsino.Count == 0)
            {
                var itens = TipoProcessoSeletivoService.BuscarTiposProcessoSeletivoSelect();
                return Json(itens);
            }
            else
            {
                var itens = TipoProcessoSeletivoService.BuscarTiposProcessoSeletivoPorNivelEnsinoSelect(new TipoProcessoSeletivoSelectFiltroData() { SeqsNivelEnsino = seqsNivelEnsino });
                return Json(itens);
            }
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarTipoProcessoPorNivelEnsino(long seqNivelEnsino)
        {
                var itens = TipoProcessoSeletivoService.BuscarTiposProcessoSeletivoPorNivelEnsinoSelect(new TipoProcessoSeletivoSelectFiltroData() { SeqsNivelEnsino = new List<long>() {seqNivelEnsino } });
                return Json(itens);
            
        }
    }
}