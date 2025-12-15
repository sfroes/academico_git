using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum ConfiguracaoComponenteCurricular : short
    {
        [EnumMember]
        [Description("Todos")]
        Nenhum = 0,

        [EnumMember]
        [Description("Sim")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Green2, "Possui configuração")]
        PossuiConfiguracao = 1,

        [EnumMember]
        [Description("Não")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Red4, "Não possui configuração")]
        NaoPossuiConfiguracao = 2,
    }
}