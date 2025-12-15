using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IntegracaoFinanceira : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Não se aplica")]
        NaoSeAplica = 1,

        [EnumMember]
        [Description("Cobrança de taxa")]
        CobrancaDeTaxa = 2,

        [EnumMember]
        [Description("Transação financeira")]
        TransacaoFinanceira = 3
    }
}