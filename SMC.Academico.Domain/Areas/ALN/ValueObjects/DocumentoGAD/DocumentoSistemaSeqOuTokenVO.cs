using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class DocumentoSistemaSeqOuTokenVO : ISMCMappable
    {
        public string NomeDocumento { get; set; }
        public string Descricao { get; set; }
        public ArquivoVO Arquivo { get; set; }
        public string TokenSistemaOrigem { get; set; }
        public bool IndicadorRastreabilidade { get; set; }
        public bool ExigeAprovacao { get; set; }
        public string EmailNotificarAssinatura { get; set; }
        public bool? HabilitaNotificacao { get; set; }
        public List<CertificadoraTokenVO> Assinaturas { get; set; }
        public List<TagTokenVO> Tags { get; set; }
        public DateTime? DataValidade { get; set; }
        public List<ParticipantesTagVO> ParticipantesComTag { get; set; }
    }
}
