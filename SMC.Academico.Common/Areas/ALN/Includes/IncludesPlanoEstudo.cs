using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesPlanoEstudo : int
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 1 << 00,

        [EnumMember]
        Itens = 1 << 01,

        [EnumMember]
        Itens_DivisaoTurma = 1 << 02,

        [EnumMember]
        Itens_ConfiguracaoComponente = 1 << 03,
    }
}
