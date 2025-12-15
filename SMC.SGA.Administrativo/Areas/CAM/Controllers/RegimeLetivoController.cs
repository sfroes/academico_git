using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CAM.Controllers
{
    public class RegimeLetivoController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IRegimeLetivoService RegimeLetivoService
        {
            get { return this.Create<IRegimeLetivoService>(); }
        }

        #endregion [ Services ]

        /// <summary>
        /// Busca a lista de regime letivo de acordo com o nível de ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Lista de regime letivo</returns>
        [HttpPost]
        [SMCAllowAnonymous]
        public JsonResult BuscarRegimesLetivoPorNivelEnsinoSelect(long seqNivelEnsino)
        {
            return Json(RegimeLetivoService.BuscarRegimesLetivoPorNivelEnsinoSelect(seqNivelEnsino));
        }
    }
}