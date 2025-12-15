using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum BancaCancelada : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum,

        [EnumMember]
        [Description("Banca cancelada")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Red2, "Banca cancelada")]
        Sim,

        [EnumMember]
        [Description("Não")]
       // [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Custom, "Não")]
        Nao = 2
    }
}