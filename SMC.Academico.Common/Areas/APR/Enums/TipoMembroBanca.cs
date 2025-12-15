using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoMembroBanca : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Coorientador = 1,

        [EnumMember]
        [Description("Membro Externo")]
        MembroExterno = 2,

        [EnumMember]
        [Description("Membro Interno")]
        MembroInterno = 3,

        [EnumMember]
        Orientador = 4,

        [EnumMember]
        Suplente = 5
    }
}