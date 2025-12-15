using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CAM.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum AbrangenciaEvento : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Geral = 1,

        [EnumMember]
        [Description("Por nível de ensino")]
        PorNivelEnsino = 2
    }
}