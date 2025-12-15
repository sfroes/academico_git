using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.FIN.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoChancelaBeneficio : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Aguardando chancela")]
        AguardandoChancela = 1,

        //[EnumMember]
        //Cancelado = 2,

        [EnumMember]
        Deferido = 3,

        [EnumMember]
        Indeferido = 4,

        [EnumMember]
        [Description("Excluído")]
        Excluido = 5
    }
}