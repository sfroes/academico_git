using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.WebApi.Models;
using SMC.Framework.Extensions;
using SMC.Framework.Service.Security;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;

namespace SMC.Academico.WebApi.Controllers
{
    public class ProcessamentoPlanoEstudoController : SMCApiControllerBase
    {
        #region [ Services ]

        private ISolicitacaoServicoService SolicitacaoServicoService
        {
            get { return Create<ISolicitacaoServicoService>(); }
        }

        #endregion [ Services ]

        [HttpPost]
        public void Post(ProcessamentoPlanoEstudoSATModel model)
        {
            var identificacao = @"JOB\Processamento Plano Estudo";
            var generic = new GenericIdentity(identificacao, "manual");
            generic.AddClaim(new System.Security.Claims.Claim("SeqUsuario", "0"));
            generic.AddClaim(new System.Security.Claims.Claim("Nome", identificacao));
            generic.AddClaim(new System.Security.Claims.Claim(SMCServiceContext.ApplicationIdKey, "SGA.Administrativo"));

            Thread.CurrentPrincipal = new GenericPrincipal(generic, null);
                        
            SolicitacaoServicoService.ProcessamentoPlanoEstudoServicoMatricula(model.Transform<ProcessamentoPlanoEstudoSATData>());
        }
    }
}