using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum NaturezaArtigo : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Completo = 1,

        [EnumMember]
        Resumo = 2,

        [EnumMember]
        [Description("Resumo expandido")]
        ResumoExpandido = 3
    }
}