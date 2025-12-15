using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CNC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum ClasseSituacaoDocumento : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        [Description("Inválido")]
        Invalido = 1,

        [EnumMember]
        [Description("Emissão em andamento")]
        EmissaoEmAndamento = 2,

        [EnumMember]
        [Description("Válido")]
        Valido = 3
    }
}