using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CNC.Enums
{
    //Como a ordenação não é pelo indice e nem pela descrição preciso desse enum para poder listar de acordo com regra de negocio
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoComponenteIntegralizacaoLegend : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        [Description("Em curso")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Blue3, "Em curso")]
        EmCurso = 1,

        [EnumMember]
        [Description("Concluído")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Green3, "Concluído")]
        Concluido = 2,

        [EnumMember]
        [Description("Reprovado")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Red3, "Reprovado")]
        Reprovado = 3,

        [EnumMember]
        [Description("Aguardando apuração de conclusão")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Pink3, "Aguardando apuração de conclusão")]
        AguardandoNota = 4,

        [EnumMember]
        [Description("A concluir")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Yellow3, "A concluir")]
        AConcluir = 5,        
    }
}
