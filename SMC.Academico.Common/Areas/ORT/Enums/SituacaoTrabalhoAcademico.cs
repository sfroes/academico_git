using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORT.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoTrabalhoAcademico : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Aguardando cadastro pelo aluno")]
        [SMCLegendItem(SMCGeometricShapes.Square, SMCLegendColors.Red2, "Aguardando cadastro pelo aluno")]
        AguardandoCadastroAluno = 1,

        [EnumMember]
        [Description("Cadastrado pelo aluno")]
        [SMCLegendItem(SMCGeometricShapes.Triangle, SMCLegendColors.Green1, "Cadastrado pelo aluno")]
        CadastradaAluno = 3,

        [EnumMember]
        [Description("Autorizado e liberado para secretaria")]
        [SMCLegendItem(SMCGeometricShapes.Star, SMCLegendColors.Green2, "Autorizado e liberado para secretaria")]
        AutorizadaLiberadaSecretaria = 2,

        [EnumMember]
        [Description("Liberado para biblioteca")]
        [SMCLegendItem(SMCGeometricShapes.Hexagon, SMCLegendColors.Green3, "Liberado para biblioteca")]
        LiberadaBiblioteca = 4,

        [EnumMember]
        [Description("Liberado para consulta")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Green4, "Liberado para consulta")]
        LiberadaConsulta = 5
    }
}