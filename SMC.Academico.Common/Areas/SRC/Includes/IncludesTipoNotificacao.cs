using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Includes
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesTipoNotificacao
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0 << 0,

        [EnumMember]
        AtributosAgendamento = 1 << 1,
    }
}