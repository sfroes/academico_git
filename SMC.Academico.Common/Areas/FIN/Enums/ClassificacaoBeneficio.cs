using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.FIN.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum ClassificacaoBeneficio : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Bolsa = 2,

        //[EnumMember]
        //Desconto = 6

        [EnumMember]
        [Description("Convênio")]
        Convenio = 99
    }
}
