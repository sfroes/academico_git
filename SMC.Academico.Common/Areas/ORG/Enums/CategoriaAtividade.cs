using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum CategoriaAtividade : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Em ativação")]
        EmAtivacao = 1,

        [EnumMember]
        Ativa = 2,

        [EnumMember]
        [Description("Em desativação")]
        EmDesativacao = 3,

        [EnumMember]
        Inativa = 4
    }
}