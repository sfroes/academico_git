using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.DCT.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoAtividadeColaborador : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Aula = 1,

        [EnumMember]
        Pesquisa = 2,

        [EnumMember]
        [Description("Orientação")]
        Orientacao = 3
    }
}