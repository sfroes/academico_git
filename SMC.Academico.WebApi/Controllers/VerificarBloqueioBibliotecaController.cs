using SMC.Academico.Common.Helper;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.WebApi.Models;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using System.Web.Mvc;

namespace SMC.Academico.WebApi.Controllers
{
    public class VerificarBloqueioBibliotecaController : SMCApiControllerBase
    {
        #region Servicos

        private IPessoaAtuacaoBloqueioService PessoaAtuacaoBloqueioService => this.Create<IPessoaAtuacaoBloqueioService>();

        #endregion Servicos

        [HttpPost]
        public void Post(VerificarBloqueioBibliotecaSATModel model)
        {
            SecurityHelper.SetupSatUser("Verificar Bloqueio Biblioteca");

            PessoaAtuacaoBloqueioService.VerificarBloqueioBibliotecaAutomatica(model.Transform<VerificarBloqueioBibliotecaSATData>());
        }
    }
}