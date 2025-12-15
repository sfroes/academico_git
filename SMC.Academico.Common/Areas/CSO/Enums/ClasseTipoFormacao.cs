using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CSO.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum ClasseTipoFormacao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Curso = 1,

        [EnumMember]
        Programa = 2
    }
}