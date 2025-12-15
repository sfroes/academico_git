using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoOrientacaoTurma : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Não gera orientação")]
        NaoGeraOrientacao = 1,

        [EnumMember]
        [Description("Gera nova orientação")]
        GeraNovaOrientacao = 2,

        [EnumMember]
        [Description("Aproveita orientação existente")]
        AproveitaOrientacaoExistente = 3
    }
}