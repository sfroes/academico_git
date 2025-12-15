using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.ORT.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORT.Controllers
{
	public class AvaliacaoTrabalhoAcademicoBancaExaminadoraController : SMCDynamicControllerBase
	{
		[SMCAuthorize(UC_ORT_002_02_04.MANTER_AGENDAMENTO_BANCA_EXAMINADORA)]
		public ActionResult HeaderEdit(AvaliacaoTrabalhoAcademicoBancaExaminadoraDynamicModel model)
		{
			return PartialView(model);
		}

        [SMCAuthorize(UC_ORT_002_02_04.MANTER_AGENDAMENTO_BANCA_EXAMINADORA)]
        public ActionResult PreencherComplementoInstituicao(bool? alunoFormado, string complementoInstituicao)
        {
            if (!alunoFormado.HasValue || alunoFormado == false)
                complementoInstituicao = string.Empty;
            return Content(complementoInstituicao);
        }

        [SMCAuthorize(UC_ORT_002_02_04.MANTER_AGENDAMENTO_BANCA_EXAMINADORA)]
        public ActionResult Editar(long? seqTrabalhoAcademico)
        {
            return View();
        }
    }
}