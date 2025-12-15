using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.GRD.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoPulaFeriado : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Não Pula")]
        NaoPula = 1,

        [EnumMember]
        [Description("Pula Conjugado")]
        PulaConjugado = 2,

        [EnumMember]
        [Description("Pula Simples")]
        PulaSimples = 3
    }
}