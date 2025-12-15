using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Controllers
{
    public class ComponenteCurricularController : SMCControllerBase
    {
        private IInstituicaoNivelTipoComponenteCurricularService InstituicaoNivelTipoComponenteCurricularService => this.Create<IInstituicaoNivelTipoComponenteCurricularService>();

        [SMCAllowAnonymous]
        public JsonResult BuscarTipoComponenteCurricularSelectLookup(long seqInstituicaoNivelResponsavel, long? seqGrupoCurricular)
        {
            List<SMCDatasourceItem> listItens = !seqGrupoCurricular.HasValue ?
                InstituicaoNivelTipoComponenteCurricularService.BuscarTipoComponenteCurricularSelect(seqInstituicaoNivelResponsavel) :
                InstituicaoNivelTipoComponenteCurricularService.BuscarTipoComponenteCurricularPorGrupoSelect(seqGrupoCurricular.Value);

            return Json(listItens);
        }

        [SMCAllowAnonymous]
        public JsonResult BuscarEntidadesPorTipoComponenteSelectLookup(long seqInstituicaoNivelResponsavel, long seqTipoComponenteCurricular)
        {
            List<SMCDatasourceItem> listItens = new List<SMCDatasourceItem>();

            if (seqInstituicaoNivelResponsavel > 0 && seqTipoComponenteCurricular > 0)
            {
                listItens = InstituicaoNivelTipoComponenteCurricularService.BuscarEntidadesPorTipoComponenteSelect(seqInstituicaoNivelResponsavel, seqTipoComponenteCurricular);
            }

            return Json(listItens);
        }
    }
}