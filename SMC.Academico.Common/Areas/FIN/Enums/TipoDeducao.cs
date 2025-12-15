using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.FIN.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoDeducao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Fixo = 1,

        [EnumMember]
        [Description("Variável")]
        Variavel = 2
    }
}
