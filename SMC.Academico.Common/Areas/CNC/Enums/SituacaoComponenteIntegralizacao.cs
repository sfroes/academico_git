using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CNC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoComponenteIntegralizacao : short
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
        [Description("A concluir")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Yellow3, "A concluir")]
        AConcluir = 4,

        [EnumMember]
        [Description("Aguardando apuração de conclusão")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Pink3, "Aguardando apuração de conclusão")]
        AguardandoNota = 5 
    }
}
