using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class GrupoDocumentoRequeridoItemListarVO : ISMCMappable
    {
        public long SeqTipoDocumento { get; set; }

        public string DescricaoTipoDocumento { get; set; }
    }
}
