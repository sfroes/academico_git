using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.DCT.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum OrigemColaborador : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Interno = 1,

        [EnumMember]
        Externo = 2,

        [EnumMember]
        [Description("Interno/Externo")]
        InternoExterno = 3
    }
}
