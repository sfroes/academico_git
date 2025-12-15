using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.FIN.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum FormaDeducao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Percentual de bolsa")]
        PercentualBolsa = 1,

        [EnumMember]
        [Description("Saldo final da parcela")]
        SaldoFinalParcela = 2,

        [EnumMember]
        [Description("Valor de bolsa")]
        ValorBolsa = 3

    }
}
