using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesEscalaApuracao
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        CriteriosAprovacao = 1,

        [EnumMember]
        Itens = 2
    }
}