using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesAlunoHistorico : int
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 1 << 00,

        [EnumMember]
        Aluno = 1 << 01,

        [EnumMember]
        HistoricosCicloLetivo = 1 << 02,

        [EnumMember]
        HistoricosCicloLetivo_PlanosEstudo = 1 << 03,

        [EnumMember]
        HistoricosCicloLetivo_PlanosEstudo_Itens = 1 << 04,

        [EnumMember]
        HistoricosCicloLetivo_PlanosEstudo_Itens_ConfiguracaoComponente = 1 << 05,

        [EnumMember]
        HistoricosCicloLetivo_PlanosEstudo_Itens_Orientacao = 1 << 06,

        [EnumMember]
        HistoricosCicloLetivo_PlanosEstudo_Itens_Orientacao_OrientacoesColaborador = 1 << 07,

        [EnumMember]
        Formacoes_ApuracoesFormacao = 1 << 08,

        [EnumMember]
        PrevisoesConclusao = 1 << 09,
    }
}