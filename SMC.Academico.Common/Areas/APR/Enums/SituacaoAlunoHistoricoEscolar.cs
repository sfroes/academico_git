using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoAlunoHistoricoEscolar : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        [Description("Com histórico escolar")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Green2, "Aluno com lançamento de nota/frequência/apuração no histórico escolar")]
        ComHistorico = 1,

        [EnumMember]
        [Description("Sem histórico escolar")]
        [SMCLegendItem(SMCGeometricShapes.Square, SMCLegendColors.Yellow1, "Aluno não possui histórico escolar")]
        SemHistorico = 2,

    }
}