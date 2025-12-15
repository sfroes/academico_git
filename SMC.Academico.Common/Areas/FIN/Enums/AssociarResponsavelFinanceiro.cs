using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.FIN.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum AssociarResponsavelFinanceiro : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Exige = 1,

        [EnumMember]
        [Description("Não permite")]
        NaoPermite = 2,

        [EnumMember]
        Permite = 3
    }
}
