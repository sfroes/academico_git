using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum ModeloRelatorio : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Autorização publicação no BDP (Texto completo)")]
        AutorizacaoPublicacaoBDP_Completo = 1,

        [EnumMember]
        [Description("Comprovante de entrega de trabalho acadêmico")]
        ComprovanteEntregaTrabalhoAcademico = 2,

        [EnumMember]
        [Description("Comprovante de cancelamento/trancamento/reabertura")]
        ComprovanteCancelamentoTrancamentoReabertura = 3,

        [EnumMember]
        [Description("Comprovante de solicitação de transferência interna")]
        ComprovanteSolicitacaoTransferenciaInterna = 4,

        [EnumMember]
        [Description("Comprovante de solicitação de alteração de formação específica")]
        ComprovanteSolicitacaoAlteracaoFormacaoEspecifica = 5,

        [EnumMember]
        [Description("Comprovante solicitação mobilidade")]
        ComprovanteSolicitacaoMobilidade = 6,

        [EnumMember]
        [Description("Protocolo de entrega de documento de conclusão")]
        ProtocoloEntregaDocumentoConclusao = 7,

        [EnumMember]
        [Description("Modelo termo de adesão - Ingressante")]
        ModeloTermoAdesaoIngressante = 8,

        [EnumMember]
        [Description("Modelo termo de adesão - Renovação")]
        ModeloTermoAdesaoRenovacao = 9,

        [EnumMember]
        [Description("Modelo termo de bolsa da reitoria")]
        ModeloTermoBolsaReitoria = 10,

        [EnumMember]
        [Description("Modelo termo de bolsa convênio")]
        ModeloTermoBolsaConvênio = 11,

        [EnumMember]
        [Description("Certificado de pós-doc")]
        CertificadoPosDoc = 12,

        [EnumMember]
        [Description("Ata de defesa de trabalho acadêmico")]
        AtaDefesaTrabalhoAcademico = 13,

        [EnumMember]
        [Description("Autorização publicação no BDP (Texto parcial e completo)")]
        AutorizacaoPublicacaoBDP_ParcialECompleto = 14,

        [EnumMember]
        [Description("Ficha catalográfica")]
        FichaCatalografica = 15
    }
}