using Newtonsoft.Json;
using SMC.Framework.Mapper;
using System;
using SMC.AssinaturaDigital.Common.Areas.CAD.Enums;
using SMC.AssinaturaDigital.Common.Areas.DOC.Enums;

namespace SMC.Academico.WebApi.Models
{
    public class CallbackDocumentoGadModel : ISMCMappable
    {
        [JsonProperty("seqDocumento")]
        public long? SeqDocumento { get; set; }

        [JsonProperty("seqDocumentoAssinado")]
        public long? SeqDocumentoAssinado { get; set; }

        [JsonProperty("seqDocumentoParticipante")]
        public long? SeqDocumentoParticipante { get; set; }

        [JsonProperty("etapa")]
        public int? Etapa { get; set; }

        [JsonProperty("identificacaoIndividual")]
        public string Cpf { get; set; }

        [JsonProperty("statusDocumento")]
        public StatusDocumento? StatusDocumento { get; set; }

        [JsonProperty("statusDocumentoAssinado")]
        public StatusDocumentoAssinado? StatusDocumentoAssinado { get; set; }

        [JsonProperty("statusDocumentoParticipante")]
        public StatusDocumentoParticipante? StatusDocumentoParticipante { get; set; }

        [JsonProperty("descricaoStatus")]
        public string DescricaoStatus { get; set; }

        [JsonProperty("tipoCallback")]
        public TipoStatus TipoCallback { get; set; }

        [JsonProperty("dataAssinatura")]
        public DateTime? DataAssinatura { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("observacao")]
        public string Observacao { get; set; }

        [JsonProperty("conteudo")]
        public byte[] Conteudo { get; set; }

        public Guid? SeqArquivo { get; set; }
    }
}