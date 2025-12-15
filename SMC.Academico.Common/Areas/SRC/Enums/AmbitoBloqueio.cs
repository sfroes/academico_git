using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum AmbitoBloqueio : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Pessoa = 1,

        [EnumMember]
        [Description("Pessoa-Atuação")]
        PessoaAtuacao = 2
    }
}