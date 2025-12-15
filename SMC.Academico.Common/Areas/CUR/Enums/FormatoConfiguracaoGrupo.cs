using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum FormatoConfiguracaoGrupo : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Carga horária")]
        CargaHoraria = 1,

        [EnumMember]
        [Description("Crédito")]
        Credito = 2,

        [EnumMember]
        [Description("Itens")]
        Itens = 3
    }
}