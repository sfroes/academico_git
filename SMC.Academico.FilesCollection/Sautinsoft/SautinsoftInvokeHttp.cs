using Newtonsoft.Json;
using SMC.Academico.Common.Enums;
using SMC.Framework;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;


namespace SMC.Academico.FilesCollection
{
    public class SautinsoftInvokeHttp
    {
        private readonly HttpClient httpClient;

        public SautinsoftInvokeHttp()
        {
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => 
                {
                    return true;
                }
            };

            this.httpClient = new HttpClient(handler);
        }

        public T Send<T>(object value, MetodoHttp metodoHttp, string rota)
        {
            string fullURL = ConfigurationManager.AppSettings["UrlSautinsoft"] + rota;
            HttpResponseMessage result = null;

            if (metodoHttp == MetodoHttp.GET)
            {
                result = httpClient.GetAsync(fullURL).Result;
            }
            else if (metodoHttp == MetodoHttp.POST)
            {
                var jsonContent = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
                result = httpClient.PostAsync(fullURL, jsonContent).Result;
            }
            else if (metodoHttp == MetodoHttp.DELETE)
            {
                result = httpClient.DeleteAsync(fullURL).Result;
            }
            else
            {
                throw new NotSupportedException($"O método HTTP '{metodoHttp}' não é suportado.");
            }

            var content = result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(content);

        }
    }
}
