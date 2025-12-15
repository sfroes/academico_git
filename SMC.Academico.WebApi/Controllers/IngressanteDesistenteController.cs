using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.WebApi.Models;
using SMC.Framework.Extensions;
using SMC.Framework.Service.Security;
using SMC.Framework.UI.Mvc;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;

namespace SMC.Academico.WebApi.Controllers
{
    public class IngressanteDesistenteController : SMCApiControllerBase
    {
        #region Services
        private IIngressanteDesistenteService IntegranteDesistenteService
        {
            get { return Create<IIngressanteDesistenteService>(); }
        }

        #endregion

        [HttpPost]
        public void Post(IngressanteDesistenteSATModel model)
        {
            var identificacao = @"JOB\Ingressante Desistente";
            var generic = new GenericIdentity(identificacao, "manual");
            generic.AddClaim(new System.Security.Claims.Claim("SeqUsuario", "0"));
            generic.AddClaim(new System.Security.Claims.Claim("Nome", identificacao));
            generic.AddClaim(new System.Security.Claims.Claim(SMCServiceContext.ApplicationIdKey, "SGA.Administrativo"));

            Thread.CurrentPrincipal = new GenericPrincipal(generic, null);

            IntegranteDesistenteService.BuscarIngressantesDesistentes(model.Transform<IngressanteDesistenteSATData>());
        }
    }
}
