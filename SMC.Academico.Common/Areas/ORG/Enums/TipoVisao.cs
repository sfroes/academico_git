using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoVisao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Visão organizacional")]
        VisaoOrganizacional = 1,

        [EnumMember]
        [Description("Visão de unidade gestora")]
        VisaoUnidade = 2,

        [EnumMember]
        [Description("Visão de localidades")]
        VisaoLocalidades = 3,

        [EnumMember]
        [Description("Visão de polos virtuais")]
        VisaoPolosVirtuais = 4

    }
}
