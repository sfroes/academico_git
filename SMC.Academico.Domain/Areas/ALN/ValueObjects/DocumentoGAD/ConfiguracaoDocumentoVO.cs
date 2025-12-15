using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class ConfiguracaoDocumentoVO : ISMCMappable
    {
        public long? Seq { get; set; }
        public string Token { get; set; }
        public string Nome { get; set; }
        public string UsuarioInclusao { get; set; }
        public DocumentoSistemaSeqOuTokenVO NovoDocumento { get; set; }
    }
}
