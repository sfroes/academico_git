using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum FinalidadeFormulario : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Parâmetro")]
        Parametro = 1,

        [EnumMember]
        [Description("Complemento de Cadastro")]
        ComplementoCadastro = 2
    }
}
