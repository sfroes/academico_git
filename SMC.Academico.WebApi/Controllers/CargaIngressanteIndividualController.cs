using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.WebApi.Models;
using SMC.Framework.Service.Security;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;

namespace SMC.Academico.WebApi.Controllers
{
    public class CargaIngressanteIndividualController : SMCApiControllerBase
    {
        #region Services

        private IIngressanteService IngressanteService => Create<IIngressanteService>();

        #endregion Services

        [HttpPost]
        public void Post(CargaIngressanteIndividualModel model)
        {
            var identificacao = $"{model.SeqSolicitante}/{model.NomeSolicitante}";
            var generic = new GenericIdentity(identificacao, "manual");
            generic.AddClaim(new System.Security.Claims.Claim("SeqUsuario", model.SeqSolicitante));
            generic.AddClaim(new System.Security.Claims.Claim("Nome", identificacao));
            generic.AddClaim(new System.Security.Claims.Claim(SMCServiceContext.ApplicationIdKey, "SGA.Administrativo"));

            Thread.CurrentPrincipal = new GenericPrincipal(generic, null);

            IngressanteService.ProcessarInscrito(model.Inscrito, model.SeqEntidadeInstituicao, model.SeqChamada);
        }
    }
}