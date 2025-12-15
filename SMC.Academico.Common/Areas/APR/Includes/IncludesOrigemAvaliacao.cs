using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesOrigemAvaliacao
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        AplicacoesAvaliacao = 1 << 0,

        [EnumMember]
        DivisoesComponente = 1 << 1,

        [EnumMember]
        DivisoesComponente_DivisaoComponente = 1 << 2,

        [EnumMember]
        DivisoesComponente_DivisaoComponente_ConfiguracaoComponente = 1 << 3,

        [EnumMember]
        EscalaApuracao = 1 << 4,

        [EnumMember]
        CriterioAprovacao = 1 << 5,

        [EnumMember]
        CriterioAprovacao_EscalaApuracao = 1 << 6,

        [EnumMember]
        CriterioAprovacao_EscalaApuracao_Itens = 1 << 7
    }
}