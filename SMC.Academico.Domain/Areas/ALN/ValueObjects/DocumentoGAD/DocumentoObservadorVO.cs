using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class DocumentoObservadorVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqDocumento { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public bool PermiteNotificacao { get; set; }

        public string Justificativa { get; set; }

        public string UsuarioInclusao { get; set; }
    }
}
