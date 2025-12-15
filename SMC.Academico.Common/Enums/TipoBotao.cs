using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoBotao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Acessar = 1,

        [EnumMember]
        Suporte = 2,

        [EnumMember]
        [Description("Manual do produto")]
        ManualProduto = 3
    }
}