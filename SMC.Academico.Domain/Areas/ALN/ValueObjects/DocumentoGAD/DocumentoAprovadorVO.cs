using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class DocumentoAprovadorVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqDocumento { get; set; }

        public long SeqParticipante { get; set; }

        public string Cpf { get; set; }
    }
}
