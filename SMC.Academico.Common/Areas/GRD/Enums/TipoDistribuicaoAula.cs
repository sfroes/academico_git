using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.GRD.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoDistribuicaoAula : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Semanal = 1,

        [EnumMember]
        Quinzenal = 2,

        [EnumMember]
        Livre = 3,

        [EnumMember]
        Concentrada = 4
    }
}