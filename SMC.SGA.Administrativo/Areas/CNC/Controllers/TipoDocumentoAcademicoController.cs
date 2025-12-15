using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Enums;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Model;
using SMC.Framework;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System.Collections.Generic;
using System.Web.Mvc;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.CNC.Controllers
{
    public class TipoDocumentoAcademicoController : SMCDynamicControllerBase
    {
        #region [Service]
        private ITagService TagService => Create<ITagService>();
        #endregion
        [SMCAuthorize(UC_CNC_004_01_01.MANTER_TIPO_DOCUMENTO_ACADEMICO)]
        public JsonResult BuscarInformacaoTag(long? seqTag)
        {
            bool TipoReadOnly = false;
            string InformacaoTag = string.Empty;

            if (seqTag != null)
            {
                var tagSelecionada = TagService.BuscarTag(seqTag.Value);
                TipoReadOnly = tagSelecionada.TipoPreenchimentoTag == TipoPreenchimentoTag.Automatico;
                InformacaoTag = tagSelecionada.InformacaoTag;
            }

            return Json(new
            {
                TipoReadOnly,
                InformacaoTag
            });
        }

        [SMCAuthorize(UC_CNC_004_01_01.MANTER_TIPO_DOCUMENTO_ACADEMICO)]
        public ActionResult PreenchePermiteEditarDado(long seqTag)
        {
            if (seqTag != 0)
            {
                var tag = TagService.BuscarTag(seqTag);

                if (tag.TipoPreenchimentoTag != TipoPreenchimentoTag.Manual)
                    return Content("False");
                else
                    return Content("True");
            }

            return Content("False");
        }
    }
}