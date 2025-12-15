using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesEscalonamento
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 1 << 0,

        [EnumMember]
        GruposEscalonamento = 1 << 1,

        [EnumMember]
        GruposEscalonamento_Parcelas = 1 << 2,

        [EnumMember]
        ProcessoEtapa = 1 << 3,

        [EnumMember]
        ProcessoEtapa_Processo = 1 << 4,

        [EnumMember]
        ProcessoEtapa_Escalonamentos = 1 << 5,

        [EnumMember]
        GruposEscalonamento_GrupoEscalonamento = 1 << 6,

        [EnumMember]
        GruposEscalonamento_GrupoEscalonamento_Itens = 1 << 7,

        [EnumMember]
        GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico = 1 << 8,

        [EnumMember]
        GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_Etapas = 1 << 9,

        [EnumMember]
        GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_Etapas_ConfiguracaoEtapa = 1 << 10,

        [EnumMember]
        GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_Etapas_HistoricosSituacao = 1 << 11,

        [EnumMember]
        GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_PessoaAtuacao = 1 << 12,

        [EnumMember]
        GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_PessoaAtuacao_DadosPessoais = 1 << 13,

        [EnumMember]
        GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_PessoaAtuacao_EnderecosEletronicos = 1 << 14,

        [EnumMember]
        GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_ConfiguracaoProcesso = 1 << 15,

        [EnumMember]
        GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_ConfiguracaoProcesso_Processo = 1 << 16,

        [EnumMember]
        GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_Etapas_ConfiguracaoEtapa_ProcessoEtapa = 1 << 17,

        [EnumMember]
        GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_Etapas_HistoricosNavegacao = 1 << 18,

        [EnumMember]
        GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_Etapas_ConfiguracaoEtapa_ProcessoEtapa_Escalonamentos = 1 << 19,

        [EnumMember]
        GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_Etapas_ConfiguracaoEtapa_ProcessoEtapa_Escalonamentos_GruposEscalonamento = 1 << 20,
    }
}