using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoMatrizCurricularOferta : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Em ativação")]
        EmAtivacao = 1,

        [EnumMember]
        Ativa = 2,

        [EnumMember]
        [Description("Em extinção")]
        EmExtincao = 3,

        [EnumMember]
        Extinta = 4
    }
}