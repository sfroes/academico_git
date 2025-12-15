using Newtonsoft.Json;
using SMC.Framework.Mapper;

namespace SMC.Academico.Common.Areas.CNC.Models
{
    public class MensagemHttp : ISMCMappable
    {
        [JsonProperty("codigo")]
        public string Codigo { get; set; }

        [JsonProperty("erro")]
        public ErroMensagemHttp erro { get; set; }

        [JsonProperty("responseStatus")]
        public string ResponseStatus { get; set; }

        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        public string UsuarioInclusao { get; set; }
    }
}
