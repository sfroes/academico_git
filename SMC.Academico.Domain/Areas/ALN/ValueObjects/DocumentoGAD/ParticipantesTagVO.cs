using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class ParticipantesTagVO : ISMCMappable
    {
        public string NomeParticipante { get; set; }
        public string EmailParticipante { get; set; }
        public string Cpf { get; set; }
        public string TagPosicaoAssinatura { get; set; }
    }
}
