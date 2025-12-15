using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CSO.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoOferta : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Confirmada = 1,

        [EnumMember]
        Cancelada = 2,

        [EnumMember]
        [Description("Não confirmada")]
        NaoConfirmada = 3
    }
}