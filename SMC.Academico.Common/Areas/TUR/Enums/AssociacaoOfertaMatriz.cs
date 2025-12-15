using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.TUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum AssociacaoOfertaMatriz : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Exige apenas uma")]
        ExigeApenasUma = 1,

        [EnumMember]
        [Description("Exige mais de uma")]
        ExigeMaisDeUma = 2,

        [EnumMember]
        [Description("Não permite")]
        NaoPermite = 3
    }
}