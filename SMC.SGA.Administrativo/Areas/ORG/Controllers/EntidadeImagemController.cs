using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using SMC.SGA.Administrativo.Areas.ORG.Models.EntidadeImagem;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORG.Controllers
{
    public class EntidadeImagemController : SMCControllerBase, ISMCDynamicController
    {

        private IEntidadeService EntidadeService
        {
            get { return Create<IEntidadeService>(); }
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarCabecalho(EntidadeImagemFiltroDynamicModel dados)
        {

            var nomeEntidade = EntidadeService.BuscarEntidadeNome(dados.SeqEntidade.Value);
               
            var model = new EntidadeImagemCabecalhoViewModel() { Nome = nomeEntidade };

            return PartialView("_Cabecalho", model);
        }

    }

}