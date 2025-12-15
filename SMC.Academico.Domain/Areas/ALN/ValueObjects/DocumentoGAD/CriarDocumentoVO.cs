using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class CriarDocumentoVO : ISMCMappable
    {
        public long? Seq { get; set; }
        public string TokenSistemaOrigem { get; set; }
        public string TokenCertificadora { get; set; }
        public string Nome { get; set; }
        public string Observacao { get; set; }
        public bool? NaoExcluido { get; set; }
        public string UsuarioInclusao { get; set; }
        public ArquivoVO Arquivo { get; set; }
        public DocumentoSistemaTokenVO NovoDocumento { get; set; }
    }
}
