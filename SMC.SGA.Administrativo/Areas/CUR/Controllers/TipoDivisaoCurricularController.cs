using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class TipoDivisaoCurricularController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private ITipoDivisaoCurricularService TipoDivisaoCurricularService
        {
            get { return this.Create<ITipoDivisaoCurricularService>(); }
        }

        #endregion [ Services ]

        /// <summary>
        /// Busca a lista de tipo de divisao curricular de acordo com o nível de ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Lista de tipos de divisão curricular</returns>
        [HttpPost]
        [SMCAllowAnonymous]
        public JsonResult BuscarTiposDivisaoCurricularNivelEnsinoSelect(long seqNivelEnsino)
        {
            return Json(TipoDivisaoCurricularService.BuscarTiposDivisaoCurricularNivelEnsinoSelect(seqNivelEnsino));
        }
    }
}