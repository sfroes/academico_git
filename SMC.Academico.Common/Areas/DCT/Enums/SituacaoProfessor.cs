using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.DCT.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoProfessor : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Normal = 1,

        [EnumMember]
        Afastado = 2,

        [EnumMember]
        Demitido = 3
    }
}
