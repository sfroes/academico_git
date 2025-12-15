using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoMaterial : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Arquivo = 1,

        [EnumMember]
        Link = 2,

        [EnumMember]
        Pasta = 3
    }
}