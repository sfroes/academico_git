using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum PossuiBloqueio : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [SMCLegendItem("smc-sga-legenda-bloqueio", "Sim")]
        Sim = 1,

        [EnumMember]
        [Description("Não")]
        [SMCLegendItem("smc-sga-legenda-desbloqueio", "Não")]
        Nao = 2
    }
}