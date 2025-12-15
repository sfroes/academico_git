using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CNC.Enums
{
    //Como a ordenação não é pelo indice e nem pela descrição preciso desse enum para poder listar de acordo com regra de negocio
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoComponenteIntegralizacaoOrderBy : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        [Description("1")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Blue3, "Em curso")]
        EmCurso = 1,

        [EnumMember]
        [Description("2")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Green3, "Concluído")]
        Concluido = 2,

        [EnumMember]
        [Description("3")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Red3, "Reprovado")]
        Reprovado = 3,

        [EnumMember]
        [Description("5")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Yellow3, "À concluir")]
        AConcluir = 4,

        [EnumMember]
        [Description("4")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Pink3, "Aguardando apuração de conclusão")]
        AguardandoNota = 5
    }
}
