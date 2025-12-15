using SMC.Academico.Common.Constants;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    [Flags]
    public enum SituacaoBanca : short
    {
        [EnumMember]
        [Description("Canceladas")]
        Canceladas = 1,

        [EnumMember]
        [Description("Confirmadas")]
        Confirmadas = 2,

        [EnumMember]
        [Description("Sem ata anexada")]
        SemAtaAnexada = 3,
    }
}