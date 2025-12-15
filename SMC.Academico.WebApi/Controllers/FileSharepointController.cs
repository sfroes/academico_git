using Newtonsoft.Json;
using RestSharp;
using SMC.Academico.WebApi.Models;
using SMC.Framework.UI.Mvc;
using System.Web.Http;

namespace SMC.Academico.WebApi.Controllers
{
    public class FileSharepointController : SMCApiControllerBase
    {
        [HttpPost]
        public string UploadFile(FileSharepointModel model)
        {
           //model.arquivo.pasta = "Home";
            var json = JsonConvert.SerializeObject(model);

            var client = new RestClient("https://web-homologacao.pucminas.br/Intra.WebApi/api/ged/POCUploadFile/");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "ALT7SESSION=ATLgEckBAgoz8StMeMiMRw$$");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var retorno = JsonConvert.DeserializeObject<RetornoSharepointModel>(response.Content);
            return retorno.LinkDocumento;           
        }

    }
}