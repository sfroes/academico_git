using Newtonsoft.Json;
using SMC.Framework.Mapper;

namespace SMC.Academico.Common.Areas.CNC.Models
{
    public class ErroMensagemHttp : ISMCMappable
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("msg")]
        public string msg { get; set; }

        [JsonProperty("link")]
        public string link { get; set; }
    }
}
