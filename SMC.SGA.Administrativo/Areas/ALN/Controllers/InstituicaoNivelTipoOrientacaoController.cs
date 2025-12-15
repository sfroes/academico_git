using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ALN.Controllers
{
    public class InstituicaoNivelTipoOrientacaoController : SMCDynamicControllerBase
    {
        [SMCAllowAnonymous]
        public ActionResult DesassociarTipoOrientacao(long seq)
        {
            try
            {
                //this.InstituicaoNivelTipoOrientacaoService.DesassociarTipoOrientacao(seq);

                // Renderiza a lista novamente
                return SMCRedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Necessário pois como não tem view a action de associar, devemos redirecionar para index afim de exibir a mensagem de erro
                ex = ex.SMCLastInnerException();
                this.ThrowRedirect(ex, "index", null);
                throw ex;
            }
        }
    }
}
