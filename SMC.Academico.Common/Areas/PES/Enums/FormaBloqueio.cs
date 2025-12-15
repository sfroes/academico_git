using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.PES.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum FormaBloqueio : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Integração")]
        Integracao = 1,

        [EnumMember]
        Manual = 2,

        [EnumMember]
        Ambos = 3,
    }
}
