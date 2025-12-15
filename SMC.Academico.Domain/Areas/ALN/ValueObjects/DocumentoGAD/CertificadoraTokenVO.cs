using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class CertificadoraTokenVO : ISMCMappable
    {
        public string TokenCertificadora { get; set; }
        public List<ParticipanteAtuacaoTokenVO> Participantes { get; set; }
        public short Ordem { get; set; }
    }
}
