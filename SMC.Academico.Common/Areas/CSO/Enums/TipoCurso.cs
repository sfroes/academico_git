using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CSO.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoCurso : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Normal = 1,

        [EnumMember]
        ABI = 2,

        [EnumMember]
        Emergencial = 3
    }
}