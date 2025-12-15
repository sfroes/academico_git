using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class ParticipanteAtuacaoTokenVO : ISMCMappable
    {
        public string Titulo { get; set; }
        public string TokenAtuacao { get; set; }
        public int Ordem { get; set; }
        public string NomeParticipante { get; set; }
        public string EmailParticipante { get; set; }
        public string Cpf { get; set; }
        public bool CertificadoDigital { get; set; }
        public string TagPosicaoAssinatura { get; set; }
    }
}
