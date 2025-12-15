using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CNC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoIdentidade : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        RG = 1,

        [EnumMember]
        Passaporte = 2,

        [EnumMember]
        [Description("Identidade estrangeira")]
        IdentidadeEstrangeira = 3
    }
}