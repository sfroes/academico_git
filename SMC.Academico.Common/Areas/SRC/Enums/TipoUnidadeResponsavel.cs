using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoUnidadeResponsavel : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Entidade Responsável")]
        EntidadeResponsavel = 1,

        [EnumMember]
        [Description("Entidade Compartilhada")]
        EntidadeCompartilhada = 2
    }
}