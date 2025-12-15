using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    public enum ResponsavelEntregaOnline : short
    {
        [EnumMember]
        [Description("")]
        [SMCLegendItem("smc-responsavel-entrega-false", "", 0)]
        Nao = 0,

        [EnumMember]
        [Description("Responsável pela entrega")]
        [SMCLegendItem(SMCGeometricShapes.Star, SMCLegendColors.Yellow2, "Responsável pela entrega")]
        Sim = 1
    }
}