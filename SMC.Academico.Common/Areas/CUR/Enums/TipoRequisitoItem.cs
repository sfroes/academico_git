using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoRequisitoItem : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Componente curricular")]
        ComponenteCurricular = 1,

        [EnumMember]
        [Description("Divisão da matriz")]
        DivisaoMatriz = 2,

        [EnumMember]
        [Description("Outros requisitos")]
        OutrosRequisitos = 3,

        [EnumMember]
        [Description("Grupo Curricular")]
        GrupoCurricular = 4
    }
}