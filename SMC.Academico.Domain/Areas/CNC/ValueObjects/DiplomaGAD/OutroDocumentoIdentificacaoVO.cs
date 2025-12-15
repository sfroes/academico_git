using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class OutroDocumentoIdentificacaoVO : ISMCMappable
    {
        public string TipoDocumento { get; set; }
        public string Identificador { get; set; }
    }
}
