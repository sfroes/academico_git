using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.ALN.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ALN.Controllers
{
    public class PessoaAtuacaoTermoIntercambioPeriodoController : SMCDynamicControllerBase
    {
        #region [ Serviços ]

        #endregion

        public ActionResult CabecalhoPessoaAtuacaoTermoIntercambioPeriodo(PessoaAtuacaoTermoIntercambioPeriodoDynamicModel model)
        {
            var cabecalhoModel = model.Transform<PessoaAtuacaoTermoIntercambioPeriodoCabecalhoViewModel>();

            return PartialView("_Cabecalho", cabecalhoModel);
        }
    }
}