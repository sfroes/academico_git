using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORT.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoParticipacaoOrientacao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Orientador = 1,

        [EnumMember]
        Coorientador = 2,

        [EnumMember]
        Supervisor = 3
    }
}
