using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.PES.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoBloqueio : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Bloqueado = 1,

        [EnumMember]
        Desbloqueado = 2
    }
}
