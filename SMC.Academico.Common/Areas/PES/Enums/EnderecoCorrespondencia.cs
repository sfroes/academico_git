using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.PES.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum EnderecoCorrespondencia : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Acadêmica")]
        Academica = 1,

        [EnumMember]
        [Description("Acadêmica e Financeira")]
        AcademicaFinanceira = 2,

        [EnumMember]
        [Description("Financeira")]
        Financeira = 3,

        [EnumMember]
        [Description("Não")]
        Nao = 4,

        [EnumMember]
        [Description("Sim")]
        Sim = 5
    }
}