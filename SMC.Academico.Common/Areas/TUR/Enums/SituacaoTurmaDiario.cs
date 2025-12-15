using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.TUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoTurmaDiario : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Aberto = 1,

        [EnumMember]
        Fechado = 2
    }
}
