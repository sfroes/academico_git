using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Controllers
{
    public class GrauAcademicoController : SMCDynamicControllerBase
    {
        #region [ Service ]

        private IGrauAcademicoService GrauAcademicoService
        {
            get { return this.Create<IGrauAcademicoService>(); }
        }

        #endregion [ Service ]

        [SMCAuthorize(UC_CSO_001_03_01.PESQUISAR_GRAU_ACADEMICO)]
        public JsonResult BuscarGrauAcademicoPorNivelEnsinoSelectLookup(List<long> seqNivelEnsino, bool retornaTodos = false)
        {
            var filtro = new GrauAcademicoFiltroData() { SeqNivelEnsino = seqNivelEnsino, GrauAcademicoAtivo = true, RetornarTodos = retornaTodos };
            List<SMCDatasourceItem> retorno = GrauAcademicoService.BuscarGrauAcademicoLookupSelect(filtro);

            return Json(retorno);
        }
    }
}