using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CNC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoAlunoFormacao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("À Concluir")]
        AConcluir = 1,

        [EnumMember]
        [Description("Provável concluinte")]
        ProvavelConcluinte = 2,

        [EnumMember]
        Concluinte = 3,

        [EnumMember]
        Formado = 4,

        [EnumMember]
        [Description("Não optado")]
        NaoOptado = 5,

        [EnumMember]
        Cancelado = 6
    }
}