using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Data;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CAM.Controllers
{
    public class InstituicaoTipoEventoController : SMCDynamicControllerBase
    {
        #region [ Serviços ]

        private IInstituicaoTipoEventoService InstituicaoTipoEventoService
        {
            get { return this.Create<IInstituicaoTipoEventoService>(); }
        }

        #endregion [ Serviços ]

        [SMCAuthorize(UC_CAM_003_02_01.PESQUISAR_PARAMETROS_INSTITUICAO_TIPO_EVENTO)]
        public JsonResult BuscarTiposEventosAGDSelect(TipoEventoFiltroViewModel filtros)
        {
            List<SMCDatasourceItem> tiposEvento = this.InstituicaoTipoEventoService.BuscarTiposEventosAGDSelect(filtros.Transform<TipoEventoFiltroData>());
            return Json(tiposEvento);
        }
    }
}