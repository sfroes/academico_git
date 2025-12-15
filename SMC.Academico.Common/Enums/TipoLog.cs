using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoLog : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Error = 1,

        [EnumMember]
        Info = 2,

        [EnumMember]
        Success = 4
    }
}
