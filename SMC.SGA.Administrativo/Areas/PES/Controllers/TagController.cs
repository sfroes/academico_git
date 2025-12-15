using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Controllers
{
    public class TagController : SMCDynamicControllerBase
    {
        [SMCAuthorize(UC_PES_005_03_01.MANTER_TAG)]
        public JsonResult PreencheTipoPreenchimento(TipoTag tipoTag)
        {
            var lista = new List<SMCDatasourceItem>();

            lista.Add(new SMCDatasourceItem() { Seq = (int)TipoPreenchimentoTag.Automatico, Descricao = TipoPreenchimentoTag.Automatico.SMCGetDescription(), Selected = tipoTag == TipoTag.Mensagem ? true : false });
            lista.Add(new SMCDatasourceItem() { Seq = (int)TipoPreenchimentoTag.Manual, Descricao = TipoPreenchimentoTag.Manual.SMCGetDescription() });

            return Json(lista);
        }

        [SMCAuthorize(UC_PES_005_03_01.MANTER_TAG)]
        public JsonResult HabilitarTipoPreenchimentoTag(TipoTag? tipoTag)
        {
            var habilitado = false;
            if (tipoTag == TipoTag.Mensagem)
                habilitado = true;
            return Json(habilitado);
        }

        [SMCAuthorize(UC_PES_005_03_01.MANTER_TAG)]
        public JsonResult HabilitarQueryOrigem(TipoTag? tipoTag, TipoPreenchimentoTag? tipoPreenchimentoTag)
        {
            var habilitado = false;
            if (tipoTag == TipoTag.DeclaracaoGenerica && tipoPreenchimentoTag == TipoPreenchimentoTag.Automatico)
                habilitado = true;
            return Json(habilitado);
        }
    }
}