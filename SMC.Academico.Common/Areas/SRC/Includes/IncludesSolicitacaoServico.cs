using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Includes
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesSolicitacaoServico
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 1 << 00,

        [EnumMember]
        PessoaAtuacao = 1 << 01,

        [EnumMember]
        PessoaAtuacao_Pessoa = 1 << 02,

        [EnumMember]
        JustificativaSolicitacaoServico = 1 << 03,

        [EnumMember]
        Etapas = 1 << 04,

        [EnumMember]
        Etapas_HistoricosNavegacao = 1 << 05,

        [EnumMember]
        Etapas_HistoricosSituacao = 1 << 06,

        [EnumMember]
        DocumentosRequeridos = 1 << 07,

        [EnumMember]
        ConfiguracaoProcesso = 1 << 08,

        [EnumMember]
        GrupoEscalonamento = 1 << 09,

        [EnumMember]
        Etapas_ConfiguracaoEtapa = 1 << 10,

        [EnumMember]
        Etapas_ConfiguracaoEtapa_ConfiguracoesBloqueio = 1 << 11,

        [EnumMember]
        PessoaAtuacao_Pessoa_DadosPessoais = 1 << 12,

        [EnumMember]
        GrupoEscalonamento_Itens = 1 << 13,

        [EnumMember]
        GrupoEscalonamento_Itens_Parcelas = 1 << 14,

        [EnumMember]
        ConfiguracaoProcesso_Processo = 1 << 15,

        [EnumMember]
        ConfiguracaoProcesso_Processo_Servico = 1 << 16,

        [EnumMember]
        DocumentosRequeridos_DocumentoRequerido_GruposDocumentoRequerido = 1 << 17,

        [EnumMember]
        Etapas_ConfiguracaoEtapa_ProcessoEtapa = 1 << 18,

        [EnumMember]
        PessoaAtuacao_EnderecosEletronicos = 1 << 19,
    }
}