using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CAM.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoChamada : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Chamada de excedentes")]
        Excedentes = 1,

        [EnumMember]
        [Description("Chamada regular")]
        Regular = 2,

        [EnumMember]
        [Description("Chamada externa")]
        Externa = 3
    }
}