using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoAvaliacao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Banca")]
        Banca = 1,

        [EnumMember]
        [Description("Prova")]
        Prova = 2,

        [EnumMember]
        [Description("Reavaliação")]
        Reavaliacao = 3,

        [EnumMember]
        [Description("Trabalho")]
        Trabalho = 4
    }
}