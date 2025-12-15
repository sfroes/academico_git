using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CSO.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum OrigemSAL : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        SAL = 1,

        [EnumMember]
        SAL_AR = 2,

        [EnumMember]
        SAL_BT = 3,

        [EnumMember]
        SAL_CEFAP = 4,

        [EnumMember]
        SAL_COP = 5,

        [EnumMember]
        SAL_ED = 6,

        [EnumMember]
        SAL_EPC = 7,

        [EnumMember]
        SAL_ICBS = 8,

        [EnumMember]
        SAL_IEC = 9,

        [EnumMember]
        SAL_INF = 10,

        [EnumMember]
        SAL_NPJ = 11,

        [EnumMember]
        SAL_OT = 12,

        [EnumMember]
        SAL_SG = 13
    }
}