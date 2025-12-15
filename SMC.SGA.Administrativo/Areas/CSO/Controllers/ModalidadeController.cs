using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Controllers
{
    public class ModalidadeController : SMCDynamicControllerBase
    {
        #region [ Service ]

        private IModalidadeService ModalidadeService
        {
            get { return this.Create<IModalidadeService>(); }
        }

        private IInstituicaoNivelModalidadeService InstituicaoNivelModalidadeService
        {
            get { return this.Create<IInstituicaoNivelModalidadeService>(); }
        }

        #endregion [ Service ]

        [SMCAuthorize(UC_CSO_001_05_01.MANTER_MODALIDADE)]
        public JsonResult BuscarModalidadePorCursoOfertaSelectLookup(CursoOfertaLookupViewModel seqCursoOferta)
        {
            List<SMCDatasourceItem> listItens = new List<SMCDatasourceItem>();

            if (seqCursoOferta != null && seqCursoOferta.Seq > 0)
                listItens = ModalidadeService.BuscarModalidadesPorCursoOfertaSelect(seqCursoOferta.Seq.Value);
            else
                listItens = InstituicaoNivelModalidadeService.BuscarModalidadesPorInstituicaoSelect();

            return Json(listItens);
        }
    }
}