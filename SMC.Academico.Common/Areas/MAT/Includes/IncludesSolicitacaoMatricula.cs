using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.MAT.Includes
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    [Flags]
    public enum IncludesSolicitacaoMatricula
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 1 << 00,

        [EnumMember]
        PessoaAtuacao = 1 << 01,

        [EnumMember]
        JustificativaSolicitacaoServico = 1 << 02,

        [EnumMember]
        Processo = 1 << 03,

        [EnumMember]
        Etapas = 1 << 04,

        [EnumMember]
        Etapas_HistoricosNavegacao = 1 << 05,

        [EnumMember]
        Etapas_HistoricosSituacao = 1 << 06,

        [EnumMember]
        DocumentosRequeridos = 1 << 07,

        [EnumMember]
        AlunosFormacao = 1 << 08,

        [EnumMember]
        AlunosHistoricos = 1 << 09,

        [EnumMember]
        AlunosHistoricosCicloLetivo = 1 << 10,

        [EnumMember]
        AlunosHistoricosSituacao = 1 << 11,

        [EnumMember]
        PlanosEstudo = 1 << 12,

        [EnumMember]
        Itens = 1 << 13,

        [EnumMember]
        ConfiguracaoProcesso = 1 << 14,

        [EnumMember]
        Itens_ConfiguracaoComponente = 1 << 15,

        [EnumMember]
        Itens_ConfiguracaoComponente_ComponenteCurricular = 1 << 16,

        [EnumMember]
        ConfiguracaoProcesso_Processo = 1 << 17,

        [EnumMember]
        ConfiguracaoProcesso_Processo_Servico = 1 << 18,

        [EnumMember]
        Itens_HistoricosSituacao = 1 << 19,

        [EnumMember]
        Itens_HistoricosSituacao_SituacaoItemMatricula = 1 << 20,

        [EnumMember]
        Itens_DivisaoTurma = 1 << 21,

        [EnumMember]
        Itens_DivisaoTurma_Turma = 1 << 22,

        [EnumMember]
        Itens_DivisaoTurma_Turma_ConfiguracoesComponente = 1 << 23,
    }
}