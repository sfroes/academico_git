using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoOrigemMaterial : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Divisão de turma")]
        DivisaoTurma = 1,

        [EnumMember]
        Entidade = 2,

        [EnumMember]
        [Description("Orientação")]
        Orientacao = 3
    }
}