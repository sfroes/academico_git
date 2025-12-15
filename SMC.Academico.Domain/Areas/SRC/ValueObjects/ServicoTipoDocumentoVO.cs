using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ServicoTipoDocumentoVO : ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqServico { get; set; }
        public long SeqTipoDocumento { get; set; }
        public string DescricaoXSD { get; set; }
    }
}