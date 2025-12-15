using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoApuracaoFrequencia : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Executada = 1,

        [EnumMember]
        [Description("Não apurada")]
        NaoApurada = 2,

        [EnumMember]
        [Description("Não executada")]
        NaoExecutada = 3
    }
}