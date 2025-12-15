using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoOrgaoRegulador : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        CAPES = 1,

        [EnumMember]
        CNPQ = 2,

        [EnumMember]
        MEC = 3
    }
}
