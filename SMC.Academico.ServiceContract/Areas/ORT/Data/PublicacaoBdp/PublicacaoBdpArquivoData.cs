using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class PublicacaoBdpArquivoData : ISMCMappable
    {
        public long Seq { get; set; }

        public TipoAutorizacao TipoAutorizacao { get; set; }

        public string NomeArquivo { get; set; }

        public long? TamanhoArquivo { get; set; }

        public string UrlArquivo { get; set; }

        public SMCUploadFile Arquivo { get; set; }
    }
}
