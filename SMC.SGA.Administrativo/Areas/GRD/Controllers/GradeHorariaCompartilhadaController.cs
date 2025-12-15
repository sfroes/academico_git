using SMC.Academico.Common.Areas.GRD.Constants;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.GRD.Controllers
{
    public class GradeHorariaCompartilhadaController : SMCDynamicControllerBase
    {
        #region [ Services ]   

        private IDivisaoTurmaService DivisaoTurmaService => Create<IDivisaoTurmaService>();

        #endregion [ Services ]

        [SMCAuthorize(UC_GRD_001_04_02.MANTER_GRADE_COMPARTILHADA)]
        public JsonResult BuscarDivisoesTurmaGradeHorariaCompartilhada(long? seqTurma)
        {
            return Json(DivisaoTurmaService.BuscarDivisõesPorTurmaParaGradeCompartilhadaSelect(seqTurma.GetValueOrDefault()));
        }
    }
}