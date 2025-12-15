using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoEtapa : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Aguardando liberação")]
        AguardandoLiberacao = 1,

        [EnumMember]
        Liberada = 2,

        [EnumMember]
        [Description("Em manutenção")]
        EmManutencao = 3,

        [EnumMember]
        Encerrada = 4
    }
}