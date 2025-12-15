using SMC.Academico.Common.Helper;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.WebApi.Models;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using System.Web.Http;

namespace SMC.Academico.WebApi.Controllers
{
    public class RealizarEnvioNotificacaoPessoaAtuacaoController : SMCApiControllerBase
    {
        #region Services

        private IEnvioNotificacaoService EnvioNotificacaoService => Create<IEnvioNotificacaoService>();

        #endregion Services

        [HttpPost]
        public void Post(RealizarEnvioNotificacaoSATModel model)
        {
            EnvioNotificacaoService.RealizarEnvioNotificacaoJob(model.Transform<RealizarEnvioNotificacaoSATData>());
        }
    }
}