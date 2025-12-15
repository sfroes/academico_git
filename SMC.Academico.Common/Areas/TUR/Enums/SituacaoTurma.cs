using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.TUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoTurma : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Cancelada = 1,

        [EnumMember]
        Ofertada = 2
    }
}