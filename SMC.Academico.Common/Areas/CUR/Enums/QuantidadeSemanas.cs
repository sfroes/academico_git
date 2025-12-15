using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum QuantidadeSemanas : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("15")]
        Quinze = 15,

        [EnumMember]
        [Description("16")]
        Dezesseis = 16,

        [EnumMember]
        [Description("17")]
        Dezessete = 17,

        [EnumMember]
        [Description("18")]
        Dezoito = 18,

        [EnumMember]
        [Description("20")]
        Vinte = 20,

        
    }
}