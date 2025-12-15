using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORT.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoAutorizacao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Parcial (somente resumo)")]
        Parcial = 1,

        [EnumMember]
        [Description("Texto Completo")]
        TextoCompleto = 2
    }
}