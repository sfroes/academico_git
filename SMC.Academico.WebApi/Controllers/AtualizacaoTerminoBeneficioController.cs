using SMC.Academico.Common.Helper;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Framework.Jobs;
using SMC.Framework.UI.Mvc;
using System.Web.Http;

namespace SMC.Academico.WebApi.Controllers
{
    public class AtualizacaoTerminoBeneficioController : SMCApiControllerBase
    {
        #region Services

        private IPessoaAtuacaoBeneficio PessoaAtuacaoBeneficio => Create<IPessoaAtuacaoBeneficio>();

        #endregion Services

        [HttpPost]
        public void Post(SMCWebJobFilterModel model)
        {
            SecurityHelper.SetupSatUser("Atualizacao beneficio");

            PessoaAtuacaoBeneficio.AtualizarTerminoBeneficio(model);
        }
    }
}