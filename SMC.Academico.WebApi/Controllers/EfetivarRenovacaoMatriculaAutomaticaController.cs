using SMC.Academico.Common.Helper;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.WebApi.Models;
using SMC.Framework.Extensions;
using SMC.Framework.Jobs;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;
using System.Web.Http;

namespace SMC.Academico.WebApi.Controllers
{
    public class EfetivarRenovacaoMatriculaAutomaticaController : SMCApiControllerBase
    {
        #region Services

        private ISolicitacaoMatriculaService SolicitacaoMatriculaService => Create<ISolicitacaoMatriculaService>();

        #endregion Services

        [HttpPost]
        public void Post(EfetivarRenovacaoMatriculaAutomaticaSATModel model)
        {
            SecurityHelper.SetupSatUser("Efetivaçao de matricula automatica");

            SolicitacaoMatriculaService.EfetivarRenovacaoMatriculaAutomatica(model.Transform<EfetivarRenovacaoMatriculaAutomaticaSATData>());
        }
    }
}