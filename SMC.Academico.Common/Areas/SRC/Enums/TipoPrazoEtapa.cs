using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoPrazoEtapa : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Dias úteis")]
        DiasUteis = 1,

        [EnumMember]
        [Description("Dias corridos")]
        DiasCorridos = 2,

        [EnumMember]
        Escalonamento = 3,

        [EnumMember]
        [Description("Período de vigência")]
        PeriodoVigencia = 4,

        [EnumMember]
        [Description("Sem prazo específico")]
        SemPrazoEspecifico = 5
    }
}