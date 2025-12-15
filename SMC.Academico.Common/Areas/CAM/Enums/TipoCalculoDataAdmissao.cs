using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CAM.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoCalculoDataAdmissao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Data atual")]
        DataAtual = 1,

        [EnumMember]
        [Description("Data início do período letivo")]
        DataInicioPeriodoLetivo = 2
    }
}