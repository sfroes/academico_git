using SMC.Academico.Common.Helper;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.WebApi.Models;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using System.Web.Mvc;

namespace SMC.Academico.WebApi.Controllers
{
    public class CurriculoDigitalController : SMCApiControllerBase
    {

        private ICurriculoService CurriculoService => this.Create<ICurriculoService>();

        [HttpPost]
        public void Post(EmissaoCurriculoDigitalSATModel model)
        {
            SecurityHelper.SetupSatUser("Emissão Currículo");
            CurriculoService.ConstruirCurriculoDigital(model.Transform<EmissaoCurriculoDigitalSATData>());
        }
    }
}