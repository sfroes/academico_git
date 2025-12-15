using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesGrupoEscalonamentoItem
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0 << 0,

        [EnumMember]
        Parcelas = 1 << 1,

        [EnumMember]
        Escalonamento = 1 << 2,

        [EnumMember]
        GrupoEscalonamento = 1 << 3
    }
}