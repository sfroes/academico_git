using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DocumentacaoComprobatoriaTipoDocumentoVO : ISMCMappable
    {
        public long SeqTipoDocumento { get; set; }

        public string DescricaoTipoDocumento { get; set; }

        public string TokenTipoDocumento { get; set; }

        public string ValorEnumLacuna { get; set; }
    }
}