using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class DocumentoSistemaTokenVO : ISMCMappable
    {
        public string NomeDocumento { get; set; }
        public string Descricao { get; set; }
        public ArquivoVO Arquivo { get; set; }
        public string TokenSistemaOrigem { get; set; }
        public bool IndicadorRastreabilidade { get; set; }
        public bool ExigeAprovacao { get; set; }
        public List<CertificadoraTokenVO> Assinaturas { get; set; }
        public List<TagTokenVO> Tags { get; set; }
        public List<DocumentoAprovadorVO> Aprovadores { get; set; }
        public List<DocumentoObservadorVO> Observadores { get; set; }
        public string UsuarioInclusao { get; set; }
        public bool? HabilitaNotificacao { get; set; }
        public bool DetectaTabela { get; set; }
        public DateTime? DataValidade { get; set; }
    }
}
