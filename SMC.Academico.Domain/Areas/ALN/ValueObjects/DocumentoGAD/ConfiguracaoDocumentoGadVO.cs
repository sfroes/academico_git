using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class ConfiguracaoDocumentoGadVO : ISMCMappable
    {
        public long SeqConfiguracaoGad { get; set; }
        public string TokenSistemaOrigemGAD { get; set; }
        public string NomeDocumento { get; set; }
        public string NomeArquivo { get; set; }
        public byte[] Conteudo { get; set; }
        public List<TagTokenVO> Tags { get; set; }
        public bool ExigeAprovacao { get; set; }
        public bool IndicadorRastreabilidade { get; set; }
        public string EmailNotificarAssinatura { get; set; }
        public bool HabilitaNotificacao { get; set; }
        public DateTime? DataValidade { get; set; }
        public List<ParticipantesTagVO> ParticipantesComTag { get; set; }
    }
}
