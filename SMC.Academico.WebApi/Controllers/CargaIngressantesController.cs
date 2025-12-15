using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.WebApi.Models;
using SMC.Framework.Extensions;
using SMC.Framework.Service.Security;
using SMC.Framework.UI.Mvc;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;

namespace SMC.Academico.WebApi.Controllers
{
    public class CargaIngressantesController : SMCApiControllerBase
    {
        #region Services

        private IChamadaService ChamadaService
        {
            get { return Create<IChamadaService>(); }
        }

        #endregion Services

        [HttpPost]
        public void Post(CargaIngressanteSATModel model)
        {
            // Remove o sequencial do nome caso venha...
            model.NomeSolicitante = model.NomeSolicitante?.Split('/').LastOrDefault();

            var identificacao = $"{model.SeqSolicitante}/{model.NomeSolicitante}";
            var generic = new GenericIdentity(identificacao, "manual");
            generic.AddClaim(new System.Security.Claims.Claim("SeqUsuario", model.SeqSolicitante));
            generic.AddClaim(new System.Security.Claims.Claim("Nome", identificacao));
            generic.AddClaim(new System.Security.Claims.Claim(SMCServiceContext.ApplicationIdKey, "SGA.Administrativo"));

            Thread.CurrentPrincipal = new GenericPrincipal(generic, null);

            ChamadaService.CargaIngressantes(model.Transform<CargaIngressanteSATData>());
        }
    }
}