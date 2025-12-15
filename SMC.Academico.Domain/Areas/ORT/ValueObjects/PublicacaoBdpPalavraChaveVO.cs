using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class PublicacaoBdpPalavraChaveVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string PalavraChave { get; set; }

    }
}