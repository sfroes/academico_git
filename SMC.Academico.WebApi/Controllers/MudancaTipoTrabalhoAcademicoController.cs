using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Academico.WebApi.Models;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using System.Web.Mvc;

namespace SMC.Academico.WebApi.Controllers
{
    public class MudancaTipoTrabalhoAcademicoController : SMCApiControllerBase
    {
        #region [ Services ]
        private IPublicacaoBdpService PublicacaoBdpService => Create<IPublicacaoBdpService>();

        #endregion [ Services ]

        [HttpPost]
        [Route("api/MudancaTipoTrabalhoAcademico/NotificarBibliotecaTrabalhoComMudanca")]
        public void NotificarBibliotecaTrabalhoComMudanca(MudancaTipoTrabalhoAcademicoSATModel model)
        {
            PublicacaoBdpService.NotificarBibliotecaTrabalhoComMudanca(model.Transform<MudancaTipoTrabalhoAcademicoSATData>());
        }
    }
}