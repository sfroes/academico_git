using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.WebApi.Models;
using SMC.Framework.Extensions;
using SMC.Framework.Service.Security;
using SMC.Framework.UI.Mvc;
using System.Security.Principal;
using System.Threading;
using System.Web.Mvc;

namespace SMC.Academico.WebApi.Controllers
{
    public class RematriculaController : SMCApiControllerBase
    {
        #region Services
        private ISolicitacaoMatriculaService SolicitacaoMatriculaService
        {
            get { return Create<ISolicitacaoMatriculaService>(); }
        }
        #endregion


        [HttpPost]
        public void Post(RematriculaJOBViewModel model)
        {
            var generic = new GenericIdentity(model.IdUsuario, "manual");
            generic.AddClaim(new System.Security.Claims.Claim("SeqUsuario", "0"));
            generic.AddClaim(new System.Security.Claims.Claim("Nome", model.IdUsuario));
            generic.AddClaim(new System.Security.Claims.Claim(SMCServiceContext.ApplicationIdKey, "SGA.Administrativo")); 

            Thread.CurrentPrincipal = new GenericPrincipal(generic, null);

            SolicitacaoMatriculaService.CriarSolicitacoesRematricula(model.Transform<RematriculaJOBData>());
        }
    }
}