using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.DCT.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoColaborador : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Ativo = 1,

        [EnumMember]
        Inativo = 2
    }
}