using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum ComponentesConfiguracaoCadastrada : short
    {
        [EnumMember]
        [Description("Somente componentes sem configuração cadastrada")]
        SemConfiguracao = 0,

        [EnumMember]
        [Description("Somente componentes com configuração cadastrada")]
        ComConfiguracao = 1,

        [EnumMember]
        [Description("Todos")]
        AmbasConfiguracao = 2,
    }
}