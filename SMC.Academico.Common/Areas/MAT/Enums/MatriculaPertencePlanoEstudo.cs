using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.MAT.Enums
{

    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum MatriculaPertencePlanoEstudo : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        [SMCLegendItem(SMCGeometricShapes.Custom, SMCLegendColors.Green2, "Solicitação de inclusão", CssClass = "fa smc-legend-solicita-inclusao")]
        Inclusao = 1,

        [EnumMember]
        [SMCLegendItem(SMCGeometricShapes.Custom, SMCLegendColors.Red2, "Solicitação de exclusão", CssClass = "fa smc-legend-solicita-exclusao")]
        Exclusao = 2,

        [EnumMember]
        [SMCLegendItem(SMCGeometricShapes.Custom, SMCLegendColors.Blue2, "Item do plano de estudo não alterado", CssClass = "fa smc-legend-plan-est-nao-alterado")]
        NaoAlterado = 3,
    }
}
