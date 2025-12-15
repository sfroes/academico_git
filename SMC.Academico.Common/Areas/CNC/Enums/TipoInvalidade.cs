using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CNC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoInvalidade : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Permanente = 1,

        [EnumMember]
        [Description("Temporário")]
        Temporario = 2
    }
}