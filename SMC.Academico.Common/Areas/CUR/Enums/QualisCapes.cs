using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum QualisCapes : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        A1 = 1,

        [EnumMember]
        A2 = 2,

        [EnumMember]
        B1 = 3,

        [EnumMember]
        B2 = 4,

        [EnumMember]
        B3 = 5,

        [EnumMember]
        B4 = 6,

        [EnumMember]
        C = 7,

        [EnumMember]
        B5 = 8,

        [EnumMember]
        A3 = 9,

        [EnumMember]
        A4 = 10
    }
}