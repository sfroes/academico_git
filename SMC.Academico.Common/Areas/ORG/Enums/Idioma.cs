using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum Idioma : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Alemão")]
        Alemao = 1,

        [EnumMember]
        Espanhol = 2,

        [EnumMember]
        [Description("Francês")]
        Frances = 3,

        [EnumMember]
        [Description("Inglês")]
        Ingles = 4
    }
}